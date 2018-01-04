namespace ManagedX.Input.Raw
{
	using Win32;

	
	/// <summary>Enumerates raw mouse input transition states.</summary>
	[System.Flags]
	public enum RawMouseButtonStateIndicators : int
	{
		
		/// <summary>No state specified.</summary>
		None = 0x0000,


		/// <summary>The left mouse button is down/pressed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_LEFT_BUTTON_DOWN" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_1_DOWN" )]
		LeftButtonDown = 0x0001,

		/// <summary>The left mouse button is up/released.</summary>
		[Source( "WinUser.h", "RI_MOUSE_LEFT_BUTTON_UP" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_1_UP" )]
		LeftButtonUp = 0x0002,


		/// <summary>The right mouse button is down/pressed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_RIGHT_BUTTON_DOWN" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_2_DOWN" )]
		RightButtonDown = 0x0004,

		/// <summary>The right mouse button is up/released.</summary>
		[Source( "WinUser.h", "RI_MOUSE_RIGHT_BUTTON_UP" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_2_UP" )]
		RightButtonUp = 0x0008,


		/// <summary>The middle mouse button is down/pressed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_MIDDLE_BUTTON_DOWN" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_3_DOWN" )]
		MiddleButtonDown = 0x0010,

		/// <summary>The middle mouse button is up/released.</summary>
		[Source( "WinUser.h", "RI_MOUSE_MIDDLE_BUTTON_UP" )]
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_3_UP" )]
		MiddleButtonUp = 0x0020,


		/// <summary>The extended mouse button 1 is down/pressed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_4_DOWN" )]
		XButton1Down = 0x0040,

		/// <summary>The extended mouse button 1 is up/released.</summary>
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_4_UP" )]
		XButton1Up = 0x0080,


		/// <summary>The extended mouse button 2 is down/pressed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_5_DOWN" )]
		XButton2Down = 0x0100,

		/// <summary>The extended mouse button 2 is up/released.</summary>
		[Source( "WinUser.h", "RI_MOUSE_BUTTON_5_UP" )]
		XButton2Up = 0x0200,


		/// <summary>The mouse wheel value changed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_WHEEL" )]
		Wheel = 0x0400,

		/// <summary>The horizontal mouse wheel value changed.</summary>
		[Source( "WinUser.h", "RI_MOUSE_HWHEEL" )]
		HorizontalWheel = 0x0800,
	
	}

}