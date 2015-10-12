using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms; // for the Message structure


namespace ManagedX.Input
{


	// TODO - make use of RawInput (at least to retrieve the mouse display name, maybe to check for device connection and to support more than one mouse ?).


	/// <summary>A mouse.</summary>
	public sealed class MouseDevice : InputDevice<MouseState, MouseButton>
	{

		/// <summary>Enumerates mouse buttons, using their virtual key code.</summary>
		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731%28v=vs.85%29.aspx</remarks>
		private enum ButtonVirtualKeyCode : int
		{

			/// <summary>The left mouse button.</summary>
			Left = VirtualKeyCode.MouseLeft,

			/// <summary>The right mouse button.</summary>
			Right = VirtualKeyCode.MouseRight,

			/// <summary>The middle mouse button.</summary>
			Middle = VirtualKeyCode.MouseMiddle,

			/// <summary>The extended button 1.</summary>
			X1 = VirtualKeyCode.MouseX1,

			/// <summary>The extended button 2.</summary>
			X2 = VirtualKeyCode.MouseX2

		}


		/// <summary>Provides access to Win32 API functions defined in WinUser.h, and stored in User32.dll.</summary>
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Retrieves the position of the mouse cursor, in screen coordinates.</summary>
			/// <param name="position">A pointer to a POINT structure that receives the screen coordinates of the cursor.</param>
			/// <returns>Returns true if successful or false otherwise.</returns>
			[DllImport( LibraryName, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetCursorPos(
				[Out] out Point position
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648390%28v=vs.85%29.aspx


			/// <summary>Moves the cursor to the specified screen coordinates.
			/// If the new coordinates are not within the screen rectangle set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the cursor stays within the rectangle.</summary>
			/// <param name="x">The new x-coordinate of the cursor, in screen coordinates.</param>
			/// <param name="y">The new y-coordinate of the cursor, in screen coordinates.</param>
			/// <returns>Returns true if successful or false otherwise.</returns>
			/// <remarks>The cursor is a shared resource. A window should move the cursor only when the cursor is in the window's client area.
			/// <para>The calling process must have WINSTA_WRITEATTRIBUTES access to the window station.</para>
			/// <para>
			/// The input desktop must be the current desktop when you call SetCursorPos.
			/// Call OpenInputDesktop to determine whether the current desktop is the input desktop.
			/// If it is not, call SetThreadDesktop with the HDESK returned by OpenInputDesktop to switch to that desktop.
			/// </para>
			/// </remarks>
			[DllImport( LibraryName, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool SetCursorPos(
				[In] int x, [In] int y
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648394%28v=vs.85%29.aspx


			/// <summary>Displays or hides the cursor.</summary>
			/// <param name="show">If true, the display count is incremented by one; otherwise, the display count is decremented by one.</param>
			/// <returns>The return value specifies the new display counter.</returns>
			/// <remarks>
			/// <para>Windows 8: Call GetCursorInfo to determine the cursor visibility.</para>
			/// This function sets an internal display counter that determines whether the cursor should be displayed.
			/// The cursor is displayed only if the display count is greater than or equal to 0.
			/// If a mouse is installed, the initial display count is 0.
			/// If no mouse is installed, the display count is –1.
			/// </remarks>
			[DllImport( LibraryName, PreserveSig = true )]
			internal static extern int ShowCursor(
				[In, MarshalAs( UnmanagedType.Bool )] bool show
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648396%28v=vs.85%29.aspx


			/// <summary>Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to <see cref="GetAsyncKeyState"/>.</summary>
			/// <param name="key">The virtual-key code.</param>
			/// <returns>If the function succeeds, the return value specifies whether the key was pressed since the last call to GetAsyncKeyState, and whether the key is currently up or down. If the most significant bit is set, the key is down, and if the least significant bit is set, the key was pressed after the previous call to GetAsyncKeyState. However, you should not rely on this last behavior; for more information, see the Remarks.
			/// The return value is zero for the following cases:
			/// <para>The current desktop is not the active desktop</para>
			/// <para>The foreground thread belongs to another process and the desktop does not allow the hook or the journal record.</para>
			/// </returns>
			/// <remarks>
			/// <para>The GetAsyncKeyState function works with mouse buttons.
			/// However, it checks on the state of the physical mouse buttons, not on the logical mouse buttons that the physical buttons are mapped to.
			/// For example, the call GetAsyncKeyState(VK_LBUTTON) always returns the state of the left physical mouse button, regardless of whether it is mapped to the left or right logical mouse button.
			/// You can determine the system's current mapping of physical mouse buttons to logical mouse buttons by calling GetSystemMetrics(SM_SWAPBUTTON) which returns true if the mouse buttons have been swapped.
			/// </para>
			/// <para>Although the least significant bit of the return value indicates whether the key has been pressed since the last query, due to the pre-emptive multitasking nature of Windows, another application can call GetAsyncKeyState and receive the "recently pressed" bit instead of your application.
			/// The behavior of the least significant bit of the return value is retained strictly for compatibility with 16-bit Windows applications (which are non-preemptive) and should not be relied upon.
			/// </para>
			/// <para>You can use the virtual-key code constants VK_SHIFT, VK_CONTROL, and VK_MENU as values for the vKey parameter.
			/// This gives the state of the SHIFT, CTRL, or ALT keys without distinguishing between left and right.
			/// </para>
			/// </remarks>
			[DllImport( LibraryName, PreserveSig = true )]
			internal static extern short GetAsyncKeyState(
				[In] ButtonVirtualKeyCode key
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646293%28v=vs.85%29.aspx


			/// <summary>Retrieves information about the global cursor.</summary>
			/// <param name="cursorInfo">A pointer to a <see cref="CursorInfo"/> structure that receives the information. Note that you must set the cbSize member to sizeof(<see cref="CursorInfo"/>) before calling this function.</param>
			/// <returns>If the function succeeds, the return value is true.
			/// If the function fails, the return value is false. To get extended error information, call GetLastError.</returns>
			[DllImport( LibraryName, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetCursorInfo(
				[In, Out] ref CursorInfo cursorInfo
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648389%28v=vs.85%29.aspx

		}


		#region Static

		private static readonly MouseDevice defaultMouse = new MouseDevice();


		/// <summary>Gets the default <see cref="MouseDevice"/>.</summary>
		public static MouseDevice Default { get { return defaultMouse; } }


		private static ButtonVirtualKeyCode GetVirtualKeyCode( MouseButton button )
		{
			if( button == MouseButton.Left )
				return ButtonVirtualKeyCode.Left;

			if( button == MouseButton.Right )
				return ButtonVirtualKeyCode.Right;

			if( button == MouseButton.Middle )
				return ButtonVirtualKeyCode.Middle;

			if( button == MouseButton.X1 )
				return ButtonVirtualKeyCode.X1;

			return ButtonVirtualKeyCode.X2;
		}

		#endregion


		private static CursorInfo cursorInfo = CursorInfo.Default;
		private static bool isCursorHidden;


		/// <summary>Retrieves information about the global cursor.</summary>
		/// <exception cref="InvalidOperationException"/>
		private static void Update()
		{
			if( !NativeMethods.GetCursorInfo( ref cursorInfo ) )
				throw new InvalidOperationException( "Failed to retrieve mouse cursor info.", Marshal.GetExceptionForHR( Marshal.GetLastWin32Error() ) );
		}


		/// <summary>Gets or sets a value indicating whether the mouse cursor is hidden.</summary>
		/// <exception cref="InvalidOperationException"/>
		public static bool IsCursorHidden
		{
			get
			{
				try
				{
					Update();
					return isCursorHidden = cursorInfo.Hidden;
				}
				catch( InvalidOperationException )
				{
					throw;
				}
			}
			set
			{
				isCursorHidden = value;
				NativeMethods.ShowCursor( !isCursorHidden );
				//if( isCursorHidden )
				//	Cursor.Hide();
				//else
				//	Cursor.Show();
			}
		}


		/// <summary>Sets the mouse cursor position.</summary>
		/// <param name="position">The desired cursor position.</param>
		/// <returns>Returns true on success, false otherwise.</returns>
		public static bool SetPosition( Point position )
		{
			return NativeMethods.SetCursorPos( position.X, position.Y );
		}


		/// <summary>Processes Windows messages to ensure the mouse wheel state is up-to-date.</summary>
		/// <param name="message">A Windows message.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Required by implementation." )]
		public static void WndProc( ref Message message )
		{
			if( message.Msg == 522 ) // WindowMessage.MouseWheel
				defaultMouse.wheelValue += ( message.WParam.ToInt32() >> 16 ) / 120;
		}


		private int wheelValue;
		private int cumulatedWheelValue;


		private MouseDevice()
			: base( 0 )
		{
			this.Initialize();
		}


		/// <summary>Gets a value indicating whether the mouse is connected; this value is always true.</summary>
		public sealed override bool IsConnected { get { return true; } }


		/// <summary>Reads and returns the mouse state.</summary>
		/// <returns>The mouse state.</returns>
		protected sealed override MouseState GetState()
		{
			const short Mask = -32768;

			var buttons = new bool[ 5 ];
			for( MouseButton b = MouseButton.Left; b <= MouseButton.X2; b++ )
				buttons[ (int)b ] = ( NativeMethods.GetAsyncKeyState( GetVirtualKeyCode( b ) ) & Mask ) == Mask;

			cumulatedWheelValue += wheelValue;

			Point position;
			if( !NativeMethods.GetCursorPos( out position ) )
				return MouseState.Empty;

			var state = new MouseState( position.X, position.Y, wheelValue, buttons );
			wheelValue = 0;
			return state;
		}


		/// <summary>Returns a value indicating whether a button has just been pressed.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>True if the button was released and is pressed, otherwise false.</returns>
		public sealed override bool HasJustBeenPressed( MouseButton button )
		{
			return base.CurrentState[ button ] && !base.PreviousState[ button ];
		}

		/// <summary>Returns a value indicating whether a button has just been released.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>True if the button was pressed and is released, otherwise false.</returns>
		public sealed override bool HasJustBeenReleased( MouseButton button )
		{
			return !base.CurrentState[ button ] && base.PreviousState[ button ];
		}


		/// <summary>Gets or sets the <see cref="MouseDevice"/> wheel value.</summary>
		public int WheelValue
		{
			get { return cumulatedWheelValue; }
			set { cumulatedWheelValue = value; }
		}


		/// <summary>Returns "Mouse".</summary>
		/// <returns>Returns "Mouse".</returns>
		public sealed override string ToString()
		{
			return Properties.Resources.Mouse;
		}

	}

}