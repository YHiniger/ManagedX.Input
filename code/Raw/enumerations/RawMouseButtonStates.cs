namespace ManagedX.Input.Raw
{
	
	/// <summary>Enumerates raw mouse input transition states.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[System.Flags]
	public enum RawMouseButtonStates : short
	{
		
		/// <summary>No state specified.</summary>
		None = 0x0000,

		
		/// <summary>The left mouse button is down/pressed.</summary>
		LeftButtonDown = 0x0001,

		/// <summary>The left mouse button is up/released.</summary>
		LeftButtonUp = 0x0002,


		/// <summary>The right mouse button is down/pressed.</summary>
		RightButtonDown = 0x0004,

		/// <summary>The right mouse button is up/released.</summary>
		RightButtonUp = 0x0008,

		
		/// <summary>The middle mouse button is down/pressed.</summary>
		MiddleButtonDown = 0x0010,

		/// <summary>The middle mouse button is up/released.</summary>
		MiddleButtonUp = 0x0020,

		
		/// <summary>The extended mouse button 1 is down/pressed.</summary>
		XButton1Down = 0x0040,

		/// <summary>The extended mouse button 1 is up/released.</summary>
		XButton1Up = 0x0080,

		
		/// <summary>The extended mouse button 2 is down/pressed.</summary>
		XButton2Down = 0x0100,

		/// <summary>The extended mouse button 2 is up/released.</summary>
		XButton2Up = 0x0200,

		
		/// <summary>The mouse wheel value changed.</summary>
		Wheel = 0x0400
	
	}

}