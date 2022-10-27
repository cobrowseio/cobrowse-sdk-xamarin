// .versionrc.js
const tracker = [
  {
    filename: 'XamarinSDK/CobrowseIO.Xamarin/Properties/AssemblyInfo.cs',
    updater: {
      readVersion: function(contents) {
        return contents.match(/\[assembly: AssemblyVersion\("([0-9]+\.[0-9]+\.[0-9]+)/)[1]
      },
      writeVersion: function (contents, version) {
        var modified = contents.replace(/(\[assembly: AssemblyVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        modified = modified.replace(/(\[assembly: AssemblyInformationalVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        return modified
      }
    }
  },
  {
    filename: 'XamarinSDK/CobrowseIO.Xamarin.Abstractions/Properties/AssemblyInfo.cs',
    updater: {
      readVersion: function(contents) {
        return contents.match(/\[assembly: AssemblyVersion\("([0-9]+\.[0-9]+\.[0-9]+)/)[1]
      },
      writeVersion: function (contents, version) {
        var modified = contents.replace(/(\[assembly: AssemblyVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        modified = modified.replace(/(\[assembly: AssemblyInformationalVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        return modified
      }
    }
  },
  {
    filename: 'XamarinSDK/CobrowseIO.Xamarin.Android/Properties/AssemblyInfo.cs',
    updater: {
      readVersion: function(contents) {
        return contents.match(/\[assembly: AssemblyVersion\("([0-9]+\.[0-9]+\.[0-9]+)/)[1]
      },
      writeVersion: function (contents, version) {
        var modified = contents.replace(/(\[assembly: AssemblyVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        modified = modified.replace(/(\[assembly: AssemblyInformationalVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        return modified
      }
    }
  },
  {
    filename: 'XamarinSDK/CobrowseIO.Xamarin.iOS/Properties/AssemblyInfo.cs',
    updater: {
      readVersion: function(contents) {
        return contents.match(/\[assembly: AssemblyVersion\("([0-9]+\.[0-9]+\.[0-9]+)/)[1]
      },
      writeVersion: function (contents, version) {
        var modified = contents.replace(/(\[assembly: AssemblyVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        modified = modified.replace(/(\[assembly: AssemblyInformationalVersion\(")([0-9\.]+)(\"\)\])/, `$1${version}$3`)
        return modified
      }
    }
  },
  {
    filename: 'CobrowseIO.Xamarin.nuspec',
    updater: {
      readVersion: function(contents) {
        return contents.match(/<version>(.*)<\/version>/)[1]
      },
      writeVersion: function (contents, version) {
        return contents.replace(/(<version>)(.*)(<\/version>)/, `$1${version}$3`)
      }
    }
  },
  {
    filename: 'CobrowseIO.AppExtension.iOS.nuspec',
    updater: {
      readVersion: function(contents) {
        return contents.match(/<version>(.*)<\/version>/)[1]
      },
      writeVersion: function (contents, version) {
        return contents.replace(/(<version>)(.*)(<\/version>)/, `$1${version}$3`)
      }
    }
  }
]

module.exports = {
  packageFiles: tracker,
  bumpFiles: tracker
}