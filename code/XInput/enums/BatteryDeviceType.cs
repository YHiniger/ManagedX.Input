namespace ManagedX.Input.XInput
{

	// XInput.h


	/// <summary>Enumerates XInput battery device types (BATTERY_DEVTYPE_*), for use with the XInputGetBatteryInformation function.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryDeviceType : byte
	{

		/// <summary>An XInput controller.</summary>
		Gamepad,

		/// <summary>An XInput headset.</summary>
		Headset

	}

}
