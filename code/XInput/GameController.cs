using System;


/*
 * IMPORTANT : legacy (XInput9_1_0) and older versions (1.2, 1.1 etc) aren't (and won't be) supported.
 * Xbox 360 and Xbox One are not supported.
 */


namespace ManagedX.Input.XInput
{
	using Design;


	/// <summary>An XInput controller, as a managed input device (see <see cref="IInputDevice"/>, <see cref="IXInputController"/>).</summary>
	public sealed class GameController : InputDevice<GamePad, GamePadButtons>, IXInputController
	{

		/// <summary>Gets the XInput "service".</summary>
		public static IXInput XInput { get { return XInputService.Instance; } }


		#region Delegates

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


		private bool isConnected;
		private Capabilities capabilities;
		private BatteryInformation batteryInfo;
		private DeadZoneMode deadZoneMode;
		private GetCapabilitiesProc getCapsProc;
		private GetBatteryInformationProc getBatteryInfoProc;
		private GetStateProc getStateProc;
		private SetStateProc setStateProc;
		private GetKeystrokeProc getKeystrokeProc;
		private GetAudioDeviceIdsProc getAudioDeviceIdsProc;
		private GetDSoundAudioDeviceGuidsProc getDSoundAudioDeviceGuidsProc;


		#region Constructor, destructor

		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		/// <param name="version">Indicates which version of the XInput API to use.</param>
		internal GameController( GameControllerIndex controllerIndex, APIVersion version )
			: base( controllerIndex )
		{
			deadZoneMode = DeadZoneMode.Circular;

			if( version == APIVersion.XInput15 )
				this.Setup15();
			else if( version == APIVersion.XInput14 )
				this.Setup14();
			else
				this.Setup13();

			this.Reset();
		}


		/// <summary>Destructor.</summary>
		~GameController()
		{
			this.SetVibration( Vibration.Zero );
		}

		#endregion


		private void Setup15()
		{
			getCapsProc = NativeMethods.XInput15GetCapabilities;
			getBatteryInfoProc = NativeMethods.XInput15GetBatteryInformation;
			getStateProc = NativeMethods.XInput15GetState;
			setStateProc = NativeMethods.XInput15SetState;
			getKeystrokeProc = NativeMethods.XInput15GetKeystroke;
			getAudioDeviceIdsProc = NativeMethods.XInput15GetAudioDeviceIds;
			getDSoundAudioDeviceGuidsProc = NativeMethods.XInput15GetDSoundAudioDeviceGuids;
		}

		private void Setup14()
		{
			getCapsProc = NativeMethods.XInput14GetCapabilities;
			getBatteryInfoProc = NativeMethods.XInput14GetBatteryInformation;
			getStateProc = NativeMethods.XInput14GetState;
			setStateProc = NativeMethods.XInput14SetState;
			getKeystrokeProc = NativeMethods.XInput14GetKeystroke;
			getAudioDeviceIdsProc = NativeMethods.XInput14GetAudioDeviceIds;
			getDSoundAudioDeviceGuidsProc = NativeMethods.XInput14GetDSoundAudioDeviceGuids;
		}

		private void Setup13()
		{
			getCapsProc = NativeMethods.XInput13GetCapabilities;
			getBatteryInfoProc = NativeMethods.XInput13GetBatteryInformation;
			getStateProc = NativeMethods.XInput13GetState;
			setStateProc = NativeMethods.XInput13SetState;
			getKeystrokeProc = NativeMethods.XInput13GetKeystroke;
			getDSoundAudioDeviceGuidsProc = NativeMethods.XInput13GetDSoundAudioDeviceGuids;
			getAudioDeviceIdsProc = NativeMethods.XInput13GetAudioDeviceIds;
		}



		/// <summary>Gets a value indicating the type of this input device.</summary>
		public sealed override InputDeviceType DeviceType { get { return InputDeviceType.HumanInterfaceDevice; } }


		/// <summary>Gets a value indicating whether this game controller is connected.</summary>
		public sealed override bool IsConnected { get { return isConnected; } }


		/// <summary>Gets the <see cref="ManagedX.Input.XInput.Capabilities">capabilities</see> of this <see cref="GameController"/>.</summary>
		public Capabilities Capabilities
		{
			get
			{
				var errorCode = getCapsProc( base.Index, 1, out capabilities );
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
				var errorCode = getBatteryInfoProc( base.Index, BatteryDeviceType.Gamepad, out batteryInfo );
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

			var errorCode = setStateProc( base.Index, ref vibration );
			return isConnected = ( errorCode == 0 );
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
					var errorCode = getKeystrokeProc( base.Index, 0, out output );
					if( errorCode != 0 )
						output = Keystroke.Empty;
				}
				return output;
			}
		}


		/// <summary>Returns the state of the controller.
		/// <para>This method is called by <see cref="Reset"/> and Update.</para>
		/// </summary>
		/// <returns>Returns the state of the controller.</returns>
		protected sealed override GamePad GetState()
		{
			State state;
			var errorCode = getStateProc( base.Index, out state );

			if( errorCode == (int)ErrorCode.NotConnected )
				isConnected = false;
			else if( errorCode == 0 )
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
		/// <para>This method is called by the constructor, and by Update when required (ie: checking whether the controller is connected).</para>
		/// </summary>
		protected sealed override void Reset()
		{
			var errorCode = getCapsProc( base.Index, 1, out capabilities );
			if( isConnected = ( errorCode == 0 ) )
				base.Reset();
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


		/// <summary>Gets the sound rendering and sound capture audio device IDs associated with the headset connected to this controller.
		/// <para>Not supported through XInput 1.3 (Windows Vista/7).</para>
		/// </summary>
		public AudioDeviceIds AudioDeviceIds
		{
			get
			{
				int renderDeviceIdLength = 0;
				int captureDeviceIdLength = 0;
				AudioDeviceIds deviceIds;
				if( getAudioDeviceIdsProc( base.Index, out deviceIds.RenderDeviceId, ref renderDeviceIdLength, out deviceIds.CaptureDeviceId, ref captureDeviceIdLength ) != 0 )
					deviceIds = AudioDeviceIds.Empty;
				return deviceIds;
			}
		}


		/// <summary>Gets the sound rendering and sound capture device GUIDs associated with the headset connected to this controller.
		/// <para>Only supported through XInput 1.3, deprecated.</para>
		/// </summary>
		public DSoundAudioDeviceGuids DSoundAudioDeviceGuids
		{
			get
			{
				DSoundAudioDeviceGuids deviceGuids;
				if( getDSoundAudioDeviceGuidsProc( base.Index, out deviceGuids.RenderDeviceGuid, out deviceGuids.CaptureDeviceGuid ) != 0 )
					deviceGuids = DSoundAudioDeviceGuids.Empty;
				return deviceGuids;
			}
		}


		/// <summary>Returns "XInput controller".</summary>
		/// <returns>Returns "XInput controller".</returns>
		public sealed override string ToString()
		{
			return Properties.Resources.GameController;
		}

	}

}