using System;


namespace ManagedX.Input.Design
{
	
	/// <summary>Defines properties and methods to properly implement a managed input device.</summary>
	public interface IInputDevice
	{

		/// <summary>Gets the (zero-based) controller index.</summary>
		GameControllerIndex Index { get; }


		/// <summary>Gets a value indicating whether the controller is connected.</summary>
		bool IsConnected { get; }


		/// <summary>Gets a value indicating the type of the input device.</summary>
		InputDeviceType DeviceType { get; }

		
		/// <summary>Updates the controller state.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		void Update( TimeSpan time );
		// TODO - should really be part of an IUpdateable interface !

	}

}