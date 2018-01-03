using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;


namespace ManagedX.Input.Raw
{

	/// <summary>The RawInput device manager.</summary>
	public static class RawInputDeviceManager
	{

		private static readonly List<Mouse> mice = new List<Mouse>( 1 );                                                // Most systems have only one mouse,
		private static readonly List<Keyboard> keyboards = new List<Keyboard>( 1 );                                     // and only one keyboard,
		private static readonly List<RawHumanInterfaceDevice> otherDevices = new List<RawHumanInterfaceDevice>( 2 );    // but for some reason the mouse and keyboard have (most of the time) a corresponding HID.
		private static bool isInitialized;



		private static void OnHIDDisconnected( object sender, EventArgs e )
		{
			var device = (RawHumanInterfaceDevice)sender;
			device.Disconnected -= OnHIDDisconnected;
			otherDevices.Remove( device );
		}

		private static void OnMouseDisconnected( object sender, EventArgs e )
		{
			var mouse = (Mouse)sender;
			mouse.Disconnected -= OnMouseDisconnected;
			mice.Remove( mouse );
		}

		private static void OnKeyboardDisconnected( object sender, EventArgs e )
		{
			var keyboard = (Keyboard)sender;
			keyboard.Disconnected -= OnKeyboardDisconnected;
			keyboards.Remove( keyboard );
		}


		private static void Initialize()
		{
			var keyboardIndex = 0;
			var mouseIndex = 0;
			var hidIndex = 0;

			var descriptors = NativeMethods.GetRawInputDeviceList();
			for( var d = 0; d < descriptors.Length; ++d )
			{
				var descriptor = descriptors[ d ];
				if( descriptor.DeviceType == InputDeviceType.Mouse )
				{
					//if( mouseIndex == 4 )
					//	continue;
					var m = new Mouse( mouseIndex++, ref descriptor );
					if( !m.IsDisconnected )
					{
						mice.Add( m );
						m.Disconnected += OnMouseDisconnected;
					}
				}
				else if( descriptor.DeviceType == InputDeviceType.Keyboard )
				{
					//if( keyboardIndex == 4 )
					//	continue;
					var k = new Keyboard( keyboardIndex++, ref descriptor );
					if( !k.IsDisconnected )
					{
						keyboards.Add( k );
						k.Disconnected += OnKeyboardDisconnected;
					}
				}
				else // if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				{
					var h = new RawHumanInterfaceDevice( hidIndex++, ref descriptor );
					if( !h.IsDisconnected )
					{
						otherDevices.Add( h );
						h.Disconnected += OnHIDDisconnected;
					}
				}
			}

			isInitialized = true;
		}


		#region Keyboard

		/// <summary>Gets the keyboard.</summary>
		public static Keyboard Keyboard
		{
			get
			{
				if( !isInitialized )
					Initialize();

				if( keyboards.Count > 0 )
					return keyboards[ 0 ];

				return null;
			}
		}


		/// <summary>Gets the keyboards.</summary>
		public static ReadOnlyCollection<Keyboard> Keyboards
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Keyboard>( keyboards );
			}
		}


		/// <summary>Returns a keyboard given its device name.</summary>
		/// <param name="deviceName">The device name of the requested keyboard.</param>
		/// <returns>Returns the requested keyboard, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static Keyboard GetKeyboardByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; ++k )
				if( keyboards[ k ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return keyboards[ k ];

			return null;
		}


		/// <summary>Returns a keyboard given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested keyboard.</param>
		/// <returns>Returns the requested keyboard, or null.</returns>
		public static Keyboard GetKeyboardByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; ++k )
				if( keyboards[ k ].DeviceHandle == deviceHandle )
					return keyboards[ k ];

			return null;
		}

		#endregion Keyboard


		#region Mouse

		/// <summary>Gets the primary mouse.</summary>
		public static Mouse Mouse
		{
			get
			{
				if( !isInitialized )
					Initialize();

				if( mice.Count > 0 )
					return mice[ 0 ];

				return null;
			}
		}


		/// <summary>Gets the mice.</summary>
		public static ReadOnlyCollection<Mouse> Mice
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Mouse>( mice );
			}
		}


		/// <summary>Returns a mouse given its device name.</summary>
		/// <param name="deviceName">The device name of the requested mouse.</param>
		/// <returns>Returns the requested mouse, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static Mouse GetMouseByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; ++m )
				if( mice[ m ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return mice[ m ];

			return null;
		}


		/// <summary>Returns a mouse given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested mouse.</param>
		/// <returns>Returns the requested mouse, or null.</returns>
		public static Mouse GetMouseByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; ++m )
				if( mice[ m ].DeviceHandle == deviceHandle )
					return mice[ m ];

			return null;
		}

		#endregion Mouse


		#region HID

		/// <summary>Gets a read-only collection containing all HIDs.</summary>
		public static ReadOnlyCollection<RawHumanInterfaceDevice> HumanInterfaceDevices
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<RawHumanInterfaceDevice>( otherDevices );
			}
		}


		/// <summary>Returns a HID given its device name.</summary>
		/// <param name="deviceName">The device name of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static RawHumanInterfaceDevice GetHIDByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; ++d )
				if( otherDevices[ d ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return otherDevices[ d ];

			return null;
		}


		/// <summary>Returns a HID given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static RawHumanInterfaceDevice GetHIDByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; ++d )
				if( otherDevices[ d ].DeviceHandle == deviceHandle )
					return otherDevices[ d ];

			return null;
		}

		#endregion HID


		/// <summary>Returns an input device given its device name.</summary>
		/// <param name="deviceName">The name of the requested input device.</param>
		/// <returns>Returns the requested input device, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static InputDevice GetDeviceByName( string deviceName )
		{
			try
			{
				var mouse = GetMouseByDeviceName( deviceName );
				if( mouse != null )
					return mouse;

				var keyboard = GetKeyboardByDeviceName( deviceName );
				if( keyboard != null )
					return keyboard;

				var hid = GetHIDByDeviceName( deviceName );
				if( hid != null )
					return hid;
			}
			catch( ArgumentException )
			{
				throw;
			}

			return null;
		}


		/// <summary>Returns an input device given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested input device.</param>
		/// <returns>Returns the requested input device, or null.</returns>
		public static InputDevice GetDeviceByHandle( IntPtr deviceHandle )
		{
			var mouse = GetMouseByDeviceHandle( deviceHandle );
			if( mouse != null )
				return mouse;

			var keyboard = GetKeyboardByDeviceHandle( deviceHandle );
			if( keyboard != null )
				return keyboard;

			var hid = GetHIDByDeviceHandle( deviceHandle );
			if( hid != null )
				return hid;

			return null;
		}


		/// <summary>Gets a read-only collection containing all raw input devices.</summary>
		public static ReadOnlyCollection<InputDevice> Devices
		{
			get
			{
				var list = new List<InputDevice>();
				list.AddRange( keyboards );
				list.AddRange( mice );
				list.AddRange( otherDevices );
				return new ReadOnlyCollection<InputDevice>( list );
			}
		}


		/// <summary>Processes window messages to ensure the mouse motion and wheel state are up-to-date.</summary>
		/// <param name="message">A Windows message.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		public static bool ProcessWindowMessage( ref Message message )
		{
			if( message.Msg == 254 ) // WindowMessage.InputDeviceChange
			{
				var wParam = message.WParam.ToInt32();
				if( wParam == 1 )
				{
					// Device arrival
				}
				else if( wParam == 2 )
				{
					// Device removal
				}
				// TODO - mark the device as disconnected on removal, otherwise initialize a new RawInputDevice.
				return true;
			}


			if( message.Msg == 255 ) // WindowMessage.Input
			{
				NativeMethods.GetRawInputData( message.LParam, out RawInput rawInput );
				if( rawInput.Header.DeviceType == InputDeviceType.Mouse )
				{
					var targetMouse = GetMouseByDeviceHandle( rawInput.Header.DeviceHandle );
					if( targetMouse == null )
						return false;

					var mouseState = rawInput.Mouse;
					if( mouseState.State.HasFlag( RawMouseStateIndicators.MoveRelative ) )
						targetMouse.motionDelta += mouseState.Motion;
					else
						targetMouse.motionDelta = mouseState.Motion;

					//targetMouse.RawButtons = mouseState.RawButtons;

					// FIXME - doesn't seem to work... may be about the horizontal wheel ?
					if( mouseState.ButtonsState.HasFlag( RawMouseButtonStateIndicators.Wheel ) )
						targetMouse.WheelValue += mouseState.WheelDelta;
				}
				//else if( rawInput.DeviceType == InputDeviceType.Keyboard )
				//{
				//	var targetKeyboard = GetKeyboardByDeviceHandle( rawInput.DeviceHandle );
				//	if( targetKeyboard == null )
				//		return false;

				//	// ...
				//}
				//else if( rawInput.DeviceType == InputDeviceType.HumanInterfaceDevice )
				//{
				//	var targetHid = GetHidByDeviceHandle( rawInput.DeviceHandle );
				//	if( targetHid == null )
				//		return false;

				//	// ...
				//}
				return true;
			}


			if( message.Msg == 522 ) // WindowMessage.MouseWheel
			{
				// the high-order short int [of WParam] indicates the wheel rotation distance, expressed in multiples or divisions of 120;
				// the low-order short int indicates the virtual key code of buttons and various modifiers (Ctrl, Shift) which are currently down (pressed).
				// LParam indicates the x (low-order) and y (high-order) coordinate of the cursor; we don't need this here.

				var delta = (int)( message.WParam.ToInt64() & 0xFFFF0000L ) >> 16;
				// works both on x64 and x86 platforms, unlike "message.WParam.ToInt32()" which causes an OverflowException.

				mice[ 0 ].wheelDelta += delta;
				// FIXME - how do we know which mouse had its wheel scrolled ?
				return true;
			}

			return false;
		}


		/// <summary>Causes the target window to receive raw input messages.
		/// <para>Important: that window must then override its WndProc method to call <see cref="ProcessWindowMessage"/> prior to its base method.</para>
		/// </summary>
		/// <param name="targetWindow">The target window.</param>
		/// <param name="options">One or more <see cref="RawInputDeviceRegistrationOptions"/>.</param>
		/// <param name="usages">At least one TLC usage.</param>
		/// <exception cref="ArgumentException"/>
		public static void Register( IWin32Window targetWindow, RawInputDeviceRegistrationOptions options, params TopLevelCollectionUsage[] usages )
		{
			if( usages == null || usages.Length == 0 )
				throw new ArgumentException( "No TLC usage specified.", "usages" );


			var windowHandle = targetWindow?.Handle ?? IntPtr.Zero;

			var devices = new RawInputDevice[ usages.Length ];
			for( var d = 0; d < usages.Length; ++d )
				devices[ d ] = new RawInputDevice( usages[ d ], options, windowHandle );

			NativeMethods.RegisterRawInputDevices( devices );
		}

	}

}