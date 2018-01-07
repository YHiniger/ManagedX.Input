namespace ManagedX.Input
{
	using Raw;


	/// <summary>Arguments for the <see cref="RawInputDeviceManager.HumanInterfaceDeviceConnected"/> event.</summary>
	public sealed class HumanInterfaceDeviceConnectedEventArgs : System.EventArgs
	{

		private readonly RawHumanInterfaceDevice device;



		internal HumanInterfaceDeviceConnectedEventArgs( RawHumanInterfaceDevice device )
			: base()
		{
			this.device = device;
		}



		/// <summary>Gets the newly connected device.</summary>
		public RawHumanInterfaceDevice Device => device;

	}

}