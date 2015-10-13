using System;


namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a managed input device.</summary>
	/// <typeparam name="TState">A structure representing the device state.</typeparam>
	/// <typeparam name="TButton">An enumeration representing the controller buttons.</typeparam>
	public interface IInputDevice<TState, TButton> : IInputDevice
		where TState : struct
		where TButton : struct
	{

		/// <summary>Gets the last known controller state.</summary>
		TState CurrentState { get; }

		/// <summary>Gets the time associated with the <see cref="CurrentState">current state</see>.</summary>
		TimeSpan CurrentStateTime { get; }


		/// <summary>Gets the previous controller state.</summary>
		TState PreviousState { get; }

		/// <summary>Gets the time associated with the <see cref="PreviousState">previous state</see>.</summary>
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