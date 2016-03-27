using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Base class for raw input devices, implemented as managed input devices.</summary>
	/// <typeparam name="TState">A structure representing the raw input device state.</typeparam>
	/// <typeparam name="TButton">An enumeration of the raw input device buttons/keys.</typeparam>
	public abstract class RawInputDevice<TState, TButton> : InputDevice<TState, TButton>
		where TState : struct
		where TButton : struct
	{


		private IntPtr deviceHandle;
		private DeviceInfo info;
		private string deviceName;
		private string displayName;



		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the input device.</param>
		/// <param name="descriptor">The raw input device descriptor.</param>
		internal RawInputDevice( GameControllerIndex controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex )
		{
			deviceHandle = descriptor.DeviceHandle;
			
			info = NativeMethods.GetRawInputDeviceInfo( deviceHandle, false );
			deviceName = NativeMethods.GetRawInputDeviceName( deviceHandle );
			
			if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				displayName = NativeMethods.GetHIDProductString( deviceName ); // obviously, only works for HIDs ...

			if( displayName == null )
			{
				// Mice and keyboards seem to require this:
				var tokens = deviceName.Substring( 4 ).Split( '#' );
				var regKey = string.Format( System.Globalization.CultureInfo.InvariantCulture, @"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Enum\{0}\{1}\{2}", tokens[ 0 ], tokens[ 1 ], tokens[ 2 ] );
				displayName = Microsoft.Win32.Registry.GetValue( regKey, "DeviceDesc", ";" ).ToString().Split( ';' )[ 1 ];
			}
		}



		/// <summary>Gets the display name of this raw input device.</summary>
		public sealed override string DisplayName { get { return string.Copy( displayName ?? string.Empty ); } }


		/// <summary>Gets a value indicating the type of this raw input device.</summary>
		public sealed override InputDeviceType DeviceType { get { return info.Type; } }


		/// <summary>Gets a handle to this raw input device.</summary>
		public IntPtr DeviceHandle { get { return deviceHandle; } }


		/// <summary>Gets the device name of this raw input device.</summary>
		public string DevicePath { get { return string.Copy( deviceName ?? string.Empty ); } }


		/// <summary>Gets information about this raw input device.</summary>
		internal DeviceInfo Info { get { return info; } }

	}

}