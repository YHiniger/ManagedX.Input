using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;


namespace ManagedX.Input.Raw
{
	
	/// <summary>A raw HID.</summary>
	public sealed class RawHumanInterfaceDevice : RawInputDevice<RawHID, int>
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


		private static readonly List<RawHumanInterfaceDevice> devices = new List<RawHumanInterfaceDevice>();


		private static void OnDeviceDisconnected( object sender, EventArgs e )
		{
			var device = (RawHumanInterfaceDevice)sender;
			device.Disconnected -= OnDeviceDisconnected;
			devices.Remove( device );
		}


		private static void Initialize()
		{
			var descriptors = NativeMethods.GetRawInputDeviceList();
			var index = 0;
			for( var d = 0; d < descriptors.Length; d++ )
			{
				var descriptor = descriptors[ d ];
				if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				{
					var device = new RawHumanInterfaceDevice( index, ref descriptor );
					if( !device.IsDisconnected )
					{
						devices.Add( device );
						device.Disconnected += OnDeviceDisconnected;
					}
				}
			}
		}

		
		/// <summary></summary>
		public static ReadOnlyCollection<RawHumanInterfaceDevice> All
		{
			get
			{
				if( devices.Count == 0 )
					Initialize();

				return new ReadOnlyCollection<RawHumanInterfaceDevice>( devices );
			}
		}





		/// <summary></summary>
		/// <param name="controllerIndex"></param>
		/// <param name="descriptor"></param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		internal RawHumanInterfaceDevice( int controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex, ref descriptor )
		{
			var time = TimeSpan.Zero;
			this.Reset( ref time );
		}



		/// <summary>Gets information about the HID.</summary>
		public HumanInterfaceDeviceInfo DeviceInfo { get { return base.Info.HumanInterfaceDeviceInfo.Value; } }


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
		}

	}

}