# Changelog

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
