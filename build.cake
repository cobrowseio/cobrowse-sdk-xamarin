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

string POD_CLONE_DIRECTORY = "cobrowse-sdk-ios-binary";

Task("ConfigureNuGetSources")
    .Does(() =>
{
    string apiKey = EnvironmentVariable("NUGET_PRIVATE_FEED_API_KEY");
    
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new NotSupportedException("No API key was found for the private NuGet feed");
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
                Password = apiKey
            });
    }
    else
    {
        Information("Private NuGet source already exists");
    }
    
    if (string.IsNullOrEmpty(apiKey))
    {
        Warning("No API key was found for the private NuGet feed");
        return;
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
    var bintrayApiEndpoint = "https://api.bintray.com/search/packages/maven?q=&g=io.cobrowse&a=cobrowse-sdk-android";
    string responseBody = HttpGet(bintrayApiEndpoint);
    JObject result = ParseJson(responseBody.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));

    cobrowseAndroidProject._VersionString = result["latest_version"].ToString();
    
    Information("Latest native Android SDK is {0}", cobrowseAndroidProject._VersionString);
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

    // TODO: This is a temprorary workaround until I find a way to create a 'fat' iOS framework from 'CobrowseIO.xcframework'
    GitCheckout(
        POD_CLONE_DIRECTORY,
        "900d81fbaad88f8fd8369d65578bd3ab8dcd6f27");

    cobrowseIosProject._VersionString 
        = cobrowseIosExtensionProject._VersionString
        = FindRegexMatchGroupInFile(
            POD_CLONE_DIRECTORY + "/" + "CobrowseIO.podspec", 
            @"s\.version = '([\S]*?)'",
            1,
            RegexOptions.Compiled).Value;
    
    Information("Latest native iOS SDK is {0}", cobrowseIosProject._VersionString);
});

Task("DownloadNativeSDKs")
    .Does(() =>
{
    foreach (BindingProject bindingProject in bindingProjects) {
        if (bindingProject is AndroidBindingProject androidBindingProject) {
            var downloadUrl = string.Format(androidBindingProject.DownloadUrl, androidBindingProject._VersionString);
            var jarPath = string.Format(androidBindingProject.JarPath, androidBindingProject._VersionString);
            DownloadFile(downloadUrl, jarPath);
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
        if (artifact._VersionString == null) {
            // There are packages which nuspec files we do not modify on each build
            // We just build these packages
            NuGetPack(artifact.NuspecFile, new NuGetPackSettings());
        } else {
            NuGetPack(artifact.NuspecFile,
                      new NuGetPackSettings {
                          Version = artifact._VersionString
                      });
        }
    }
});

Task("PushToPrivateFeed")
    .Does(() =>
{
    string apiKey = EnvironmentVariable("NUGET_PRIVATE_FEED_API_KEY");
    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        string name = System.IO.Path.GetFileName(package.FullPath);
        Information("Publishing {0}", name);
        NuGetPush(name, new NuGetPushSettings
        {
            ApiKey = apiKey,
            SkipDuplicate = true,
            Source = "cobrowse-nuget-feed"
        });
    }
});

Task("PushToNuGetOrg")
    .Does(() =>
{
    string apiKey = EnvironmentVariable("NUGET_ORG_API_KEY");
    
    if (string.IsNullOrEmpty(apiKey))
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
            ApiKey = apiKey,
            SkipDuplicate = true,
            Source = "https://api.nuget.org/v3/index.json"
        });
    }
});

Task("Default")
    .IsDependentOn("ConfigureNuGetSources")
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