# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## 3.6.0 (2023-03-14)


### Features

* add delegate method that's called the first time a session is fetched from the server. ([0e1b37e](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/0e1b37e81aea59c5f9c0ab1230fb6c4b66ee291c))
* Expose new capabilities and unredaction APIs ([#31](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/31)) ([1873a53](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/1873a53dcdd26852393c25f95e45f776ab652edd))
* integrate default consent prompts from the native SDKs. ([966df11](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/966df11ab45dfc2ab4fd3477c84f4d5990ec97f5))
* make the Nuget packages to include corresponding xcframeworks. ([0c9c0be](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/0c9c0bec509e8754048ded1a198f85f8647a5b38))
* reflect new `FullDeviceState` enumeration in the cross-platform API. ([69fba07](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/69fba078b79c1e15c717ef2bab4bc795ca2eedbb))
* reflect new native API in Android and iOS SDKs. ([b79c1f4](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/b79c1f488301bd80dbc53378daaa40c683d1829e))
* replace fat iOS framework with XCFramework bundle. ([6952794](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6952794424a6676f8a5748fc51a77d095325fe7d))
* SDK now targets Android 11 (API 30). ([6a71878](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6a71878023b772250cb3ce4e53d0237aaeb19e83))
* update native SDKs ([#22](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/22)) ([5d0e30c](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/5d0e30c42bc546397a73b5daa54432c382c94969))
* update native SDKs ([#24](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/24)) ([740cf50](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/740cf505d197b6d536b952ba2757daa6c08da431))
* update native SDKs ([#25](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/25)) ([5d0a381](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/5d0a381cfaadb4451c3f68e47d4f70b71077ecf2))
* update native SDKs ([#26](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/26)) ([fdebc7a](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/fdebc7a777a4456fc8ef3114e6bf70bc21694553))


### Bug Fixes

* add a dummy objc-defined class to SwiftCBOR. ([6dcab84](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6dcab8465bb25651bf1a2fe00c5a28911b6aec60))
* add NoBindingEmbedding to the iOS binding projects. ([e11debd](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/e11debd7375f6dafebcf0e56358718f2126605cb)), closes [/github.com/xamarin/xamarin-macios/issues/10774#issuecomment-791518403](https://github.com/cobrowseio//github.com/xamarin/xamarin-macios/issues/10774/issues/issuecomment-791518403)
* linking XCFramework when consuming it from Nuget. ([9700f82](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/9700f82d83bdabe9b6442ae58dfea21973d6ac57))
* stop using deprecated @(JavaDocJar) build action. ([4f1f4f9](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/4f1f4f94475181d45fb5412f48da601be66997b5))
* SwiftCBOR and Starscream NuGet dependency versions. ([#20](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/20)) ([ac12333](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/ac123337b306ded0219676874d2ecb6e220a6993))
* Xamarin.iOS.SwiftRuntimeSupport URL in README.md ([833f7fa](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/833f7fa5b19194181c9ce36f6c1c339a66fc6a99))

### [3.5.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/7eb8ecd0b3380af4a2d6ccb57b57f4cef0eb3ff4...f8e14b8bd28bd1718414ce04f86d7f3db4e03e41) (2022-09-19)

- Android SDK requires targeting Android 12 (API 31)
- New API to allow customization of full device request behavior
- New full device state API
- Android Native SDK [`2.23.0`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#2230-2022-09-06)
- iOS Native SDK [`2.21.2`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#2212-2022-08-29)

### [3.4.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/72f46c5a9875bd473bdefc30f5c1b4d3fd808ed3...7eb8ecd0b3380af4a2d6ccb57b57f4cef0eb3ff4) (2022-02-11)

**Special Consideration**

Due to recent Google Play Store policy changes regarding use of the Accessibility Service APIs there are extra changes required to continue using our support for full device remote control. Please follow the docs here to ensure your Accessibility Service will still function: https://docs.cobrowse.io/sdk-features/full-device-capabilities

- iOS SDK now uses XCFramework instead of a single universal framework
- Added delegate method that's called the first time a session is fetched from the server
- Android Native SDK [`2.18.1`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#2181-2022-02-07)
- iOS Native SDK [`2.17.1`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#2171-2022-02-07)

### [3.3.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/dd8d665297d862e55c3e6cff3b902650f4be292b...72f46c5a9875bd473bdefc30f5c1b4d3fd808ed3) (2021-11-28)

- SDK now targets Android 11 (API 30)
- Android Native SDK [`2.14.1`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#2141-2021-11-15)
- iOS Native SDK [`2.16.0`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#2160-2021-11-15)

### [3.2.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/b1e462b5765b24fb9a41485678300592a93e6161...dd8d665297d862e55c3e6cff3b902650f4be292b) (2021-07-14)

- Added new API for setting desired full device state
- Added support for overriding the default remote control consent prompt
- Android Native SDK [`2.12.1`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#2121-2021-07-13)
- iOS Native SDK [`2.14.0`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#2140---2021-07-13)

### [3.1.3](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/5b02f087bdcc1709bc4292e9a441b51c1d12aa60...b1e462b5765b24fb9a41485678300592a93e6161) (2021-05-06)

- Android Native SDK [`2.10.2`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md)
- iOS Native SDK [`2.12.1`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md)

### [3.1.1](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/af3439a9df9dc1e93f1b5643d5fe69c8e4a9f193...5b02f087bdcc1709bc4292e9a441b51c1d12aa60) (2020-09-19)

- Android Native SDK [`2.6.0`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#250---2020-08-24)
- iOS Native SDK [`2.8.2`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#280---2020-08-24)

### [3.1.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/28ed3e8aaa9d19de8733f2e8800fc0c7f0dc4879...af3439a9df9dc1e93f1b5643d5fe69c8e4a9f193) (2020-08-25)

- Add new `CobrowseIO.License`, `CobrowseIO.CustomData`, `CobrowseIO.Api` properties
- Add new `Session.Agent` property to the cross-platform wrapper API
- The SDK now has `LinkerSafe` attribute
- Android Native SDK [`2.5.0`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#250---2020-08-24)
- iOS Native SDK [`2.8.1`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#280---2020-08-24)

### [3.0.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/27638c65b855d2e899e6626f47e68c99d6d52578...28ed3e8aaa9d19de8733f2e8800fc0c7f0dc4879) (2020-05-07)

- Add cross-platform wrapper API (.NET Standard 1.0)
- Android Native SDK [`2.2.0`](https://github.com/cobrowseio/cobrowse-sdk-android-binary/blob/master/CHANGELOG.md#200---2019-11-04)
- iOS Native SDK [`2.4.0`](https://github.com/cobrowseio/cobrowse-sdk-ios-binary/blob/master/CHANGELOG.md#240---2020-02-19)
