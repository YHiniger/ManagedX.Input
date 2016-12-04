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

		private const int MaxSupportedKeyboards = 4;    // FIXME - should be 2


		[Win32.Source( "WinUser.h" )]
		[SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


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


		///// <summary>Processes window messages to ensure the mouse motion and wheel state are up-to-date.</summary>
		///// <param name="message">A Windows message.</param>
		//[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Required by implementation." )]
		//public static void WndProc( ref System.Windows.Forms.Message message )
		//{
		//	if( message.Msg == 254 ) // WindowMessage.InputDeviceChange
		//	{
		//		var wParam = message.WParam.ToInt32();
		//		// Device arrival (wParam == 1) or removal (wParam == 2)
		//		// TODO - mark the device as disconnected on removal, otherwise initialize a new RawInputDevice.
		//	}
		//	else if( message.Msg == 255 ) // WindowMessage.Input
		//	{
		//		RawInput rawInput;
		//		NativeMethods.GetRawInputData( message.LParam, out rawInput );
		//		if( rawInput.DeviceType == InputDeviceType.Keyboard )
		//		{
		//			Keyboard targetKeyboard;
		//			if( !keyboards.TryGetValue( rawInput.DeviceHandle, out targetKeyboard ) )
		//				return;
		//		}
		//	}
		//}



		private KeyboardDeviceInfo info;


		
		internal Keyboard( GameControllerIndex controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( (int)controllerIndex, ref descriptor )
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
			var buffer = new byte[ 256 ];
			if( !( base.IsDisconnected = !SafeNativeMethods.GetKeyboardState( buffer ) ) )
				return new KeyboardState() { Data = buffer };

			var lastException = NativeMethods.GetExceptionForLastWin32Error();
			if( lastException.HResult == (int)Win32.ErrorCode.NotConnected )
				return KeyboardState.Empty;

			throw new Win32Exception( "Failed to retrieve keyboard state.", lastException );
		}


		/// <summary>Resets the state and information about this <see cref="Keyboard"/>.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected sealed override void Reset( TimeSpan time )
		{
			base.Reset( time );
			
			var deviceInfo = base.Info.KeyboardInfo;
			if( deviceInfo != null && deviceInfo.HasValue )
				info = deviceInfo.Value;
			else
				info = KeyboardDeviceInfo.Empty;
		}


		#region Device info

		/// <summary>Gets the number of function keys present on this <see cref="Keyboard"/>.</summary>
		public int FunctionKeyCount { get { return info.FunctionKeyCount; } }


		/// <summary>Gets the number of LED indicators present on this <see cref="Keyboard"/>.</summary>
		public int IndicatorCount { get { return info.IndicatorCount; } }


		/// <summary>Gets the type of this <see cref="Keyboard"/>.</summary>
		public int KeyboardType { get { return info.KeyboardType; } }
		

		/// <summary>Gets the sub-type of this <see cref="Keyboard"/>.</summary>
		public int KeyboardSubtype { get { return info.KeyboardSubtype; } }
		
		
		/// <summary>Gets the scan code mode of this <see cref="Keyboard"/>.</summary>
		public int Mode { get { return info.Mode; } }

		
		/// <summary>Gets the total number of keys present of this <see cref="Keyboard"/>.</summary>
		public int TotalKeyCount { get { return info.TotalKeyCount; } }

		#endregion Device info


		/// <summary>Gets the index of this <see cref="Keyboard"/>.</summary>
		new public GameControllerIndex Index { get { return (GameControllerIndex)base.Index; } }

	}

}