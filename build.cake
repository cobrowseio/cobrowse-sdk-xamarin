#addin nuget:?package=Cake.FileHelpers&version=3.2.1
#addin nuget:?package=Cake.Json&version=4.0.0
#addin nuget:?package=Cake.Http&version=0.7.0
#addin nuget:?package=Cake.Git&version=0.21.0
#addin nuget:?package=Newtonsoft.Json&version=12.0.3
#addin nuget:?package=NuGet.Protocol&version=5.4.0

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

class Artifact {
    public string AssemblyInfoPath { get; set; }
    public string NuspecPath { get; set; }
    public string VersionString { get; set; }
    public string Patch { get; set; }
    public string ExistingNugetId { get; set; }
    public Version ExistingNugetVersion { get; set; }
    public Version Version
    {
        get
        {
            if (string.IsNullOrEmpty(VersionString))
                return null;
            if (string.IsNullOrEmpty(Patch))
                return new Version(VersionString);
            return new Version(VersionString + Patch);
        }
    }
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
    JarPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST.aar",
    Patch = EnvironmentVariable("COBROWSE_ANDROID_PATCH"),
    ExistingNugetId = "CobrowseIO.Android"
};

IosArtifact cobrowseIosArtifact = new IosArtifact {
    AssemblyInfoPath = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs",
    FrameworkPath = "./iOS/CobrowseIO.iOS/CobrowseIO.framework",
    NuspecPath = "CobrowseIO.iOS.nuspec",
    Patch = EnvironmentVariable("COBROWSE_IOS_PATCH"),
    ExistingNugetId = "CobrowseIO.iOS"
};

IosArtifact cobrowseIosExtensionArtifact = new IosArtifact {
    AssemblyInfoPath = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs",
    FrameworkPath = "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIOAppExtension.framework",
    NuspecPath = "CobrowseIO.AppExtension.iOS.nuspec",
    Patch = EnvironmentVariable("COBROWSE_IOS_PATCH"),
    ExistingNugetId = "CobrowseIO.AppExtension.iOS"
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

    var nugetPackages = GetFiles("./*.nupkg");
    foreach (var package in nugetPackages)
    {
        DeleteFile(package);
    }
});

Task("FindExistingNuGetVersions")
    .Does(async () =>
{
    foreach (var artifact in allNuGetArtifacts) {
        if (artifact.ExistingNugetId == null)
            continue;


        var repository =
                NuGet.Protocol.FactoryExtensionsV3.GetCoreV3(
                    NuGet.Protocol.Core.Types.Repository.Factory,
                    "https://api.nuget.org/v3/index.json");
        var resource = await repository.GetResourceAsync<NuGet.Protocol.Core.Types.FindPackageByIdResource>();

        IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
            artifact.ExistingNugetId,
            new NuGet.Protocol.Core.Types.SourceCacheContext(),
            NuGet.Common.NullLogger.Instance,
            System.Threading.CancellationToken.None);

        NuGetVersion version = versions
            .Where(v => !v.IsPrerelease)
            .LastOrDefault();

        if (version == null) {
            Information($"No NuGet version of {artifact.ExistingNugetId} is available");
        } else {
            artifact.ExistingNugetVersion = version.Version;
            Information($"Found version {artifact.ExistingNugetVersion} of {artifact.ExistingNugetId}");
        }
    }
});

Task("FindLatestVersions")
    .Does(() =>
{
    var bintrayApiEndpoint = "https://api.bintray.com/search/packages/maven?q=&g=io.cobrowse&a=cobrowse-sdk-android";
    string responseBody = HttpGet(bintrayApiEndpoint);
    JObject result = ParseJson(responseBody.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));

    cobrowseAndroidArtifact.VersionString = result["latest_version"].ToString();
    
    if(!DirectoryExists(POD_CLONE_DIRECTORY)) {
        GitClone("https://github.com/cobrowseio/cobrowse-sdk-ios-binary.git", 
                 POD_CLONE_DIRECTORY);
    } else {
        GitPull(POD_CLONE_DIRECTORY, "CakeBuild", "CakeBuild@cobrowse.io");
    }

    cobrowseIosArtifact.VersionString 
        = cobrowseIosExtensionArtifact.VersionString
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
        var downloadUrl = string.Format(artifact.DownloadUrl, artifact.VersionString);
        var jarPath = string.Format(artifact.JarPath, artifact.VersionString);

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
            artifact.Version.ToString());
        ReplaceRegexInFiles(
            artifact.AssemblyInfoPath, 
            "(?<=AssemblyInformationalVersion\\(\")(.+?)(?=\"\\))", 
            artifact.Version.ToString());

        XmlPoke(
            artifact.NuspecPath,
            "/ns:package/ns:metadata/ns:version",
            artifact.Version.ToString(),
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
        if (artifact.Version == null) {
            // There are a few packages which nuspec files we do not modify on each build
            // We just build these packages
            NuGetPack(artifact.NuspecPath, new NuGetPackSettings());
        } else {
            string suffix;
            if (artifact.ExistingNugetVersion == null) {
                suffix = null;
            } else if (artifact.ExistingNugetVersion.Major == artifact.Version.Major && artifact.ExistingNugetVersion.Minor == artifact.Version.Minor) {
                suffix = null;
            } else {
                suffix = "pre1";
            }
            NuGetPack(artifact.NuspecPath,
                      new NuGetPackSettings {
                          Version = artifact.Version.ToString(),
                          Suffix = suffix
                      });
        }
    }
});


Task("Default")
    .IsDependentOn("RestoreNuGetPackages")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindExistingNuGetVersions")
    .IsDependentOn("FindLatestVersions")
    .IsDependentOn("DownloadAndroidJars")
    .IsDependentOn("CopyIosFrameworks")
    .IsDependentOn("UpdateVersions")
    .IsDependentOn("Build")
    .IsDependentOn("Pack");

Task("UpdateBindings")
    .IsDependentOn("CleanUp")
    .IsDependentOn("FindExistingNuGetVersions")
    .IsDependentOn("FindLatestVersions")
    .IsDependentOn("DownloadAndroidJars")
    .IsDependentOn("CopyIosFrameworks")
    .IsDependentOn("UpdateVersions");

RunTarget(target);