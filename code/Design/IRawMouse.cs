namespace ManagedX.Input.Design
{
	using Raw;


	/// <summary>Defines properties and methods to properly implement a mouse as a managed raw input device.</summary>
	public interface IRawMouse : IRawInputDevice, IMouse
	{

		/// <summary>Gets information about the mouse device.</summary>
		MouseDeviceInfo DeviceInfo { get; }

	}
	
}