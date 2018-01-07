namespace ManagedX.Input
{

	/// <summary>Arguments for the <see cref="InputDeviceManager.KeyboardConnected"/> event.</summary>
	public sealed class KeyboardConnectedEventArgs : System.EventArgs
	{

		private readonly Keyboard keyboard;



		internal KeyboardConnectedEventArgs( Keyboard keyboard )
			: base()
		{
			this.keyboard = keyboard;
		}



		/// <summary>Gets the newly connected keyboard.</summary>
		public Keyboard Keyboard => keyboard;

	}

}