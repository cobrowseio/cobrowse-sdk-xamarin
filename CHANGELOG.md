# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## [3.8.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/v3.7.0...v3.8.0) (2023-08-18)


### Features

* update native SDKs ([#48](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/48)) ([0fb5a3c](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/0fb5a3c080907c850a33580499b29da746b0ab7e))
* update native SDKs ([#49](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/49)) ([b415c62](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/b415c62a4a7ae5e6c1e2dd182c2d86511649051d))
* update native SDKs ([#50](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/50)) ([d6ae9c8](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/d6ae9c806e44c059e6ea2c81ddd600c28d754b31))
* update native SDKs ([#51](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/51)) ([60f6618](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/60f6618fc2748d7163285d78c0ab51f752ca488f))
* update native SDKs ([#52](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/52)) ([ce6c676](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/ce6c67606568ca7631a8f25099ebcf96173ec38a))

## [3.7.0](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/v3.6.1...v3.7.0) (2023-07-24)


### Features

* allow clients to prevent devices from being registered (`canRegister: false` option) ([#41](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/41)) ([766dba5](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/766dba5ecd3fe569f75cad290e4111bdfbf7ddc4))
* Reflect new `webviewRedactedViews` SDK's API  ([#47](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/47)) ([1e9a125](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/1e9a12510bcbbed292d3d298f96be0b8f7d7fdfc))
* update native SDKs ([#34](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/34)) ([6043077](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6043077fbdd6f356455bd560d78291e3288b12df))
* update native SDKs ([#35](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/35)) ([ee3c2a1](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/ee3c2a154415f1832f995bd2b6c1e4cef6d3a434))
* update native SDKs ([#36](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/36)) ([6c91094](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6c91094d85e55ed63d35b002df02749da92a2768))
* update native SDKs ([#39](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/39)) ([1bf5bb8](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/1bf5bb81cf12dcb454fb1bd0d57e0a9d6d175341))
* update native SDKs ([#40](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/40)) ([e7e9dab](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/e7e9dabf8744d880b755f914283ec8ef589f791c))

### [3.6.1](https://github.com/cobrowseio/cobrowse-sdk-xamarin/compare/v3.6.0...v3.6.1) (2023-03-14)

### Features

* Android Native SDK [`2.25.1`](https://github.com/cobrowseio/cobrowse-sdk-android/blob/master/CHANGELOG.md#2251-2023-03-13)
* iOS Native SDK [`2.25.0`](https://github.com/cobrowseio/cobrowse-sdk-apple/blob/master/CHANGELOG.md#2250-2023-03-09)

## 3.6.0 (2023-03-14)

### Features

* Add delegate method that's called the first time a session is fetched from the server. ([0e1b37e](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/0e1b37e81aea59c5f9c0ab1230fb6c4b66ee291c))
* Expose new capabilities and unredaction APIs ([#31](https://github.com/cobrowseio/cobrowse-sdk-xamarin/issues/31)) ([1873a53](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/1873a53dcdd26852393c25f95e45f776ab652edd))
* Integrate default consent prompts from the native SDKs. ([966df11](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/966df11ab45dfc2ab4fd3477c84f4d5990ec97f5))
* Reflect new `FullDeviceState` API. ([69fba07](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/69fba078b79c1e15c717ef2bab4bc795ca2eedbb))
* Replace fat iOS framework with XCFramework bundle. ([6952794](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/6952794424a6676f8a5748fc51a77d095325fe7d))
* Android Native SDK [`2.25.1`](https://github.com/cobrowseio/cobrowse-sdk-android/blob/master/CHANGELOG.md#2251-2023-03-13)
* iOS Native SDK [`2.25.0`](https://github.com/cobrowseio/cobrowse-sdk-apple/blob/master/CHANGELOG.md#2250-2023-03-09)

### Bug Fixes

* Add `NoBindingEmbedding` to the iOS binding projects. ([e11debd](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/e11debd7375f6dafebcf0e56358718f2126605cb)), closes [/github.com/xamarin/xamarin-macios/issues/10774#issuecomment-791518403](https://github.com/cobrowseio//github.com/xamarin/xamarin-macios/issues/10774/issues/issuecomment-791518403)
* Linking XCFramework when consuming it from Nuget. ([9700f82](https://github.com/cobrowseio/cobrowse-sdk-xamarin/commit/9700f82d83bdabe9b6442ae58dfea21973d6ac57))
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
