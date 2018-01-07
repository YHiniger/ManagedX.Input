using System;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Base class for all managed input devices.</summary>
	public abstract class InputDevice
	{

		internal static Exception GetException( int errorCode )
		{
			return Marshal.GetExceptionForHR( errorCode ) ?? new Win32Exception( errorCode );
		}



		private bool isDisabled;
		private bool isDisconnected;



		internal InputDevice()
		{
		}



		/// <summary>Gets or sets a value indicating whether the device is disabled.
		/// <para>Disabled devices are not updated.</para>
		/// </summary>
		public bool IsDisabled { get => isDisabled; set => isDisabled = value; }


		/// <summary>When overridden, gets a value indicating the type of the <see cref="InputDevice"/>.</summary>
		public abstract InputDeviceType DeviceType { get; }


		/// <summary>Gets the friendly name of this <see cref="InputDevice"/>.</summary>
		public abstract string DisplayName { get; }


		/// <summary>Raised when this <see cref="InputDevice"/> is disconnected.</summary>
		public event EventHandler Disconnected;
		
		/// <summary>Raises the <see cref="Disconnected"/> event.
		/// <para>Called when the value of <see cref="IsDisconnected"/> changed.</para>
		/// </summary>
		protected virtual void OnDisconnected()
		{
			this.Disconnected?.Invoke( this, EventArgs.Empty );
		}


		/// <summary>Gets a value indicating whether this <see cref="InputDevice"/> is disconnected.</summary>
		public bool IsDisconnected
		{
			get => isDisconnected;
			internal set
			{
				if( value != isDisconnected )
				{
					isDisconnected = value;
					if( isDisconnected )
						this.OnDisconnected();
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