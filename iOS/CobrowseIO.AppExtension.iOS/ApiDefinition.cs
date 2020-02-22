using System;

using ObjCRuntime;
using Foundation;
using UIKit;
using ReplayKit;

namespace CobrowseIOSdk.AppExtension
{
	// @interface CobrowseIOReplayKitExtension : RPBroadcastSampleHandler
	[BaseType(typeof(RPBroadcastSampleHandler))]
	interface CobrowseIOReplayKitExtension
	{
	}
}

