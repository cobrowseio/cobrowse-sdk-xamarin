#addin nuget:?package=Cake.FileHelpers&version=3.2.1
#addin nuget:?package=Cake.Json&version=4.0.0
#addin nuget:?package=Cake.Http&version=0.7.0
#addin nuget:?package=Cake.Git&version=0.21.0
#addin nuget:?package=Newtonsoft.Json&version=12.0.3
#addin nuget:?package=NuGet.Protocol&version=5.4.0

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
string nugetPrivateFeedReadApiKey = Argument("nugetPrivateFeedReadApiKey", string.Empty);
string nugetPrivateFeedWriteApiKey = Argument("nugetPrivateFeedWriteApiKey", string.Empty);

string POD_CLONE_DIRECTORY = "cobrowse-sdk-ios-binary";
string AAR_CLONE_DIRECTORY = "cobrowse-sdk-android-binary";

Task("ConfigureNuGetSources")
    .Does(() =>
{
    if (string.IsNullOrEmpty(nugetPrivateFeedReadApiKey))
    {
        Warning("No API key was found for the private NuGet feed");
        return;
    }
    
    // Generate a new token: https://dev.azure.com/cobrowse-xamarin-sdk/_usersSettings/tokens
    string privateFeed = "https://pkgs.dev.azure.com/cobrowse-xamarin-sdk/cobrowse-xamarin-sdk-nuget/_packaging/cobrowse-nuget-feed/nuget/v3/index.json";
    
    if (!NuGetHasSource(privateFeed))
    {
        Information("Private NuGet source is missing, adding...");
        NuGetAddSource(
            "cobrowse-nuget-feed",
            privateFeed,
            new NuGetSourcesSettings
            {
                UserName = "user",
                Password = nugetPrivateFeedReadApiKey
            });
    }
    else
    {
        Information("Private NuGet source already exists");
    }
});

Task("FindNextNuGetPackageVersion")
    .Does(() =>
    {
        string version = GetCobrowseNuGetVersion();
        Information("Next {0} package version will be {1}", COBROWSE_NUGET_PACKAGE_ID, version);
        cobrowseArtifact.VersionString 
            = cobrowseIosExtensionArtifact.VersionString 
            = version;
    });

Task("Clean")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        foreach (string csprojFile in artifact.CsprojFiles) {
            CleanDirectory(System.IO.Directory.GetParent(csprojFile) + "/bin");
            CleanDirectory(System.IO.Directory.GetParent(csprojFile) + "/obj");
        }
    }
});

Task("RestoreNuGetPackages")
    .Does(() =>
{
    NuGetRestore(slnPath);
});

Task("CleanUp")
    .Does(() =>
{    
    foreach (BindingProject bindingProject in bindingProjects) {
        if (bindingProject is AndroidBindingProject androidBindingProject) {
            if (FileExists(androidBindingProject.JarPath)) {
                DeleteFile(androidBindingProject.JarPath);
            }
        } else if (bindingProject is IosBindingProject iosBindingProject) {
            if (DirectoryExists(iosBindingProject.FrameworkPath)) {
                DeleteDirectory(iosBindingProject.FrameworkPath,
                                new DeleteDirectorySettings {
                                    Recursive = true
                                });
            }
        }
    }

    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        DeleteFile(package);
    }
});

Task("FindLatestAndroidVersions")
    .Does(() =>
{
    if(!DirectoryExists(AAR_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-android-binary", 
                 AAR_CLONE_DIRECTORY);
    } else {
        GitCheckout(AAR_CLONE_DIRECTORY, "master");
        GitPull(AAR_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    cobrowseAndroidProject.VersionString = FindRegexMatchGroupInFile(
            AAR_CLONE_DIRECTORY + "/io/cobrowse/cobrowse-sdk-android/" + "maven-metadata.xml", 
            @"\<release\>([\S]*?)\<\/release\>",
            1,
            RegexOptions.Compiled).Value;
    
    Information("Latest native Android SDK is {0}", cobrowseAndroidProject.VersionString);
});

Task("FindLatestIosVersions")
    .Does(() =>
{
    if(!DirectoryExists(POD_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-ios-binary.git", 
                 POD_CLONE_DIRECTORY);
    } else {
        GitCheckout(POD_CLONE_DIRECTORY, "master");
        GitPull(POD_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    // Starting 2.6.0, Cobrowse.io iOS SDK uses xcframework instead of regular framework
    // This function creates a 'fat' framework which will be used in next tasks
    void _BuildUniversal() {
        var targetDirectory=POD_CLONE_DIRECTORY;
        var targetFrameworkName="CobrowseIO";
        var frameworkDirectory=$"{targetFrameworkName}.framework";
        var xcFrameworkDirectory=$"{targetFrameworkName}.xcframework";

        var iphoneFrameworkDirectory= DirectoryExists($"{targetDirectory}/{xcFrameworkDirectory}/ios-arm64_armv7/{frameworkDirectory}")
            ? $"{targetDirectory}/{xcFrameworkDirectory}/ios-arm64_armv7/{frameworkDirectory}"
            : $"{targetDirectory}/{xcFrameworkDirectory}/ios-armv7_arm64/{frameworkDirectory}";
        var simulatorFrameworkDirectory=$"{targetDirectory}/{xcFrameworkDirectory}/ios-arm64_i386_x86_64-simulator/{frameworkDirectory}";

        if (DirectoryExists($"./{targetDirectory}/{frameworkDirectory}")) {
            DeleteDirectory($"./{targetDirectory}/{frameworkDirectory}", new DeleteDirectorySettings {
                Recursive = true,
                Force = true
            });
        }
        CopyDirectory($"{iphoneFrameworkDirectory}", $"{targetDirectory}/{frameworkDirectory}");
        CopyDirectory($"{simulatorFrameworkDirectory}/Modules/", $"{targetDirectory}/{frameworkDirectory}/Modules/");
        // No suport form iOS Simulators on Apple Silicon for now
        StartProcess(
            "lipo",
            new ProcessSettings {
                Arguments = $"-remove arm64 \"{simulatorFrameworkDirectory}/{targetFrameworkName}\" -o \"{simulatorFrameworkDirectory}/{targetFrameworkName}\""
            });
        StartProcess(
            "lipo", 
            new ProcessSettings { 
                Arguments = $"-create -output \"./{targetDirectory}/{frameworkDirectory}/{targetFrameworkName}\" \"{iphoneFrameworkDirectory}/{targetFrameworkName}\" \"{simulatorFrameworkDirectory}/{targetFrameworkName}\"" 
            });
    }

    _BuildUniversal();

    cobrowseIosProject.VersionString 
        = cobrowseIosExtensionProject.VersionString
        = FindRegexMatchGroupInFile(
            POD_CLONE_DIRECTORY + "/" + "CobrowseIO.podspec", 
            @"s\.version = '([\S]*?)'",
            1,
            RegexOptions.Compiled).Value;
    
    Information("Latest native iOS SDK is {0}", cobrowseIosProject.VersionString);
});

Task("DownloadNativeSDKs")
    .Does(() =>
{
    foreach (BindingProject bindingProject in bindingProjects) {
        if (bindingProject is AndroidBindingProject androidBindingProject) {
            string jarPath = AAR_CLONE_DIRECTORY + "/io/cobrowse/cobrowse-sdk-android/" + androidBindingProject.VersionString + "/cobrowse-sdk-android-" + androidBindingProject.VersionString + ".aar";
            CopyFile(jarPath,
                     androidBindingProject.JarPath);
        } else if (bindingProject is IosBindingProject iosBindingProject) {
            string dirName = System.IO.Path.GetFileName(iosBindingProject.FrameworkPath);
            CopyDirectory(POD_CLONE_DIRECTORY + "/" + dirName,
                          iosBindingProject.FrameworkPath);
        }
    }
});

Task("UpdateAssemblyVersions")
    .Does(() =>
{
    void _SetAseemblyVersion(string filePath, Version version) {
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

    foreach (var bindingProject in bindingProjects) {
        _SetAseemblyVersion(bindingProject.AssemblyInfoFile, bindingProject.Version);
    }
    
    foreach (var nugetArtifact in nugetArtifacts) {
        if (nugetArtifact.AssemblyInfoFiles == null) {
            continue;
        }
        foreach (string assemblyInfoFile in nugetArtifact.AssemblyInfoFiles) {
            _SetAseemblyVersion(assemblyInfoFile, nugetArtifact.Version);
        }
    }
});

Task("Build")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        foreach (string csprojFile in artifact.CsprojFiles) {
            MSBuild(csprojFile, settings => settings.SetConfiguration(buildConfiguration));
        }
    }
});

Task("Pack")
    .Does(() =>
{
    foreach (NuGetArtifact artifact in nugetArtifacts) {
        if (artifact.VersionString == null) {
            // There are packages which nuspec files we do not modify on each build
            // We just build these packages
            NuGetPack(artifact.NuspecFile, new NuGetPackSettings());
        } else {
            NuGetPack(artifact.NuspecFile,
                      new NuGetPackSettings {
                          Version = artifact.VersionString
                      });
        }
    }
});

Task("PushToPrivateFeed")
    .Does(() =>
{
    if (string.IsNullOrEmpty(nugetPrivateFeedWriteApiKey))
    {
        Warning("No API key was found for the private NuGet feed");
        return;
    }
    
    string apiKey = EnvironmentVariable("NUGET_PRIVATE_FEED_API_KEY");
    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        string name = System.IO.Path.GetFileName(package.FullPath);
        Information("Publishing {0}", name);
        NuGetPush(name, new NuGetPushSettings
        {
            ApiKey = nugetPrivateFeedWriteApiKey,
            SkipDuplicate = true,
            Source = "cobrowse-nuget-feed"
        });
    }
});

Task("PushToNuGetOrg")
    .Does(() =>
{
    if (string.IsNullOrEmpty(nugetOrgApiKey))
    {
        Warning("No API key was found for NuGet.org");
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
    .IsDependentOn("ConfigureNuGetSources")
    .IsDependentOn("FindNextNuGetPackageVersion")
    .IsDependentOn("Clean")
    .IsDependentOn("RestoreNuGetPackages")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindLatestAndroidVersions")
    .IsDependentOn("FindLatestIosVersions")
    .IsDependentOn("DownloadNativeSDKs")
    .IsDependentOn("UpdateAssemblyVersions")
    .IsDependentOn("Build")
    .IsDependentOn("Pack")
    .IsDependentOn("PushToPrivateFeed")
    .IsDependentOn("PushToNuGetOrg");

Task("UpdateBindings")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindLatestAndroidVersions")
    .IsDependentOn("FindLatestIosVersions")
    .IsDependentOn("DownloadNativeSDKs")
    .IsDependentOn("UpdateAssemblyVersions");

RunTarget(target);