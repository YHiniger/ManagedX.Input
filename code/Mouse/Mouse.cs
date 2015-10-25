using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;


namespace ManagedX.Input
{
	using Raw;

	
	/// <summary>A mouse.</summary>
	public sealed class Mouse : RawInputDevice<MouseState, MouseButton>
	{

		/// <summary>Enumerates mouse buttons, using their <see cref="VirtualKeyCode"/>.</summary>
		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731%28v=vs.85%29.aspx</remarks>
		private enum ButtonVirtualKeyCode : int
		{

			/// <summary>No button.</summary>
			None = VirtualKeyCode.None,

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


		#region Static

		private static ButtonVirtualKeyCode ToVirtualKeyCode( int buttonIndex )
		{
			if( buttonIndex == (int)MouseButton.Left )
				return ButtonVirtualKeyCode.Left;

			if( buttonIndex == (int)MouseButton.Right )
				return ButtonVirtualKeyCode.Right;

			if( buttonIndex == (int)MouseButton.Middle )
				return ButtonVirtualKeyCode.Middle;

			if( buttonIndex == (int)MouseButton.X1 )
				return ButtonVirtualKeyCode.X1;

			if( buttonIndex == (int)MouseButton.X2 )
				return ButtonVirtualKeyCode.X2;

			return ButtonVirtualKeyCode.None;
		}


		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Retrieves information about the global cursor.</summary>
			/// <param name="cursorInfo">A valid <see cref="CursorInfo"/> structure that receives the information.</param>
			/// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.
			/// <para>To get extended error information, call GetLastError.</para>
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetCursorInfo(
				[In, Out] ref CursorInfo cursorInfo
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648389%28v=vs.85%29.aspx


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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool SetCursorPos(
				[In] int x,
				[In] int y
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648394%28v=vs.85%29.aspx


			/// <summary>Displays or hides the cursor.</summary>
			/// <param name="show">If true, the display count is incremented by one; otherwise, the display count is decremented by one.</param>
			/// <returns>The return value specifies the new display counter.</returns>
			/// <remarks>
			/// <para>Windows 8: Call <see cref="GetCursorInfo"/> to determine the cursor cursorState.</para>
			/// This function sets an internal display counter that determines whether the cursor should be displayed.
			/// The cursor is displayed only if the display count is greater than or equal to 0.
			/// If a mouse is installed, the initial display count is 0.
			/// If no mouse is installed, the display count is –1.
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern short GetAsyncKeyState(
				[In] ButtonVirtualKeyCode key
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646293%28v=vs.85%29.aspx

		}


		private static readonly Dictionary<IntPtr, Mouse> mice = new Dictionary<IntPtr, Mouse>( 1 );
		private static MouseCursorOptions cursorState;

		
		private static void Initialize()
		{
			var allDevices = NativeMethods.GetRawInputDeviceList();
			var index = 0;
			for( int d = 0; d < allDevices.Length; d++ )
			{
				var descriptor = allDevices[ d ];
				if( descriptor.DeviceType == InputDeviceType.Mouse )
				{
					var mouse = new Mouse( (GameControllerIndex)index, ref descriptor );
					mice.Add( mouse.DeviceHandle, mouse );
					if( ++index == 4 ) // "only" 4 mice are supported
						break;
				}
			}
		}


		/// <summary>Gets the default (primary) mouse.</summary>
		public static Mouse Default
		{
			get
			{
				if( mice.Count == 0 )
					Initialize();

				foreach( var mouse in mice.Values )
					return mouse;

				return null;
			}
		}


		/// <summary>Gets a read-only collection containing all known (up to 4) mice.</summary>
		public static System.Collections.ObjectModel.ReadOnlyCollection<Mouse> All
		{
			get
			{
				if( mice.Count == 0 )
					Initialize();

				var array = new Mouse[ mice.Count ];
				mice.Values.CopyTo( array, 0 );
				return new System.Collections.ObjectModel.ReadOnlyCollection<Mouse>( array );
			}
		}


		/// <summary>Gets or sets a value indicating the state of the mouse cursor.
		/// <para>Note: <see cref="MouseCursorOptions.Suppressed"/> is handled as <see cref="MouseCursorOptions.Hidden"/>.</para>
		/// </summary>
		public static MouseCursorOptions CursorState
		{
			get { return cursorState; }
			set
			{
				if( value.HasFlag( MouseCursorOptions.Suppressed ) )
					value = MouseCursorOptions.Hidden;

				cursorState = value;

				if( cursorState == MouseCursorOptions.Showing )
					while( SafeNativeMethods.ShowCursor( true ) < 0 ) ;
				else
					while( SafeNativeMethods.ShowCursor( false ) >= 0 ) ;
			}
		}


		/// <summary>Sets the mouse cursor position.
		/// <para>A window should move the cursor only when the cursor is in the window's client area.</para>
		/// </summary>
		/// <param name="position">The new mouse cursor position, relative to the desktop.</param>
		/// <exception cref="System.ComponentModel.Win32Exception"/>
		public static void SetCursorPosition( Point position )
		{
			if( !SafeNativeMethods.SetCursorPos( position.X, position.Y ) )
				throw new System.ComponentModel.Win32Exception( "Failed to set mouse cursor location.", GetLastWin32Exception() );
		}


		/// <summary>Causes the target window to receive raw mouse input messages.
		/// <para>Important: that window must then override its WndProc method to call <see cref="WndProc"/> prior to its base method.</para>
		/// </summary>
		/// <param name="targetWindow">The target window.</param>
		/// <param name="options">One or more <see cref="RawInputDeviceRegistrationOptions"/>.</param>
		public static void Register( IWin32Window targetWindow, RawInputDeviceRegistrationOptions options )
		{
			var device = RawInputDevice.Mouse;
			device.targetWindowHandle = ( targetWindow == null ) ? IntPtr.Zero : targetWindow.Handle;
			device.flags = options;

			if( mice.Count == 0 )
				Initialize();

			try
			{
				NativeMethods.RegisterRawInputDevices( device );
			}
			catch( Exception )
			{
				throw;
			}
		}


		/// <summary>Processes window messages to ensure the mouse motion and wheel state are up-to-date.</summary>
		/// <param name="message">A Windows message.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Required by implementation." )]
		public static void WndProc( ref Message message )
		{
			if( message.Msg == 254 ) // WindowMessage.InputDeviceChange
			{
				var wParam = message.WParam.ToInt32();
				// Device arrival (wParam == 1) or removal (wParam == 2)
				// TODO - mark the device as disconnected on removal, otherwise initialize a new RawInputDevice.
			}
			else if( message.Msg == 255 ) // WindowMessage.Input
			{
				RawInput rawInput;
				NativeMethods.GetRawInputData( message.LParam, out rawInput );
				if( rawInput.DeviceType == InputDeviceType.Mouse )
				{
					Mouse targetMouse;
					if( !mice.TryGetValue( rawInput.DeviceHandle, out targetMouse ) )
						return;

					var mouseState = rawInput.Mouse.Value;
					if( mouseState.State.HasFlag( RawMouseStates.MoveRelative ) )
					{
						targetMouse.motion.X += mouseState.LastX;
						targetMouse.motion.Y += mouseState.LastY;
					}

					// FIXME - doesn't seem to work... may be about the horizontal wheel ?
					if( mouseState.ButtonsState.HasFlag( RawMouseButtonStates.Wheel ) )
						targetMouse.wheelValue += mouseState.WheelDelta;
				}
			}
			else if( message.Msg == 522 ) // WindowMessage.MouseWheel
				foreach( var mouse in mice.Values )
				{
					mouse.wheelDelta += ( message.WParam.ToInt32() >> 16 );
					break;
				}
		}

		#endregion // Static



		private bool disconnected;
		private Point motion;
		private int wheelDelta;
		private int wheelValue;


		#region Constructor, destructor

		/// <summary>Constructor.</summary>
		/// <param name="controllerIndex">The index of the mouse.</param>
		/// <param name="descriptor">The raw input device descriptor for the mouse.</param>
		private Mouse( GameControllerIndex controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex, ref descriptor )
		{
			var zero = TimeSpan.Zero;
			this.Reset( ref zero );
		}

		
		/// <summary>Destructor.</summary>
		~Mouse()
		{
			if( mice.ContainsValue( this ) )
				mice.Remove( this.DeviceHandle );
		}

		#endregion // Constructor, destructor


		/// <summary>Resets the mouse device.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		protected sealed override void Reset( ref TimeSpan time )
		{
			motion = Point.Zero;
			wheelDelta = 0;

			base.Reset( ref time );
		}


		/// <summary>Gets a value indicating whether the mouse is disconnected.</summary>
		public sealed override bool Disconnected { get { return disconnected; } }


		/// <summary>Returns the mouse state.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns the mouse state.</returns>
#if DEBUG
		/// <exception cref="Win32Exception"/>
#endif
		protected sealed override MouseState GetState()
		{
			const short Mask = -32768;
			const int MaxSupportedButtonCount = 5;


			var cursorInfo = CursorInfo.Default;
			if( disconnected = !SafeNativeMethods.GetCursorInfo( ref cursorInfo ) )
			{
#if DEBUG
				throw new Win32Exception( "Failed to retrieve mouse cursor position.", GetLastWin32Exception() );
#else
				wheelValue = wheelDelta = 0;
				motion = Point.Zero;
				return MouseState.Empty;
#endif
			}
			cursorState = cursorInfo.State;

			var buttons = 0;
			for( var b = 0; b < MaxSupportedButtonCount; b++ )
				if( ( SafeNativeMethods.GetAsyncKeyState( ToVirtualKeyCode( b ) ) & Mask ) == Mask )
					buttons |= 1 << b;

			wheelDelta /= 120;

			var state = new MouseState( ref cursorInfo.ScreenPosition, ref motion, wheelDelta, (MouseButtons)buttons );

			motion = Point.Zero;
			wheelValue += wheelDelta;
			wheelDelta = 0;

			return state;
		}


		/// <summary>Gets a value indicating whether a button is pressed in the current state and released in the previous state.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed in the current state and released in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( MouseButton button )
		{
			return this.CurrentState[ button ] && !this.PreviousState[ button ];
		}


		/// <summary>Gets a value indicating whether a button is released in the current state and pressed in the previous state.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is released in the current state and pressed in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( MouseButton button )
		{
			return !this.CurrentState[ button ] && this.PreviousState[ button ];
		}


		/// <summary>Gets or sets the cumulated wheel value.</summary>
		public int WheelValue
		{
			get { return wheelValue; }
			set { wheelValue = value; }
		}


		/// <summary>Gets information about the mouse.</summary>
		public MouseDeviceInfo DeviceInfo { get { return base.Info.MouseInfo.Value; } }
		
	}

}