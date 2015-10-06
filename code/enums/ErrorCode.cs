namespace ManagedX.Input.XInput
{

	/// <summary>Enumerates some of Windows error codes (defined in WinError.h) known to be returned by XInput functions.</summary>
	internal enum ErrorCode : int
	{
		
		/// <summary>The operation was successful.</summary>
		None = 0,

		/// <summary>[0x0000048F] The controller is not connected.</summary>
		NotConnected = 1167,

		/// <summary>[0x000010D2] ...</summary>
		Empty = 4306

	}

}
