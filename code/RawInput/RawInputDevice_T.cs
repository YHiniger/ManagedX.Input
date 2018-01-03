using System;


namespace ManagedX.Input.Raw
{

	/// <summary>Base class for managed RawInput-based input devices.</summary>
	/// <typeparam name="TState">A structure representing the raw input device state.</typeparam>
	/// <typeparam name="TButton">An enumeration of the raw input device buttons/keys, or a value type.</typeparam>
	public abstract class RawInputDevice<TState, TButton> : InputDevice<TState, TButton>
		where TState : struct, IEquatable<TState>
		where TButton : struct
	{


		private readonly IntPtr deviceHandle;
		private readonly string deviceName;
		private readonly string displayName;
		internal readonly DeviceInfo Info;



		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the device; must be unique per device type (<see cref="InputDeviceType"/>), and at least equal to 0.</param>
		/// <param name="descriptor">A descriptor for the device.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		internal RawInputDevice( int controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex )
		{
			deviceHandle = descriptor.DeviceHandle;
			deviceName = NativeMethods.GetRawInputDeviceName( deviceHandle );

			Info = NativeMethods.GetRawInputDeviceInfo( deviceHandle, false );

			if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				displayName = NativeMethods.GetHIDProductString( deviceName );
			else
			{
				// Mice and keyboards seem to require this:
				var tokens = deviceName.Substring( 4 ).Split( '#' );
				var regKey = string.Format( System.Globalization.CultureInfo.InvariantCulture, @"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Enum\{0}\{1}\{2}", tokens[ 0 ], tokens[ 1 ], tokens[ 2 ] );
				displayName = Microsoft.Win32.Registry.GetValue( regKey, "DeviceDesc", ";" ).ToString().Split( ';' )[ 1 ];
			}
		}



		/// <summary>Gets a value indicating the type of this raw input device.</summary>
		public sealed override InputDeviceType DeviceType => Info.DeviceType;


		/// <summary>Gets the display name of this raw input device.</summary>
		public sealed override string DisplayName => string.Copy( displayName ?? string.Empty );


		/// <summary>Gets the handle of this raw input device.</summary>
		public IntPtr DeviceHandle => deviceHandle;


		/// <summary>Gets the device name of this raw input device.</summary>
		public string DeviceName => string.Copy( deviceName ?? string.Empty );

	}

}