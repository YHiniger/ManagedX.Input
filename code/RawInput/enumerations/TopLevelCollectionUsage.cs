namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Helper for <see cref="TopLevelCollectionUsage"/>.</summary>
	internal enum UsagePage : int
	{

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_UNDEFINED" )]
		Undefined,

		// Usage is a member of the GenericUsage enumeration.
		[Source( "HIDUsage.h", "HID_USAGE_PAGE_GENERIC" )]
		Generic,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_SIMULATION" )]
		Simulation,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_VR" )]
		VR,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_SPORT" )]
		Sport,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_GAME" )]
		Game,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_GENERIC_DEVICE" )]
		GenericDevice,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_KEYBOARD" )]
		Keyboard,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_LED" )]
		LED,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_BUTTON" )]
		Button,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_ORDINAL" )]
		Ordinal,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_TELEPHONY" )]
		Telephony,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_CONSUMER" )]
		Consumer,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_DIGITIZER" )]
		Digitizer,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_HAPTICS" )]
		Haptics,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_PID" )]
		PID,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_UNICODE" )]
		Unicode,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_ALPHANUMERIC" )]
		Alphanumeric = 20,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_SENSOR" )]
		Sensor = 32,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_BARCODE_SCANNER" )]
		BarcodeScanner = 0x8C,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_WEIGHING_DEVICE" )]
		WeighingDevice,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_MAGNETIC_STRIPE_READER" )]
		MagneticStripeReader,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_CAMERA_CONTROL" )]
		CameraControl = 0x90,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_ARCADE" )]
		Arcade,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_MICROSOFT_BLUETOOTH_HANDSFREE" )]
		MicrosoftBluetoothHandsfree = 0xFFF3,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_VENDOR_DEFINED_BEGIN" )]
		VendorDefinedBegin = 0xFF00,

		[Source( "HIDUsage.h", "HID_USAGE_PAGE_UNDEFINED" )]
		VendorDefinedEnd = 0xFFFF

	}


	/// <summary>Usage for <see cref="UsagePage.Generic"/>.
	/// <para>Helper for <see cref="TopLevelCollectionUsage"/>.</para>
	/// </summary>
	internal enum GenericUsage : int
	{

		Undefined,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_POINTER" )]
		Pointer,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_MOUSE" )]
		Mouse,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_JOYSTICK" )]
		Joystick = 4,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_GAMEPAD" )]
		Gamepad,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_KEYBOARD" )]
		Keyboard,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_KEYPAD" )]
		Keypad,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_MULTI_AXIS_CONTROLLER" )]
		MultiAxisController,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_TABLET_PC_SYSTEM_CTL" )]
		TabletPCSystemControl,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_PORTABLE_DEVICE_CONTROL" )]
		PortableDeviceControl = 0x0D,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_INTERACTIVE_CONTROL" )]
		InteractiveControl,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_COUNTED_BUFFER" )]
		CountedBuffer = 0x3A,

		[Source( "HIDUsage.h", "HID_USAGE_GENERIC_SYSTEM_CTL" )]
		SystemControl = 0x80

	}



	/// <summary>Enumerates some top-level collection (TLC) UsagePage and Usage combinations.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff538842%28v=vs.85%29.aspx</remarks>
	public enum TopLevelCollectionUsage : int
	{
		
		/// <summary>Undefined.</summary>
		Undefined = 0,


		#region Generic

		/// <summary>Generic page, no usage specified.</summary>
		GenericPage = UsagePage.Generic,


		/// <summary>Pointers.</summary>
		Pointer = UsagePage.Generic | ( GenericUsage.Pointer << 16 ),

		/// <summary>Mice.</summary>
		Mouse = UsagePage.Generic | ( GenericUsage.Mouse << 16 ),

		/// <summary>Joysticks.</summary>
		Joystick = UsagePage.Generic | ( GenericUsage.Joystick << 16 ),

		/// <summary>Gamepads.</summary>
		Gamepad = UsagePage.Generic | ( GenericUsage.Gamepad << 16 ),

		/// <summary>Keyboards.</summary>
		Keyboard = UsagePage.Generic | ( GenericUsage.Keyboard << 16 ),

		/// <summary>Keypads.</summary>
		Keypad = UsagePage.Generic | ( GenericUsage.Keypad << 16 ),

		/// <summary>Multi-axis controller.</summary>
		MultiAxisController = UsagePage.Generic | ( GenericUsage.MultiAxisController << 16 ),

		/// <summary>System controls.</summary>
		SystemControl = UsagePage.Generic | ( GenericUsage.SystemControl << 16 ),

		#endregion Generic


		#region Consumer

		/// <summary>Consumer page, no usage specified.</summary>
		ConsumerPage = UsagePage.Consumer,


		/// <summary>Consumer control.</summary>
		ConsumerControl = UsagePage.Consumer | ( 1 << 16 ),

		#endregion Consumer

	}

}