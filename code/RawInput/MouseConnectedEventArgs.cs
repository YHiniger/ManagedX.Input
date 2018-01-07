namespace ManagedX.Input
{

	/// <summary>Arguments for the <see cref="InputDeviceManager.MouseConnected"/> event.</summary>
	public sealed class MouseConnectedEventArgs : System.EventArgs
	{

		private readonly Mouse mouse;



		internal MouseConnectedEventArgs( Mouse mouse )
			: base()
		{
			this.mouse = mouse;
		}



		/// <summary>Gets the newly connected mouse.</summary>
		public Mouse Mouse => mouse;

	}

}