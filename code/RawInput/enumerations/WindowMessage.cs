namespace ManagedX.Input.Raw
{
	
	/// <summary>Enumerates RawInput window messages.</summary>
	internal enum WindowMessage : int
	{

		/// <summary>Sent to the window that registered to receive raw input.</summary>
		InputDeviceChange = 254,

		/// <summary>Sent to the window that is getting raw input.</summary>
		Input,

	}

}