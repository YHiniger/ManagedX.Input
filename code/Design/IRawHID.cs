namespace ManagedX.Input.Design
{
	using Raw;


	/// <summary>Defines properties and methods to properly implement a human interface device (HID), as a managed raw input device.</summary>
	/// <typeparam name="TButton"></typeparam>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
	public interface IRawHID<TButton> : IRawInputDevice, IInputDevice<RawHID, TButton>
		where TButton : struct
	{

		/// <summary>Gets information about the device.</summary>
		HumanInterfaceDeviceInfo DeviceInfo { get; }

	}

}