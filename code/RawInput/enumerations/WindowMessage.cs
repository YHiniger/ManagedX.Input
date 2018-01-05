namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Enumerates RawInput-related window messages.</summary>
	internal enum WindowMessage : int
	{

		/// <summary>Sent to the window that registered to receive raw input.</summary>
		[Source( "WinUser.h", "WM_INPUT_DEVICE_CHANGE" )]
		InputDeviceChange = 254,

		/// <summary>Sent to the window that is getting raw input.</summary>
		[Source( "WinUser.h", "WM_INPUT" )]
		Input,

		/// <summary>Posted to the window with the keyboard focus when a nonsystem key is pressed.
		/// A nonsystem key is a key that is pressed when the ALT key is not pressed.
		/// </summary>
		[Source( "WinUser.h", "WM_KEYDOWN" )]
		KeyDown = 0x0100,

		/// <summary>Posted to the window with the keyboard focus when a nonsystem key is released.
		/// A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
		/// </summary>
		[Source( "WinUser.h", "WM_KEYUP" )]
		KeyUp,

		/// <summary>Posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key.
		/// It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window.
		/// The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
		/// </summary>
		[Source( "WinUser.h", "WM_SYSKEYDOWN" )]
		SysKeyDown = 0x0104,

		/// <summary>Posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down.
		/// It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window.
		/// The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
		/// </summary>
		[Source( "WinUser.h", "WM_SYSKEYUP" )]
		SysKeyUp,

	}

}