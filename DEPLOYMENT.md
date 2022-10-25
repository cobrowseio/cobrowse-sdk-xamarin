## Deployment

This SDK is available on NuGet.org. To deploy a new version:

```
./build.sh --nugetOrgApiKey="$(NUGET_ORG_API_KEY)" --cobrowseNuGetPackageVersion="$(COBROWSE_NUGET_PACKAGE_VERSION)" 
```

- `NUGET_ORG_API_KEY` - [a _push_ API key](https://www.nuget.org/account/apikeys) to access NuGet.org
- `COBROWSE_NUGET_PACKAGE_VERSION` - desired package version (e.g. `3.2.0`)
