namespace ManagedX.Input
{

	/// <summary>Enumerates (raw) input device types.</summary>
	public enum InputDeviceType : int
	{

		/// <summary>Data comes from a mouse.</summary>
		Mouse = 0,

		/// <summary>Data comes from a keyboard.</summary>
		Keyboard = 1,

		/// <summary>Data comes from an HID that is not a keyboard or a mouse.</summary>
		HumanInterfaceDevice = 2

	}

}