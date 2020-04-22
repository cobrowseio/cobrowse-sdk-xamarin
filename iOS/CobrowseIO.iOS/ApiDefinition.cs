using System;
using Xamarin.CobrowseIO;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Xamarin.CobrowseIO
{
	// @interface CBIOAgent : NSObject
	[BaseType(typeof(NSObject), Name = "CBIOAgent")]
	interface Agent
	{
		// @property NSString * _Nonnull name;
		[Export("name")]
		string Name { get; set; }

		// @property NSString * _Nonnull id;
		[Export("id")]
		string Id { get; set; }

		// +(instancetype _Nullable)from:(NSDictionary * _Nonnull)dict;
		[Static]
		[Export("from:")]
		[return: NullAllowed]
		Agent From(NSDictionary dict);
	}

	// @interface CBIORESTResource : NSObject
	[BaseType(typeof(NSObject), Name = "CBIORESTResource")]
	interface RESTResource
	{
		// -(NSString * _Nullable)id;
		[NullAllowed, Export("id")]
		string Id { get; }
	}

	// @interface CBIODevice : CBIORESTResource
	[BaseType(typeof(RESTResource), Name = "CBIODevice")]
	interface Device
	{
		// @property NSData * _Nullable token;
		[NullAllowed, Export("token", ArgumentSemantic.Assign)]
		NSData Token { get; set; }

		// -(NSString * _Nonnull)id;
		[Export("id")]
		string Id { get; }
	}

	// @interface CBIOKeyPress : NSObject
	[BaseType(typeof(NSObject), Name = "CBIOKeyPress")]
	interface KeyPress
	{
		// @property (readonly) NSString * _Nonnull key;
		[Export("key")]
		string Key { get; }

		// @property (readonly) NSString * _Nonnull code;
		[Export("code")]
		string Code { get; }

		// @property (readonly) NSString * _Nonnull state;
		[Export("state")]
		string State { get; }
	}

	// @protocol CBIOResponder <NSObject>
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "CBIOResponder")]
	interface Responder
	{
		// @optional -(void)cobrowseTouchesBegan:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
		[Export("cobrowseTouchesBegan:withEvent:")]
		void CobrowseTouchesBegan(NSSet<Touch> touches, TouchEvent @event);

		// @optional -(void)cobrowseTouchesMoved:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
		[Export("cobrowseTouchesMoved:withEvent:")]
		void CobrowseTouchesMoved(NSSet<Touch> touches, TouchEvent @event);

		// @optional -(void)cobrowseTouchesEnded:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
		[Export("cobrowseTouchesEnded:withEvent:")]
		void CobrowseTouchesEnded(NSSet<Touch> touches, TouchEvent @event);

		// @optional -(void)cobrowseTouchesCancelled:(NSSet<CBIOTouch *> * _Nonnull)touches withEvent:(CBIOTouchEvent * _Nonnull)event;
		[Export("cobrowseTouchesCancelled:withEvent:")]
		void CobrowseTouchesCancelled(NSSet<Touch> touches, TouchEvent @event);

		// @optional -(void)cobrowseKeyDown:(CBIOKeyPress * _Nonnull)event;
		[Export("cobrowseKeyDown:")]
		void CobrowseKeyDown(KeyPress @event);
	}

    // TODO remove CB prefix
	// typedef const void (^CBErrorSessionBlock)(NSError * _Nullable, CBIOSession * _Nullable);
	delegate void CBErrorSessionBlock([NullAllowed] NSError error, [NullAllowed] Session session);

	// @interface CBIOSession : CBIORESTResource
	[BaseType(typeof(RESTResource), Name = "CBIOSession")]
	interface Session
	{
		// -(_Bool)isPending;
		[Export("isPending")]
		bool IsPending { get; }

		// -(_Bool)isAuthorizing;
		[Export("isAuthorizing")]
		bool IsAuthorizing { get; }

		// -(_Bool)hasAgent;
		[Export("hasAgent")]
		bool HasAgent { get; }

		// -(_Bool)isActive;
		[Export("isActive")]
		bool IsActive { get; }

		// -(_Bool)isEnded;
		[Export("isEnded")]
		bool IsEnded { get; }

		// -(void)fetch:(CBErrorSessionBlock _Nullable)callback;
		[Export("fetch:")]
		void Fetch([NullAllowed] CBErrorSessionBlock callback);

		// -(void)activate:(CBErrorSessionBlock _Nullable)callback;
		[Export("activate:")]
		void Activate([NullAllowed] CBErrorSessionBlock callback);

		// -(void)end:(CBErrorSessionBlock _Nullable)callback;
		[Export("end:")]
		void End([NullAllowed] CBErrorSessionBlock callback);

		// -(NSString * _Nullable)code;
		[NullAllowed, Export("code")]
		string Code { get; }

		// -(NSString * _Nonnull)state;
		[Export("state")]
		string State { get; }

		// -(CBIOAgent * _Nullable)agent;
		[NullAllowed, Export("agent")]
		Agent Agent { get; }
	}

	// @interface CBIOTouch : NSObject
	[BaseType(typeof(NSObject), Name = "CBIOTouch")]
	interface Touch : INativeObject
	{
		// @property (readonly) UIView * _Nonnull target;
		[Export("target")]
		UIView Target { get; }

		// -(CGPoint)position;
		[Export("position")]
		CGPoint Position { get; }
	}

	// @interface CBIOTouchEvent : NSObject
	[BaseType(typeof(NSObject), Name = "CBIOTouchEvent")]
	interface TouchEvent
	{
		// @property (readonly) CGPoint position;
		[Export("position")]
		CGPoint Position { get; }

		// @property (readonly) NSDate * _Nonnull timestamp;
		[Export("timestamp")]
		NSDate Timestamp { get; }
	}

	// @protocol CobrowseIODelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface CobrowseIODelegate
	{
		// @required -(void)cobrowseSessionDidUpdate:(CBIOSession * _Nonnull)session;
		[Abstract]
		[Export("cobrowseSessionDidUpdate:")]
		void CobrowseSessionDidUpdate(Session session);

		// @required -(void)cobrowseSessionDidEnd:(CBIOSession * _Nonnull)session;
		[Abstract]
		[Export("cobrowseSessionDidEnd:")]
		void CobrowseSessionDidEnd(Session session);

		// @optional -(_Bool)cobrowseShouldAllowTouchEvent:(CBIOTouchEvent * _Nonnull)touchEvent forSession:(CBIOSession * _Nonnull)session;
		[Export("cobrowseShouldAllowTouchEvent:forSession:")]
		bool CobrowseShouldAllowTouchEvent(TouchEvent touchEvent, Session session);

		// @optional -(_Bool)cobrowseShouldAllowKeyEvent:(CBIOKeyPress * _Nonnull)keyEvent forSession:(CBIOSession * _Nonnull)session;
		[Export("cobrowseShouldAllowKeyEvent:forSession:")]
		bool CobrowseShouldAllowKeyEvent(KeyPress keyEvent, Session session);

		// @optional -(_Bool)cobrowseShouldCaptureWindow:(UIWindow * _Nonnull)window;
		[Export("cobrowseShouldCaptureWindow:")]
		bool CobrowseShouldCaptureWindow(UIWindow window);

		// @optional -(void)cobrowseHandleSessionRequest:(CBIOSession * _Nonnull)session;
		[Export("cobrowseHandleSessionRequest:")]
		void CobrowseHandleSessionRequest(Session session);

		// @optional -(void)cobrowseShowSessionControls:(CBIOSession * _Nonnull)session;
		[Export("cobrowseShowSessionControls:")]
		void CobrowseShowSessionControls(Session session);

		// @optional -(void)cobrowseHideSessionControls:(CBIOSession * _Nonnull)session;
		[Export("cobrowseHideSessionControls:")]
		void CobrowseHideSessionControls(Session session);

		// @optional -(NSArray<UIView *> * _Nonnull)cobrowseRedactedViewsForViewController:(UIViewController * _Nonnull)vc;
		[Export("cobrowseRedactedViewsForViewController:")]
		UIView[] CobrowseRedactedViewsForViewController(UIViewController vc);
	}

	// @interface CBIOViewController : UIViewController
	[BaseType(typeof(UIViewController), Name = "CBIOViewController")]
	interface CobrowseViewController
	{
		// -(instancetype _Nonnull)loadSession:(NSString * _Nonnull)codeOrId;
		[Export("loadSession:")]
		CobrowseViewController LoadSession(string codeOrId);

		// -(void)endSession:(id _Nonnull)sender __attribute__((ibaction));
		[Export("endSession:")]
		void EndSession(NSObject sender);
	}

	// @protocol CobrowseIORedacted <NSObject>
	/*
	  Check whether adding [Model] to this declaration is appropriate.
	  [Model] is used to generate a C# class that implements this protocol,
	  and might be useful for protocols that consumers are supposed to implement,
	  since consumers can subclass the generated class instead of implementing
	  the generated interface. If consumers are not supposed to implement this
	  protocol, then [Model] is redundant and will generate code that will never
	  be used.
	*/
	[Protocol(Name = "CobrowseIORedacted"), Model]
	[BaseType(typeof(NSObject))]
	interface CobrowseIORedacted
	{
		// @required -(NSArray<UIView *> * _Nonnull)redactedViews;
		[Abstract]
		[Export("redactedViews")]
		UIView[] RedactedViews { get; }
	}

	// @interface CobrowseIO : NSObject
	[BaseType(typeof(NSObject), Name = "CobrowseIO")]
	interface CobrowseIO
	{
		// @property CBLicense * _Nonnull license;
		[Export("license")]
		[Internal]
		string License { get; set; }

		// @property NSString * _Nonnull api;
		[Export("api")]
		string Api { get; set; }

		// @property NSDictionary<NSString *,NSObject *> * _Nonnull customData;
		[Export("customData", ArgumentSemantic.Assign)]
		[Internal]
		NSDictionary<NSString, NSObject> CustomData { get; set; }

		[Wrap("WeakDelegate")]
		[NullAllowed]
		[Internal]
		CobrowseIODelegate Delegate { get; set; }

		// @property id<CobrowseIODelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly) NSString * _Nonnull deviceId;
		[Export("deviceId")]
		string DeviceId { get; }

		// @property (readonly) CBIODevice * _Nonnull device;
		[Export("device")]
		Device Device { get; }

		// +(instancetype _Nonnull)instance;
		[Static]
		[Export("instance")]
		CobrowseIO Instance();

		// -(instancetype _Nonnull)start;
		[Export("start")]
		CobrowseIO Start();

		// -(instancetype _Nonnull)stop;
		[Export("stop")]
		CobrowseIO Stop();

		// -(instancetype _Nonnull)stop:(void (^ _Nonnull)(NSError * _Nullable))callback;
		[Export("stop:")]
		CobrowseIO Stop(Action<NSError> callback);

		// -(instancetype _Nonnull)createSession:(CBErrorSessionBlock _Nullable)callback;
		[Export("createSession:")]
		CobrowseIO CreateSession([NullAllowed] CBErrorSessionBlock callback);

		// -(instancetype _Nonnull)getSession:(NSString * _Nonnull)idOrCode callback:(CBErrorSessionBlock _Nullable)callback;
		[Export("getSession:callback:")]
		CobrowseIO GetSession(string idOrCode, [NullAllowed] CBErrorSessionBlock callback);

		// -(CBIOSession * _Nullable)currentSession;
		[NullAllowed, Export("currentSession")]
		Session CurrentSession { get; }

		// +(BOOL)isCobrowseNotification:(NSDictionary * _Nonnull)userInfo;
		[Static]
		[Export("isCobrowseNotification:")]
		bool IsCobrowseNotification(NSDictionary userInfo);

		// +(void)onPushNotification:(NSDictionary * _Nonnull)userInfo;
		[Static]
		[Export("onPushNotification:")]
		void OnPushNotification(NSDictionary userInfo);
	}
}