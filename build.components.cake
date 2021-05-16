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
        JarPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST.aar"
    },
    cobrowseIosProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs",
        FrameworkPath = "./iOS/CobrowseIO.iOS/CobrowseIO.framework"
    },
    cobrowseIosExtensionProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs",
        FrameworkPath = "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIOAppExtension.framework"
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
    var packageList = NuGetList(COBROWSE_NUGET_PACKAGE_ID, new NuGetListSettings { AllVersions = true, Prerelease = true });
    Version currentVersion = default;
    int currentVersionSuffix = default;
    foreach (NuGetListItem package in packageList)
    {
        if (package.Name != COBROWSE_NUGET_PACKAGE_ID)
        {
            continue;
        }
        //Information("Found package {0}, version {1}", package.Name, package.Version);
        if (package.Version.Contains("-pre"))
        {
            Version packageVersion = new Version(package.Version.Split("-pre")[0]);
            int packageSuffix = int.Parse(package.Version.Split("-pre")[1]);
            if (currentVersion == default || currentVersion < packageVersion || (currentVersion == packageVersion && currentVersionSuffix < packageSuffix))
            {
                currentVersion = packageVersion;
                currentVersionSuffix = packageSuffix;
            }
        }
        else
        {
            Version packageVersion = new Version(package.Version);
            if (currentVersion == default || currentVersion < packageVersion)
            {
                currentVersion = packageVersion;
                currentVersionSuffix = default;
            }
        }
    }

    if (currentVersion == default)
    {
        throw new Exception(string.Format("Cannot find any {0} version", COBROWSE_NUGET_PACKAGE_ID));
    }

    string newVersion = currentVersionSuffix == 0
        ? "" + currentVersion.Major + "." + currentVersion.Minor + "." + (currentVersion.Build + 1)
        : currentVersion.ToString();
    string newVersionSuffix = currentVersionSuffix == 0
        ? "01"
        : currentVersionSuffix >= 10
            ? (currentVersionSuffix + 1).ToString()
            : "0" + (currentVersionSuffix + 1);

    return newVersion + "-pre" + newVersionSuffix;
}