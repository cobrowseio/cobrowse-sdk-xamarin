#!/usr/bin/env bash

# debug log
set -x

# restore nuget packages
nuget restore

# build Android projects
msbuild /p:Configuration=Release /t:Build Android/NumbersJava.Android/NumbersJava.Android.csproj
msbuild /p:Configuration=Release /t:Build Android/CborJava.Android/CborJava.Android.csproj
msbuild /p:Configuration=Release /t:Build Android/CobrowseIO.Android/CobrowseIO.Android.csproj

# build iOS projects
msbuild /p:Configuration=Release /t:Build iOS/SwiftCBOR.iOS/SwiftCBOR.iOS.csproj
msbuild /p:Configuration=Release /t:Build iOS/Starscream.iOS/Starscream.iOS.csproj
msbuild /p:Configuration=Release /t:Build iOS/CobrowseIO.iOS/CobrowseIO.iOS.csproj
msbuild /p:Configuration=Release /t:Build iOS/CobrowseIO.AppExtension.iOS/CobrowseIO.AppExtension.iOS.csproj

# create nupkg archives
nuget pack NumbersJava.Android.nuspec -OutputDirectory LocalPackages/
nuget pack CborJava.Android.nuspec -OutputDirectory LocalPackages/
nuget pack CobrowseIO.Android.nuspec -OutputDirectory LocalPackages/
nuget pack SwiftCBOR.iOS.nuspec -OutputDirectory LocalPackages/
nuget pack Starscream.iOS.nuspec -OutputDirectory LocalPackages/
nuget pack CobrowseIO.iOS.nuspec -OutputDirectory LocalPackages/
nuget pack CobrowseIO.AppExtension.iOS.nuspec -OutputDirectory LocalPackages/

# push nuget packages to NuGet.org
# nuget push LocalPackages/*.nupkg <api-key>