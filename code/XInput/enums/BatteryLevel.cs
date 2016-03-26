namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates XInput gamepad battery levels, used in the <see cref="BatteryInformation"/> structure.
	/// <para>This enumeration is equivalent to the native <code>BATTERY_LEVEL_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryLevel : byte
	{

		/// <summary>The battery is empty.</summary>
		[Native( "XInput.h", "BATTERY_LEVEL_EMPTY" )]
		Empty,

		/// <summary>The battery level is low.</summary>
		[Native( "XInput.h", "BATTERY_LEVEL_LOW" )]
		Low,

		/// <summary>The battery level is medium.</summary>
		[Native( "XInput.h", "BATTERY_LEVEL_MEDIUM" )]
		Medium,

		/// <summary>The battery level is high.</summary>
		[Native( "XInput.h", "BATTERY_LEVEL_FULL" )]
		Full

	}

}