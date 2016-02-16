using System;


namespace ManagedX.Input.Design
{
	
	/// <summary>Defines properties and a method to properly implement a managed input device.</summary>
	public interface IInputDevice : ManagedX.Design.IDevice
	{

		/// <summary>Gets the index of the input device.
		/// <para>Multiple input devices can have the same index, only if their <see cref="DeviceType"/> differ.</para>
		/// </summary>
		GameControllerIndex Index { get; }


		/// <summary>Gets the type of the input device.</summary>
		InputDeviceType DeviceType { get; }

		
		/// <summary>Gets a value indicating whether the input device is disconnected.</summary>
		bool Disconnected { get; }


		/// <summary>Updates the input device state.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		void Update( TimeSpan time );

	}

}