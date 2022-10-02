string COBROWSE_NUGET_PACKAGE_ID = "CobrowseIO.Xamarin";

class NuGetArtifact {
    public string[] CsprojFiles { get; set; }
    public string[] AssemblyInfoFiles { get; set; }    
    public string NuspecFile { get; set; }
    public string VersionString { get; set; }
    public Version Version => TrimVersionString(VersionString);
}

abstract class BindingProject {
    public string AssemblyInfoFile { get; set; }
    public string VersionString { get; set; }
    public Version Version => TrimVersionString(VersionString);
}

class AndroidBindingProject : BindingProject {
    public string JarPath { get; set; }
    public string JavadocPath { get; set; }
}

class IosBindingProject : BindingProject {
    public string FrameworkPath { get; set; }
}

AndroidBindingProject cobrowseAndroidProject;
IosBindingProject cobrowseIosProject;
IosBindingProject cobrowseIosExtensionProject;

BindingProject[] bindingProjects = new BindingProject[] {
    cobrowseAndroidProject = new AndroidBindingProject {
        AssemblyInfoFile = "./Android/CobrowseIO.Android/Properties/AssemblyInfo.cs",
        JarPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST.aar",
        JavadocPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST-javadoc.jar",
    },
    cobrowseIosProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs",
        FrameworkPath = "./iOS/CobrowseIO.iOS/CobrowseIO.xcframework"
    },
    cobrowseIosExtensionProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs",
        FrameworkPath = "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIOAppExtension.xcframework"
    }
};

NuGetArtifact numbersJavaArtifact, cborJavaArtifact, swiftCborArtifact, starscreamArtifact, cobrowseArtifact, cobrowseIosExtensionArtifact;

NuGetArtifact[] nugetArtifacts = new NuGetArtifact[] {
    numbersJavaArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./Android/NumbersJava.Android/NumbersJava.Android.csproj" },
        NuspecFile = "./NumbersJava.Android.nuspec"
    },
    cborJavaArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./Android/CborJava.Android/CborJava.Android.csproj" },
        NuspecFile = "./CborJava.Android.nuspec"
    },
    swiftCborArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/SwiftCBOR.iOS/SwiftCBOR.iOS.csproj" },
        NuspecFile = "./SwiftCBOR.iOS.nuspec"
    },
    starscreamArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/Starscream.iOS/Starscream.iOS.csproj" },
        NuspecFile = "./Starscream.iOS.nuspec"
    },
    cobrowseArtifact = new NuGetArtifact {
        CsprojFiles = new [] 
        { 
            "./Android/CobrowseIO.Android/CobrowseIO.Android.csproj",
            "./iOS/CobrowseIO.iOS/CobrowseIO.iOS.csproj",
            "./XamarinSDK/CobrowseIO.Xamarin.Abstractions/CobrowseIO.Xamarin.Abstractions.csproj",
            "./XamarinSDK/CobrowseIO.Xamarin/CobrowseIO.Xamarin.csproj",
            "./XamarinSDK/CobrowseIO.Xamarin.Android/CobrowseIO.Xamarin.Android.csproj",
            "./XamarinSDK/CobrowseIO.Xamarin.iOS/CobrowseIO.Xamarin.iOS.csproj",
        },
        AssemblyInfoFiles = new []
        {
            "./XamarinSDK/CobrowseIO.Xamarin.Abstractions/Properties/AssemblyInfo.cs",
            "./XamarinSDK/CobrowseIO.Xamarin/Properties/AssemblyInfo.cs",
            "./XamarinSDK/CobrowseIO.Xamarin.Android/Properties/AssemblyInfo.cs",
            "./XamarinSDK/CobrowseIO.Xamarin.iOS/Properties/AssemblyInfo.cs",
        },
        NuspecFile = "./CobrowseIO.Xamarin.nuspec"
    },
    cobrowseIosExtensionArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIO.AppExtension.iOS.csproj" },
        NuspecFile = "./CobrowseIO.AppExtension.iOS.nuspec"
    },
};

static Version TrimVersionString(string versionString) {
    if (string.IsNullOrEmpty(versionString)) {
        return null;
    }
    string versionCode = versionString.Split('-')[0];
    return new Version(versionCode);
}

string GetCobrowseNuGetVersion() {
    string passedVersion = Argument("cobrowseNuGetPackageVersion", string.Empty);
    return !string.IsNullOrEmpty(passedVersion)
        ? passedVersion
        : CalculateNextNuGetVersion();
}

string CalculateNextNuGetVersion() {
    // Previously a pre-release version was calculated by increasing `-pre` suffix of the last published pre-release package.
    // However, because Github Packages does not support `nuget list` API there is no easy way to find the last pre-release version,
    // so the build script calculates a `-pre` suffix from current UTC time.
    var packageList = NuGetList(COBROWSE_NUGET_PACKAGE_ID, new NuGetListSettings { AllVersions = true, Prerelease = false });
    Version currentVersion = default;
    foreach (NuGetListItem package in packageList)
    {
        if (package.Name != COBROWSE_NUGET_PACKAGE_ID)
        {
            continue;
        }
        Information("Found package {0}, version {1}", package.Name, package.Version);
        Version packageVersion = new Version(package.Version);
        if (currentVersion == default /* Not initialized */
            || currentVersion < packageVersion /* Older version */ )
        {
            currentVersion = packageVersion;
        }
    }

    if (currentVersion == default)
    {
        throw new Exception(string.Format("Cannot find any {0} version", COBROWSE_NUGET_PACKAGE_ID));
    }

    string newVersion = "" + currentVersion.Major + "." + currentVersion.Minor + "." + (currentVersion.Build + 1);
    string newVersionSuffix = DateTime.UtcNow.ToString("yyMMdd.HHmm");

    return newVersion + "-pre" + newVersionSuffix;
}