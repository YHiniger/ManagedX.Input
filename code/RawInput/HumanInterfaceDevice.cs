using System;


namespace ManagedX.Input.Raw
{

	/// <summary>A raw HID.
	/// <para>This class is not yet implemented.</para>
	/// </summary>
	public sealed class HumanInterfaceDevice : RawInputDevice<HumanInterfaceDeviceState, int>
	{


		internal HumanInterfaceDevice( ref RawInputDeviceDescriptor descriptor )
			: base( ref descriptor )
		{
			this.Reset( TimeSpan.Zero );
		}



		/// <summary>Retrieves the device state and returns it.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns a <see cref="RawHID"/> structure representing the current state of the device.</returns>
		protected sealed override HumanInterfaceDeviceState GetState()
		{
			return base.state;
		}


		/// <summary>Resets the state and information about this <see cref="HumanInterfaceDevice"/>.</summary>
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


		/// <summary>Gets a description of this <see cref="HumanInterfaceDevice"/>.</summary>
		public HumanInterfaceDeviceInfo Description => base.info.HumanInterfaceDeviceInfo;

	}

}