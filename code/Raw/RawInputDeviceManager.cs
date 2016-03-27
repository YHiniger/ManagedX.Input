using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;


namespace ManagedX.Input.Raw
{
	
	/// <summary>A RawInput device manager.</summary>
	public static class RawInputDeviceManager
	{

		private static readonly List<Mouse> mice = new List<Mouse>( 1 );
		private static readonly List<Keyboard> keyboards = new List<Keyboard>( 1 );
		private static readonly List<RawHumanInterfaceDevice> otherDevices = new List<RawHumanInterfaceDevice>();
		private static bool isInitialized;


		private static void OnHidDisconnected( object sender, EventArgs e )
		{
			var device = (RawHumanInterfaceDevice)sender;
			device.Disconnected -= OnHidDisconnected;
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
			for( var d = 0; d < descriptors.Length; d++ )
			{
				var descriptor = descriptors[ d ];
				if( descriptor.DeviceType == InputDeviceType.Mouse )
				{
					if( mouseIndex == 4 )
						continue;
					var m = new Mouse( (GameControllerIndex)( mouseIndex++ ), ref descriptor );
					if( !m.IsDisconnected )
					{
						mice.Add( m );
						m.Disconnected += OnMouseDisconnected;
					}
				}
				else if( descriptor.DeviceType == InputDeviceType.Keyboard )
				{
					if( keyboardIndex == 4 )
						continue;
					var k = new Keyboard( (GameControllerIndex)( keyboardIndex++ ), ref descriptor );
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
						h.Disconnected += OnHidDisconnected;
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


		/// <summary></summary>
		/// <param name="deviceName"></param>
		/// <returns></returns>
		public static Keyboard GetKeyboardByDeviceName( string deviceName )
		{
			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; k++ )
				if( keyboards[ k ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return keyboards[ k ];

			return null;
		}


		/// <summary></summary>
		/// <param name="deviceHandle"></param>
		/// <returns></returns>
		public static Keyboard GetKeyboardByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; k++ )
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


		/// <summary></summary>
		/// <param name="deviceName"></param>
		/// <returns></returns>
		public static Mouse GetMouseByDeviceName( string deviceName )
		{
			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; m++ )
				if( mice[ m ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return mice[ m ];

			return null;
		}


		/// <summary></summary>
		/// <param name="deviceHandle"></param>
		/// <returns></returns>
		public static Mouse GetMouseByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; m++ )
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


		/// <summary></summary>
		/// <param name="deviceName"></param>
		/// <returns></returns>
		public static RawHumanInterfaceDevice GetHidByDeviceName( string deviceName )
		{
			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; d++ )
				if( otherDevices[ d ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return otherDevices[ d ];

			return null;
		}


		/// <summary></summary>
		/// <param name="deviceHandle"></param>
		/// <returns></returns>
		public static RawHumanInterfaceDevice GetHidByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; d++ )
				if( otherDevices[ d ].DeviceHandle == deviceHandle )
					return otherDevices[ d ];

			return null;
		}

		#endregion HID


		/// <summary></summary>
		/// <param name="deviceName"></param>
		/// <returns></returns>
		public static InputDevice GetDeviceByName( string deviceName )
		{
			var mouse = GetMouseByDeviceName( deviceName );
			if( mouse != null )
				return mouse;

			var keyboard = GetKeyboardByDeviceName( deviceName );
			if( keyboard != null )
				return keyboard;

			var hid = GetHidByDeviceName( deviceName );
			if( hid != null )
				return hid;
			
			return null;
		}


		/// <summary></summary>
		/// <param name="deviceHandle"></param>
		/// <returns></returns>
		public static InputDevice GetDeviceByHandle( IntPtr deviceHandle )
		{
			var mouse = GetMouseByDeviceHandle( deviceHandle );
			if( mouse != null )
				return mouse;

			var keyboard = GetKeyboardByDeviceHandle( deviceHandle );
			if( keyboard != null )
				return keyboard;

			var hid = GetHidByDeviceHandle( deviceHandle );
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
				list.AddRange( mice );
				list.AddRange( keyboards );
				list.AddRange( otherDevices );
				return new ReadOnlyCollection<InputDevice>( list );
			}
		}


		/// <summary>Processes window messages to ensure the mouse motion and wheel state are up-to-date.</summary>
		/// <param name="message">A Windows message.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Required by implementation." )]
		public static void WndProc( ref Message message )
		{
			if( message.Msg == 255 ) // WindowMessage.Input
			{
				RawInput rawInput;
				NativeMethods.GetRawInputData( message.LParam, out rawInput );
				if( rawInput.DeviceType == InputDeviceType.Mouse )
				{
					Mouse targetMouse = null;
					for( var m = 0; m < mice.Count; m++ )
					{
						if( mice[ m ].DeviceHandle == rawInput.DeviceHandle )
						{
							targetMouse = mice[ m ];
							break;
						}
					}

					if( targetMouse == null )
						return;

					var mouseState = rawInput.Mouse.Value;
					if( mouseState.State.HasFlag( RawMouseStateIndicators.MoveRelative ) )
						targetMouse.motionDelta += new Point( mouseState.LastX, mouseState.LastY );

					// FIXME - doesn't seem to work... may be about the horizontal wheel ?
					if( mouseState.ButtonsState.HasFlag( RawMouseButtonStateIndicators.Wheel ) )
						targetMouse.WheelValue += mouseState.WheelDelta;
				}
				//else if( rawInput.DeviceType == InputDeviceType.Keyboard )
				//{
				//}
				//else if( rawInput.DeviceType == InputDeviceType.HumanInterfaceDevice )
				//{
				//}
				return;
			}


			if( message.Msg == 522 ) // WindowMessage.MouseWheel
			{
				var delta = message.WParam.ToInt32() >> 16;
				// FIXME - find the mouse whose wheel has been scrolled, something in the form:
				// miceByHandle[ message.LParam ].wheelDelta += delta;
				if( mice.Count > 0 )
					mice[ 0 ].wheelDelta += delta;
				return;
			}


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
			}
		}

	}

}