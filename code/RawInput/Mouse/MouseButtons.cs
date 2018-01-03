namespace ManagedX.Input
{

	/// <summary>Enumerates flags indicating the state of a mouse button.</summary>
	[System.Flags]
	internal enum MouseButtons : int
	{
		
		/// <summary>No button pressed.</summary>
		None = 0x0000,

		/// <summary>The left mouse button is pressed.</summary>
		Left = 0x0001,

		/// <summary>The right mouse button is pressed.</summary>
		Right = 0x0002,

		/// <summary>The middle mouse button is pressed.</summary>
		Middle = 0x0004,

		/// <summary>The extended mouse button 1 is pressed.</summary>
		X1 = 0x0008,

		/// <summary>The extended mouse button 2 is pressed.</summary>
		X2 = 0x0010

	}

}