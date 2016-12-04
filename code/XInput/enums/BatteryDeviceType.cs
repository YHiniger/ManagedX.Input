namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates XInput battery device types, for use with the XInputGetBatteryInformation function.
	/// <para>This enumeration is equivalent to the native <code>BATTERY_DEVTYPE_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryDeviceType : byte
	{

		/// <summary>An XInput controller.</summary>
		[Source( "XInput.h", "BATTERY_DEVTYPE_GAMEPAD" )]
		Gamepad,

		/// <summary>An XInput headset.</summary>
		[Source( "XInput.h", "BATTERY_DEVTYPE_HEADSET" )]
		Headset

	}

}