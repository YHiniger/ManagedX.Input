using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	// Ideally, an input device should be identifiable by its device type and index.
	

	/// <summary>Base class for all managed input devices.</summary>
	public abstract class InputDevice : Design.IInputDevice
	{


		/// <summary>Returns an exception for the last Win32 error.</summary>
		/// <returns>Returns an exception for the last Win32 error.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate" )]
		protected static Exception GetLastWin32Exception()
		{
			var errorCode = Marshal.GetLastWin32Error();
			var ex = Marshal.GetExceptionForHR( errorCode );
			if( ex == null )
				ex = new System.ComponentModel.Win32Exception( errorCode );
			return ex;
		}


		private GameControllerIndex index;



		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the device; must be unique per device type (<see cref="InputDeviceType"/>).</param>
		internal InputDevice( GameControllerIndex controllerIndex )
		{
			index = controllerIndex;
		}



		/// <summary>Gets the identifier of this <see cref="InputDevice"/>.</summary>
		public abstract string Identifier { get; }


		/// <summary>Gets the friendly name of this <see cref="InputDevice"/>.</summary>
		public abstract string DisplayName { get; }


		/// <summary>Gets the index of this input device.</summary>
		public GameControllerIndex Index { get { return index; } }


		/// <summary>When overridden, gets a value indicating the type of the input device.</summary>
		public abstract InputDeviceType DeviceType { get; }


		/// <summary>When overridden, gets a value indicating whether the input device is disconnected.</summary>
		public abstract bool Disconnected { get; }


		/// <summary>When overridden, updates the state of the input device.</summary>
		/// <param name="time">The current time.</param>
		public abstract void Update( TimeSpan time );

	}

}