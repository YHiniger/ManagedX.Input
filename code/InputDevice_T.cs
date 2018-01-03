using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{

	/// <summary>Base class for all managed input devices.</summary>
	/// <typeparam name="TState">A structure representing the device state.</typeparam>
	/// <typeparam name="TButton">An enumeration of the device buttons/keys.</typeparam>
	public abstract class InputDevice<TState, TButton> : InputDevice
		where TState : struct, IEquatable<TState>
		where TButton : struct
	{

		private TState currentState;
		private TimeSpan currentStateTime;
		private TState previousState;
		private TimeSpan previousStateTime;



		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the device; must be unique per device type (<see cref="InputDeviceType"/>), and at least equal to 0.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		internal InputDevice( int controllerIndex )
			: base( controllerIndex )
		{
		}



		#region State

		/// <summary>When overridden, reads and returns the input device state.
		/// <para>This method is called by <see cref="Reset"/> and <see cref="Update"/> to retrieve the device state (<see cref="CurrentState"/>).</para>
		/// It's also responsible of keeping the Disconnected property synchronized.
		/// </summary>
		/// <returns>Returns the input device state.</returns>
		[SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Disambiguation: this method retrieves the device state." )]
		protected abstract TState GetState();


		/// <summary>Gets the current state.</summary>
		public TState CurrentState => currentState;

		/// <summary>Gets the time associated with the <see cref="CurrentState"/>.</summary>
		public TimeSpan CurrentStateTime => currentStateTime;


		/// <summary>Gets the previous state.</summary>
		public TState PreviousState => previousState;

		/// <summary>Gets the time associated with the <see cref="PreviousState"/>.</summary>
		public TimeSpan PreviousStateTime => previousStateTime;

		#endregion State


		/// <summary>Copies the <see cref="CurrentState"/> to the <see cref="PreviousState"/>, and calls <see cref="GetState"/> to update the former.
		/// <para>If the device is not connected, calls <see cref="Reset"/> prior to the copy and state update.</para>
		/// </summary>
		/// <param name="time">The time elapsed since the application start.</param>
		public sealed override void Update( TimeSpan time )
		{
			//if( base.IsDisconnected )
			//{
			//	if( ( time - currentStateTime ).TotalSeconds < 2.0 )
			//		return;
			//}
			
			previousState = currentState;
			previousStateTime = currentStateTime;
			
			currentStateTime = time;
			currentState = this.GetState();
		}


		/// <summary>Reads the device state through <see cref="GetState"/>, and copies it to the <see cref="PreviousState"/> and the <see cref="CurrentState"/>.
		/// <para>This method must be called in the constructor of the "final classes" for proper initialization.</para>
		/// </summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected virtual void Reset( TimeSpan time )
		{
			previousStateTime = currentStateTime = time;
			previousState = currentState = this.GetState();	// NOTE - GetState is in charge of setting Disconnected to the appropriate value.
		}


		/// <summary>When overridden, returns a value indicating whether a button (or key) has just been pressed.</summary>
		/// <param name="button">A button (or key).</param>
		/// <returns>Returns true if the button (or key) is released in the <see cref="PreviousState">previous state</see> and is pressed in the <see cref="CurrentState">current state</see>, otherwise returns false.</returns>
		public abstract bool HasJustBeenPressed( TButton button );


		/// <summary>When overridden, returns a value indicating whether a button (or key) has just been released.</summary>
		/// <param name="button">A button (or key).</param>
		/// <returns>Returns true if the button (or key) is pressed in the <see cref="PreviousState">previous state</see> and is released in the <see cref="CurrentState">current state</see>, otherwise returns false.</returns>
		public abstract bool HasJustBeenReleased( TButton button );

	}

}