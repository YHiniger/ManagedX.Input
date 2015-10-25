using System;


namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a managed input device (see <see cref="IInputDevice"/>).</summary>
	/// <typeparam name="TState">A structure representing the device state.</typeparam>
	/// <typeparam name="TButton">An enumeration representing the device buttons.</typeparam>
	public interface IInputDevice<TState, TButton> : IInputDevice
		where TState : struct
		where TButton : struct
	{

		/// <summary>Gets the state retrieved by the last call to <see cref="IInputDevice.Update"/>.</summary>
		TState CurrentState { get; }

		/// <summary>Gets the time associated with the <see cref="CurrentState"/>.</summary>
		TimeSpan CurrentStateTime { get; }


		/// <summary>Gets the state which was the <see cref="CurrentState"/> before the last call to <see cref="IInputDevice.Update"/>.</summary>
		TState PreviousState { get; }

		/// <summary>Gets the time associated with the <see cref="PreviousState"/>.</summary>
		TimeSpan PreviousStateTime { get; }


		/// <summary>Returns a value indicating whether a button was released in the previous state but is pressed in the current state.</summary>
		/// <param name="button">A controller button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> was released in the previous state and is pressed in the current state, otherwise returns false.</returns>
		bool HasJustBeenPressed( TButton button );


		/// <summary>Gets a value indicating whether a button was pressed in the previous state but is released in the current state.</summary>
		/// <param name="button">A controller button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> was pressed in the previous state and is released in the current state, otherwise returns false.</returns>
		bool HasJustBeenReleased( TButton button );

	}

}