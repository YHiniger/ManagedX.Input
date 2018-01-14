using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;


namespace ManagedX.Input
{
	using Raw;
	using Win32;
	using XInput;


	/// <summary>The input device manager (or input service).</summary>
	public static class InputDeviceManager
	{

		#region XInput

		private static XInputVersion GetXInputVersion()
		{
#if XINPUT_14
			if( XInput14GameController.IsAvailable )
				return XInputVersion.XInput14;
#elif XINPUT_13
			if( XInput13GameController.IsAvailable )
				return XInputVersion.XInput13;
#else

			if( XInput14GameController.IsAvailable )
				return XInputVersion.XInput14;

			if( XInput13GameController.IsAvailable )
				return XInputVersion.XInput13;
#endif
			return XInputVersion.NotSupported;
		}


		private static List<GameController> InitializeXInput()
		{
			var list = new List<GameController>( GameController.MaxControllerCount );

			if( version == XInputVersion.XInput14 )
			{
				for( var c = 0; c < XInput14GameController.MaxControllerCount; ++c )
					list.Add( new XInput14GameController( (GameControllerIndex)c ) );
			}
			else if( version == XInputVersion.XInput13 )
			{
				for( var c = 0; c < XInput13GameController.MaxControllerCount; ++c )
					list.Add( new XInput13GameController( (GameControllerIndex)c ) );
			}

			return list;
		}


		private static readonly XInputVersion version = GetXInputVersion();
		private static readonly List<GameController> gameControllers = InitializeXInput();
		private static bool xInputEnabled = true;



		/// <summary>Gets the version of the underlying XInput API.</summary>
		public static Version Version
		{
			get
			{
				if( version == XInputVersion.XInput14 )
					return new Version( 1, 4 );

				if( version == XInputVersion.XInput13 )
					return new Version( 1, 3 );

				return new Version( 0, 0 );
			}
		}


		/// <summary>Gets a read-only collection of all supported XInput game controllers.</summary>
		public static ReadOnlyCollection<GameController> GameControllers => new ReadOnlyCollection<GameController>( gameControllers );


		/// <summary>Returns an XInput <see cref="GameController"/> given its index.</summary>
		/// <param name="index">The index of the requested game controller.</param>
		/// <returns>Returns the XInput game controller associated with the specified <paramref name="index"/>.</returns>
		public static GameController GetController( GameControllerIndex index ) => gameControllers[ (int)index ];

		#endregion XInput


		#region RawInput

		/// <summary>Options for the <see cref="NativeMethods.RegisterDeviceNotificationW"/> function.</summary>
		/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363431%28v=vs.85%29.aspx</remarks>
		[Flags]
		private enum RegisterDeviceNotificationOptions : int
		{

			/// <summary>The handle parameter is a window handle.</summary>
			[Source( "WinUser.h", "DEVICE_NOTIFY_WINDOW_HANDLE" )]
			WindowHandle = 0x00000000,

			/// <summary>The handle parameter is a service status handle.</summary>
			[Source( "WinUser.h", "DEVICE_NOTIFY_SERVICE_HANDLE" )]
			ServiceHandle = 0x00000001,


			/// <summary>Notifies the recipient of device interface events for all device interface classes (the dbcc_classguid member is ignored).
			/// <para>This value can be used only if the dbch_devicetype member is DBT_DEVTYP_DEVICEINTERFACE.</para>
			/// </summary>
			[Source( "WinUser.h", "DEVICE_NOTIFY_ALL_INTERFACE_CLASSES" )]
			AllInterfaceClasses = 0x00000004,

		}


		///// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646307(v=vs.85).aspx</remarks>
		//private enum MapVirtualKeyMapType : int
		//{

		//	/// <summary>The code parameter is a virtual-key code and is translated into a scan code.
		//	/// If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned.
		//	/// If there is no translation, the function returns 0.
		//	/// </summary>
		//	[Source( "WinUser.h", "MAPVK_VK_TO_VSC" )]
		//	VirtualKeyToScanCode,

		//	/// <summary>The code parameter is a scan code and is translated into a virtual-key code that does not distinguish between left- and right-hand keys.
		//	/// If there is no translation, the function returns 0.
		//	/// </summary>
		//	[Source( "WinUser.h", "MAPVK_VSC_TO_VK" )]
		//	ScanCodeToVirtualKey,

		//	/// <summary>The code parameter is a virtual-key code and is translated into an unshifted character value in the low order word of the return value.
		//	/// Dead keys (diacritics) are indicated by setting the top bit of the return value.
		//	/// If there is no translation, the function returns 0.
		//	/// </summary>
		//	[Source( "WinUser.h", "MAPVK_VK_TO_CHAR" )]
		//	VirtualKeyToChar,

		//	/// <summary>The code parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys.
		//	/// If there is no translation, the function returns 0.
		//	/// </summary>
		//	[Source( "WinUser.h", "MAPVK_VSC_TO_VK_EX" )]
		//	ScanCodeToVirtualKeyEx,

		//	/// <summary>The code parameter is a virtual-key code and is translated into a scan code.
		//	/// If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned.
		//	/// If the scan code is an extended scan code, the high byte of the code value can contain either 0xE0 or 0xE1 to specify the extended scan code.
		//	/// If there is no translation, the function returns 0.
		//	/// </summary>
		//	[Source( "WinUser.h", "MAPVK_VK_TO_VSC_EX" )]
		//	VirtualKeyToScanCodeEx

		//}


		[System.Security.SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Enumerates the raw input devices attached to the system.</summary>
			/// <param name="descriptors">An array of <see cref="RawInputDeviceDescriptor"/> structures for the devices attached to the system. If null, the number of devices is returned in <paramref name="deviceCount"/>.</param>
			/// <param name="deviceCount">If <paramref name="descriptors"/> is null, receives the number of devices attached to the system; otherwise, specifies the number of <see cref="RawInputDeviceDescriptor"/> structures that can be contained in the buffer <paramref name="descriptors"/> points to.
			/// <para>If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with <see cref="ErrorCode.InsufficientBuffer"/>.</para>
			/// </param>
			/// <param name="size">The size, in bytes, of a <see cref="RawInputDeviceDescriptor"/> structure.</param>
			/// <returns>If the function is successful, the return value is the number of devices stored in the buffer pointed to by <paramref name="descriptors"/>.
			/// <para>On any other error, the function returns -1 and GetLastError returns the error indication.</para>
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645598%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static int GetRawInputDeviceList(
				[Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 ), Optional] RawInputDeviceDescriptor[] descriptors,
				[In, Out] ref int deviceCount,
				[In] int size
			);


			#region GetRawInputDeviceInfo

			/// <summary>Retrieves information about the raw input device.</summary>
			/// <param name="deviceHandle">A handle to the raw input device. This comes from the lParam of the WM_INPUT message, from the <see cref="RawInputHeader.DeviceHandle"/> member, or from GetRawInputDeviceList.
			/// <para>It can also be null if an application inserts input data, for example, by using SendInput.</para>
			/// </param>
			/// <param name="command">Specifies what data will be returned in <paramref name="data"/>. This parameter can be one of the following values:
			/// <para><see cref="GetInfoCommand.PreParsedData"/></para>
			/// <para><see cref="GetInfoCommand.DeviceInfo"/></para>
			/// For the <see cref="GetInfoCommand.DeviceName"/>, use the alternate function signature.
			/// </param>
			/// <param name="data">A pointer to a buffer that contains the information specified by <paramref name="command"/>.
			/// <para>If <paramref name="command"/> is <see cref="GetInfoCommand.DeviceInfo"/>, set the Size member of <see cref="DeviceInfo"/> to sizeof( <see cref="DeviceInfo"/> ) before calling GetRawInputDeviceInfo.</para>
			/// </param>
			/// <param name="dataSize">The size, in bytes, of the data in <paramref name="data"/>.</param>
			/// <returns>If successful, this function returns a non-negative number indicating the number of bytes copied to <paramref name="data"/>.
			/// <para>
			/// If <paramref name="data"/> is not large enough for the data, the function returns -1.
			/// If <paramref name="data"/> is null, the function returns a value of zero.
			/// In both of these cases, <paramref name="dataSize"/> is set to the minimum size required for the <paramref name="data"/> buffer.
			/// Call <see cref="Marshal.GetLastWin32Error"/> to identify any other errors.
			/// </para>
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645597%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static int GetRawInputDeviceInfoW(
				[In, Optional] IntPtr deviceHandle,
				[In] GetInfoCommand command,
				[In, Out, Optional] ref DeviceInfo data,
				[In, Out] ref int dataSize
			);

			/// <summary>Retrieves the name of a raw input device.</summary>
			/// <param name="deviceHandle">A handle to the raw input device. This comes from the LParam of the WM_INPUT message, from the RAWINPUTHEADER.DeviceHandle, or from GetRawInputDeviceList. It can also be NULL if an application inserts input data, for example, by using SendInput.</param>
			/// <param name="command">Must be <see cref="GetInfoCommand.DeviceName"/>.</param>
			/// <param name="data">A pointer to a buffer that receives the information specified by <paramref name="command"/>.</param>
			/// <param name="dataSize">The length, in characters, of the name in <paramref name="data"/>.</param>
			/// <returns>If successful, this function returns a positive number indicating the number of characters (not bytes!) copied to <paramref name="data"/>.
			/// If <paramref name="data"/> is not large enough for the data, the function returns -1.
			/// If <paramref name="data"/> is null, the function returns a value of zero.
			/// In both of these cases, <paramref name="dataSize"/> is set to the minimum size required for the <paramref name="data"/> buffer.
			/// Call GetLastError to identify any other errors.
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645597%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static int GetRawInputDeviceInfoW(
				[In, Optional] IntPtr deviceHandle,
				[In] GetInfoCommand command,
				[In, Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 3 ), Optional] string data,
				[In, Out] ref int dataSize
			);

			#endregion GetRawInputDeviceInfo


			/// <summary>Registers the devices that supply the raw input data.</summary>
			/// <param name="rawInputDevices">An array of <see cref="RawInputDevice"/> structures that represent the devices that supply the raw input.</param>
			/// <param name="deviceCount">The number of <see cref="RawInputDevice"/> structures pointed to by <paramref name="rawInputDevices"/>.</param>
			/// <param name="size">The size, in bytes, of a <see cref="RawInputDevice"/> structure.</param>
			/// <returns>Returns true if the function succeeds, otherwise returns false; if the function fails, call GetLastError for more information.</returns>
			/// <remarks>
			/// To receive WM_INPUT messages, an application must first register the raw input devices using RegisterRawInputDevices. By default, an application does not receive raw input.
			/// <para>To receive WM_INPUT_DEVICE_CHANGE messages, an application must specify the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> flag for each device class that is specified by the UsagePage and Usage fields of the <see cref="RawInputDevice"/> structure.
			/// By default, an application does not receive WM_INPUT_DEVICE_CHANGE notifications for raw input device arrival and removal.</para>
			/// <para>If a <see cref="RawInputDevice"/> structure has the <see cref="RawInputDeviceRegistrationOptions.Remove"/> flag set and the <see cref="RawInputDevice.TargetWindowHandle">TargetWindowHandle</see> parameter is not set to null, then parameter validation will fail.</para>
			/// </remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			extern internal static bool RegisterRawInputDevices(
				[In, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] RawInputDevice[] rawInputDevices,
				[In] int deviceCount,
				[In] int size
			);
			// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645600%28v=vs.85%29.aspx
			//BOOL WINAPI RegisterRawInputDevices(
			//  _In_ PCRAWINPUTDEVICE pRawInputDevices,
			//  _In_ UINT             uiNumDevices,
			//  _In_ UINT             cbSize
			//);


			/// <summary>Retrieves the information about the raw input devices for the current application.</summary>
			/// <param name="rawInputDevices">An array of <see cref="RawInputDevice"/> structures for the application.</param>
			/// <param name="deviceCount">The number of <see cref="RawInputDevice"/> structures in <paramref name="rawInputDevices"/>.</param>
			/// <param name="structSize">The size, in bytes, of a <see cref="RawInputDevice"/> structure.</param>
			/// <returns>
			/// If successful, the function returns a non-negative number that is the number of <see cref="RawInputDevice"/> structures written to the buffer.
			/// If the <paramref name="rawInputDevices"/> buffer is too small or null, the function sets the last error as ERROR_INSUFFICIENT_BUFFER(0x7A), returns -1, and sets <paramref name="deviceCount"/> to the required number of devices.
			/// If the function fails for any other reason, it returns -1. For more details, call GetLastError.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static int GetRegisteredRawInputDevices(
				[Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 ), Optional] RawInputDevice[] rawInputDevices,
				[In, Out] ref int deviceCount,
				[In] int structSize
			);


			/// <summary>Retrieves the raw input, one <see cref="RawInput"/> structure at a time, from the specified device.
			/// <para>In contrast, <see cref="GetRawInputBuffer"/> gets an array of <see cref="RawInput"/> structures.</para>
			/// </summary>
			/// <param name="rawInputHandle">A handle to the <see cref="RawInput"/> structure. This comes from the LParam in <see cref="WindowMessage.Input"/>.</param>
			/// <param name="command">The command flag.</param>
			/// <param name="data">A pointer to the data that comes from the <see cref="RawInput"/> structure. This depends on the value of <paramref name="command"/>. If <paramref name="data"/> is null, the required size of the buffer is returned in <paramref name="size"/>.</param>
			/// <param name="size">The size, in bytes, of the data in <paramref name="data"/>.</param>
			/// <param name="headerSize">The size, in bytes, of the <see cref="RawInputHeader"/> structure.</param>
			/// <returns>
			/// If <paramref name="data"/> is null and the function is successful, the return value is 0.
			/// <para>If <paramref name="data"/> is not null and the function is successful, the return value is the number of bytes copied into <paramref name="data"/>.</para>
			/// If there is an error, the return value is -1.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			unsafe extern internal static int GetRawInputData(
				[In] IntPtr rawInputHandle,
				[In] GetDataCommand command,
				[Out] void* data,
				[In, Out] ref int size,
				[In] int headerSize
			);


			/// <summary>Performs a buffered read of the raw input data.</summary>
			/// <param name="data">A pointer to a buffer of <see cref="RawInput"/> structures that contain the raw input data.
			/// If null, the minimum required buffer, in bytes, is returned in <paramref name="size"/>.
			/// </param>
			/// <param name="size">The size, in bytes, of a <see cref="RawInput"/> structure.</param>
			/// <param name="headerSize">The size, in bytes, of the <see cref="RawInputHeader"/> structure.</param>
			/// <returns>
			/// If <paramref name="data"/> is null and the function is successful, the return value is zero.
			/// If <paramref name="data"/> is not null and the function is successful, the return value is the number of <see cref="RawInput"/> structures written to <paramref name="data"/>.
			/// If an error occurs, the return value is (UINT)-1. Call GetLastError for the error code.
			/// </returns>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			unsafe extern internal static int GetRawInputBuffer(
				[Out] RawInput* data,
				[In, Out] ref int size,
				[In] int headerSize
			);


			/// <summary>Calls the default raw input procedure to provide default processing for any raw input messages that an application does not process.
			/// This function ensures that every message is processed.
			/// <para>DefRawInputProc is called with the same parameters received by the window procedure.</para>
			/// For use with <see cref="GetRawInputBuffer"/>.
			/// </summary>
			/// <param name="rawInputs">An array of <see cref="RawInput"/> structures.</param>
			/// <param name="rawInputCount">The number of <see cref="RawInput"/> structures pointed to by <paramref name="rawInputs"/>.</param>
			/// <param name="headerSize">The size, in bytes, of the <see cref="RawInputHeader"/> structure.</param>
			/// <returns>If successful, the function returns S_OK. Otherwise it returns an error value.</returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645594(v=vs.85).aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal extern static int DefRawInputProc(
				[In, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 )] RawInput[] rawInputs,
				[In] int rawInputCount,
				[In] int headerSize
			);


			/// <summary>Registers the device or type of device for which a window will receive notifications.</summary>
			/// <param name="handle">A handle to the window or service that will receive device events for the devices specified in the NotificationFilter parameter.
			/// The same window handle can be used in multiple calls to RegisterDeviceNotification.
			/// <para>Services can specify either a window handle or service status handle.</para>
			/// </param>
			/// <param name="notificationFilter">A pointer to a block of data that specifies the type of device for which notifications should be sent. This block always begins with the DEV_BROADCAST_HDR structure. The data following this header is dependent on the value of the dbch_devicetype member, which can be DBT_DEVTYP_DEVICEINTERFACE or DBT_DEVTYP_HANDLE.</param>
			/// <param name="options"></param>
			/// <returns>
			/// If the function succeeds, the return value is a device notification handle.
			/// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363431%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static IntPtr RegisterDeviceNotificationW(
				[In] IntPtr handle,
				[In] DeviceBroadcastHeader notificationFilter,
				[In] RegisterDeviceNotificationOptions options
			);


			/// <summary>Closes the specified device notification handle.</summary>
			/// <param name="deviceNotificationHandle">Device notification handle returned by the <see cref="RegisterDeviceNotificationW"/> function.</param>
			/// <returns>
			/// If the function succeeds, the return value is nonzero.
			/// If the function fails, the return value is zero. To get extended error information, call GetLastError.
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363475%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			extern internal static bool UnregisterDeviceNotification(
				[In] IntPtr deviceNotificationHandle
			);


			///// <summary>Retrieves the active input locale identifier (formerly called the keyboard layout).</summary>
			///// <param name="threadId">The identifier of the thread to query, or 0 for the current thread.</param>
			///// <returns>The return value is the input locale identifier for the thread. The low word contains a Language Identifier for the input language and the high word contains a device handle to the physical layout of the keyboard.</returns>
			///// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646296(v=vs.85).aspx</remarks>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			//extern internal static IntPtr GetKeyboardLayout(
			//	[In] int threadId
			//);


			///// <summary>Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code. The function translates the codes using the input language and an input locale identifier.</summary>
			///// <param name="code">The <see cref="VirtualKeyCode"/> or scan code for a key. How this value is interpreted depends on the value of the <paramref name="mapType"/> parameter.
			///// <para>Starting with Windows Vista, the high byte of the <paramref name="code"/> value can contain either 0xE0 or 0xE1 to specify the extended scan code.</para>
			///// </param>
			///// <param name="mapType">The translation to perform. The value of this parameter depends on the value of the <paramref name="code"/> parameter.</param>
			///// <param name="hkl">Input locale identifier to use for translating the specified code. This parameter can be any input locale identifier previously returned by the LoadKeyboardLayout function.</param>
			///// <returns>The return value is either a scan code, a <see cref="VirtualKeyCode"/> code, or a character value, depending on the value of <paramref name="code"/> and <paramref name="mapType"/>. If there is no translation, the return value is zero.</returns>
			///// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646307(v=vs.85).aspx</remarks>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//extern internal static int MapVirtualKeyExW(
			//	[In] int code,
			//	[In] MapVirtualKeyMapType mapType,
			//	[In, Out, Optional] IntPtr hkl
			//);


			///// <summary>Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or characters.</summary>
			///// <param name="virtKey">The virtual-key code to be translated.</param>
			///// <param name="scanCode">The hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up.</param>
			///// <param name="keyState">A 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down.</param>
			///// <param name="buffer">The buffer that receives the translated Unicode character or characters. However, this buffer may be returned without being null-terminated even though the variable name suggests that it is null-terminated.</param>
			///// <param name="bufferSize">The size, in characters, of the buffer pointed to by the <paramref name="buffer"/> parameter.</param>
			///// <param name="behaviorCharacteristics">The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are reserved.</param>
			///// <param name="hkl">The input locale identifier used to translate the specified code. This parameter can be any input locale identifier previously returned by the LoadKeyboardLayout function.</param>
			///// <returns></returns>
			///// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms646322(v=vs.85).aspx</remarks>
			//[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			//extern internal static int ToUnicodeEx(
			//	[In] VirtualKeyCode virtKey,
			//	[In] int scanCode,
			//	[In, MarshalAs( UnmanagedType.LPArray, SizeConst = 256 )] byte[] keyState,
			//	[Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 4 )] string buffer,
			//	[In] int bufferSize,
			//	[In] int behaviorCharacteristics,
			//	[In, Optional] IntPtr hkl
			//);


			/// <summary>Returns the embedded string of a top-level collection that identifies the manufacturer's product.</summary>
			/// <param name="hidDeviceObject">Specifies an open handle to a top-level collection.</param>
			/// <param name="buffer">Pointer to a caller-allocated buffer that the routine uses to return the requested product string. The routine returns a NULL-terminated wide character string.</param>
			/// <param name="bufferLength">Specifies the length, in bytes, of a caller-allocated buffer provided at Buffer. If the buffer is not large enough to return the entire NULL-terminated embedded string, the routine returns nothing in the buffer.</param>
			/// <returns>Returns true if the function successfully returns the entire NULL-terminated embedded string, otherwise returns false.</returns>
			[DllImport( "HID.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			[return: MarshalAs( UnmanagedType.Bool )]
			extern internal static bool HidD_GetProductString(
				[In] SafeFileHandle hidDeviceObject,
				[Out, MarshalAs( UnmanagedType.LPWStr )] string buffer,
				[In] int bufferLength
			);
			// HidSdi.h
			//BOOLEAN __stdcall HidD_GetProductString(
			//  _In_  HANDLE HidDeviceObject,
			//  _Out_ PVOID  Buffer,
			//  _In_  ULONG  BufferLength
			//);


			/// <summary></summary>
			/// <param name="fileName"></param>
			/// <param name="desiredAccess"></param>
			/// <param name="shareMode"></param>
			/// <param name="securityAttributes"></param>
			/// <param name="creationDisposition"></param>
			/// <param name="flagsAndAttributes"></param>
			/// <param name="templateFile"></param>
			/// <returns></returns>
			[DllImport( "Kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static SafeFileHandle CreateFileW(
				[In] string fileName,
				[In] FileAccess desiredAccess,
				[In] FileShare shareMode,
				[In, Optional] IntPtr securityAttributes,
				[In] FileMode creationDisposition,
				[In] FileAttributes flagsAndAttributes,
				[In, Optional] IntPtr templateFile
			);
			//HANDLE WINAPI CreateFile(
			//  _In_     LPCTSTR               lpFileName,
			//  _In_     DWORD                 dwDesiredAccess,
			//  _In_     DWORD                 dwShareMode,
			//  _In_opt_ LPSECURITY_ATTRIBUTES lpSecurityAttributes,
			//  _In_     DWORD                 dwCreationDisposition,
			//  _In_     DWORD                 dwFlagsAndAttributes,
			//  _In_opt_ HANDLE                hTemplateFile
			//);

		}



		private static readonly List<Mouse> mice = new List<Mouse>( 1 );                                        // Most systems have only one mouse
		private static readonly List<Keyboard> keyboards = new List<Keyboard>( 1 );                             // and one keyboard,
		private static readonly List<HumanInterfaceDevice> otherDevices = new List<HumanInterfaceDevice>( 2 );	// but for some reason the mouse and keyboard have a corresponding HID.
		private static bool isInitialized;
		private static readonly List<InputDevice> updateList = new List<InputDevice>();
		private static bool rawInputEnabled = false;



		private static void Initialize()
		{
			var descriptors = GetRawInputDeviceList();
			for( var d = 0; d < descriptors.Length; ++d )
			{
				var descriptor = descriptors[ d ];
				if( descriptors[ d ].DeviceType == InputDeviceType.Mouse )
				{
					var m = new Mouse( ref descriptors[ d ] );
					mice.Add( m );
					m.IsDisabledChanged += OnDeviceDisabledChanged;
					updateList.Add( m );
				}
				else if( descriptors[ d ].DeviceType == InputDeviceType.Keyboard )
				{
					var k = new Keyboard( ref descriptors[ d ] );
					keyboards.Add( k );
					k.IsDisabledChanged += OnDeviceDisabledChanged;
					updateList.Add( k );
				}
				else if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				{
					var h = new HumanInterfaceDevice( ref descriptor );
					otherDevices.Add( h );
					h.IsDisabledChanged += OnDeviceDisabledChanged;
					updateList.Add( h );
				}
#if DEBUG
				else
					throw new NotSupportedException( "Unsupported raw input device type." );
#endif
			}

			isInitialized = true;
		}


		private static void OnDeviceDisabledChanged( object sender, EventArgs e )
		{
			var device = (InputDevice)sender;
			if( device.IsDisabled )
				updateList.Remove( device );
			else //if( !updateList.Contains( device ) )
				updateList.Add( device );
		}



		/// <summary>Returns an array of raw input devices attached to the system.</summary>
		/// <returns>Returns an array of raw input devices attached to the system.</returns>
		/// <exception cref="InvalidDataException"/>
		/// <exception cref="Win32Exception"/>
		internal static RawInputDeviceDescriptor[] GetRawInputDeviceList()
		{
			int errorCode;
			var size = Marshal.SizeOf<RawInputDeviceDescriptor>();

			var deviceCount = 0;

			var result = NativeMethods.GetRawInputDeviceList( null, ref deviceCount, size );
			if( result == -1 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve raw input device count.", InputDevice.GetException( errorCode ) );
			}

			if( deviceCount < 0 )
				throw new InvalidDataException( "Invalid raw input device count: " + deviceCount.ToString( System.Globalization.CultureInfo.InvariantCulture ) );

			var list = new RawInputDeviceDescriptor[ deviceCount ];
			result = NativeMethods.GetRawInputDeviceList( list, ref deviceCount, size );
			if( result == -1 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve raw input device list.", InputDevice.GetException( errorCode ) );
			}

			if( result == list.Length )
				return list;

			throw new InvalidDataException( "Failed to retrieve raw input device count: device count mismatch." );
		}


		/// <summary>Registers the devices that supply the raw input data.</summary>
		/// <param name="rawInputDevices">An array of <see cref="RawInputDevice"/> structures representing the devices which supply the raw input; must not be null nor empty.</param>
		/// <remarks>
		/// To receive <see cref="WindowMessage.Input"/> messages, an application must first register the raw input devices using RegisterRawInputDevices. By default, an application does not receive raw input.
		/// <para>To receive <see cref="WindowMessage.InputDeviceChange"/> messages, an application must specify the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> flag for each device class that is specified by the UsagePage and Usage fields of the <see cref="RawInputDevice"/> structure .
		/// By default, an application does not receive <see cref="WindowMessage.InputDeviceChange"/> notifications for raw input device arrival and removal.</para>
		/// <para>If a <see cref="RawInputDevice"/> structure has the <see cref="RawInputDeviceRegistrationOptions.Remove"/> flag set and the <see cref="RawInputDevice.TargetWindowHandle"/> parameter is not set to null, then parameter validation will fail.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		/// <exception cref="Win32Exception"/>
		internal static void RegisterRawInputDevices( params RawInputDevice[] rawInputDevices )
		{
			if( rawInputDevices == null )
				throw new ArgumentNullException( "rawInputDevices" );

			var deviceCount = rawInputDevices.Length;
			if( deviceCount == 0 )
				throw new ArgumentException( "Array is empty.", "rawInputDevices" );

			RawInputDevice rawInputDevice;
			for( var d = 0; d < deviceCount; d++ )
			{
				rawInputDevice = rawInputDevices[ d ];
				if( rawInputDevice.RegistrationOptions.HasFlag( RawInputDeviceRegistrationOptions.Remove ) && ( rawInputDevice.TargetWindowHandle != IntPtr.Zero ) )
					throw new ArgumentException( "Target window handle must be null when Remove flag is set." );
			}

			if( !NativeMethods.RegisterRawInputDevices( rawInputDevices, deviceCount, Marshal.SizeOf<RawInputDevice>() ) )
			{
				var errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to register raw input device(s).", InputDevice.GetException( errorCode ) );
			}
		}


		/// <summary>Returns the raw input from the specified device.</summary>
		/// <param name="rawInputHandle">A handle to the <see cref="RawInput"/> structure. This comes from the LParam in WM_INPUT.</param>
		/// <param name="rawInput"></param>
		/// <exception cref="Win32Exception"/>
		/// <exception cref="InvalidDataException"/>
		unsafe internal static void GetRawInputData( IntPtr rawInputHandle, out RawInput rawInput )
		{
			var headerSize = Marshal.SizeOf<RawInputHeader>();
			var inputSize = Marshal.SizeOf<RawInput>();

			fixed ( void* data = &rawInput )
			{
				var count = NativeMethods.GetRawInputData( rawInputHandle, GetDataCommand.Input, data, ref inputSize, headerSize );

				if( count == -1 )
				{
					var errorCode = Marshal.GetLastWin32Error();
					throw new Win32Exception( "Failed to retrieve raw input data.", InputDevice.GetException( errorCode ) );
				}
			}
		}


		/// <summary>Returns a <see cref="DeviceInfo"/> structure containing information about a raw input device.</summary>
		/// <param name="deviceHandle">A handle to the raw input device.
		/// <para>This comes from the <see cref="Message.LParam"/> of the <see cref="WindowMessage.Input"/> message, from the <see cref="RawInputHeader.DeviceHandle"/> member, or from <see cref="GetRawInputDeviceList"/>.</para>
		/// It can also be null if an application inserts input data, for example, by using SendInput.
		/// </param>
		/// <param name="preParsed">Set to true to use pre-parsed data, false otherwise; defaults to false.</param>
		/// <returns>Returns a <see cref="DeviceInfo"/> structure containing information about a raw input device.</returns>
		/// <exception cref="NotSupportedException"/>
		/// <exception cref="Win32Exception"/>
		internal static DeviceInfo GetRawInputDeviceInfo( IntPtr deviceHandle, bool preParsed = false )
		{
			var deviceInfo = DeviceInfo.Default;
			var deviceInfoStructSize = deviceInfo.StructSize;

			var result = NativeMethods.GetRawInputDeviceInfoW( deviceHandle, preParsed ? GetInfoCommand.PreParsedData : GetInfoCommand.DeviceInfo, ref deviceInfo, ref deviceInfoStructSize );
			if( result != deviceInfoStructSize )
			{
				var errorCode = Marshal.GetLastWin32Error();
				if( result == -1 )
					throw new NotSupportedException( "Failed to retrieve raw input device info: DeviceInfo size mismatch." );
				else if( result == 0 )
					throw new NotSupportedException( "Failed to retrieve raw input device info: bad implementation." );
				else
					throw new Win32Exception( "Failed to retrieve raw input device info.", InputDevice.GetException( errorCode ) );
			}
			return deviceInfo;
		}


		/// <summary>Returns the device name (or device path) of a raw input device.</summary>
		/// <param name="deviceHandle">A handle to the raw input device. This comes from the <see cref="Message.LParam"/> of the <see cref="WindowMessage.Input"/> message, from <see cref="RawInputHeader.DeviceHandle"/>, or from <see cref="GetRawInputDeviceList"/>.
		/// <para>It can also be <see cref="IntPtr.Zero"/> if an application inserts input data, for example, by using SendInput.</para>
		/// </param>
		/// <returns>Returns the name of a raw input device.</returns>
		/// <exception cref="NotSupportedException"/>
		/// <exception cref="Win32Exception"/>
		internal static string GetRawInputDeviceName( IntPtr deviceHandle )
		{
			int errorCode;
			var charCount = 0;

			var result = NativeMethods.GetRawInputDeviceInfoW( deviceHandle, GetInfoCommand.DeviceName, null, ref charCount );
			if( result != 0 )
			{
				errorCode = Marshal.GetLastWin32Error();
				if( result > 0 )
					throw new NotSupportedException( "Failed to retrieve raw input device name buffer size: bad implementation." );
				throw new Win32Exception( "Failed to retrieve raw input device name buffer size.", InputDevice.GetException( errorCode ) );
			}

			if( charCount <= 0 )
				throw new NotSupportedException( "Invalid raw input device name character count." );

			var buffer = new string( '\0', charCount );
			result = NativeMethods.GetRawInputDeviceInfoW( deviceHandle, GetInfoCommand.DeviceName, buffer, ref charCount );
			if( result == -1 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve raw input device name.", InputDevice.GetException( errorCode ) );
			}
			if( result == 0 )
				throw new NotSupportedException( "Failed to retrieve raw input device name: bad implementation." );

			if( result == charCount )
				return buffer;

			throw new NotSupportedException( "Failed to retrieve raw input device name: string length mismatch (bad implementation)." );
		}


		/// <summary>Returns the embedded string of a top-level collection that identifies the manufacturer's product, or null.</summary>
		/// <param name="deviceName">The device name (or device path) of a RawInput device.</param>
		/// <returns>Returns the embedded string of a top-level collection that identifies the manufacturer's product, or null.</returns>
		internal static string GetHIDProductString( string deviceName )
		{
			const int CharCount = 256;              // must be at least 128 chars (126 + 2)
			const int ByteCount = CharCount * 2;    // 2 bytes per (unicode) char

			using( var hidHandle = NativeMethods.CreateFileW( deviceName, FileAccess.Read, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero ) )
			{
				var output = new string( '\0', CharCount );
				if( NativeMethods.HidD_GetProductString( hidHandle, output, ByteCount ) )
					return output.TrimEnd( '\0' );
			}

			return null;
		}


		///// <summary></summary>
		///// <param name="windowHandle"></param>
		///// <param name="header"></param>
		///// <param name="allInterfaceClasses"></param>
		///// <returns></returns>
		//public static IntPtr RegisterWindowDeviceNotification( IntPtr windowHandle, DeviceBroadcastHeader header, bool allInterfaceClasses )
		//{
		//	var options = RegisterDeviceNotificationOptions.WindowHandle;
		//	if( allInterfaceClasses )
		//		options |= RegisterDeviceNotificationOptions.AllInterfaceClasses;

		//	return NativeMethods.RegisterDeviceNotificationW( windowHandle, header, options );
		//}


		///// <summary></summary>
		///// <param name="serviceHandle"></param>
		///// <param name="header"></param>
		///// <param name="allInterfaceClasses"></param>
		///// <returns></returns>
		//public static IntPtr RegisterServiceDeviceNotification( IntPtr serviceHandle, DeviceBroadcastHeader header, bool allInterfaceClasses )
		//{
		//	var options = RegisterDeviceNotificationOptions.ServiceHandle;
		//	if( allInterfaceClasses )
		//		options |= RegisterDeviceNotificationOptions.AllInterfaceClasses;

		//	return NativeMethods.RegisterDeviceNotificationW( serviceHandle, header, options );
		//}


		///// <summary>Performs a buffered read of the raw input data.</summary>
		///// <returns></returns>
		///// <exception cref="Win32Exception"/>
		//unsafe internal static RawInput[] GetRawInputBuffer()
		//{
		//	int errorCode;
		//	var headerSize = Marshal.SizeOf<RawInputHeader>();
		//	var size = 0;

		//	var result = NativeMethods.GetRawInputBuffer( null, ref size, headerSize );
		//	if( result != 0 )
		//	{
		//		errorCode = Marshal.GetLastWin32Error();
		//		throw new Win32Exception( "Failed to retrieve raw input buffer size.", InputDevice.GetException( errorCode ) );
		//	}

		//	if( size < 0 )
		//		throw new InvalidDataException( "Invalid RawInput buffer size." );

		//	var inputSize = Marshal.SizeOf<RawInput>();
		//	var count = size / inputSize;
		//	var buffer = new RawInput[ count ];

		//	if( count > 0 )
		//	{
		//		fixed ( RawInput* ptr = &buffer[ 0 ] )
		//			result = NativeMethods.GetRawInputBuffer( ptr, ref inputSize, headerSize );
		//		if( result < 0 )
		//		{
		//			errorCode = Marshal.GetLastWin32Error();
		//			throw new Win32Exception( "Failed to retrieve raw input buffer.", InputDevice.GetException( errorCode ) );
		//		}

		//		if( result != count )
		//			throw new InvalidDataException( "RawInput buffer size mismatch." );
		//	}

		//	return buffer;
		//}


		///// <summary>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application.</summary>
		///// <returns>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application; the array can be empty, but not null.</returns>
		///// <exception cref="InvalidDataException"/>
		///// <exception cref="Win32Exception"/>
		//internal static RawInputDevice[] GetRegisteredRawInputDevices()
		//{
		//	int errorCode;
		//	var structSize = Marshal.SizeOf<RawInputDevice>();
		//	var deviceCount = 0;

		//	var result = NativeMethods.GetRegisteredRawInputDevices( null, ref deviceCount, structSize );
		//	if( result == -1 )
		//	{
		//		errorCode = Marshal.GetLastWin32Error();
		//		throw new Win32Exception( "Failed to retrieve registered raw input device count.", InputDevice.GetException( errorCode ) );
		//	}
		//	if( result != 0 )
		//	{
		//		throw new InvalidDataException( "Failed to retrieve registered raw input device count." );
		//	}

		//	if( deviceCount < 0 )
		//		throw new InvalidDataException( "Invalid registered raw input device count." );

		//	var rawInputDevices = new RawInputDevice[ deviceCount ];
		//	result = NativeMethods.GetRegisteredRawInputDevices( rawInputDevices, ref deviceCount, structSize );
		//	if( result == -1 )
		//	{
		//		errorCode = Marshal.GetLastWin32Error();
		//		throw new Win32Exception( "Failed to retrieve registered raw input devices.", InputDevice.GetException( errorCode ) );
		//	}

		//	return rawInputDevices;
		//}


		#region Mouse

		/// <summary>Gets a read-only collection of all connected mouse devices.</summary>
		public static ReadOnlyCollection<Mouse> Mice
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Mouse>( mice );
			}
		}


		/// <summary>Returns a mouse given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested mouse.</param>
		/// <returns>Returns the requested mouse, or null.</returns>
		public static Mouse GetMouseByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; ++m )
				if( mice[ m ].DeviceHandle == deviceHandle )
					return mice[ m ];

			return null;
		}


		/// <summary>Returns a mouse given its device name.</summary>
		/// <param name="deviceName">The device name of the requested mouse.</param>
		/// <returns>Returns the requested mouse, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static Mouse GetMouseByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var m = 0; m < mice.Count; ++m )
				if( mice[ m ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return mice[ m ];

			return null;
		}


		/// <summary>Returns a mouse given its id.</summary>
		/// <param name="id">The id of the requested mouse.</param>
		/// <returns>Returns the mouse with the corresponding id, or null if no mouse matches.</returns>
		public static Mouse GetMouseById( int id )
		{
			for( var m = 0; m < mice.Count; ++m )
			{
				if( mice[ m ].Description.Id == id )
					return mice[ m ];
			}
			return null;
		}


		/// <summary>Raised when a mouse is connected to the system.
		/// <para>Requires mice (see <see cref="TopLevelCollectionUsage.Mouse"/>) to be registered with the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> option.</para>
		/// </summary>
		public static event EventHandler<MouseConnectedEventArgs> MouseConnected;

		#endregion Mouse


		#region Keyboard

		/// <summary>Gets a read-only collection of all connected keyboards.</summary>
		public static ReadOnlyCollection<Keyboard> Keyboards
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Keyboard>( keyboards );
			}
		}


		/// <summary>Returns a keyboard given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested keyboard.</param>
		/// <returns>Returns the requested keyboard, or null.</returns>
		public static Keyboard GetKeyboardByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; ++k )
				if( keyboards[ k ].DeviceHandle == deviceHandle )
					return keyboards[ k ];

			return null;
		}


		/// <summary>Returns a keyboard given its device name.</summary>
		/// <param name="deviceName">The device name of the requested keyboard.</param>
		/// <returns>Returns the requested keyboard, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static Keyboard GetKeyboardByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var k = 0; k < keyboards.Count; ++k )
				if( keyboards[ k ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return keyboards[ k ];

			return null;
		}


		/// <summary>Raised when a keyboard is connected to the system.
		/// <para>Requires keyboards (see <see cref="TopLevelCollectionUsage.Keyboard"/>) to be registered with the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> option.</para>
		/// </summary>
		public static event EventHandler<KeyboardConnectedEventArgs> KeyboardConnected;

		#endregion Keyboard


		#region HID

		/// <summary>Gets a read-only collection containing all other human-interface devices (HID).</summary>
		public static ReadOnlyCollection<HumanInterfaceDevice> HumanInterfaceDevices
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<HumanInterfaceDevice>( otherDevices );
			}
		}


		/// <summary>Returns a HID given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		public static HumanInterfaceDevice GetHumanInterfaceDeviceByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; ++d )
				if( otherDevices[ d ].DeviceHandle == deviceHandle )
					return otherDevices[ d ];

			return null;
		}


		/// <summary>Returns a HID given its device name.</summary>
		/// <param name="deviceName">The device name of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static HumanInterfaceDevice GetHumanInterfaceDeviceByDeviceName( string deviceName )
		{
			if( string.IsNullOrWhiteSpace( deviceName ) )
			{
				if( deviceName == null )
					throw new ArgumentNullException( "deviceName" );
				throw new ArgumentException( "Invalid device name.", "deviceName" );
			}

			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; ++d )
				if( otherDevices[ d ].DeviceName.Equals( deviceName, StringComparison.Ordinal ) )
					return otherDevices[ d ];

			return null;
		}


		/// <summary>Raised when a <see cref="HumanInterfaceDevice"/> is connected to the system.
		/// <para>Requires HIDs to be registered with the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> option.</para>
		/// </summary>
		public static event EventHandler<HumanInterfaceDeviceConnectedEventArgs> HumanInterfaceDeviceConnected;

		#endregion HID


		/// <summary>Returns an input device given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested input device.</param>
		/// <returns>Returns the requested input device, or null.</returns>
		public static InputDevice GetDeviceByHandle( IntPtr deviceHandle )
		{
			var mouse = GetMouseByDeviceHandle( deviceHandle );
			if( mouse != null )
				return mouse;

			var keyboard = GetKeyboardByDeviceHandle( deviceHandle );
			if( keyboard != null )
				return keyboard;

			var hid = GetHumanInterfaceDeviceByDeviceHandle( deviceHandle );
			if( hid != null )
				return hid;

			return null;
		}


		/// <summary>Returns an input device given its device name.</summary>
		/// <param name="deviceName">The name of the requested input device.</param>
		/// <returns>Returns the requested input device, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		public static InputDevice GetDeviceByName( string deviceName )
		{
			try
			{
				var mouse = GetMouseByDeviceName( deviceName );
				if( mouse != null )
					return mouse;

				var keyboard = GetKeyboardByDeviceName( deviceName );
				if( keyboard != null )
					return keyboard;

				var hid = GetHumanInterfaceDeviceByDeviceName( deviceName );
				if( hid != null )
					return hid;
			}
			catch( ArgumentException )
			{
				throw;
			}

			return null;
		}


		/// <summary>Gets a read-only collection containing all input devices.</summary>
		public static ReadOnlyCollection<InputDevice> Devices
		{
			get
			{
				if( !isInitialized )
					Initialize();

				var list = new List<InputDevice>( mice.Count + keyboards.Count + otherDevices.Count + gameControllers.Count );
				list.AddRange( mice );
				list.AddRange( keyboards );
				list.AddRange( otherDevices );
				list.AddRange( gameControllers );
				return new ReadOnlyCollection<InputDevice>( list );
			}
		}


		/// <summary>Processes input window messages (<see cref="WindowMessage.Input"/> and <see cref="WindowMessage.InputDeviceChange"/>).</summary>
		/// <param name="message">A window message.</param>
		/// <returns>Returns true if the message has been processed, false otherwise.</returns>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		public static bool PreFilterMessage( [In] ref Message message )
		{
			if( !rawInputEnabled )
				return false;

			if( message.Msg == (int)WindowMessage.Input )
			{
				GetRawInputData( message.LParam, out RawInput input );

				if( input.Header.DeviceType == InputDeviceType.Mouse )
				{
					var mouse = GetMouseByDeviceHandle( input.Header.DeviceHandle );
					if( mouse == null )
					{
						// TODO - re-enumerate devices ?
					}
					else if( !mouse.IsDisabled )
						mouse.Update( ref input );
				}
				else if( input.Header.DeviceType == InputDeviceType.Keyboard )
				{
					var keyboard = GetKeyboardByDeviceHandle( input.Header.DeviceHandle );
					if( keyboard == null )
					{
						// TODO - re-enumerate devices ?
					}
					else if( !keyboard.IsDisabled )
						keyboard.Update( ref input );
				}
				//else if( input.Header.DeviceType == InputDeviceType.HumanInterfaceDevice )
				//{
				//	var targetHid = GetHIDByDeviceHandle( input.Header.DeviceHandle );
				//	if( targetHid == null )
				//	{
				//		// TODO - re-enumerate devices ?
				//	}
				//	else if( !targetHid.IsDisabled )
				//		targetHid.Update( ref input );
				//}

				//NativeMethods.DefRawInputProc( new RawInput[] { input }, 1, Marshal.SizeOf<RawInputHeader>() );

				return true;
			}


			if( message.Msg == (int)WindowMessage.InputDeviceChange )
			{
				var wParam = message.WParam.ToInt32();
				if( wParam == 1 )       // device arrival
				{
					var deviceList = GetRawInputDeviceList();
					for( var d = 0; d < deviceList.Length; ++d )
					{
						var dev = deviceList[ d ];
						if( dev.DeviceType == InputDeviceType.Mouse )
						{
							if( GetMouseByDeviceHandle( dev.DeviceHandle ) == null )
							{
								var mouse = new Mouse( ref dev )
								{
									IsDisabled = mice.Count > 0
								};
								mice.Add( mouse );
								if( !mouse.IsDisabled )
									updateList.Add( mouse );
								MouseConnected?.Invoke( null, new MouseConnectedEventArgs( mouse ) );
								break;
							}
						}
						else if( dev.DeviceType == InputDeviceType.Keyboard )
						{
							if( GetKeyboardByDeviceHandle( dev.DeviceHandle ) == null )
							{
								var keyboard = new Keyboard( ref dev )
								{
									IsDisabled = keyboards.Count > 0
								};
								keyboards.Add( keyboard );
								if( !keyboard.IsDisabled )
									updateList.Add( keyboard );
								KeyboardConnected?.Invoke( null, new KeyboardConnectedEventArgs( keyboard ) );
								break;
							}
						}
						else if( dev.DeviceType == InputDeviceType.HumanInterfaceDevice )
						{
							if( GetHumanInterfaceDeviceByDeviceHandle( dev.DeviceHandle ) == null )
							{
								var hid = new HumanInterfaceDevice( ref dev )
								{
									IsDisabled = true
								};
								otherDevices.Add( hid );
								if( !hid.IsDisabled )
									updateList.Add( hid );
								HumanInterfaceDeviceConnected?.Invoke( null, new HumanInterfaceDeviceConnectedEventArgs( hid ) );
								break;
							}
						}
					}
				}
				else if( wParam == 2 )  // device removal
				{
					var device = GetDeviceByHandle( message.LParam );
					if( device != null )
					{
						if( !device.IsDisabled )
							updateList.Remove( device );

						if( device.DeviceType == InputDeviceType.Mouse )
							mice.Remove( (Mouse)device );
						else if( device.DeviceType == InputDeviceType.Keyboard )
							keyboards.Remove( (Keyboard)device );
						else if( device.DeviceType == InputDeviceType.HumanInterfaceDevice )
							otherDevices.Remove( (HumanInterfaceDevice)device );

						device.IsDisconnected = true;
					}
				}

				return true;
			}

			return false;
		}


		/// <summary>Causes the target window to receive raw input messages.
		/// <para>Important: that window must then override its WndProc method to call <see cref="PreFilterMessage"/> prior to its base method.</para>
		/// </summary>
		/// <param name="targetWindowHandle">The handle of the target window.</param>
		/// <param name="options">One or more <see cref="RawInputDeviceRegistrationOptions"/>.</param>
		/// <param name="usage">Top-level collection usage.</param>
		/// <param name="usages">Optional: other top-level collection usages.</param>
		public static void Register( IntPtr targetWindowHandle, RawInputDeviceRegistrationOptions options, TopLevelCollectionUsage usage, params TopLevelCollectionUsage[] usages )
		{
			if( usages == null )
				usages = new TopLevelCollectionUsage[ 0 ];

			var devices = new RawInputDevice[ usages.Length + 1 ];
			devices[ 0 ] = new RawInputDevice( usage, options, targetWindowHandle );
			for( var d = 0; d < usages.Length; ++d )
				devices[ d + 1 ] = new RawInputDevice( usages[ d ], options, targetWindowHandle );

			RegisterRawInputDevices( devices );

			rawInputEnabled = true;
		}


		/// <summary>Causes the target window to receive raw input messages.
		/// <para>Important: that window must then override its WndProc method to call <see cref="PreFilterMessage"/> prior to its base method.
		/// The base method doesn't need to be called if <see cref="PreFilterMessage"/> returns true.
		/// </para>
		/// </summary>
		/// <param name="targetWindow">The target window; must not be null.</param>
		/// <param name="options">One or more <see cref="RawInputDeviceRegistrationOptions"/>.</param>
		/// <param name="usage">Top-level collection usage.</param>
		/// <param name="usages">Optional: other top-level collection usages.</param>
		/// <exception cref="ArgumentNullException"/>
		public static void Register( IWin32Window targetWindow, RawInputDeviceRegistrationOptions options, TopLevelCollectionUsage usage, params TopLevelCollectionUsage[] usages )
		{
			if( targetWindow == null )
				throw new ArgumentNullException( "targetWindow" );

			Register( targetWindow.Handle, options, usage, usages );
		}

		#endregion RawInput


		/// <summary>Updates the state of all enabled input devices.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		public static void Update( TimeSpan time )
		{
			if( xInputEnabled )
			{
				for( var c = 0; c < gameControllers.Count; ++c )
				{
					if( !gameControllers[ c ].IsDisabled )
						gameControllers[ c ].Update( time );
				}
			}

			if( rawInputEnabled )
			{
				for( var d = 0; d < updateList.Count; ++d )
				{
					var device = updateList[ d ];
					if( !device.IsDisabled && !device.IsDisconnected )
						device.Update( time );
				}
			}
		}

	}

}