// .versionrc.js
const tracker = [
  {
    filename: 'SDK/CobrowseIO/CobrowseIO.csproj',
    updater: {
      readVersion: function(contents) {
        return contents.match(/<Version>(.*)<\/Version>/)[1]
      },
      writeVersion: function (contents, version) {
        return contents.replace(/(<Version>)(.*)(<\/Version>)/, `$1${version}$3`)
      }
    }
  },
  {
    filename: 'CobrowseIO.DotNet.iOS.AppExtension.nuspec',
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
    filename: 'CobrowseIO.DotNet.nuspec',
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