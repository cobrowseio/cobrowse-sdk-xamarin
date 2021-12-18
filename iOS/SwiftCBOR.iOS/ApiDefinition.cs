using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace SwiftCBOR.iOS
{
    /* SwiftCBOR doesn't expose any its own Swift classes
     * See SwiftCBOR.framework/Headers/SwiftCBOR-Swift.h
     * In order to avoid build issues caused by this,
     * add a dummy file to SwiftCBOR sources:
     *
     * XamarinSwiftHelper.swift:
     * 
     * ```
     * import Foundation
     * 
     * @objc(XamarinSwiftHelper)
     * open class XamarinSwiftHelper : NSObject {
     *     open func getValue() -> String {
     *         return "SwiftCBOR";
     *     }
     * }
     * ```
     */

    // @interface XamarinSwiftHelper : NSObject
    [BaseType(typeof(NSObject))]
    interface XamarinSwiftHelper
    {
        // -(NSString * _Nonnull)getValue __attribute__((warn_unused_result));
        [Export("getValue")]
        string Value { get; }
    }

}

