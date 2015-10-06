using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;


namespace ManagedX.Input.XInput
{

	/// <summary>Provides access to XInput functions.</summary>
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{

		// https://msdn.microsoft.com/en-us/library/windows/desktop/ee417005%28v=vs.85%29.aspx


		private const string LibraryName15 = "XInput1_5.dll";	// Windows 10
		private const string LibraryName14 = "XInput1_4.dll";	// Windows 8, 8.1
		private const string LibraryName13 = "XInput1_3.dll";	// Windows Vista, 7


		#region Delegates

		private delegate void EnableProc(
			bool enable
		);

		private delegate int GetCapabilitiesProc(
			GameControllerIndex userIndex,
			int flags,
			out Capabilities capabilities
		);

		private delegate int GetBatteryInformationProc(
			GameControllerIndex userIndex,
			BatteryDeviceType deviceType,
			out BatteryInformation batteryInformation
		);

		private delegate int GetStateProc(
			GameControllerIndex userIndex,
			out State state
		);

		private delegate int SetStateProc(
			GameControllerIndex userIndex,
			ref Vibration vibration
		);

		private delegate int GetKeystrokeProc(
			GameControllerIndex userIndex,
			int reserved,
			out Keystroke keystroke
		);

		private delegate int GetAudioDeviceIdsProc(
			GameControllerIndex userIndex,
			out string renderDeviceId,
			ref int renderDeviceIdLength,
			out string captureDeviceId,
			ref int captureDeviceIdLength
		);

		private delegate int GetDSoundAudioDeviceGuidsProc(
			GameControllerIndex userIndex,
			out Guid dSoundRenderGuid,
			out Guid dSoundCaptureGuid
		);

		#endregion


		[SuppressMessage( "Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate" )]
		private static readonly string libraryName;
		private static readonly Version version;
		[SuppressMessage( "Microsoft.Performance", "CA1802:UseLiteralsWhereAppropriate" )]
		private static readonly int maxControllerCount;
		private static readonly EnableProc enableProc;
		private static readonly GetCapabilitiesProc getCapsProc;
		private static readonly GetBatteryInformationProc getBatteryInfoProc;
		private static readonly GetStateProc getStateProc;
		private static readonly SetStateProc setStateProc;
		private static readonly GetKeystrokeProc getKeystrokeProc;
		private static readonly GetAudioDeviceIdsProc getAudioDeviceIdsProc;
		private static readonly GetDSoundAudioDeviceGuidsProc getDSoundAudioDeviceGuidsProc;


		internal static string LibraryName { get { return string.Copy( libraryName ); } }

		/// <summary>Gets the version of the XInput library.</summary>
		internal static Version Version { get { return version; } }

		internal static int MaxControllerCount { get { return maxControllerCount; } }


		/// <summary>Static constructor.</summary>
		[SuppressMessage( "Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline" )]
		static NativeMethods()
		{
			var windows8Version = new Version( 6, 2 );
			var windows10Version = new Version( 10, 0 );

			var windowsVersion = Environment.OSVersion.Version;
			if( windowsVersion >= windows10Version ) // Windows 10: XInput 1.5
			{
				libraryName = LibraryName15;
				version = new Version( 1, 5 );
				maxControllerCount = 8;
				enableProc = Enable15;
				getCapsProc = GetCapabilities15;
				getBatteryInfoProc = GetBatteryInformation15;
				getStateProc = GetState15;
				setStateProc = SetState15;
				getKeystrokeProc = GetKeystroke15;
				getAudioDeviceIdsProc = GetAudioDeviceIds15;
				getDSoundAudioDeviceGuidsProc = GetDSoundAudioDeviceGuidsNull;
			}
			else if( windowsVersion >= windows8Version ) // Windows 8, 8.1: XInput 1.4
			{
				libraryName = LibraryName14;
				version = new Version( 1, 4 );
				maxControllerCount = 4;
				enableProc = Enable14;
				getCapsProc = GetCapabilities14;
				getBatteryInfoProc = GetBatteryInformation14;
				getStateProc = GetState14;
				setStateProc = SetState14;
				getKeystrokeProc = GetKeystroke14;
				getAudioDeviceIdsProc = GetAudioDeviceIds14;
				getDSoundAudioDeviceGuidsProc = GetDSoundAudioDeviceGuidsNull;
			}
			else // Windows Vista, 7: XInput 1.3
			{
				libraryName = LibraryName13;
				version = new Version( 1, 3 );
				maxControllerCount = 4;
				enableProc = Enable13;
				getCapsProc = GetCapabilities13;
				getBatteryInfoProc = GetBatteryInformation13;
				getStateProc = GetState13;
				setStateProc = SetState13;
				getKeystrokeProc = GetKeystroke13;
				getAudioDeviceIdsProc = GetAudioDeviceIdsNull;
				getDSoundAudioDeviceGuidsProc = GetDSoundAudioDeviceGuids13;
			}
		}


		/// <summary>Sets the reporting state of XInput.
		/// <para>Not supported by XInput 1.3.</para>
		/// </summary>
		/// <param name="enable">If set to false, XInput will only send neutral data in response to <see cref="XInputGetState"/> (all buttons up, axes centered, and triggers at 0).
		/// <see cref="XInputSetState"/> calls will be registered but not sent to the device.
		/// Sending any value other than false will restore reading and writing functionality to normal.
		/// </param>
		internal static void XInputEnable( bool enable )
		{
			enableProc( enable );
		}

		/// <summary>Retrieves the capabilities and features of a connected controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="flags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: 1.</param>
		/// <param name="capabilities">A <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <seealso cref="ErrorCode">error code</seealso> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>The legacy XINPUT 9.1.0 version (included in Windows Vista and later) always returned a fixed set of capabilities regardless of attached device.</remarks>
		internal static int XInputGetCapabilities( GameControllerIndex userIndex, int flags, out Capabilities capabilities )
		{
			return getCapsProc( userIndex, flags, out capabilities );
		}

		/// <summary>Retrieves the battery type and charge status of a wireless controller.
		/// Beware that this function might not be available on Windows Vista.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="deviceType">Specifies which device associated with this user index should be queried.</param>
		/// <param name="batteryInformation">A <see cref="BatteryInformation"/> structure that receives the battery information.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</returns>
		internal static int XInputGetBatteryInformation( GameControllerIndex userIndex, BatteryDeviceType deviceType, out BatteryInformation batteryInformation )
		{
			return getBatteryInfoProc( userIndex, deviceType, out batteryInformation );
		}

		/// <summary>Retrieves the current state of the specified controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="state">A <see cref="State"/> structure that receives the current state of the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code</see> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>
		/// When XInputGetState is used to retrieve controller data, the left and right triggers are each reported separately.
		/// For legacy reasons, when DirectInput retrieves controller data, the two triggers share the same axis.
		/// The legacy behavior is noticeable in the current Game Device Control Panel, which uses DirectInput for controller state.
		/// </remarks>
		internal static int XInputGetState( GameControllerIndex userIndex, out State state )
		{
			return getStateProc( userIndex, out state );
		}

		/// <summary>Sends data to a connected controller.
		/// This function is used to activate the vibration function of a controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code defined in Winerror.h</see>.</para>
		/// </returns>
		internal static int XInputSetState( GameControllerIndex userIndex, ref Vibration vibration )
		{
			return setStateProc( userIndex, ref vibration );
		}

		/// <summary>Retrieves a gamepad input event.</summary>
		/// <param name="userIndex">Index of the user's controller. Can be a <see cref="GameControllerIndex"/> value, or XUSER_INDEX_ANY(255) to fetch the next available input event from any user.</param>
		/// <param name="reserved">Must be set to 0.</param>
		/// <param name="keystroke">Receives a <see cref="Keystroke"/> structure for an input event.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.
		/// If no new keys have been pressed, the return value is <see cref="ErrorCode.Empty"/>.
		/// If the controller is not connected or the user has not activated it, the return value is <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, the return value is an error code defined in Winerror.h.
		/// </returns>
		internal static int XInputGetKeystroke( GameControllerIndex userIndex, int reserved, out Keystroke keystroke )
		{
			return getKeystrokeProc( userIndex, reserved, out keystroke );
		}

		/// <summary>Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
		/// <para>Not supported by XInput 1.3.</para>
		/// </summary>
		/// <param name="userIndex">Index of the gamer associated with the device.</param>
		/// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
		/// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
		/// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function will also return <see cref="ErrorCode.None"/> with null as the values for <paramref name="renderDeviceId"/> and <paramref name="captureDeviceId"/>.
		/// If the controller port device is not physically connected, the function will return <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it will return a valid Win32 error code.
		/// </returns>
		internal static int XInputGetAudioDeviceIds( GameControllerIndex userIndex, out string renderDeviceId, out string captureDeviceId )
		{
			int renderDeviceIdLength = 0;
			int captureDeviceIdLength = 0;
			return getAudioDeviceIdsProc( userIndex, out renderDeviceId, ref renderDeviceIdLength, out captureDeviceId, ref captureDeviceIdLength );
		}

		/// <summary>Gets the sound rendering and sound capture device GUIDs that are associated with the headset connected to the specified controller.
		/// <para>Only supported by XInput 1.3.</para>
		/// </summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="dSoundRenderGuid">Receives the <see cref="Guid"/> of the headset sound rendering device.</param>
		/// <param name="dSoundCaptureGuid">Receives the <see cref="Guid"/> of the headset sound capture device.</param>
		/// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function also retrieves <see cref="ErrorCode.None"/> with <see cref="Guid.Empty"/> as the values for <paramref name="dSoundRenderGuid"/> and <paramref name="dSoundCaptureGuid"/>.
		/// If the controller port device is not physically connected, the function returns <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it returns a valid Win32 error code.</returns>
		internal static int XInputGetDSoundAudioDeviceGuids( GameControllerIndex userIndex, out Guid dSoundRenderGuid, out Guid dSoundCaptureGuid )
		{
			return getDSoundAudioDeviceGuidsProc( userIndex, out dSoundRenderGuid, out dSoundCaptureGuid );
		}


		#region XInput 1.5

		/// <summary>Sets the reporting state of XInput.</summary>
		/// <param name="enable">If set to false, XInput will only send neutral data in response to <see cref="GetState15"/> (all buttons up, axes centered, and triggers at 0).
		/// <see cref="SetState15"/> calls will be registered but not sent to the device.
		/// Sending any value other than false will restore reading and writing functionality to normal.
		/// </param>
		[DllImport( LibraryName15, EntryPoint = "XInputEnable", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern void Enable15(
			[In, MarshalAs( UnmanagedType.Bool )] bool enable
		);


		/// <summary>Retrieves the capabilities and features of a connected controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="flags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: 1.</param>
		/// <param name="capabilities">A <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <seealso cref="ErrorCode">error code</seealso> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>The legacy XINPUT 9.1.0 version (included in Windows Vista and later) always returned a fixed set of capabilities regardless of attached device.</remarks>
		[DllImport( LibraryName15, EntryPoint = "XInputGetCapabilities", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetCapabilities15(
			[In] GameControllerIndex userIndex,
			[In] int flags,
			[Out] out Capabilities capabilities
		);


		/// <summary>Retrieves the battery type and charge status of a wireless controller.
		/// Beware that this function might not be available on Windows Vista.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="deviceType">Specifies which device associated with this user index should be queried.</param>
		/// <param name="batteryInformation">A <see cref="BatteryInformation"/> structure that receives the battery information.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</returns>
		[DllImport( LibraryName15, EntryPoint = "XInputGetBatteryInformation", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetBatteryInformation15(
			[In] GameControllerIndex userIndex,
			[In] BatteryDeviceType deviceType,
			[Out] out BatteryInformation batteryInformation
		);


		/// <summary>Retrieves the current state of the specified controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="state">A <see cref="State"/> structure that receives the current state of the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code</see> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>
		/// When XInputGetState is used to retrieve controller data, the left and right triggers are each reported separately.
		/// For legacy reasons, when DirectInput retrieves controller data, the two triggers share the same axis.
		/// The legacy behavior is noticeable in the current Game Device Control Panel, which uses DirectInput for controller state.
		/// </remarks>
		[DllImport( LibraryName15, EntryPoint = "XInputGetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetState15(
			[In] GameControllerIndex userIndex,
			[Out] out State state
		);


		/// <summary>Sends data to a connected controller.
		/// This function is used to activate the vibration function of a controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code defined in Winerror.h</see>.</para>
		/// </returns>
		[DllImport( LibraryName15, EntryPoint = "XInputSetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int SetState15(
			[In] GameControllerIndex userIndex,
			[In] ref Vibration vibration
		);


		/// <summary>Retrieves a gamepad input event.</summary>
		/// <param name="userIndex">Index of the user's controller. Can be a <see cref="GameControllerIndex"/> value, or XUSER_INDEX_ANY(255) to fetch the next available input event from any user.</param>
		/// <param name="reserved">Must be set to 0.</param>
		/// <param name="keystroke">Receives a <see cref="Keystroke"/> structure for an input event.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.
		/// If no new keys have been pressed, the return value is <see cref="ErrorCode.Empty"/>.
		/// If the controller is not connected or the user has not activated it, the return value is <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, the return value is an error code defined in Winerror.h.
		/// </returns>
		[DllImport( LibraryName15, EntryPoint = "XInputGetKeystroke", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetKeystroke15(
			[In] GameControllerIndex userIndex,
			[In] int reserved,
			[Out] out Keystroke keystroke
		);


		/// <summary>Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.</summary>
		/// <param name="userIndex">Index of the gamer associated with the device.</param>
		/// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
		/// <param name="renderDeviceIdLength">Size, in wide-chars, of the render device ID string buffer.</param>
		/// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
		/// <param name="captureDeviceIdLength">Size, in wide-chars, of capture device ID string buffer.</param>
		/// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function will also return <see cref="ErrorCode.None"/> with null as the values for <paramref name="renderDeviceId"/> and <paramref name="captureDeviceId"/>.
		/// If the controller port device is not physically connected, the function will return <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it will return a valid Win32 error code.
		/// </returns>
		[DllImport( LibraryName15, EntryPoint = "XInputGetAudioDeviceIds", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetAudioDeviceIds15(
			[In] GameControllerIndex userIndex,
			[Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 2 ), Optional] out string renderDeviceId,
			[In, Out, Optional] ref int renderDeviceIdLength,
			[Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 4 ), Optional] out string captureDeviceId,
			[In, Out, Optional] ref int captureDeviceIdLength
		);

		#endregion


		#region XInput 1.4

		/// <summary>Sets the reporting state of XInput.</summary>
		/// <param name="enable">If set to false, XInput will only send neutral data in response to <see cref="GetState14"/> (all buttons up, axes centered, and triggers at 0).
		/// <see cref="SetState14"/> calls will be registered but not sent to the device.
		/// Sending any value other than false will restore reading and writing functionality to normal.
		/// </param>
		[DllImport( LibraryName14, EntryPoint = "XInputEnable", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern void Enable14(
			[In, MarshalAs( UnmanagedType.Bool )] bool enable
		);

	
		/// <summary>Retrieves the capabilities and features of a connected controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="flags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: 1.</param>
		/// <param name="capabilities">A <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <seealso cref="ErrorCode">error code</seealso> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>The legacy XINPUT 9.1.0 version (included in Windows Vista and later) always returned a fixed set of capabilities regardless of attached device.</remarks>
		[DllImport( LibraryName14, EntryPoint = "XInputGetCapabilities", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetCapabilities14(
			[In] GameControllerIndex userIndex,
			[In] int flags,
			[Out] out Capabilities capabilities
		);


		/// <summary>Retrieves the battery type and charge status of a wireless controller.
		/// Beware that this function might not be available on Windows Vista.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="deviceType">Specifies which device associated with this user index should be queried.</param>
		/// <param name="batteryInformation">A <see cref="BatteryInformation"/> structure that receives the battery information.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</returns>
		[DllImport( LibraryName14, EntryPoint = "XInputGetBatteryInformation", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetBatteryInformation14(
			[In] GameControllerIndex userIndex,
			[In] BatteryDeviceType deviceType,
			[Out] out BatteryInformation batteryInformation
		);


		/// <summary>Retrieves the current state of the specified controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="state">A <see cref="State"/> structure that receives the current state of the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code</see> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>
		/// When XInputGetState is used to retrieve controller data, the left and right triggers are each reported separately.
		/// For legacy reasons, when DirectInput retrieves controller data, the two triggers share the same axis.
		/// The legacy behavior is noticeable in the current Game Device Control Panel, which uses DirectInput for controller state.
		/// </remarks>
		[DllImport( LibraryName14, EntryPoint = "XInputGetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetState14(
			[In] GameControllerIndex userIndex,
			[Out] out State state
		);


		/// <summary>Sends data to a connected controller.
		/// This function is used to activate the vibration function of a controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code defined in Winerror.h</see>.</para>
		/// </returns>
		[DllImport( LibraryName14, EntryPoint = "XInputSetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int SetState14(
			[In] GameControllerIndex userIndex,
			[In] ref Vibration vibration
		);

		/// <summary>Retrieves a gamepad input event.</summary>
		/// <param name="userIndex">Index of the user's controller. Can be a <see cref="GameControllerIndex"/> value, or XUSER_INDEX_ANY(255) to fetch the next available input event from any user.</param>
		/// <param name="reserved">Must be set to 0.</param>
		/// <param name="keystroke">Receives a <see cref="Keystroke"/> structure for an input event.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.
		/// If no new keys have been pressed, the return value is <see cref="ErrorCode.Empty"/>.
		/// If the controller is not connected or the user has not activated it, the return value is <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, the return value is an error code defined in Winerror.h.
		/// </returns>
		[DllImport( LibraryName14, EntryPoint = "XInputGetKeystroke", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetKeystroke14(
			[In] GameControllerIndex userIndex,
			[In] int reserved,
			[Out] out Keystroke keystroke
		);


		/// <summary>Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.</summary>
		/// <param name="userIndex">Index of the gamer associated with the device.</param>
		/// <param name="renderDeviceId">Windows Core Audio device ID string for render (speakers).</param>
		/// <param name="renderDeviceIdLength">Size, in wide-chars, of the render device ID string buffer.</param>
		/// <param name="captureDeviceId">Windows Core Audio device ID string for capture (microphone).</param>
		/// <param name="captureDeviceIdLength">Size, in wide-chars, of capture device ID string buffer.</param>
		/// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function will also return <see cref="ErrorCode.None"/> with null as the values for <paramref name="renderDeviceId"/> and <paramref name="captureDeviceId"/>.
		/// If the controller port device is not physically connected, the function will return <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it will return a valid Win32 error code.
		/// </returns>
		[DllImport( LibraryName14, EntryPoint = "XInputGetAudioDeviceIds", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetAudioDeviceIds14(
			[In] GameControllerIndex userIndex,
			[Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 2 ), Optional] out string renderDeviceId,
			[In, Out, Optional] ref int renderDeviceIdLength,
			[Out, MarshalAs( UnmanagedType.LPWStr, SizeParamIndex = 4 ), Optional] out string captureDeviceId,
			[In, Out, Optional] ref int captureDeviceIdLength
		);

		#endregion


		#region XInput 1.3

		/// <summary>Sets the reporting state of XInput.</summary>
		/// <param name="enable">If set to false, XInput will only send neutral data in response to <see cref="GetState14"/> (all buttons up, axes centered, and triggers at 0).
		/// <see cref="SetState14"/> calls will be registered but not sent to the device.
		/// Sending any value other than false will restore reading and writing functionality to normal.
		/// </param>
		[DllImport( LibraryName13, EntryPoint = "XInputEnable", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern void Enable13(
			[In, MarshalAs( UnmanagedType.Bool )] bool enable
		);


		/// <summary>Retrieves the capabilities and features of a connected controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="flags">Input flags that identify the controller type. If this value is 0, then the capabilities of all controllers connected to the system are returned. Currently, only one value is supported: 1.</param>
		/// <param name="capabilities">A <see cref="Capabilities"/> structure that receives the controller capabilities.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <seealso cref="ErrorCode">error code</seealso> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>The legacy XINPUT 9.1.0 version (included in Windows Vista and later) always returned a fixed set of capabilities regardless of attached device.</remarks>
		[DllImport( LibraryName13, EntryPoint = "XInputGetCapabilities", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetCapabilities13(
			[In] GameControllerIndex userIndex,
			[In] int flags,
			[Out] out Capabilities capabilities
		);


		/// <summary>Retrieves the battery type and charge status of a wireless controller.
		/// Beware that this function might not be available on Windows Vista.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="deviceType">Specifies which device associated with this user index should be queried.</param>
		/// <param name="batteryInformation">A <see cref="BatteryInformation"/> structure that receives the battery information.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</returns>
		[DllImport( LibraryName13, EntryPoint = "XInputGetBatteryInformation", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetBatteryInformation13(
			[In] GameControllerIndex userIndex,
			[In] BatteryDeviceType deviceType,
			[Out] out BatteryInformation batteryInformation
		);


		/// <summary>Retrieves the current state of the specified controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="state">A <see cref="State"/> structure that receives the current state of the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code</see> (defined in WinError.h).</para>
		/// </returns>
		/// <remarks>
		/// When XInputGetState is used to retrieve controller data, the left and right triggers are each reported separately.
		/// For legacy reasons, when DirectInput retrieves controller data, the two triggers share the same axis.
		/// The legacy behavior is noticeable in the current Game Device Control Panel, which uses DirectInput for controller state.
		/// </remarks>
		[DllImport( LibraryName13, EntryPoint = "XInputGetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetState13(
			[In] GameControllerIndex userIndex,
			[Out] out State state
		);


		/// <summary>Sends data to a connected controller.
		/// This function is used to activate the vibration function of a controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure containing the vibration information to send to the controller.</param>
		/// <returns>
		/// <para>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.</para>
		/// <para>If the controller is not connected, the return value is <see cref="ErrorCode.NotConnected"/>.</para>
		/// <para>If the function fails, the return value is an <see cref="ErrorCode">error code defined in Winerror.h</see>.</para>
		/// </returns>
		[DllImport( LibraryName13, EntryPoint = "XInputSetState", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int SetState13(
			[In] GameControllerIndex userIndex,
			[In] ref Vibration vibration
		);


		/// <summary>Retrieves a gamepad input event.</summary>
		/// <param name="userIndex">Index of the user's controller. Can be a <see cref="GameControllerIndex"/> value, or XUSER_INDEX_ANY(255) to fetch the next available input event from any user.</param>
		/// <param name="reserved">Must be set to 0.</param>
		/// <param name="keystroke">Receives a <see cref="Keystroke"/> structure for an input event.</param>
		/// <returns>If the function succeeds, the return value is <see cref="ErrorCode.None"/>.
		/// If no new keys have been pressed, the return value is <see cref="ErrorCode.Empty"/>.
		/// If the controller is not connected or the user has not activated it, the return value is <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, the return value is an error code defined in Winerror.h.
		/// </returns>
		[DllImport( LibraryName13, EntryPoint = "XInputGetKeystroke", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetKeystroke13(
			[In] GameControllerIndex userIndex,
			[In] int reserved,
			[Out] out Keystroke keystroke
		);


		/// <summary>Gets the sound rendering and sound capture device GUIDs that are associated with the headset connected to the specified controller.</summary>
		/// <param name="userIndex">Index of the user's controller.</param>
		/// <param name="dSoundRenderGuid">Receives the <see cref="Guid"/> of the headset sound rendering device.</param>
		/// <param name="dSoundCaptureGuid">Receives the <see cref="Guid"/> of the headset sound capture device.</param>
		/// <returns>If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function also retrieves <see cref="ErrorCode.None"/> with <see cref="Guid.Empty"/> as the values for <paramref name="dSoundRenderGuid"/> and <paramref name="dSoundCaptureGuid"/>.
		/// If the controller port device is not physically connected, the function returns <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it returns a valid Win32 error code.</returns>
		[DllImport( LibraryName13, EntryPoint = "XInputGetDSoundAudioDeviceGuids", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
		private static extern int GetDSoundAudioDeviceGuids13(
			[In] GameControllerIndex userIndex,
			[Out] out Guid dSoundRenderGuid,
			[Out] out Guid dSoundCaptureGuid
		);

		#endregion


		private static int GetDSoundAudioDeviceGuidsNull(
			GameControllerIndex userIndex,
			out Guid dSoundRenderGuid,
			out Guid dSoundCaptureGuid
		)
		{
			dSoundCaptureGuid = dSoundRenderGuid = Guid.Empty;
			return 0;
		}

		
		private static int GetAudioDeviceIdsNull(
			GameControllerIndex userIndex,
			out string renderDeviceId,
			ref int renderDeviceIdLength,
			out string captureDeviceId,
			ref int captureDeviceIdLength
		)
		{
			renderDeviceId = captureDeviceId = null;
			renderDeviceIdLength = captureDeviceIdLength = 0;
			return 0;
		}

	}

}