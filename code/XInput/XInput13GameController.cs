using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{
	using Win32;


	// An XInput 1.3 device.
	internal sealed class XInput13GameController : GameController
	{

		internal const string LibraryName = "XInput1_3.dll";	// Windows Vista, 7: requires the DirectX End-User Runtime (June 2010)


		// Provides access to the XInput 1.3 API functions.
		[Native( "XInput.h" )]
		[System.Security.SuppressUnmanagedCodeSecurity]
		private static class SafeNativeMethods
		{

			// https://msdn.microsoft.com/en-us/library/windows/desktop/ee417005%28v=vs.85%29.aspx


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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputGetCapabilities(
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputGetBatteryInformation(
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputGetState(
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputSetState(
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputGetKeystroke(
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
			[DllImport( LibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = true, SetLastError = false )]
			internal static extern int XInputGetDSoundAudioDeviceGuids(
				[In] GameControllerIndex userIndex,
				[Out] out Guid dSoundRenderGuid,
				[Out] out Guid dSoundCaptureGuid
			);

		}



		private Capabilities capabilities;
		private BatteryInformation batteryInfo;
		private State rawState;



		internal XInput13GameController( GameControllerIndex controllerIndex )
			: base( controllerIndex )
		{
			var zero = TimeSpan.Zero;
			this.Reset( ref zero );
		}



		public sealed override Capabilities Capabilities { get { return capabilities; } }


		public sealed override BatteryInformation BatteryInfo
		{
			get
			{
				var errorCode = SafeNativeMethods.XInputGetBatteryInformation( base.Index, BatteryDeviceType.Gamepad, out batteryInfo );

				if( errorCode == (int)ErrorCode.NotConnected )
					base.IsDisconnected = true;

				return batteryInfo;
			}
		}

	
		public sealed override bool SetVibration( Vibration vibration )
		{
			if( base.Disabled || !( capabilities.HasLeftMotor || capabilities.HasRightMotor ) )
				return false;

			var errorCode = SafeNativeMethods.XInputSetState( base.Index, ref vibration );

			if( errorCode == (int)ErrorCode.NotConnected )
				base.IsDisconnected = true;

			return errorCode == 0;
		}


		public sealed override Keystroke Keystroke
		{
			get
			{
				var output = Keystroke.Empty;
				if( !base.IsDisconnected && capabilities.IsSet( Caps.PluginModuleDeviceSupported ) )
				{
					var errorCode = SafeNativeMethods.XInputGetKeystroke( base.Index, 0, out output );
					if( errorCode != 0 )
						output = Keystroke.Empty;
				}
				return output;
			}
		}


		protected sealed override void Reset( ref TimeSpan time )
		{
			var errorCode = SafeNativeMethods.XInputGetCapabilities( base.Index, 1, out capabilities );
			if( errorCode == (int)ErrorCode.NotConnected )
				base.IsDisconnected = true;
			else if( errorCode == 0 )
				base.Reset( ref time );
		}


		protected sealed override GamePad GetState()
		{
			if( !base.Disabled )
			{
				var errorCode = SafeNativeMethods.XInputGetState( base.Index, out rawState );

				if( errorCode == (int)ErrorCode.NotConnected )
					base.IsDisconnected = true;
				else if( errorCode == 0 )
				{
					var state = rawState.GamePadState;
					if( base.DeadZoneMode != DeadZoneMode.None )
					{
						state.ApplyThumbSticksDeadZone( base.DeadZoneMode, GamePad.DefaultLeftThumbDeadZone, GamePad.DefaultRightThumbDeadZone );
						state.ApplyTriggersDeadZone();
					}
					return state;
				}
			}

			return GamePad.Empty;
		}


		public sealed override DSoundAudioDeviceGuids DSoundAudioDeviceGuids
		{
			get
			{
				DSoundAudioDeviceGuids deviceGuids;
				if( SafeNativeMethods.XInputGetDSoundAudioDeviceGuids( base.Index, out deviceGuids.RenderDeviceGuid, out deviceGuids.CaptureDeviceGuid ) != 0 )
					deviceGuids = DSoundAudioDeviceGuids.Empty;
				return deviceGuids;
			}
		}


		public sealed override AudioDeviceIds AudioDeviceIds { get { return AudioDeviceIds.Empty; } }

	}

}