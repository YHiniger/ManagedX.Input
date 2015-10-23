namespace ManagedX.Input.Design
{
	using Raw;


	/// <summary>Defines properties and methods to properly implement a keyboard, as a managed raw input device.</summary>
	public interface IRawKeyboard : IRawInputDevice, IKeyboard
	{

		/// <summary>Gets information about the keyboard device.</summary>
		KeyboardDeviceInfo DeviceInfo { get; }

	}

}