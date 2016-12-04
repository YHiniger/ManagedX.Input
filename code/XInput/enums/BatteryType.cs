using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates XInput gamepad battery types, used in the <see cref="BatteryInformation"/> structure.
	/// <para>This enumeration is equivalent to the native <code>BATTERY_TYPE_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryType : byte
	{

		/// <summary>The game controller is not connected.</summary>
		[Source( "XInput.h", "BATTERY_TYPE_DISCONNECTED" )]
		Disconnected,

		/// <summary>The game controller is a wired device and does not have a battery.</summary>
		[Source( "XInput.h", "BATTERY_TYPE_WIRED" )]
		Wired,

		/// <summary>The game controller has an alkaline battery.</summary>
		[Source( "XInput.h", "BATTERY_TYPE_ALKALINE" )]
		Alkaline,

		/// <summary>The game controller has a nickel metal hydride battery.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ni" )]
		[Source( "XInput.h", "BATTERY_TYPE_NIMH" )]
		NiMH,

		/// <summary>The game controller has an unknown battery type.</summary>
		[Source( "XInput.h", "BATTERY_TYPE_UNKNOWN" )]
		Unknown = 255

	}

}
