using System;


namespace ManagedX.Input
{
	using Design;


	/// <summary>Default implementation of the <see cref="IInputDevice&lt;TState,TButton&gt;"/> interface.
	/// <para>Base class for input devices, including game controllers.</para>
	/// </summary>
	/// <typeparam name="TState">A structure representing the input device state.</typeparam>
	/// <typeparam name="TButton">An enumeration representing the controller buttons (or key).</typeparam>
	public abstract class InputDevice<TState, TButton> : IInputDevice<TState, TButton>
		where TState : struct
		where TButton : struct
	{

		private GameControllerIndex index;
		private TimeSpan currentStateTime, previousStateTime;
		private TState currentState, previousState;


		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of this input device.</param>
		protected InputDevice( GameControllerIndex controllerIndex )
		{
			index = controllerIndex;
		}



		/// <summary>Gets the index of this input device.</summary>
		public GameControllerIndex Index { get { return index; } }


		/// <summary>When overridden, gets a value indicating whether the input device is connected.</summary>
		public abstract bool IsConnected { get; }

		
		/// <summary>When overridden, gets a value indicating the type of the input device.</summary>
		public abstract InputDeviceType DeviceType { get; }


		#region States


		/// <summary>When overridden, reads and returns the input device state.
		/// <para>This method is called by <see cref="Reset"/> and <see cref="Update"/> to retrieve the device state (<see cref="CurrentState"/>).</para>
		/// </summary>
		/// <returns>Returns the input device state.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Disambiguation: this method retrieves the device state." )]
		protected abstract TState GetState();


		/// <summary>Gets the current state.</summary>
		public TState CurrentState { get { return currentState; } }

		/// <summary>Gets the time associated with the <see cref="CurrentState">current state</see>.</summary>
		public TimeSpan CurrentStateTime { get { return currentStateTime; } }


		/// <summary>Gets the previous state.</summary>
		public TState PreviousState { get { return previousState; } }

		/// <summary>Gets the time associated with the <see cref="PreviousState">previous state</see>.</summary>
		public TimeSpan PreviousStateTime { get { return previousStateTime; } }


		#endregion


		/// <summary>Reads the device state through <see cref="GetState"/>, and copies it to the <see cref="PreviousState"/> and the <see cref="CurrentState"/>.
		/// <para>This method must be called in the constructor of the "final classes" for proper initialization.</para>
		/// </summary>
		protected virtual void Reset()
		{
			//previousStateTime = currentStateTime = TimeSpan.Zero;
			previousState = currentState = this.GetState();
		}


		/// <summary>Copies the <see cref="CurrentState"/> to the <see cref="PreviousState"/>, and calls <see cref="GetState"/> to update the former.
		/// <para>If the device is not connected, calls <see cref="Reset"/> prior to the copy and state update.</para>
		/// </summary>
		/// <param name="time">The current time.</param>
		public void Update( TimeSpan time )
		{
			if( !this.IsConnected )
				this.Reset();
			
			previousState = currentState;
			previousStateTime = currentStateTime;
			
			currentStateTime = time;
			currentState = this.GetState();
		}


		#region Buttons


		/// <summary>When overridden, returns a value indicating whether a button (or key) has just been pressed.</summary>
		/// <param name="button">A button (or key).</param>
		/// <returns>Returns true if the button (or key) is released in the <see cref="PreviousState">previous state</see> and is pressed in the <see cref="CurrentState">current state</see>, otherwise returns false.</returns>
		public abstract bool HasJustBeenPressed( TButton button );


		/// <summary>When overridden, returns a value indicating whether a button (or key) has just been released.</summary>
		/// <param name="button">A button (or key).</param>
		/// <returns>Returns true if the button (or key) is pressed in the <see cref="PreviousState">previous state</see> and is released in the <see cref="CurrentState">current state</see>, otherwise returns false.</returns>
		public abstract bool HasJustBeenReleased( TButton button );


		#endregion
		
	}

}