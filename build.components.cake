string COBROWSE_NUGET_PACKAGE_ID = "CobrowseIO.Xamarin";

class NuGetArtifact {
    public string[] CsprojFiles { get; set; }
    public string[] AssemblyInfoFiles { get; set; }
    public string NuspecFile { get; set; }
}

abstract class BindingProject {
    public string AssemblyInfoFile { get; set; }
}

class AndroidBindingProject : BindingProject {
    public string CsprojFile { get; set; }
}

class IosBindingProject : BindingProject {
}

AndroidBindingProject cobrowseAndroidProject;
IosBindingProject cobrowseIosProject;
IosBindingProject cobrowseIosExtensionProject;

BindingProject[] bindingProjects = new BindingProject[] {
    cobrowseAndroidProject = new AndroidBindingProject {
        AssemblyInfoFile = "./Android/CobrowseIO.Android/Properties/AssemblyInfo.cs",
        CsprojFile = "./Android/CobrowseIO.Android/CobrowseIO.Android.csproj",
    },
    cobrowseIosProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.iOS/Properties/AssemblyInfo.cs"
    },
    cobrowseIosExtensionProject = new IosBindingProject {
        AssemblyInfoFile = "./iOS/CobrowseIO.AppExtension.iOS/Properties/AssemblyInfo.cs"
    }
};

NuGetArtifact numbersJavaArtifact, cborJavaArtifact, cobrowseArtifact, cobrowseIosExtensionArtifact;

NuGetArtifact[] nugetArtifacts = new NuGetArtifact[] {
    numbersJavaArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./Android/NumbersJava.Android/NumbersJava.Android.csproj" },
        NuspecFile = "./NumbersJava.Android.nuspec"
    },
    cborJavaArtifact = new NuGetArtifact {
        CsprojFiles = new [] { "./Android/CborJava.Android/CborJava.Android.csproj" },
        NuspecFile = "./CborJava.Android.nuspec"
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