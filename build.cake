#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0

#addin nuget:?package=Cake.FileHelpers&version=3.2.1
#addin nuget:?package=Cake.Json&version=4.0.0
#addin nuget:?package=Cake.Http&version=0.7.0
#addin nuget:?package=Cake.Git&version=0.21.0
#addin nuget:?package=Newtonsoft.Json&version=11.0.2

using System.Text.RegularExpressions;
using Newtonsoft.Json;

var target = Argument("target", "Default");

var slnPath = "./CobrowseIO.sln";
var buildConfiguration = Argument("configuration", "Release");

// Patch version code can be set manually
var PATCH = EnvironmentVariable("COBROWSE_PATCH") == null ? string.Empty : '.' + EnvironmentVariable("COBROWSE_PATCH");
// NuGet version suffix can be set manually
var NUGET_SUFFIX = EnvironmentVariable("COBROWSE_NUGET_SUFFIX") == null ? string.Empty : '-' + EnvironmentVariable("COBROWSE_NUGET_SUFFIX");

string POD_CLONE_DIRECTORY = "cobrowse-sdk-ios-binary";

class Artifact {
    public string AssemblyInfoPath { get; set; }
    public string NuspecPath { get; set; }
    public string Version { get; set; }
}

class AndroidArtifact : Artifact {
    public string DownloadUrl  { get; set; }
    public string JarPath { get; set; }
}

class IosArtifact : Artifact {
    public string FrameworkPath { get; set; }
}

AndroidArtifact cobrowseAndroidArtifact = new AndroidArtifact {
    AssemblyInfoPath = "./Android/CobrowseIO.Android/Properties/AssemblyInfo.cs",
    NuspecPath = "./CobrowseIO.Android.nuspec",
    DownloadUrl = "https://jcenter.bintray.com/io/cobrowse/cobrowse-sdk-android/{0}/cobrowse-sdk-android-{0}.aar",
    JarPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST.aar"
};

IosArtifact cobrowseIosArtifact = new IosArtifact {
    AssemblyInfoPath = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs",
    FrameworkPath = "./iOS/CobrowseIO.iOS/CobrowseIO.framework",
    NuspecPath = "CobrowseIO.iOS.nuspec"
};

IosArtifact cobrowseIosExtensionArtifact = new IosArtifact {
    AssemblyInfoPath = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs",
    FrameworkPath = "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIOAppExtension.framework",
    NuspecPath = "CobrowseIO.AppExtension.iOS.nuspec"
};

AndroidArtifact[] androidCobrowseArtifacts = new [] {
    cobrowseAndroidArtifact
};

IosArtifact[] iosCobrowseArtifacts = new [] {
    cobrowseIosArtifact,
    cobrowseIosExtensionArtifact
};

Artifact[] cobrowseArtifacts = new Artifact[] {
    cobrowseAndroidArtifact,
    cobrowseIosArtifact,
    cobrowseIosExtensionArtifact
};

Artifact[] allNuGetArtifacts = new Artifact[] {
    new Artifact {
        NuspecPath = "./NumbersJava.Android.nuspec"
    },
    new Artifact {
        NuspecPath = "./CborJava.Android.nuspec"
    },
    cobrowseAndroidArtifact,
    cobrowseIosArtifact,
    cobrowseIosExtensionArtifact,
    new Artifact {
        NuspecPath = "./SwiftCBOR.iOS.nuspec"
    },
    new Artifact {
        NuspecPath = "./Starscream.iOS.nuspec"
    }
};


Task("RestoreNuGetPackages")
    .Does(() =>
{
    NuGetRestore(slnPath);
});

Task("CleanUp")
    .Does(() =>
{
    foreach(var artifact in androidCobrowseArtifacts) {
        if (FileExists(artifact.JarPath)) {
            DeleteFile(artifact.JarPath);
        }
    }
    
    foreach(var artifact in iosCobrowseArtifacts) {
        if(DirectoryExists(artifact.FrameworkPath)) {
            DeleteDirectory(artifact.FrameworkPath,
                            new DeleteDirectorySettings {
                                Recursive = true
                            });
        }
    }
});

Task("FindLatestVersions")
    .Does(() =>
{
    var bintrayApiEndpoint = "https://api.bintray.com/search/packages/maven?q=&g=io.cobrowse&a=cobrowse-sdk-android";
    string responseBody = HttpGet(bintrayApiEndpoint);
    JObject result = ParseJson(responseBody.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));

    cobrowseAndroidArtifact.Version = result["latest_version"].ToString();
    
    if(!DirectoryExists(POD_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-ios-binary.git", 
                 POD_CLONE_DIRECTORY);
    } else {
        GitPull(POD_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    cobrowseIosArtifact.Version 
        = cobrowseIosExtensionArtifact.Version
        = FindRegexMatchGroupInFile(
            POD_CLONE_DIRECTORY + "/" + "CobrowseIO.podspec", 
            @"s\.version = '([\S]*?)'",
            1,
            RegexOptions.Compiled).Value;
});

Task("DownloadAndroidJars")
    .Does(() =>
{
    foreach(var artifact in androidCobrowseArtifacts) {
        var downloadUrl = string.Format(artifact.DownloadUrl, artifact.Version);
        var jarPath = string.Format(artifact.JarPath, artifact.Version);

        DownloadFile(downloadUrl, jarPath);
    }
});

Task("CopyIosFrameworks")
    .Does(() =>
{
    foreach(var artifact in iosCobrowseArtifacts) {
        string dirName = System.IO.Path.GetFileName(artifact.FrameworkPath);
        CopyDirectory(POD_CLONE_DIRECTORY + "/" + dirName,
                      artifact.FrameworkPath);
    }
});

Task("UpdateVersions")
    .Does(() => 
{
    foreach(var artifact in cobrowseArtifacts) {
        ReplaceRegexInFiles(
            artifact.AssemblyInfoPath, 
            "(?<=AssemblyVersion\\(\")(.+?)(?=\"\\))", 
            artifact.Version + PATCH);
        ReplaceRegexInFiles(
            artifact.AssemblyInfoPath, 
            "(?<=AssemblyInformationalVersion\\(\")(.+?)(?=\"\\))", 
            artifact.Version + PATCH);

        XmlPoke(
            artifact.NuspecPath,
            "/ns:package/ns:metadata/ns:version",
            artifact.Version + PATCH + NUGET_SUFFIX,
            new XmlPokeSettings {
                Namespaces = new Dictionary<string, string> {
                { "ns", "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd" }
                }
            });
    }
});

Task("Build")
    .Does(() =>
{
    MSBuild(slnPath, settings => settings.SetConfiguration(buildConfiguration));
});

Task("Pack")
    .Does(() =>
{
    foreach(var artifact in allNuGetArtifacts) {
        NuGetPackSettings settings = artifact.Version == null
            ? new NuGetPackSettings()
            : new NuGetPackSettings {
                  Version = artifact.Version + PATCH + NUGET_SUFFIX
              };
        NuGetPack(artifact.NuspecPath, settings);
    }
});


Task("Default")
    .IsDependentOn("RestoreNuGetPackages")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindLatestVersions")
    .IsDependentOn("DownloadAndroidJars")
    .IsDependentOn("CopyIosFrameworks")
    .IsDependentOn("UpdateVersions")
    .IsDependentOn("Build")
    .IsDependentOn("Pack");

Task("UpdateBindings")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindLatestVersions")
    .IsDependentOn("DownloadAndroidJars")
    .IsDependentOn("CopyIosFrameworks")
    .IsDependentOn("UpdateVersions");

RunTarget(target);