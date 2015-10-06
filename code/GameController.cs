using System;


/*
 * By default, this library uses XInput1_3.dll (available on Windows Vista and later versions).
 * To specify which version to use, define one of the following conditional compilation symbols in your project settings, depending on the target platform:
 *	XINPUT_1_5 : Windows X only; work in progress, not yet tested
 *	XINPUT_1_4 : Windows 8 and Windows 8.1
 *
 * IMPORTANT : legacy (XInput9_1_0) and older versions (1.2, 1.1 etc) aren't (and won't be) supported.
 * Xbox 360 and Xbox One are not supported.
 */


namespace ManagedX.Input.XInput
{
	using Design;


	/// <summary>An XInput controller; inherits from <see cref="InputDevice&lt;TState, TButton&gt;"/> and implements <see cref="IXInputController"/>.</summary>
	public sealed partial class GameController : InputDevice<GamePad, GamePadButtons>, IXInputController
	{

		/// <summary>Gets the maximum number of game controllers supported by XInput:
		/// <list type="bullet">
		/// <item><description>8 on Windows 10,</description></item>
		/// <item><description>4 on Windows Vista/7/8/8.1</description></item>
		/// </list>
		/// </summary>
		public static int MaxControllerCount { get { return NativeMethods.MaxControllerCount; } }


		#region Static members

		private static readonly IXInput service = new XInputService();

		/// <summary>Gets an interface providing access to all XInput controllers.</summary>
		public static IXInput XInput { get { return service; } }


		//private static bool IsWindows8OrGreater()
		//{
		//	OperatingSystem oS = Environment.OSVersion;
		//	return ( oS.Platform == PlatformID.Win32NT ) && ( oS.Version.Major >= 6 ) && ( oS.Version.Minor >= 2 );
		//}


		#endregion // Static members


		private GameControllerIndex index;
		private bool isConnected;
		private Capabilities capabilities;
		private BatteryInformation batteryInfo;
		private DeadZoneMode deadZoneMode;


		#region Constructor, destructor

		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		internal GameController( GameControllerIndex controllerIndex )
			: base( (int)controllerIndex )
		{
			index = controllerIndex;
			deadZoneMode = DeadZoneMode.Circular;
		}


		/// <summary>Destructor.</summary>
		~GameController()
		{
			this.SetVibration( Vibration.Zero );
		}

		#endregion


		/// <summary>Gets the <see cref="GameControllerIndex">index</see> of this <see cref="GameController">game controller</see>.</summary>
		new public GameControllerIndex Index { get { return index; } }


		/// <summary>Gets a value indicating whether this game controller is connected.</summary>
		public sealed override bool IsConnected { get { return isConnected; } }


		/// <summary>Gets the <see cref="ManagedX.Input.XInput.Capabilities">capabilities</see> of this <see cref="GameController"/>.</summary>
		public Capabilities Capabilities
		{
			get
			{
				var errorCode = NativeMethods.XInputGetCapabilities( index, 1, out capabilities );
				if( errorCode == (int)ErrorCode.NotConnected )
					isConnected = false;
				else if( errorCode == 0 )
					isConnected = true;

				return capabilities;
			}
		}


		/// <summary>Gets <see cref="BatteryInformation">information</see> about the battery type and charge level.</summary>
		/// <returns>Gets information about the battery type and charge level.</returns>
		public BatteryInformation BatteryInfo
		{
			get
			{
				var errorCode = NativeMethods.XInputGetBatteryInformation( index, BatteryDeviceType.Gamepad, out batteryInfo );

				if( errorCode == (int)ErrorCode.NotConnected )
					isConnected = false;
				else if( errorCode == 0 )
					isConnected = true;

				return batteryInfo;
			}
		}


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public bool SetVibration( Vibration vibration )
		{
			if( !( capabilities.HasLeftMotor || capabilities.HasRightMotor ) )
				return false;

			var errorCode = NativeMethods.XInputSetState( index, ref vibration );
			return isConnected = errorCode == 0;
		}
		

		///// <summary>Sends a vibration state to the controller (XInput 1.5 only, not yet supported/implemented).</summary>
		///// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		///// <param name="triggers">This parameter is currently ignored.</param>
		///// <returns>Returns false if the controller is not connected or has no vibration support, otherwise returns true.</returns>
		//public bool SetVibration( Vibration vibration, Vibration triggers )
		//{
		//	return this.SetVibration( vibration );
		//}


		/// <summary>Gets or sets a value indicating the type of dead zone to apply.</summary>
		public DeadZoneMode DeadZoneMode
		{
			get { return deadZoneMode; }
			set { deadZoneMode = value; }
		}


		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and Xbox 360/One).</para>
		/// </summary>
		public Keystroke Keystroke
		{
			get
			{
				var output = Keystroke.Empty;
				if( isConnected && capabilities.IsSet( Caps.PluginModuleDeviceSupported ) )
				{
					var errorCode = NativeMethods.XInputGetKeystroke( index, 0, out output );
					if( errorCode != 0 )
						output = Keystroke.Empty;
				}
				return output;
			}
		}


		/// <summary>Retrieves and returns the current state of the controller. This method is called by <see cref="Update"/>.</summary>
		/// <returns>Returns the current state of the controller.</returns>
		protected sealed override GamePad GetState()
		{
			State state;
			var errorCode = NativeMethods.XInputGetState( index, out state );
			bool success = ( errorCode == 0 );

			if( errorCode == (int)ErrorCode.NotConnected )
				isConnected = false;
			else if( success )
			{
				isConnected = true;
				//previousStatePacketNumber = currentStatePacketNumber;
				//currentStatePacketNumber = state.PacketNumber;
				var gamePad = state.GamePadState;
				if( deadZoneMode != DeadZoneMode.None )
				{
					gamePad.ApplyThumbSticksDeadZone( deadZoneMode, GamePad.DefaultLeftThumbDeadZone, GamePad.DefaultRightThumbDeadZone );
					gamePad.ApplyTriggersDeadZone();
				}
				return gamePad;
			}

			return GamePad.Empty;
		}


		/// <summary>Initializes the game controller.
		/// <para>This method is called by <see cref="Update"/> when required (ie: checking whether the controller is connected).</para>
		/// </summary>
		protected sealed override void Initialize()
		{
			int errorCode = NativeMethods.XInputGetCapabilities( index, 1, out capabilities );
			if( isConnected = ( errorCode == 0 ) )
				base.Initialize();
		}


		/// <summary>Updates the game pad state.</summary>
		/// <param name="time">A time indicator (ie: the time elapsed since the start of the program).</param>
		public sealed override void Update( TimeSpan time )
		{
			if( !isConnected )
				this.Initialize();

			if( isConnected )
			{
				base.Update( time );

				// THINKABOUTME - apply vibration based on a private VibrationSequence ?
			}
		}


		/// <summary>Returns a value indicating whether a button is pressed in the current state, but was released in the previous state.</summary>
		/// <param name="button">A gamepad button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed in the current state and was released in the previous state; otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( GamePadButtons button )
		{
			return !this.PreviousState.Buttons.HasFlag( button ) && this.CurrentState.Buttons.HasFlag( button );
		}


		/// <summary>Returns a value indicating whether a button is released in the current state, but was pressed in the previous state.</summary>
		/// <param name="button">A gamepad button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is released in the current state and was pressed in the previous state; otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( GamePadButtons button )
		{
			return this.PreviousState.Buttons.HasFlag( button ) && !this.CurrentState.Buttons.HasFlag( button );
		}


		/// <summary>Retrieves the sound rendering and sound capture audio device IDs that are associated with the headset connected to the specified controller.
		/// <para>Not supported by XInput 1.3.</para>
		/// </summary>
		/// <param name="renderDeviceId">Receives the Windows Core Audio device ID string for render (speakers); on Windows Vista and 7, this is always null.</param>
		/// <param name="captureDeviceId">Receives the Windows Core Audio device ID string for capture (microphone); on Windows Vista and 7, this is always null.</param>
		/// <returns>
		/// If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function will also return <see cref="ErrorCode.None"/> with null as the values for <paramref name="renderDeviceId"/> and <paramref name="captureDeviceId"/>.
		/// If the controller port device is not physically connected, the function will return <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it will return a valid Win32 error code.
		/// </returns>
		public bool GetAudioDeviceIds( out string renderDeviceId, out string captureDeviceId )
		{
			return NativeMethods.XInputGetAudioDeviceIds( this.index, out renderDeviceId, out captureDeviceId ) == 0;
		}


		/// <summary>Gets the sound rendering and sound capture device GUIDs that are associated with the headset connected to the specified controller.
		/// <para>Only supported by XInput 1.3.</para>
		/// </summary>
		/// <param name="dSoundRenderGuid">Receives the <see cref="Guid"/> of the headset sound rendering device; on Windows 8 and greater, this is always an empty GUID.</param>
		/// <param name="dSoundCaptureGuid">Receives the <see cref="Guid"/> of the headset sound capture device; on Windows 8 and greater, this is always an empty GUID.</param>
		/// <returns>
		/// If the function successfully retrieves the device IDs for render and capture, the return code is <see cref="ErrorCode.None"/>.
		/// If there is no headset connected to the controller, the function also retrieves <see cref="ErrorCode.None"/> with <see cref="Guid.Empty"/> as the values for <paramref name="dSoundRenderGuid"/> and <paramref name="dSoundCaptureGuid"/>.
		/// If the controller port device is not physically connected, the function returns <see cref="ErrorCode.NotConnected"/>.
		/// If the function fails, it returns a valid Win32 error code.
		/// </returns>
		public bool GetDSoundAudioDeviceGuids( out Guid dSoundRenderGuid, out Guid dSoundCaptureGuid )
		{
			return NativeMethods.XInputGetDSoundAudioDeviceGuids( this.index, out dSoundRenderGuid, out dSoundCaptureGuid ) == 0;
		}


		/// <summary>Returns "XInput controller".</summary>
		/// <returns>Returns "XInput controller".</returns>
		public sealed override string ToString()
		{
			return Properties.Resources.GameController;
		}

	}

}