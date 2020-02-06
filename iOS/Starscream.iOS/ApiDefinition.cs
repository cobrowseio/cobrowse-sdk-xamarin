using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace Starscream.iOS
{

    // @interface FoundationStream : NSObject <NSStreamDelegate>
    [BaseType(typeof(NSObject), Name = "_TtC10Starscream16FoundationStream")]
    [Protocol]
    interface FoundationStream : INSStreamDelegate
    {
        // -(void)stream:(NSStream * _Nonnull)aStream handleEvent:(NSStreamEvent)eventCode;
        [Export("stream:handleEvent:")]
        void Stream(NSStream aStream, NSStreamEvent eventCode);
    }

    // @interface WebSocket : NSObject <NSStreamDelegate>
    [BaseType(typeof(NSObject), Name = "_TtC10Starscream9WebSocket")]
    [DisableDefaultCtor]
    [Protocol]
    interface WebSocket : INSStreamDelegate
    {
    }

    [Static]
    partial interface Constants
    {
        // extern double StarscreamVersionNumber;
        [Field("StarscreamVersionNumber", "__Internal")]
        double StarscreamVersionNumber { get; }

        // extern const unsigned char [] StarscreamVersionString;
        [Field("StarscreamVersionString", "__Internal")]
        IntPtr StarscreamVersionString { get; }
    }

}

