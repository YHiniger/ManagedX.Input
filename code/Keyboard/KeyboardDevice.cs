using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Input
{


	/// <summary>A keyboard.</summary>
	public sealed class KeyboardDevice : InputDevice<KeyboardState, Key>
	{

		private const short AsyncKeyDownMask = 0x0001;
		private const short AsyncKeyToggleMask = unchecked( (short)0x8000 );


		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			///// <summary>Determines whether a key is up or down at the time the function is called, and whether the key was pressed after a previous call to <see cref="GetAsyncKeyState"/>.</summary>
			///// <param name="key">The <see cref="Key">virtual-key code</see>.</param>
			///// <returns>The return value specifies the status of the specified virtual key, as follows:
			///// If the high-order bit is 1, the key is down; otherwise, it is up.
			///// If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.
			///// </returns>
			//[DllImport( LibraryName, PreserveSig = true )]
			//internal static extern short GetAsyncKeyState(
			//	[In] Key key
			//);
			//// http://msdn.microsoft.com/en-us/library/windows/desktop/ms646293%28v=vs.85%29.aspx


			/// <summary>Copies the status of the 256 virtual keys to the specified buffer.</summary>
			/// <param name="state">Receives a 256-byte array containing the status data for each virtual key.</param>
			/// <returns>Returns true on success, otherwise returns false.</returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			internal static extern bool GetKeyboardState(
				[Out, MarshalAs( UnmanagedType.LPArray, SizeConst = 256 )] byte[] state
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646299%28v=vs.85%29.aspx
			//BOOL GetKeyboardState(
			//	_Out_writes_(256) PBYTE lpKeyState
			//);

		}


		#region Static fields


		private static readonly KeyboardDevice defaultKeyboard = new KeyboardDevice();

		/// <summary>Gets the default <see cref="KeyboardDevice"/>.</summary>
		public static KeyboardDevice Default { get { return defaultKeyboard; } }

		
		#endregion



		private bool isConnected;
		private byte[] state;


		private KeyboardDevice()
			: base( 0 )
		{
			state = new byte[ 256 ];
		}


		/// <summary>Gets a value indicating whether the keyboard is connected.</summary>
		public sealed override bool IsConnected { get { return isConnected; } }


		/// <summary>Retrieves the keyboard state and returns it.</summary>
		/// <returns>Returns a <see cref="KeyboardState"/> structure representing the current state of the keyboard.</returns>
		protected sealed override KeyboardState GetState()
		{
			if( !SafeNativeMethods.GetKeyboardState( state ) )
			{
				var errorCode = Marshal.GetLastWin32Error();
				isConnected = false;
				if( errorCode == (int)XInput.ErrorCode.NotConnected )
					return KeyboardState.Default;

				var exception = Marshal.GetExceptionForHR( errorCode );
				if( exception == null )
					return KeyboardState.Default;

				throw exception;
			}

			isConnected = true;
			return new KeyboardState( state );
		}


		/// <summary>Returns a value indicating whether a key was released in the previous state and is pressed in the current state.</summary>
		/// <param name="button">A <see cref="Key"/> value.</param>
		/// <returns>Returns true if the key was released in the previous state and is pressed in the current state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( Key button )
		{
			return base.CurrentState[ button ] && !base.PreviousState[ button ];
		}


		/// <summary>Returns a value indicating whether a key was pressed in the previous state and is released in the current state.</summary>
		/// <param name="button">A <see cref="Key"/> value.</param>
		/// <returns>Returns true if the key was pressed in the previous state and is released in the current state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( Key button )
		{
			return !base.CurrentState[ button ] && base.PreviousState[ button ];
		}


		/// <summary>Returns "Keyboard".</summary>
		/// <returns>Returns "Keyboard".</returns>
		public sealed override string ToString()
		{
			return Properties.Resources.Keyboard;
		}

	}

}