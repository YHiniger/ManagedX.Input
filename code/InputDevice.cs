using System;


namespace ManagedX.Input
{

	/// <summary>Base class for all managed input devices.</summary>
	public abstract class InputDevice
	{

		private GameControllerIndex index;
		private bool isDisconnected;



		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the device; must be unique per device type (<see cref="InputDeviceType"/>).</param>
		internal InputDevice( GameControllerIndex controllerIndex )
		{
			index = controllerIndex;
		}



		/// <summary>Gets the friendly name of this <see cref="InputDevice"/>.</summary>
		public abstract string DisplayName { get; }


		/// <summary>Gets the index of this <see cref="InputDevice"/>.</summary>
		public GameControllerIndex Index { get { return index; } }


		/// <summary>When overridden, gets a value indicating the type of the <see cref="InputDevice"/>.</summary>
		public abstract InputDeviceType DeviceType { get; }


		/// <summary>Raised when this <see cref="InputDevice"/> is disconnected.</summary>
		public event EventHandler Disconnected;
		
		/// <summary>Raises the <see cref="Disconnected"/> event.
		/// <para>Called when the value of <see cref="IsDisconnected"/> changed.</para>
		/// </summary>
		protected virtual void OnDisconnectedChanged()
		{
			if( isDisconnected )
			{
				var disconnectedEvent = this.Disconnected;
				if( disconnectedEvent != null )
					disconnectedEvent( this, EventArgs.Empty );
			}
		}


		/// <summary>Gets a value indicating whether this <see cref="InputDevice"/> is disconnected.</summary>
		public bool IsDisconnected
		{
			get { return isDisconnected; }
			protected set
			{
				if( value != isDisconnected )
				{
					isDisconnected = value;
					this.OnDisconnectedChanged();
				}
			}
		}


		/// <summary>When overridden, updates the state of the <see cref="InputDevice"/>.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		public abstract void Update( TimeSpan time );


		/// <summary>Returns the <see cref="DisplayName"/> of this <see cref="InputDevice"/>.</summary>
		/// <returns>Returns the <see cref="DisplayName"/> of this <see cref="InputDevice"/>.</returns>
		public sealed override string ToString()
		{
			return this.DisplayName;
		}

	}

}