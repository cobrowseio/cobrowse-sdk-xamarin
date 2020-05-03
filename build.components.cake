class NuGetArtifact {
    public string[] CsprojFiles { get; set; }
    public string NuspecFile { get; set; }
    public string Version { get; set; }
}

abstract class BindingProject {
    public string AssemblyInfoPath { get; set; }
    public string NativeVersion { get; set; }
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
        AssemblyInfoPath = "./Android/CobrowseIO.Android/Properties/AssemblyInfo.cs",
        DownloadUrl = "https://jcenter.bintray.com/io/cobrowse/cobrowse-sdk-android/{0}/cobrowse-sdk-android-{0}.aar",
        JarPath = "./Android/CobrowseIO.Android/Jars/cobrowse-sdk-android-LATEST.aar"
    },
    cobrowseIosProject = new IosBindingProject {
        AssemblyInfoPath = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs",
        FrameworkPath = "./iOS/CobrowseIO.iOS/CobrowseIO.framework"
    },
    cobrowseIosExtensionProject = new IosBindingProject {
        AssemblyInfoPath = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs",
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
            "./iOS/CobrowseIO.iOS/CobrowseIO.iOS.csproj"
        },
        NuspecFile = "./CobrowseIO.Xamarin.nuspec",
        Version = GetCobrowseNuGetVersion()
    },
    new NuGetArtifact {
        CsprojFiles = new [] { "./iOS/CobrowseIO.AppExtension.iOS/CobrowseIO.AppExtension.iOS.csproj" },
        NuspecFile = "./CobrowseIO.AppExtension.iOS.nuspec",
        Version = GetCobrowseNuGetVersion()
    },
};

string GetCobrowseNuGetVersion() {
    string version = EnvironmentVariable("COBROWSE_NUGET_PACKAGE_VERSION");
    return !string.IsNullOrEmpty(version)
        ? version
        : null;
}