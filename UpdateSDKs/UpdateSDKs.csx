#!/usr/bin/env dotnet-script

/*
 * This is a C# script which does the following:
 * - Pull the latest Android binaries
 * - Pull the latest iOS binaries
 * - Reflects the latest native Android SDK in the Android bindings project
 * - Reflects the latest native iOS SDK in the iOS bindings project
 *
 * https://github.com/dotnet-script/dotnet-script
 */

#r "nuget: LibGit2Sharp, 0.29.0"

using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using LibGit2Sharp;

private readonly string _root = new FileInfo(GetSourceFile()).Directory.Parent.FullName;
private readonly string _androidBinary = "cobrowse-sdk-android-binary";
private readonly string _iOSBinary = "cobrowse-sdk-ios-binary";

using (var android = new Repository(Path.Combine(_root, _androidBinary)))
{
    Console.WriteLine("Android repository is at {0}", android.Info.Path);
    android.PullLatest();

    string maven = Path.Combine(android.Info.WorkingDirectory, "io", "cobrowse", "cobrowse-sdk-android", "maven-metadata.xml");
    Debug.Assert(File.Exists(maven));

    string androidVersion = Regex.Match(File.ReadAllText(maven), @"\<release\>([\S]*?)\<\/release\>").Groups[1].Value;
    Console.WriteLine("Latest native Android SDK is {0}", androidVersion);

    string csproj = Path.Combine(_root, "Android", "CobrowseIO.Android", "CobrowseIO.Android.csproj");
    Debug.Assert(File.Exists(csproj));

    string text = File.ReadAllText(csproj);

    // Replace Version
    text = new Regex(@"<Version>[0-9]+\.[0-9]+\.[0-9]+<\/Version>")
        .Replace(text,
                 "<Version>" + androidVersion + "</Version>");

    // Replace AndroidLibrary
    text = new Regex("(?<=AndroidLibrary\\ Include\\=\").*(?=\")")
        .Replace(text,
                 @"..\..\" + _androidBinary + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + ".aar");

    // Replace JavaDocJar
    text = new Regex("(?<=JavaDocJar\\ Include\\=\").*(?=\")")
        .Replace(text,
                 @"..\..\" + _androidBinary + @"\io\cobrowse\cobrowse-sdk-android\" + androidVersion + @"\cobrowse-sdk-android-" + androidVersion + "-javadoc.jar");

    // Replace pom comment
    text = new Regex(@"https:\/\/github\.com\/cobrowseio\/cobrowse-sdk-android-binary\/blob\/master\/io\/cobrowse\/cobrowse-sdk-android\/[0-9]+\.[0-9]+\.[0-9]+\/cobrowse-sdk-android-[0-9]+\.[0-9]+\.[0-9]+\.pom")
        .Replace(text,
                 "https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/io/cobrowse/cobrowse-sdk-android/" + androidVersion + "/cobrowse-sdk-android-" + androidVersion + ".pom");

    File.WriteAllText(csproj, text);
}

using (var iOS = new Repository(Path.Combine(_root, _iOSBinary)))
{
    Console.WriteLine("iOS repository is at {0}", iOS.Info.Path);
    iOS.PullLatest();

    string podspec = Path.Combine(iOS.Info.WorkingDirectory, "CobrowseIO.podspec");
    Debug.Assert(File.Exists(podspec));

    string iOSVersion = Regex.Match(File.ReadAllText(podspec), @"s\.version = '([\S]*?)'").Groups[1].Value;
    Console.WriteLine("Latest native iOS SDK is {0}", iOSVersion);

    string[] csprojs = new string[]
    {
        Path.Combine(_root, "iOS", "CobrowseIO.AppExtension.iOS", "CobrowseIO.AppExtension.iOS.csproj"),
        Path.Combine(_root, "iOS", "CobrowseIO.iOS", "CobrowseIO.iOS.csproj")
    };
    foreach (string csproj in csprojs)
    {
        Debug.Assert(File.Exists(csproj));

        string text = File.ReadAllText(csproj);

        // Replace Version
        text = new Regex(@"<Version>[0-9]+\.[0-9]+\.[0-9]+<\/Version>")
            .Replace(text,
                     "<Version>" + iOSVersion + "</Version>");

        File.WriteAllText(csproj, text);
    }    
}

private static string GetSourceFile([CallerFilePath] string file = "") => file;

private static void PullLatest(this Repository repo)
{
    Commands.Checkout(repo, "master");
    Commands.Pull(
        repo,
        new Signature(
            new Identity("Cobrowse.io Bot", "github@cobrowse.io"),
            DateTimeOffset.Now),
        new PullOptions { MergeOptions = new MergeOptions { FastForwardStrategy = FastForwardStrategy.Default } });
}
