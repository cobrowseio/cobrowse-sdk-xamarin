using System;

using ObjCRuntime;
using Foundation;
using UIKit;
using ReplayKit;

namespace CobrowseIOSdk.AppExtension
{
	// @interface CBORSocket : NSObject
	[BaseType(typeof(NSObject), Name = "_TtC22CobrowseIOAppExtension10CBORSocket")]
	[DisableDefaultCtor]
	interface CBORSocket
	{
		// @property (nonatomic) double maxReconnectDelay;
		[Export("maxReconnectDelay")]
		double MaxReconnectDelay { get; set; }

		// @property (nonatomic) double minReconnectDelay;
		[Export("minReconnectDelay")]
		double MinReconnectDelay { get; set; }

		// -(instancetype _Nonnull)initWithRequest:(NSURLRequest * _Nonnull (^ _Nonnull)(void))request __attribute__((objc_designated_initializer));
		[Export("initWithRequest:")]
		[DesignatedInitializer]
		IntPtr Constructor(Func<NSUrlRequest> request);

		// -(void)onMessage:(NSString * _Nonnull)event listener:(void (^ _Nonnull)(NSDictionary * _Nonnull))listener;
		[Export("onMessage:listener:")]
		void OnMessage(string @event, Action<NSDictionary> listener);

		// -(void)onConnect:(void (^ _Nonnull)(void))listener;
		[Export("onConnect:")]
		void OnConnect(Action listener);

		// -(void)onDisconnect:(void (^ _Nonnull)(void))listener;
		[Export("onDisconnect:")]
		void OnDisconnect(Action listener);

		// -(uint64_t)messageLag __attribute__((warn_unused_result));
		[Export("messageLag")]
		ulong MessageLag { get; }

		// -(BOOL)isConneced __attribute__((warn_unused_result));
		[Export("isConneced")]
		bool IsConneced { get; }

		// -(void)disconnect;
		[Export("disconnect")]
		void Disconnect();

		// -(void)send:(NSString * _Nonnull)event data:(NSDictionary<NSString *,id> * _Nonnull)data completion:(void (^ _Nullable)(void))completion;
		[Export("send:data:completion:")]
		void Send(string @event, NSDictionary<NSString, NSObject> data, [NullAllowed] Action completion);

		// -(void)sendPing;
		[Export("sendPing")]
		void SendPing();
	}

	// @interface CobrowseIOReplayKitExtension : RPBroadcastSampleHandler
	[BaseType(typeof(RPBroadcastSampleHandler))]
	interface CobrowseIOReplayKitExtension
	{
	}
}

