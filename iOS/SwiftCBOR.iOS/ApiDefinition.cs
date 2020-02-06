using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace SwiftCBOR.iOS
{
	// SwiftCBOR doesn't expose any its own Swift classes
	// See SwiftCBOR.framework/Headers/SwiftCBOR-Swift.h

	// @interface XamarinSwiftHelper : NSObject
	[BaseType(typeof(NSObject))]
	interface XamarinSwiftHelper
	{
		// -(NSString * _Nonnull)getValue __attribute__((warn_unused_result));
		[Export("getValue")]
		string Value { get; }
	}

}

