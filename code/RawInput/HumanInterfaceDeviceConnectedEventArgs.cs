namespace ManagedX.Input
{
	using Raw;


	/// <summary>Arguments for the <see cref="InputDeviceManager.HumanInterfaceDeviceConnected"/> event.</summary>
	public sealed class HumanInterfaceDeviceConnectedEventArgs : System.EventArgs
	{

		private readonly HumanInterfaceDevice device;



		internal HumanInterfaceDeviceConnectedEventArgs( HumanInterfaceDevice device )
			: base()
		{
			this.device = device;
		}



		/// <summary>Gets the newly connected HID.</summary>
		public HumanInterfaceDevice Device => device;

	}

}