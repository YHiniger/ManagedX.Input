using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h
	// http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx


	/// <summary>Enumerates XInput gamepad battery types (BATTERY_TYPE_*), used in the <see cref="BatteryInformation"/> structure.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum BatteryType : byte
	{

		/// <summary>The game controller is not connected.</summary>
		Disconnected,

		/// <summary>The game controller is a wired device and does not have a battery.</summary>
		Wired,

		/// <summary>The game controller has an alkaline battery.</summary>
		Alkaline,

		/// <summary>The game controller has a nickel metal hydride battery.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Ni" )]
		NiMH,

		/// <summary>The game controller has an unknown battery type.</summary>
		Unknown = 255

	}

}
