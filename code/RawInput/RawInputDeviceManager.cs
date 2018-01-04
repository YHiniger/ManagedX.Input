using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;


namespace ManagedX.Input.Raw
{

	/// <summary>The RawInput device manager.</summary>
	public static class RawInputDeviceManager
	{

		[System.Security.SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{

			private const string LibraryName = "User32.dll";
			// WinUser.h


			/// <summary>Enumerates the raw input devices attached to the system.</summary>
			/// <param name="rawInputDeviceList">An array of <see cref="RawInputDeviceDescriptor"/> structures for the devices attached to the system. If null, the number of devices is returned in <paramref name="deviceCount"/>.</param>
			/// <param name="deviceCount">If <paramref name="rawInputDeviceList"/> is null, receives the number of devices attached to the system; otherwise, specifies the number of RAWINPUTDEVICELIST structures that can be contained in the buffer <paramref name="rawInputDeviceList"/> points to.
			/// <para>If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with ERROR_INSUFFICIENT_BUFFER.</para>
			/// </param>
			/// <param name="size">The size, in bytes, of a <see cref="RawInputDeviceDescriptor"/> structure.</param>
			/// <returns>If the function is successful, the return value is the number of devices stored in the buffer pointed to by <paramref name="rawInputDeviceList"/>.
			/// <para>On any other error, the function returns (UINT) -1 and GetLastError returns the error indication.</para>
			/// </returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645598%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
			extern internal static int GetRawInputDeviceList(
				[Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 ), Optional] RawInputDeviceDescriptor[] rawInputDeviceList,
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


			/// <summary></summary>
			/// <param name="windowHandle"></param>
			/// <param name="notificationFilter"></param>
			/// <param name="flags"></param>
			/// <returns></returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363431%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			extern internal static IntPtr RegisterDeviceNotificationW(
				[In] IntPtr windowHandle,
				[In] object notificationFilter, // FIXME - DeviceNotificationBase
				[In] int flags
			);


			/// <summary></summary>
			/// <param name="deviceNotificationHandle"></param>
			/// <returns></returns>
			/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363475%28v=vs.85%29.aspx</remarks>
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			[return: MarshalAs( UnmanagedType.Bool )]
			extern internal static bool UnregisterDeviceNotification(
				[In] IntPtr deviceNotificationHandle
			);



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
		/// To receive WM_INPUT messages, an application must first register the raw input devices using RegisterRawInputDevices. By default, an application does not receive raw input.
		/// <para>To receive WM_INPUT_DEVICE_CHANGE messages, an application must specify the <see cref="RawInputDeviceRegistrationOptions.DevNotify"/> flag for each device class that is specified by the UsagePage and Usage fields of the <see cref="RawInputDevice"/> structure .
		/// By default, an application does not receive WM_INPUT_DEVICE_CHANGE notifications for raw input device arrival and removal.</para>
		/// <para>If a <see cref="RawInputDevice"/> structure has the <see cref="RawInputDeviceRegistrationOptions.Remove"/> flag set and the <see cref="RawInputDevice.TargetWindowHandle">TargetWindowHandle</see> parameter is not set to null, then parameter validation will fail.</para>
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

				if( count != inputSize )
					throw new InvalidOperationException( "Bad implementation of GetRawInputData." );
			}
		}


		/// <summary>Performs a buffered read of the raw input data.</summary>
		/// <returns></returns>
		/// <exception cref="Win32Exception"/>
		unsafe internal static RawInput[] GetRawInputBuffer()
		{
			int errorCode;
			var headerSize = Marshal.SizeOf<RawInputHeader>();
			var size = 0;

			var result = NativeMethods.GetRawInputBuffer( null, ref size, headerSize );
			if( result != 0 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve raw input buffer size.", InputDevice.GetException( errorCode ) );
			}

			if( size < 0 )
				throw new InvalidDataException( "Invalid RawInput buffer size." );

			var inputSize = Marshal.SizeOf<RawInput>();
			var count = size / inputSize;
			var buffer = new RawInput[ count ];

			if( count > 0 )
			{
				fixed( RawInput* ptr = &buffer[ 0 ] )
					result = NativeMethods.GetRawInputBuffer( ptr, ref inputSize, headerSize );
				if( result < 0 )
				{
					errorCode = Marshal.GetLastWin32Error();
					throw new Win32Exception( "Failed to retrieve raw input buffer.", InputDevice.GetException( errorCode ) );
				}

				if( result != count )
					throw new InvalidDataException( "RawInput buffer size mismatch." );
			}

			return buffer;
		}


		/// <summary>Returns a <see cref="DeviceInfo"/> structure containing information about a raw input device.</summary>
		/// <param name="deviceHandle">A handle to the raw input device.
		/// <para>This comes from the LParam of the WM_INPUT message, from the <see cref="RawInputHeader.DeviceHandle"/> member, or from GetRawInputDeviceList.</para>
		/// It can also be null if an application inserts input data, for example, by using SendInput.
		/// </param>
		/// <param name="preParsed">Set to true to use pre-parsed data, false otherwise; defaults to false.</param>
		/// <returns>Returns a <see cref="DeviceInfo"/> structure containing information about a raw input device.</returns>
		/// <exception cref="NotSupportedException"/>
		/// <exception cref="Win32Exception"/>
		internal static DeviceInfo GetRawInputDeviceInfo( IntPtr deviceHandle, bool preParsed )
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
		/// <param name="deviceHandle">A handle to the raw input device. This comes from the LParam of the WM_INPUT message, from <see cref="RawInputHeader.DeviceHandle"/>, or from GetRawInputDeviceList.
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
		/// <param name="deviceName"></param>
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


		/// <summary>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application.</summary>
		/// <returns>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application; the array can be empty, but not null.</returns>
		/// <exception cref="InvalidDataException"/>
		/// <exception cref="Win32Exception"/>
		internal static RawInputDevice[] GetRegisteredRawInputDevices()
		{
			int errorCode;
			var structSize = Marshal.SizeOf( typeof( RawInputDevice ) );
			var deviceCount = 0;

			var result = NativeMethods.GetRegisteredRawInputDevices( null, ref deviceCount, structSize );
			if( result == -1 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve registered raw input device count.", InputDevice.GetException( errorCode ) );
			}
			if( result != 0 )
			{
				throw new InvalidDataException( "Failed to retrieve registered raw input device count." );
			}

			if( deviceCount < 0 )
				throw new InvalidDataException( "Invalid registered raw input device count." );

			var rawInputDevices = new RawInputDevice[ deviceCount ];
			result = NativeMethods.GetRegisteredRawInputDevices( rawInputDevices, ref deviceCount, structSize );
			if( result == -1 )
			{
				errorCode = Marshal.GetLastWin32Error();
				throw new Win32Exception( "Failed to retrieve registered raw input devices.", InputDevice.GetException( errorCode ) );
			}

			return rawInputDevices;
		}



		private static readonly List<Mouse> mice = new List<Mouse>( 1 );                                                // Most systems have only one mouse
		private static readonly List<Keyboard> keyboards = new List<Keyboard>( 1 );                                     // and one keyboard,
		private static readonly List<RawHumanInterfaceDevice> otherDevices = new List<RawHumanInterfaceDevice>( 2 );    // but for some reason the mouse and keyboard have (most of the time) a corresponding HID.
		private static bool isInitialized;



		private static void OnHIDDisconnected( object sender, EventArgs e )
		{
			var device = (RawHumanInterfaceDevice)sender;
			device.Disconnected -= OnHIDDisconnected;
			otherDevices.Remove( device );
		}

		private static void OnMouseDisconnected( object sender, EventArgs e )
		{
			var mouse = (Mouse)sender;
			mouse.Disconnected -= OnMouseDisconnected;
			mice.Remove( mouse );
		}

		private static void OnKeyboardDisconnected( object sender, EventArgs e )
		{
			var keyboard = (Keyboard)sender;
			keyboard.Disconnected -= OnKeyboardDisconnected;
			keyboards.Remove( keyboard );
		}


		private static void Initialize()
		{
			var keyboardIndex = 0;
			var mouseIndex = 0;
			var hidIndex = 0;

			var descriptors = GetRawInputDeviceList();
			for( var d = 0; d < descriptors.Length; ++d )
			{
				var descriptor = descriptors[ d ];
				if( descriptor.DeviceType == InputDeviceType.Mouse )
				{
					//if( mouseIndex == 4 )
					//	continue;
					var m = new Mouse( mouseIndex++, ref descriptor );
					if( !m.IsDisconnected )
					{
						mice.Add( m );
						m.Disconnected += OnMouseDisconnected;
					}
				}
				else if( descriptor.DeviceType == InputDeviceType.Keyboard )
				{
					//if( keyboardIndex == 4 )
					//	continue;
					var k = new Keyboard( keyboardIndex++, ref descriptor );
					if( !k.IsDisconnected )
					{
						keyboards.Add( k );
						k.Disconnected += OnKeyboardDisconnected;
					}
				}
				else // if( descriptor.DeviceType == InputDeviceType.HumanInterfaceDevice )
				{
					var h = new RawHumanInterfaceDevice( hidIndex++, ref descriptor );
					if( !h.IsDisconnected )
					{
						otherDevices.Add( h );
						h.Disconnected += OnHIDDisconnected;
					}
				}
			}

			isInitialized = true;
		}


		#region Keyboard

		/// <summary>Gets the keyboard.</summary>
		public static Keyboard Keyboard
		{
			get
			{
				if( !isInitialized )
					Initialize();

				if( keyboards.Count > 0 )
					return keyboards[ 0 ];

				return null;
			}
		}


		/// <summary>Gets the keyboards.</summary>
		public static ReadOnlyCollection<Keyboard> Keyboards
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Keyboard>( keyboards );
			}
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

		#endregion Keyboard


		#region Mouse

		/// <summary>Gets the primary mouse.</summary>
		public static Mouse Mouse
		{
			get
			{
				if( !isInitialized )
					Initialize();

				if( mice.Count > 0 )
					return mice[ 0 ];

				return null;
			}
		}


		/// <summary>Gets the mice.</summary>
		public static ReadOnlyCollection<Mouse> Mice
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<Mouse>( mice );
			}
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

		#endregion Mouse


		#region HID

		/// <summary>Gets a read-only collection containing all HIDs.</summary>
		public static ReadOnlyCollection<RawHumanInterfaceDevice> HumanInterfaceDevices
		{
			get
			{
				if( !isInitialized )
					Initialize();

				return new ReadOnlyCollection<RawHumanInterfaceDevice>( otherDevices );
			}
		}


		/// <summary>Returns a HID given its device name.</summary>
		/// <param name="deviceName">The device name of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static RawHumanInterfaceDevice GetHIDByDeviceName( string deviceName )
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


		/// <summary>Returns a HID given its handle.</summary>
		/// <param name="deviceHandle">The handle of the requested HID.</param>
		/// <returns>Returns the requested HID, or null.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static RawHumanInterfaceDevice GetHIDByDeviceHandle( IntPtr deviceHandle )
		{
			if( !isInitialized )
				Initialize();

			for( var d = 0; d < otherDevices.Count; ++d )
				if( otherDevices[ d ].DeviceHandle == deviceHandle )
					return otherDevices[ d ];

			return null;
		}

		#endregion HID


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

				var hid = GetHIDByDeviceName( deviceName );
				if( hid != null )
					return hid;
			}
			catch( ArgumentException )
			{
				throw;
			}

			return null;
		}


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

			var hid = GetHIDByDeviceHandle( deviceHandle );
			if( hid != null )
				return hid;

			return null;
		}


		/// <summary>Gets a read-only collection containing all raw input devices.</summary>
		public static ReadOnlyCollection<InputDevice> Devices
		{
			get
			{
				var list = new List<InputDevice>();
				list.AddRange( keyboards );
				list.AddRange( mice );
				list.AddRange( otherDevices );
				return new ReadOnlyCollection<InputDevice>( list );
			}
		}


		private static bool ProcessInputMessage( IntPtr rawInputHandle )
		{
			GetRawInputData( rawInputHandle, out RawInput input );

			if( input.Header.DeviceType == InputDeviceType.Mouse )
			{
				var mouse = GetMouseByDeviceHandle( input.Header.DeviceHandle );
				if( mouse == null )
					return false;   // TODO - re-enumerate devices !

				if( input.Mouse.State.HasFlag( RawMouseStateIndicators.AttributesChanged ) )
				{
					// TODO - what are those attributes ?
				}

				if( input.Mouse.State.HasFlag( RawMouseStateIndicators.MoveAbsolute ) )
					mouse.State.Motion = input.Mouse.Motion;
				else
					mouse.State.Motion += input.Mouse.Motion;

				var buttonsState = input.Mouse.ButtonsState;
				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.Wheel ) )
					mouse.WheelValue += input.Mouse.WheelDelta / 120;

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.HorizontalWheel ) )
				{
					//mouse.HorizontalWheelValue += input.Mouse.ExtraInformation / 120; ?
				}

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.LeftButtonDown ) )
					mouse.State.Buttons |= MouseButtons.Left;
				else if( buttonsState.HasFlag( RawMouseButtonStateIndicators.LeftButtonUp ) )
					mouse.State.Buttons &= ~MouseButtons.Left;

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.RightButtonDown ) )
					mouse.State.Buttons |= MouseButtons.Right;
				else if( buttonsState.HasFlag( RawMouseButtonStateIndicators.RightButtonUp ) )
					mouse.State.Buttons &= ~MouseButtons.Right;

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.MiddleButtonDown ) )
					mouse.State.Buttons |= MouseButtons.Middle;
				else if( buttonsState.HasFlag( RawMouseButtonStateIndicators.MiddleButtonUp ) )
					mouse.State.Buttons &= ~MouseButtons.Middle;

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.XButton1Down ) )
					mouse.State.Buttons |= MouseButtons.X1;
				else if( buttonsState.HasFlag( RawMouseButtonStateIndicators.XButton1Up ) )
					mouse.State.Buttons &= ~MouseButtons.X1;

				if( buttonsState.HasFlag( RawMouseButtonStateIndicators.XButton2Down ) )
					mouse.State.Buttons |= MouseButtons.X2;
				else if( buttonsState.HasFlag( RawMouseButtonStateIndicators.XButton2Up ) )
					mouse.State.Buttons &= ~MouseButtons.X2;
			}
			//else if( input.Header.DeviceType == InputDeviceType.Keyboard )
			//{
			//	var targetKeyboard = GetKeyboardByDeviceHandle( input.Header.DeviceHandle );
			//	if( targetKeyboard == null )
			//		return false;

			//	var state = input.Keyboard;
			//	// ...
			//}
			//else if( input.Header.DeviceType == InputDeviceType.HumanInterfaceDevice )
			//{
			//	var targetHid = GetHIDByDeviceHandle( input.Header.DeviceHandle );
			//	if( targetHid == null )
			//		return false;

			//	var state = input.HumanInterfaceDevice;
			//	// ...
			//}

			//NativeMethods.DefRawInputProc( new RawInput[] { input }, 1, Marshal.SizeOf<RawInputHeader>() );

			return true;
		}

		/// <summary>Processes window messages to ensure the mouse motion and wheel state are up-to-date.</summary>
		/// <param name="message">A Windows message.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		public static bool ProcessWindowMessage( ref Message message )
		{
			if( message.Msg == (int)WindowMessage.InputDeviceChange )
			{
				var wParam = message.WParam.ToInt32();
				if( wParam == 1 )
				{
					// Device arrival
				}
				else if( wParam == 2 )
				{
					// Device removal
				}
				// TODO - mark the device as disconnected on removal, otherwise initialize a new RawInputDevice.
				return true;
			}


			if( message.Msg == (int)WindowMessage.Input )
				return ProcessInputMessage( message.LParam );

			/*
			if( message.Msg == 522 ) // WindowMessage.MouseWheel
			{
				// the high-order short int [of WParam] indicates the wheel rotation distance, expressed in multiples or divisions of 120;
				// the low-order short int indicates the virtual key code of buttons and various modifiers (Ctrl, Shift) which are currently down (pressed).
				// LParam indicates the x (low-order) and y (high-order) coordinate of the cursor; we don't need this here.

				var delta = (int)( message.WParam.ToInt64() & 0xFFFF0000L ) >> 16;
				// works both on x64 and x86 platforms, unlike "message.WParam.ToInt32()" which causes an OverflowException.

				mice[ 0 ].wheelDelta += delta;
				// FIXME - how do we know which mouse had its wheel scrolled ?
				return true;
			}
			*/

			return false;
		}


		/// <summary>Causes the target window to receive raw input messages.
		/// <para>Important: that window must then override its WndProc method to call <see cref="ProcessWindowMessage"/> prior to its base method.</para>
		/// </summary>
		/// <param name="targetWindow">The target window.</param>
		/// <param name="options">One or more <see cref="RawInputDeviceRegistrationOptions"/>.</param>
		/// <param name="usages">At least one TLC usage.</param>
		/// <exception cref="ArgumentException"/>
		public static void Register( IWin32Window targetWindow, RawInputDeviceRegistrationOptions options, params TopLevelCollectionUsage[] usages )
		{
			if( usages == null || usages.Length == 0 )
				throw new ArgumentException( "No TLC usage specified.", "usages" );


			var windowHandle = targetWindow?.Handle ?? IntPtr.Zero;

			var devices = new RawInputDevice[ usages.Length ];
			for( var d = 0; d < usages.Length; ++d )
				devices[ d ] = new RawInputDevice( usages[ d ], options, windowHandle );

			RegisterRawInputDevices( devices );
		}

	}

}