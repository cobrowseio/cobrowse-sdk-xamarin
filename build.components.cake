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
    public string DownloadUrl  { get; set; }
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
        DownloadUrl = "https://jcenter.bintray.com/io/cobrowse/cobrowse-sdk-android/{0}/cobrowse-sdk-android-{0}.aar",
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

NuGetArtifact[] nugetArtifacts = new NuGetArtifact[] {
    new NuGetArtifact {
        CsprojFiles = new [] { "./Android/NumbersJava.Android/NumbersJava.Android.csproj" },
        NuspecFile = "./NumbersJava.Android.nuspec"
    },
    new NuGetArtifact {
        CsprojFiles = new [] { "./Android/CborJava.Android/CborJava.Android.csproj" },
        NuspecFile = "./CborJava.Android.nuspec"
    },
    new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/SwiftCBOR.iOS/SwiftCBOR.iOS.csproj" },
        NuspecFile = "./SwiftCBOR.iOS.nuspec"
    },
    new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/Starscream.iOS/Starscream.iOS.csproj" },
        NuspecFile = "./Starscream.iOS.nuspec"
    },
    new NuGetArtifact {
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
        NuspecFile = "./CobrowseIO.Xamarin.nuspec",
        VersionString = GetCobrowseNuGetVersion()
    },
    new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIO.AppExtension.iOS.csproj" },
        NuspecFile = "./CobrowseIO.AppExtension.iOS.nuspec",
        VersionString = GetCobrowseNuGetVersion()
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
    string version = Argument("cobrowseNuGetPackageVersion", string.Empty);
    return !string.IsNullOrEmpty(version)
        ? version
        : null;
}