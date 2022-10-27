#addin nuget:?package=Cake.FileHelpers&version=4.0.1
#addin nuget:?package=Cake.Json&version=6.0.1
#addin nuget:?package=Cake.Http&version=1.3.0
#addin nuget:?package=Cake.Git&version=1.1.0
#addin nuget:?package=Newtonsoft.Json&version=13.0.1
#addin nuget:?package=NuGet.Protocol&version=6.3.0

#load "build.components.cake"

using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Versioning;
using Newtonsoft.Json;

var target = Argument("target", "Default");

var slnPath = "./CobrowseIO.sln";
var buildConfiguration = Argument("configuration", "Release");

string nugetOrgApiKey = Argument("nugetOrgApiKey", string.Empty);

string POD_CLONE_DIRECTORY = "cobrowse-sdk-ios-binary";
string AAR_CLONE_DIRECTORY = "cobrowse-sdk-android-binary";

// Cleans built binaries and nuget packages.
Task("Clean")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        foreach (string csprojFile in artifact.CsprojFiles) {
            string bin = System.IO.Directory.GetParent(csprojFile) + "/bin";
            string obj = System.IO.Directory.GetParent(csprojFile) + "/obj";
            if (DirectoryExists(bin))
            {
                DeleteDirectory(bin, new DeleteDirectorySettings { Recursive = true });
            }
            if (DirectoryExists(obj))
            {
                DeleteDirectory(obj, new DeleteDirectorySettings { Recursive = true });
            }
        }
    }

    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        DeleteFile(package);
    }
});

// Downloads native SDK binaries.
Task("DownloadBindings")
    .Does(() =>
{
    // Clone latest master of Android binaries repo
    if(!DirectoryExists(AAR_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-android-binary", 
                 AAR_CLONE_DIRECTORY);
    } else {
        GitCheckout(AAR_CLONE_DIRECTORY, "master");
        GitPull(AAR_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }
    string androidCurrentVersion
        = FindRegexMatchGroupInFile(
            cobrowseAndroidProject.AssemblyInfoFile,
            @"AssemblyVersion\(""([0-9]+\.[0-9]+\.[0-9]+)",
            1,
            RegexOptions.Compiled).Value;
    Information("Current Android SDK is {0}", androidCurrentVersion);

    // Clone specific version of iOS binaries repo
    if(!DirectoryExists(POD_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-ios-binary.git", 
                 POD_CLONE_DIRECTORY);
    } else {
        GitCheckout(POD_CLONE_DIRECTORY, "master");
        GitPull(POD_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }
    string iOSCurrentVersion
        = FindRegexMatchGroupInFile(
            cobrowseIosProject.AssemblyInfoFile, 
            @"AssemblyVersion\(""([0-9]+\.[0-9]+\.[0-9]+)",
            1,
            RegexOptions.Compiled).Value;
    GitCheckout(POD_CLONE_DIRECTORY, "v" + iOSCurrentVersion);
    Information("Current iOS SDK is {0}", iOSCurrentVersion);
});

Task("UpdateBindings")
    .Does(() =>
{
    if(!DirectoryExists(AAR_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-android-binary", 
                 AAR_CLONE_DIRECTORY);
    } else {
        GitCheckout(AAR_CLONE_DIRECTORY, "master");
        GitPull(AAR_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    string androidVersion
        = FindRegexMatchGroupInFile(
            AAR_CLONE_DIRECTORY + "/io/cobrowse/cobrowse-sdk-android/" + "maven-metadata.xml", 
            @"\<release\>([\S]*?)\<\/release\>",
            1,
            RegexOptions.Compiled).Value;
    Information("Latest native Android SDK is {0}", androidVersion);

    if(!DirectoryExists(POD_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-ios-binary.git", 
                 POD_CLONE_DIRECTORY);
    } else {
        GitCheckout(POD_CLONE_DIRECTORY, "master");
        GitPull(POD_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    string iOSVersion
        = FindRegexMatchGroupInFile(
            POD_CLONE_DIRECTORY + "/" + "CobrowseIO.podspec", 
            @"s\.version = '([\S]*?)'",
            1,
            RegexOptions.Compiled).Value;
    Information("Latest native iOS SDK is {0}", iOSVersion);

    void _SetAssemblyVersion(string filePath, Version version) {
        if (version == null) {
            return;
        }
        ReplaceRegexInFiles(
            filePath,
            "(?<=AssemblyVersion\\(\")(.+?)(?=\"\\))", 
            version.ToString());
        ReplaceRegexInFiles(
            filePath,
            "(?<=AssemblyInformationalVersion\\(\")(.+?)(?=\"\\))", 
            version.ToString());
    }

    foreach (BindingProject bindingProject in bindingProjects) {
        if (bindingProject is AndroidBindingProject androidBindingProject) {
            // TODO replace LibraryProjectZip in Android csproj file
            string jarPath = @"..\..\" + AAR_CLONE_DIRECTORY + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + ".aar";
            ReplaceRegexInFiles(
                androidBindingProject.CsprojFile,
                "(?<=LibraryProjectZip\\ Include\\=\").*(?=\")",
                jarPath);
            // TODO replace JavaDocJar in Android csproj file
            string javadocPath = @"..\..\" + AAR_CLONE_DIRECTORY + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + "-javadoc.jar";
            ReplaceRegexInFiles(
                androidBindingProject.CsprojFile,
                "(?<=JavaDocJar\\ Include\\=\").*(?=\")",
                javadocPath);
            _SetAssemblyVersion(bindingProject.AssemblyInfoFile, Version.Parse(androidVersion));
        } else if (bindingProject is IosBindingProject iosBindingProject) {
            // No need to modify iOS csproj file but need to set new version in the assembly info
            _SetAssemblyVersion(bindingProject.AssemblyInfoFile, Version.Parse(iOSVersion));
        }
    }
});

// Restores nuget packages.
Task("Restore")
    .Does(() =>
{
    NuGetRestore(slnPath);
});

// Builds all prjects in the solution
Task("Build")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        foreach (string csprojFile in artifact.CsprojFiles) {
            MSBuild(csprojFile, settings => settings.SetConfiguration(buildConfiguration));
        }
    }
});

// Packs all nuget packages
Task("Pack")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        NuGetPack(artifact.NuspecFile, new NuGetPackSettings());
    }
});

// Pushes previously created nuget package to nuget.org
Task("Push")
    .Does(() =>
{
    if (string.IsNullOrEmpty(nugetOrgApiKey))
    {
        Error("No API key was found for NuGet.org");
        return;
    }
    
    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        string name = System.IO.Path.GetFileName(package.FullPath);
        bool isPrerelease = name.Contains("-pre");
        if (isPrerelease)
        {
            Information("{0} is a prerelease package, skipping...", name);
            continue;
        }
        Information("Publishing {0}", name);
        NuGetPush(name, new NuGetPushSettings
        {
            ApiKey = nugetOrgApiKey,
            SkipDuplicate = true,
            Source = "https://api.nuget.org/v3/index.json"
        });
    }
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("DownloadBindings")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Pack")
    .IsDependentOn("Push");

Task("UpdateNativeSDKs")
    .IsDependentOn("Clean")
    .IsDependentOn("UpdateBindings")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Pack");

RunTarget(target);