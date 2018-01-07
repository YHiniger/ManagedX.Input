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

		/// <summary>The internal buffer for the state being built.</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		protected TState state;
		private readonly IntPtr deviceHandle;
		private readonly string deviceName;
		private readonly string displayName;
		internal DeviceInfo info;



		/// <summary>Constructor.</summary>
		/// <param name="descriptor">A descriptor for the device.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		internal RawInputDevice( ref RawInputDeviceDescriptor descriptor )
			: base()
		{
			deviceHandle = descriptor.DeviceHandle;
			deviceName = RawInputDeviceManager.GetRawInputDeviceName( deviceHandle );

			if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				displayName = RawInputDeviceManager.GetHIDProductString( deviceName );
			else
			{
				// Mice and keyboards seem to require this:
				var tokens = deviceName.Substring( 4 ).Split( '#' );
				var regKey = string.Format( System.Globalization.CultureInfo.InvariantCulture, @"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Enum\{0}\{1}\{2}", tokens[ 0 ], tokens[ 1 ], tokens[ 2 ] );
				displayName = Microsoft.Win32.Registry.GetValue( regKey, "DeviceDesc", ";" ).ToString().Split( ';' )[ 1 ];
			}
		}



		/// <summary>Gets a value indicating the type of this raw input device.</summary>
		public sealed override InputDeviceType DeviceType => info.DeviceType;


		/// <summary>Gets the display name of this raw input device.</summary>
		public sealed override string DisplayName => string.Copy( displayName ?? string.Empty );


		/// <summary>Gets the handle of this raw input device.</summary>
		public IntPtr DeviceHandle => deviceHandle;


		/// <summary>Gets the device name of this raw input device.</summary>
		public string DeviceName => string.Copy( deviceName ?? string.Empty );


		/// <summary>Reads the device state through GetState, and copies it to the PreviousState and the CurrentState.
		/// <para>This method must be called in the constructor of the "final classes" for proper initialization.</para>
		/// </summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected override void Reset( TimeSpan time )
		{
			info = RawInputDeviceManager.GetRawInputDeviceInfo( deviceHandle, false );

			base.Reset( time );
		}


		internal abstract void Update( ref RawInput input );

	}

}