using System;


namespace ManagedX.Input.Raw
{

	/// <summary>A raw HID.
	/// <para>This class is not yet implemented.</para>
	/// </summary>
	public sealed class RawHumanInterfaceDevice : RawInputDevice<RawHID, int>
	{


		internal RawHumanInterfaceDevice( ref RawInputDeviceDescriptor descriptor )
			: base( ref descriptor )
		{
			this.Reset( TimeSpan.Zero );
		}



		/// <summary>Retrieves the device state and returns it.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns a <see cref="RawHID"/> structure representing the current state of the device.</returns>
		protected sealed override RawHID GetState()
		{
			return base.state;
		}


		/// <summary>Returns a value indicating whether a button is pressed in the current state and released in the previous state.</summary>
		/// <param name="button">A button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed in the current state and released in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( int button )
		{
			return false;
		}


		/// <summary>Returns a value indicating whether a button is released in the current state and pressed in the previous state.</summary>
		/// <param name="button">A button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is released in the current state and pressed in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( int button )
		{
			return false;
		}


		/// <summary>Resets the state and information about this <see cref="RawHumanInterfaceDevice"/>.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected sealed override void Reset( TimeSpan time )
		{
			base.Reset( time );
		}


		unsafe internal sealed override void Update( ref RawInput input )
		{
			//var hid = input.HumanInterfaceDevice;
			//var buffer = new byte[ hid.Count ][];
			//for( var b = 0; b < hid.Count; ++b )
			//{
			//	buffer[ b ] = new byte[ hid.Size ];
			//	System.Runtime.InteropServices.Marshal.Copy( hid.RawData + ( hid.Size * b ), buffer[ b ], 0, hid.Size );
			//}
		}


		/// <summary>Gets a description of this <see cref="RawHumanInterfaceDevice"/>.</summary>
		public HumanInterfaceDeviceInfo Description => base.info.HumanInterfaceDeviceInfo;

	}

}