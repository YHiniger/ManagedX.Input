namespace ManagedX.Input.XInput
{

	// XInput.h
	// http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx
	

	/// <summary>Enumerates XInput gamepad battery levels (BATTERY_LEVEL_*), used in the <see cref="BatteryInformation"/> structure.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryLevel : byte
	{

		/// <summary>The battery is empty.</summary>
		Empty,

		/// <summary>The battery level is low.</summary>
		Low,

		/// <summary>The battery level is medium.</summary>
		Medium,

		/// <summary>The battery level is high.</summary>
		Full

	}

}