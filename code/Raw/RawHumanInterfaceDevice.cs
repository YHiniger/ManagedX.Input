using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;


namespace ManagedX.Input.Raw
{
	
	/// <summary>A raw HID.</summary>
	public sealed class RawHumanInterfaceDevice : RawInputDevice<RawHID, int>
	{


		///// <summary></summary>
		///// <param name="window"></param>
		///// <param name="options"></param>
		//public static void Register( IWin32Window window, RawInputDeviceRegistrationOptions options )
		//{
		//	var rawInputDevice = RawInputDevice.Gamepad;
		//	rawInputDevice.targetWindowHandle = ( window == null ) ? IntPtr.Zero : window.Handle;
		//	rawInputDevice.flags = options;
		//	NativeMethods.RegisterRawInputDevices( rawInputDevice );

		//	//rawInputDevice = RawInputDevice.Joystick;
		//	//rawInputDevice.targetWindowHandle = ( window == null ) ? IntPtr.Zero : window.Handle;
		//	//rawInputDevice.flags = options;
		//	//NativeMethods.RegisterRawInputDevices( rawInputDevice );
		//}


		private HumanInterfaceDeviceInfo info;



		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		internal RawHumanInterfaceDevice( int controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex, ref descriptor )
		{
			var time = TimeSpan.Zero;
			this.Reset( ref time );
		}



		/// <summary></summary>
		public int VendorId { get { return info.VendorId; } }

		
		/// <summary></summary>
		public int ProductId { get { return info.ProductId; } }


		/// <summary></summary>
		public int Version { get { return info.VersionNumber; } }


		/// <summary></summary>
		public int TopLevelCollection { get { return info.TopLevelCollection; } }


		/// <summary></summary>
		/// <returns></returns>
		protected sealed override RawHID GetState()
		{
			return RawHID.Empty;
		}


		/// <summary></summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public sealed override bool HasJustBeenPressed( int button )
		{
			return false;
		}


		/// <summary></summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public sealed override bool HasJustBeenReleased( int button )
		{
			return false;
		}


		/// <summary></summary>
		/// <param name="time"></param>
		protected sealed override void Reset( ref TimeSpan time )
		{
			base.Reset( ref time );

			var deviceInfo = base.Info.HumanInterfaceDeviceInfo;
			if( deviceInfo != null && deviceInfo.HasValue )
				info = deviceInfo.Value;
			else
				info = HumanInterfaceDeviceInfo.Empty;
		}

	}

}