using System;


namespace ManagedX.Input
{
	using Design;


	/// <summary>Base class for input devices and (game) controllers; implements the <see cref="IInputDevice&lt;TState,TButton&gt;"/> interface.</summary>
	/// <typeparam name="TState">A structure representing the input device state.</typeparam>
	/// <typeparam name="TButton">An enumeration representing the controller buttons (or key).</typeparam>
	public abstract class InputDevice<TState, TButton> : IInputDevice<TState, TButton>
		where TState : struct
		where TButton : struct
	{

		private int index;
		private TimeSpan currentStateTime, previousStateTime;
		private TState currentState, previousState;


		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The zero-based index of this controller/input device; must not be negative.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="controllerIndex"/> is negative.</exception>
		protected InputDevice( int controllerIndex )
		{
			if( controllerIndex < 0 )
				throw new ArgumentOutOfRangeException( "controllerIndex" );

			index = controllerIndex;
		}


		/// <summary>Gets the index of this controller.</summary>
		public int Index { get { return index; } }


		/// <summary>When overridden, gets a value indicating whether the controller is connected.</summary>
		public abstract bool IsConnected { get; }



		#region States


		/// <summary>When overridden, reads and returns the input device state.
		/// <para>This method is called by <see cref="Initialize"/> and <see cref="Update"/> to retrieve the device state (<see cref="CurrentState"/>).</para>
		/// </summary>
		/// <returns>The input device state.</returns>
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


		/// <summary>Reads the initial state, and sets the <see cref="PreviousState">previous state</see> with the same value as the <see cref="CurrentState">current state</see>.</summary>
		protected virtual void Initialize()
		{
			previousState = currentState = this.GetState();
		}


		/// <summary>Saves the <see cref="CurrentState">current state</see> to the <see cref="PreviousState">previous state</see>, and calls <see cref="GetState"/> to update the former.</summary>
		/// <param name="time">The current time.</param>
		public virtual void Update( TimeSpan time )
		{
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