using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace ManagedX.Input.Raw
{
	
	/// <summary>A raw HID.</summary>
	public abstract class RawHumanInterfaceDevice : RawInputDevice<RawHID, int>
	{


		/// <summary></summary>
		/// <param name="window"></param>
		/// <param name="options"></param>
		public static void Register( IWin32Window window, RawInputDeviceRegistrationOptions options )
		{
			var rawInputDevice = RawInputDevice.Gamepad;
			rawInputDevice.targetWindowHandle = ( window == null ) ? IntPtr.Zero : window.Handle;
			rawInputDevice.flags = options;
			NativeMethods.RegisterRawInputDevices( rawInputDevice );

			//rawInputDevice = RawInputDevice.Joystick;
			//rawInputDevice.targetWindowHandle = ( window == null ) ? IntPtr.Zero : window.Handle;
			//rawInputDevice.flags = options;
			//NativeMethods.RegisterRawInputDevices( rawInputDevice );
		}



		/// <summary></summary>
		/// <param name="controllerIndex"></param>
		/// <param name="descriptor"></param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#" )]
		protected RawHumanInterfaceDevice( GameControllerIndex controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex, ref descriptor )
		{
			//var time = TimeSpan.Zero;
			//this.Reset( ref time );
		}


		/// <summary>Gets information about the HID.</summary>
		public HumanInterfaceDeviceInfo DeviceInfo { get { return base.Info.HumanInterfaceDeviceInfo.Value; } }

	}

}