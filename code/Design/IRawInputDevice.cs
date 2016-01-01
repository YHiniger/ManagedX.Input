using System;


namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a raw input device, as a managed input device (see <see cref="IInputDevice"/>).</summary>
	public interface IRawInputDevice : IInputDevice
	{

		/// <summary>Gets a handle to the raw input device.</summary>
		IntPtr DeviceHandle { get; }


		/// <summary>Gets the name of the raw input device.</summary>
		string DevicePath { get; }
		

		/// <summary>Gets the display name of the raw input device.</summary>
		string DisplayName { get; }

	}

}