using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Input.Raw
{

	/// <summary>Provides access to native raw input functions.
	/// <para>For desktop applications only.</para>
	/// </summary>
	internal static class NativeMethods
	{
				
		private const string LibraryName = "User32.dll";
		// WinUser.h


		/// <summary>Returns an exception corresponding to the last Win32 error.</summary>
		/// <returns>Returns an exception corresponding to the last Win32 error.</returns>
		internal static Exception GetExceptionForLastWin32Error()
		{
			var errorCode = Marshal.GetLastWin32Error();
			var ex = Marshal.GetExceptionForHR( errorCode );
			if( ex == null )
				ex = new Win32Exception( errorCode );
			return ex;
		}



		#region GetRawInputDeviceList

		// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645598%28v=vs.85%29.aspx


		/// <summary>Enumerates the raw input devices attached to the system.</summary>
		/// <param name="rawInputDeviceList">An array of <see cref="RawInputDeviceDescriptor"/> structures for the devices attached to the system. If null, the number of devices is returned in <paramref name="deviceCount"/>.</param>
		/// <param name="deviceCount">If <paramref name="rawInputDeviceList"/> is null, receives the number of devices attached to the system; otherwise, specifies the number of RAWINPUTDEVICELIST structures that can be contained in the buffer <paramref name="rawInputDeviceList"/> points to.
		/// <para>If this value is less than the number of devices attached to the system, the function returns the actual number of devices in this variable and fails with ERROR_INSUFFICIENT_BUFFER.</para>
		/// </param>
		/// <param name="size">The size, in bytes, of a <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <returns>If the function is successful, the return value is the number of devices stored in the buffer pointed to by <paramref name="rawInputDeviceList"/>.
		/// <para>On any other error, the function returns (UINT) -1 and GetLastError returns the error indication.</para>
		/// </returns>
		[SuppressUnmanagedCodeSecurity]
		[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		private static extern int GetRawInputDeviceList(
			[Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 ), Optional] RawInputDeviceDescriptor[] rawInputDeviceList,
			[In, Out] ref int deviceCount,
			[In] int size
		);


		/// <summary>Returns an array of raw input devices attached to the system.</summary>
		/// <returns>Returns an array of raw input devices attached to the system.</returns>
		/// <exception cref="InvalidDataException"/>
		/// <exception cref="Win32Exception"/>
		internal static RawInputDeviceDescriptor[] GetRawInputDeviceList()
		{
			var size = Marshal.SizeOf( typeof( RawInputDeviceDescriptor ) );

			var deviceCount = 0;

			var result = GetRawInputDeviceList( null, ref deviceCount, size );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input device count.", GetExceptionForLastWin32Error() );

			if( deviceCount < 0 )
				throw new InvalidDataException( "Invalid raw input device count: " + deviceCount.ToString( System.Globalization.CultureInfo.InvariantCulture ) );

			var list = new RawInputDeviceDescriptor[ deviceCount ];
			result = GetRawInputDeviceList( list, ref deviceCount, size );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input device list.", GetExceptionForLastWin32Error() );

			if( result == list.Length )
				return list;

			throw new InvalidDataException( "Failed to retrieve raw input device count: device count mismatch." );
		}

		#endregion GetRawInputDeviceList


		#region GetRawInputDeviceInfo (+GetRawInputDeviceName)

		// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645597%28v=vs.85%29.aspx


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
		[SuppressUnmanagedCodeSecurity]
		[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		private static extern int GetRawInputDeviceInfoW(
			[In, Optional] IntPtr deviceHandle,
			[In] GetInfoCommand command,
			[In, Out, Optional] ref DeviceInfo data,
			[In, Out] ref int dataSize
		);


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
			var deviceInfoStructSize = deviceInfo.StructureSize;

			var result = GetRawInputDeviceInfoW( deviceHandle, preParsed ? GetInfoCommand.PreParsedData : GetInfoCommand.DeviceInfo, ref deviceInfo, ref deviceInfoStructSize );
			if( result == deviceInfoStructSize )
				return deviceInfo;

			if( result == -1 )
				throw new NotSupportedException( "Failed to retrieve raw input device info: DeviceInfo size mismatch." );
			else if( result == 0 )
				throw new NotSupportedException( "Failed to retrieve raw input device info: bad implementation." );
			else
				throw new Win32Exception( "Failed to retrieve raw input device info.", GetExceptionForLastWin32Error() );
		}



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
		[SuppressUnmanagedCodeSecurity]
		[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		private static extern int GetRawInputDeviceInfoW(
			[In, Optional] IntPtr deviceHandle,
			[In] GetInfoCommand command,
			[In, Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 3 ), Optional] string data,
			[In, Out] ref int dataSize
		);


		/// <summary>Returns the device name (or device path) of a raw input device.</summary>
		/// <param name="deviceHandle">A handle to the raw input device. This comes from the LParam of the WM_INPUT message, from <see cref="RawInputHeader.DeviceHandle"/>, or from GetRawInputDeviceList.
		/// <para>It can also be <see cref="IntPtr.Zero"/> if an application inserts input data, for example, by using SendInput.</para>
		/// </param>
		/// <returns>Returns the name of a raw input device.</returns>
		/// <exception cref="NotSupportedException"/>
		/// <exception cref="Win32Exception"/>
		internal static string GetRawInputDeviceName( IntPtr deviceHandle )
		{
			var charCount = 0;

			var result = GetRawInputDeviceInfoW( deviceHandle, GetInfoCommand.DeviceName, null, ref charCount );
			if( result != 0 )
			{
				if( result > 0 )
					throw new NotSupportedException( "Failed to retrieve raw input device name buffer size: bad implementation." );
				
				throw new Win32Exception( "Failed to retrieve raw input device name buffer size.", GetExceptionForLastWin32Error() );
			}

			if( charCount <= 0 )
				throw new NotSupportedException( "Invalid raw input device name character count." );

			var buffer = new string( '\0', charCount );
			result = GetRawInputDeviceInfoW( deviceHandle, GetInfoCommand.DeviceName, buffer, ref charCount );
			if( result == charCount )
				return buffer;

			if( result == 0 )
				throw new NotSupportedException( "Failed to retrieve raw input device name: bad implementation." );

			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input device name.", GetExceptionForLastWin32Error() );
			
			throw new NotSupportedException( "Failed to retrieve raw input device name: string length mismatch (bad implementation)." );
		}

		#endregion GetRawInputDeviceInfo (+GetRawInputDeviceName)


		#region RegisterRawInputDevices

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
		private static extern bool RegisterRawInputDevices(
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


			if( !RegisterRawInputDevices( rawInputDevices, deviceCount, Marshal.SizeOf( typeof( RawInputDevice ) ) ) )
				throw new Win32Exception( "Failed to register raw input device(s).", GetExceptionForLastWin32Error() );
		}

		#endregion RegisterRawInputDevices


		#region GetRegisteredRawInputDevices

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
		private static extern int GetRegisteredRawInputDevices(
			[Out, MarshalAs( UnmanagedType.LPArray, SizeParamIndex = 1 ), Optional] RawInputDevice[] rawInputDevices,
			[In, Out] ref int deviceCount,
			[In] int structSize
		);


		/// <summary>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application.</summary>
		/// <returns>Returns an array of <see cref="RawInputDevice"/> structures containing information about the raw input devices for the current application; the array can be empty, but not null.</returns>
		/// <exception cref="InvalidDataException"/>
		/// <exception cref="Win32Exception"/>
		internal static RawInputDevice[] GetRegisteredRawInputDevices()
		{
			var structSize = Marshal.SizeOf( typeof( RawInputDevice ) );
			var deviceCount = 0;
			
			var result = GetRegisteredRawInputDevices( null, ref deviceCount, structSize );
			if( result != 0 )
			{
				if( result == -1 )
					throw new Win32Exception( "Failed to retrieve registered raw input device count.", GetExceptionForLastWin32Error() );

				throw new InvalidDataException( "Failed to retrieve registered raw input device count." );
			}

			if( deviceCount < 0 )
				throw new InvalidDataException( "Invalid registered raw input device count." );

			var rawInputDevices = new RawInputDevice[ deviceCount ];
			result = GetRegisteredRawInputDevices( rawInputDevices, ref deviceCount, structSize );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve registered raw input devices.", GetExceptionForLastWin32Error() );

			return rawInputDevices;
		}

		#endregion GetRegisteredRawInputDevices


		#region GetRawInputData

		/// <summary>Retrieves the raw input from the specified device.
		/// <para>GetRawInputData gets the raw input one <see cref="RawInput"/> structure at a time. In contrast, GetRawInputBuffer gets an array of <see cref="RawInput"/> structures.</para>
		/// </summary>
		/// <param name="rawInputHandle">A handle to the <see cref="RawInput"/> structure. This comes from the lParam in WM_INPUT.</param>
		/// <param name="command">The command flag.</param>
		/// <param name="data">A pointer to the data that comes from the <see cref="RawInput"/> structure. This depends on the value of <paramref name="command"/>. If <paramref name="data"/> is null, the required size of the buffer is returned in <paramref name="size"/>.</param>
		/// <param name="size">The size, in bytes, of the data in <paramref name="data"/>.</param>
		/// <param name="headerSize">The size, in bytes, of the <see cref="RawInputHeader"/> structure.</param>
		/// <returns>
		/// If <paramref name="data"/> is null and the function is successful, the return value is 0.
		/// <para>If <paramref name="data"/> is not null and the function is successful, the return value is the number of bytes copied into <paramref name="data"/>.</para>
		/// If there is an error, the return value is -1.
		/// </returns>
		[SuppressUnmanagedCodeSecurity]
		[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		private static extern int GetRawInputData(
			[In] IntPtr rawInputHandle,
			[In] GetDataCommand command,
			[Out, Optional] out RawInput data,
			[In, Out] ref int size,
			[In] int headerSize
		);
		//UINT WINAPI GetRawInputData(
		//  _In_      HRAWINPUT hRawInput,
		//  _In_      UINT      uiCommand,
		//  _Out_opt_ LPVOID    pData,
		//  _Inout_   PUINT     pcbSize,
		//  _In_      UINT      cbSizeHeader
		//);


		/// <summary>Returns the raw input from the specified device.</summary>
		/// <param name="rawInputHandle">A handle to the <see cref="RawInput"/> structure. This comes from the LParam in WM_INPUT.</param>
		/// <param name="rawInput"></param>
		/// <exception cref="Win32Exception"/>
		/// <exception cref="InvalidDataException"/>
		internal static void GetRawInputData( IntPtr rawInputHandle, out RawInput rawInput )
		{
			var headerSize = Marshal.SizeOf( typeof( RawInputHeader ) );
			var size = Marshal.SizeOf( typeof( RawInput ) );

			var result = GetRawInputData( rawInputHandle, GetDataCommand.Input, out rawInput, ref size, headerSize );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input data.", GetExceptionForLastWin32Error() );
		}

		#endregion GetRawInputData


		#region GetRawInputBuffer

		/// <summary>Performs a buffered read of the raw input data.</summary>
		/// <param name="data">A pointer to a buffer of <see cref="RawInput"/> structures that contain the raw input data. If null, the minimum required buffer, in bytes, is returned in <paramref name="size"/>.</param>
		/// <param name="size">The size, in bytes, of a <see cref="RawInput"/> structure.</param>
		/// <param name="headerSize">The size, in bytes, of the <see cref="RawInputHeader"/> structure.</param>
		/// <returns>
		/// If <paramref name="data"/> is null and the function is successful, the return value is zero.
		/// If <paramref name="data"/> is not null and the function is successful, the return value is the number of <see cref="RawInput"/> structures written to <paramref name="data"/>.
		/// If an error occurs, the return value is (UINT)-1. Call GetLastError for the error code.
		/// </returns>
		[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		private static extern int GetRawInputBuffer(
			[Out, Optional] RawInput[] data,
			[In, Out] ref int size,
			[In] int headerSize
		);


		/// <summary>Performs a buffered read of the raw input data.</summary>
		/// <param name="buffer"></param>
		/// <exception cref="Win32Exception"/>
		internal static void GetRawInputBuffer( out RawInput[] buffer )
		{
			var headerSize = Marshal.SizeOf( typeof( RawInputHeader ) );
			var size = 0;
			
			var result = GetRawInputBuffer( null, ref size, headerSize );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input buffer size.", GetExceptionForLastWin32Error() );

			var count = size / Marshal.SizeOf( typeof( RawInput ) );
			buffer = new RawInput[ count ];
			
			result = GetRawInputBuffer( buffer, ref size, headerSize );
			if( result == -1 )
				throw new Win32Exception( "Failed to retrieve raw input buffer.", GetExceptionForLastWin32Error() );
		}

		#endregion GetRawInputBuffer



		/// <summary>Returns the embedded string of a top-level collection that identifies the manufacturer's product.</summary>
		/// <param name="hidDeviceObject">Specifies an open handle to a top-level collection.</param>
		/// <param name="buffer">Pointer to a caller-allocated buffer that the routine uses to return the requested product string. The routine returns a NULL-terminated wide character string.</param>
		/// <param name="bufferLength">Specifies the length, in bytes, of a caller-allocated buffer provided at Buffer. If the buffer is not large enough to return the entire NULL-terminated embedded string, the routine returns nothing in the buffer.</param>
		/// <returns>Returns true if the function successfully returns the entire NULL-terminated embedded string, otherwise returns false.</returns>
		[DllImport( "HID.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		private static extern bool HidD_GetProductString(
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
		private static extern SafeFileHandle CreateFileW(
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


		/// <summary>Returns the embedded string of a top-level collection that identifies the manufacturer's product.</summary>
		/// <param name="deviceName"></param>
		/// <returns>Returns the embedded string of a top-level collection that identifies the manufacturer's product, or null.</returns>
		internal static string GetHIDProductString( string deviceName )
		{
			const int CharCount = 256;				// must be at least 128 chars (126 + 2)
			const int ByteCount = CharCount * 2;	// 2 bytes per (unicode) char

			using( var hidHandle = CreateFileW( deviceName, FileAccess.Read, FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, FileAttributes.Normal, IntPtr.Zero ) )
			{
				var output = new string( '\0', CharCount );
				if( HidD_GetProductString( hidHandle, output, ByteCount ) )
					return output.TrimEnd( '\0' );
			}
			
			return null;
		}

	}

}