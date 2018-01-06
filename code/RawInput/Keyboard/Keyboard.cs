using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Input
{
	using Raw;


	/// <summary>A keyboard.</summary>
	public sealed class Keyboard : RawInputDevice<KeyboardState, Key>
	{

		//private const int MaxSupportedKeyboards = 4;    // FIXME - should be 2


		private enum ScanCode : short
		{
			None = 0x0000,
			//Escape,
			//D1, D2, D3, D4, D5, D6, D7, D8, D9, D0,
			//Minus,
			//Equals,
			//Backspace,
			//Tab,
			//Q, W, E, R, T, Y, U, I, O, P,
			//LeftBracket, RightBracket, Enter,
			//LeftControl,
			//A, S, D, F, G, H, J, K, L,
			//Semicolon, Apostrophe, Grave,
			LeftShift = 0x2A,
			//Backslash,
			//Z = 0x2C,
			//X = 0x2D,
			//C = 0x2E,
			//V = 0x2F,
			//B = 0x30,
			//N = 0x31,
			//M = 0x32,
			//Comma = 0x33,
			//Period = 0x34,
			//Slash = 0x35,
			RightShift = 0x36,
			//Multiply = 0x37,
			//LeftAlt = 0x38,
			//Space = 0x39,
			//CapsLock = 0x3A,
			//F1 = 0x3B,
			//F2 = 0x3C,
			//F3 = 0x3D,
			//F4 = 0x3E,
			//F5 = 0x3F,
			//F6 = 0x40,
			//F7 = 0x41,
			//F8 = 0x42,
			//F9 = 0x43,
			//F10 = 0x44,
			//NumLock = 0x45,
			//ScrollLock = 0x46,
			//Numpad7 = 0x47,
			//Numpad8 = 0x48,
			//Numpad9 = 0x49,
			//NumpadMinus = 0x4A,
			//Numpad4 = 0x4B,
			//Numpad5 = 0x4C,
			//Numpad6 = 0x4D,
			//NumpadPlus = 0x4E,
			//Numpad1 = 0x4F,
			//Numpad2 = 0x50,
			//Numpad3 = 0x51,
			//Numpad0 = 0x52,
			//NumpadPeriod = 0x53,
			//AltPrintScreen = 0x54, /* Alt + print screen. MapVirtualKeyEx( VK_SNAPSHOT, MAPVK_VK_TO_VSC_EX, 0 ) returns scancode 0x54. */
			//BracketAngle = 0x56, /* Key between the left shift and Z. */
			//F11 = 0x57,
			//F12 = 0x58,
			//OEM1 = 0x5a, /* VK_OEM_WSCTRL */
			//OEM2 = 0x5b, /* VK_OEM_FINISH */
			//OEM3 = 0x5c, /* VK_OEM_JUMP */
			//EraseEOF = 0x5d,
			//OEM4 = 0x5e, /* VK_OEM_BACKTAB */
			//OEM5 = 0x5f, /* VK_OEM_AUTO */
			//Zoom = 0x62,
			//Help = 0x63,
			//F13 = 0x64,
			//F14 = 0x65,
			//F15 = 0x66,
			//F16 = 0x67,
			//F17 = 0x68,
			//F18 = 0x69,
			//F19 = 0x6a,
			//F20 = 0x6b,
			//F21 = 0x6c,
			//F22 = 0x6d,
			//F23 = 0x6e,
			//OEM6 = 0x6f, /* VK_OEM_PA3 */
			//Katakana = 0x70,
			//OEM7 = 0x71, /* VK_OEM_RESET */
			//F24 = 0x76,
			//SBCSChar = 0x77,
			//Convert = 0x79,
			//Nonconvert = 0x7B, /* VK_OEM_PA1 */

			KeyboardOverrun = 0xFF,

			//MediaPrevious = unchecked((short)0xE010),
			//MediaNext = unchecked((short)0xE019),
			//NumpadEnter = unchecked((short)0xE01C),
			//RightControl = unchecked((short)0xE01D),
			//VolumeMute = unchecked((short)0xE020),
			//LaunchApp2 = unchecked((short)0xE021),
			//MediaPlay = unchecked((short)0xE022),
			//MediaStop = unchecked((short)0xE024),
			//VolumeDown = unchecked((short)0xE02E),
			//VolumeUp = unchecked((short)0xE030),
			//BrowserHome = unchecked((short)0xE032),
			//NumpadDivide = unchecked((short)0xE035),
			//PrintScreen = unchecked((short)0xE037),
			/*
			printScreen:
			- make: 0xE02A 0xE037
			- break: 0xE0B7 0xE0AA
			- MapVirtualKeyEx( VK_SNAPSHOT, MAPVK_VK_TO_VSC_EX, 0 ) returns scancode 0x54;
			- There is no VK_KEYDOWN with VK_SNAPSHOT.
			*/
			//RightAlt = unchecked((short)0xE038),
			//Cancel = unchecked((short)0xE046), /* CTRL + Pause */
			//Home = unchecked((short)0xE047),
			//Up = unchecked((short)0xE048),
			//PageUp = unchecked((short)0xE049),
			//Left = unchecked((short)0xE04B),
			//Right = unchecked((short)0xE04D),
			//End = unchecked((short)0xE04F),
			//Down = unchecked((short)0xE050),
			//PageDown = unchecked((short)0xE051),
			//Insert = unchecked((short)0xE052),
			//Delete = unchecked((short)0xE053),
			//MetaLeft = unchecked((short)0xE05B),
			//MetaRight = unchecked((short)0xE05C),
			//Application = unchecked((short)0xE05D),
			//Power = unchecked((short)0xE05E),
			//Sleep = unchecked((short)0xE05F),
			//Wake = unchecked((short)0xE063),
			//Browser_search = unchecked((short)0xE065),
			//Browser_favorites = unchecked((short)0xE066),
			//Browser_refresh = unchecked((short)0xE067),
			//Browser_stop = unchecked((short)0xE068),
			//BrowserForward = unchecked((short)0xE069),
			//BrowserBack = unchecked((short)0xE06A),
			//LaunchApp1 = unchecked((short)0xE06B),
			//LaunchEMail = unchecked((short)0xE06C),
			//LaunchMedia = unchecked((short)0xE06D),
		}


		[Win32.Source( "WinUser.h" )]
		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Copies the status of the 256 virtual keys to the specified buffer.</summary>
			/// <param name="state">Receives a 256-byte array containing the status data for each virtual key.</param>
			/// <returns>Returns true on success, otherwise returns false.</returns>
			/// <msdn>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646299%28v=vs.85%29.aspx</msdn>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			extern internal static bool GetKeyboardState(
				[Out, MarshalAs( UnmanagedType.LPArray, SizeConst = 256 )] byte[] state
			);


			///// <summary>Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to <see cref="GetAsyncKeyState"/>.</summary>
			///// <param name="key">The virtual-key code.</param>
			///// <returns>If the function succeeds, the return value specifies whether the key was pressed since the last call to GetAsyncKeyState, and whether the key is currently up or down. If the most significant bit is set, the key is down, and if the least significant bit is set, the key was pressed after the previous call to GetAsyncKeyState. However, you should not rely on this last behavior; for more information, see the Remarks.
			///// The return value is zero for the following cases:
			///// <para>The current desktop is not the active desktop</para>
			///// <para>The foreground thread belongs to another process and the desktop does not allow the hook or the journal record.</para>
			///// </returns>
			///// <remarks>
			///// <para>The GetAsyncKeyState function works with mouse buttons.
			///// However, it checks on the state of the physical mouse buttons, not on the logical mouse buttons that the physical buttons are mapped to.
			///// For example, the call GetAsyncKeyState(VK_LBUTTON) always returns the state of the left physical mouse button, regardless of whether it is mapped to the left or right logical mouse button.
			///// You can determine the system's current mapping of physical mouse buttons to logical mouse buttons by calling GetSystemMetrics(SM_SWAPBUTTON) which returns true if the mouse buttons have been swapped.
			///// </para>
			///// <para>Although the least significant bit of the return value indicates whether the key has been pressed since the last query, due to the pre-emptive multitasking nature of Windows, another application can call GetAsyncKeyState and receive the "recently pressed" bit instead of your application.
			///// The behavior of the least significant bit of the return value is retained strictly for compatibility with 16-bit Windows applications (which are non-preemptive) and should not be relied upon.
			///// </para>
			///// <para>You can use the virtual-key code constants VK_SHIFT, VK_CONTROL, and VK_MENU as values for the vKey parameter.
			///// This gives the state of the SHIFT, CTRL, or ALT keys without distinguishing between left and right.
			///// </para>
			///// </remarks>
			///// <msdn>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646293%28v=vs.85%29.aspx</msdn>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//extern internal static short GetAsyncKeyState(
			//	[In] Key key
			//);


			///// <summary>Retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled (on, off—alternating each time the key is pressed).</summary>
			///// <param name="key">A virtual key.
			///// If the desired virtual key is a letter or digit (A through Z, a through z, or 0 through 9), key must be set to the ASCII value of that character.
			///// For other keys, it must be a virtual-key code.
			///// <para>If a non-English keyboard layout is used, virtual keys with values in the range ASCII A through Z and 0 through 9 are used to specify most of the character keys.
			///// For example, for the German keyboard layout, the virtual key of value ASCII O (0x4F) refers to the "o" key, whereas VK_OEM_1 refers to the "o with umlaut" key.
			///// </para>
			///// </param>
			///// <returns>The return value specifies the status of the specified virtual key, as follows:
			///// - If the high-order bit is 1 (0x80), the key is down; otherwise, it is up.
			///// - If the low-order bit is 1 (0x01), the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.
			///// </returns>
			///// <remarks>The key status returned from this function changes as a thread reads key messages from its message queue.
			///// The status does not reflect the interrupt-level state associated with the hardware.
			///// Use the <see cref="GetAsyncKeyState"/> function to retrieve that information.
			///// <para>An application calls GetKeyState in response to a keyboard-input message. This function retrieves the state of the key when the input message was generated.</para>
			///// To retrieve state information for all the virtual keys, use the GetKeyboardState function.
			///// <para>An application can use the virtual key code constants VK_SHIFT, VK_CONTROL, and VK_MENU as values for the <paramref name="key"/> parameter.
			///// This gives the status of the SHIFT, CTRL, or ALT keys without distinguishing between left and right.
			///// An application can also use the following virtual-key code constants as values for <paramref name="key"/> to distinguish between the left and right instances of those keys:
			///// VK_LSHIFT, VK_RSHIFT, VK_LCONTROL, VK_RCONTROL, VK_LMENU, VK_RMENU
			///// </para>
			///// These left- and right-distinguishing constants are available to an application only through the GetKeyboardState, SetKeyboardState, GetAsyncKeyState, GetKeyState, and MapVirtualKey functions.
			///// </remarks>
			///// <msdn>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646301(v=vs.85).aspx</msdn>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//extern internal static short GetKeyState(
			//	[In] Key key
			//);

		}


		private static readonly byte[] globalState = new byte[ 256 ];
		private static KeyboardLEDIndicators leds;


		/// <summary>Gets a value indicating which LED indicators are active.</summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LEDs" )]
		public static KeyboardLEDIndicators LEDs => leds;


		private static void UpdateGlobal()
		{
			leds = KeyboardLEDIndicators.None;
			if( SafeNativeMethods.GetKeyboardState( globalState ) )
			{
				if( ( globalState[ (int)Key.NumLock ] & 0x01 ) != 0 )
					leds |= KeyboardLEDIndicators.NumLock;

				if( ( globalState[ (int)Key.CapsLock ] & 0x01 ) != 0 )
					leds |= KeyboardLEDIndicators.CapsLock;

				if( ( globalState[ (int)Key.ScrollLock ] & 0x01 ) != 0 )
					leds |= KeyboardLEDIndicators.ScrollLock;
			}
		}




		internal Keyboard( ref RawInputDeviceDescriptor descriptor )
			: base( ref descriptor )
		{
			this.Reset( TimeSpan.Zero );
		}

		

		/// <summary>Returns a value indicating whether a key is pressed in the current state and released in the previous state.</summary>
		/// <param name="button">A keyboard key.</param>
		/// <returns>Returns true if the key specified by <paramref name="button"/> is pressed in the current state and released in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( Key button )
		{
			if( base.IsDisconnected )
				return false;

			return base.CurrentState[ button ] && !base.PreviousState[ button ];
		}


		/// <summary>Returns a value indicating whether a key is released in the current state and pressed in the previous state.</summary>
		/// <param name="button">A keyboard key.</param>
		/// <returns>Returns true if the key specified by <paramref name="button"/> is released in the current state and pressed in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( Key button )
		{
			if( base.IsDisconnected )
				return false;

			return !base.CurrentState[ button ] && base.PreviousState[ button ];
		}


		/// <summary>Retrieves the keyboard state and returns it.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns a <see cref="KeyboardState"/> structure representing the current state of the keyboard.</returns>
		/// <exception cref="Win32Exception"/>
		protected sealed override KeyboardState GetState()
		{
			//if( !SafeNativeMethods.GetKeyboardState( State.Data ) )
			//{
			//	var errorCode = Marshal.GetLastWin32Error();
			//	base.IsDisconnected = true;
			//	if( errorCode == (int)Win32.ErrorCode.NotConnected )
			//		return KeyboardState.Empty;
			//	throw new Win32Exception( "Failed to retrieve keyboard state.", GetException( errorCode ) );
			//}

			UpdateGlobal();

			return new KeyboardState( state.Data );
		}


		/// <summary>Resets the state and information about this <see cref="Keyboard"/>.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected sealed override void Reset( TimeSpan time )
		{
			state = KeyboardState.Empty;

			base.Reset( time );
		}


		internal sealed override void Update( ref RawInput input )
		{
			var raw = input.Keyboard;
			var E0 = raw.MakeCodeInfo.HasFlag( ScanCodeCharacteristics.E0 );
			//var E1 = state.MakeCodeInfo.HasFlag( ScanCodeCharacteristics.E1 );
			//var scancode = unchecked((int)state.MakeCode);
			//if( E0 )
			//	scancode |= 0x0000E000;
			//else if( E1 )
			//	scancode |= 0x0000E100;

			//var key = ScanCodeToKey( scancode );
			//if( key == Key.None )
			//{
			var key = (Key)raw.VirtualKey;
			if( key == Key.ShiftKey )
			{
				var scanCode = (ScanCode)raw.MakeCode;
				if( scanCode == ScanCode.LeftShift )
					key = Key.LShiftKey;
				else if( scanCode == ScanCode.RightShift )
					key = Key.RShiftKey;
			}
			else if( key == Key.ControlKey )
				key = E0 ? Key.RControlKey : Key.LControlKey;
			else if( key == Key.Menu )
				key = E0 ? Key.RAlt : Key.LAlt;
			//}

			if( raw.MakeCodeInfo.HasFlag( ScanCodeCharacteristics.Break ) )
				base.state.Data[ (int)key ] &= 0x7F;
			else
				base.state.Data[ (int)key ] |= KeyboardState.KeyDownMask;
		}



		/// <summary>Gets a description of this <see cref="Keyboard"/>.</summary>
		public KeyboardDeviceInfo Description => base.info.KeyboardInfo;

	}

}