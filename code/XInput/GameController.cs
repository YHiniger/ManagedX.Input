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

		/// <summary>Gets an interface to the managed XInput service.</summary>
		public static IXInput Service { get { return XInputService.Instance; } }


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
		private bool isDisabled;


		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		/// <param name="version">Indicates which version of the XInput API to use.</param>
		internal GameController( GameControllerIndex controllerIndex, XInputVersion version )
			: base( controllerIndex )
		{
			deadZoneMode = DeadZoneMode.Circular;

			if( version >= XInputVersion.XInput14 )
				this.Setup14();
			else
				this.Setup13();

			var zero = TimeSpan.Zero;
			this.Reset( ref zero );

			isDisabled = base.IsDisconnected;
		}



		//private void Setup15()
		//{
		//	getCapsProc = SafeNativeMethods.XInput15GetCapabilities;
		//	getBatteryInfoProc = SafeNativeMethods.XInput15GetBatteryInformation;
		//	getStateProc = SafeNativeMethods.XInput15GetState;
		//	setStateProc = SafeNativeMethods.XInput15SetState;
		//	getKeystrokeProc = SafeNativeMethods.XInput15GetKeystroke;
		//	getAudioDeviceIdsProc = SafeNativeMethods.XInput15GetAudioDeviceIds;
		//	getDSoundAudioDeviceGuidsProc = SafeNativeMethods.XInput15GetDSoundAudioDeviceGuids;
		//}

		private void Setup14()
		{
			getCapsProc = SafeNativeMethods.XInput14GetCapabilities;
			getBatteryInfoProc = SafeNativeMethods.XInput14GetBatteryInformation;
			getStateProc = SafeNativeMethods.XInput14GetState;
			setStateProc = SafeNativeMethods.XInput14SetState;
			getKeystrokeProc = SafeNativeMethods.XInput14GetKeystroke;
			getAudioDeviceIdsProc = SafeNativeMethods.XInput14GetAudioDeviceIds;
			getDSoundAudioDeviceGuidsProc = SafeNativeMethods.XInput14GetDSoundAudioDeviceGuids;
		}

		private void Setup13()
		{
			getCapsProc = SafeNativeMethods.XInput13GetCapabilities;
			getBatteryInfoProc = SafeNativeMethods.XInput13GetBatteryInformation;
			getStateProc = SafeNativeMethods.XInput13GetState;
			setStateProc = SafeNativeMethods.XInput13SetState;
			getKeystrokeProc = SafeNativeMethods.XInput13GetKeystroke;
			getDSoundAudioDeviceGuidsProc = SafeNativeMethods.XInput13GetDSoundAudioDeviceGuids;
			getAudioDeviceIdsProc = SafeNativeMethods.XInput13GetAudioDeviceIds;
		}


		/// <summary>Gets the identifier of this <see cref="GameController"/>.</summary>
		public sealed override string Identifier { get { return "XInput#" + (int)this.Index; } }


		/// <summary>Gets the friendly name of this <see cref="GameController"/>.</summary>
		public sealed override string DisplayName { get { return Properties.Resources.GameController + " " + (int)this.Index; } }


		/// <summary>Gets a value indicating the type of this input device.</summary>
		public sealed override InputDeviceType DeviceType { get { return InputDeviceType.HumanInterfaceDevice; } }


		/// <summary>Gets the <see cref="ManagedX.Input.XInput.Capabilities">capabilities</see> of this <see cref="GameController"/>.</summary>
		public Capabilities Capabilities
		{
			get
			{
				var errorCode = getCapsProc( base.Index, 1, out capabilities );

				if( errorCode == (int)ErrorCode.NotConnected )
					base.IsDisconnected = true;

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
					base.IsDisconnected = true;

				return batteryInfo;
			}
		}


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public bool SetVibration( Vibration vibration )
		{
			if( isDisabled || !( capabilities.HasLeftMotor || capabilities.HasRightMotor ) )
				return false;

			var errorCode = setStateProc( base.Index, ref vibration );
			
			if( errorCode == (int)ErrorCode.NotConnected )
				base.IsDisconnected = true;
			
			return errorCode == 0;
		}
		

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
				if( !base.IsDisconnected && capabilities.IsSet( Caps.PluginModuleDeviceSupported ) )
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
			if( !isDisabled )
			{
				State state;
				var errorCode = getStateProc( base.Index, out state );

				if( errorCode == (int)ErrorCode.NotConnected )
					base.IsDisconnected = true;
				else if( errorCode == 0 )
				{
					if( deadZoneMode != DeadZoneMode.None )
					{
						state.GamePadState.ApplyThumbSticksDeadZone( deadZoneMode, GamePad.DefaultLeftThumbDeadZone, GamePad.DefaultRightThumbDeadZone );
						state.GamePadState.ApplyTriggersDeadZone();
					}
					return state.GamePadState;
				}
			}

			return GamePad.Empty;
		}


		/// <summary>Initializes the game controller.
		/// <para>This method is called by the constructor, and by Update when required (ie: checking whether the controller is connected).</para>
		/// </summary>
		/// <param name="time">The time elapsed since the application start.</param>
		protected sealed override void Reset( ref TimeSpan time )
		{
			var errorCode = getCapsProc( base.Index, 1, out capabilities );
			if( !( base.IsDisconnected = ( errorCode != 0 ) ) )
				base.Reset( ref time );
		}


		/// <summary>Returns a value indicating whether a button is pressed in the current state, but was released in the previous state.</summary>
		/// <param name="button">A gamepad button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed in the current state and was released in the previous state; otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( GamePadButtons button )
		{
			return !base.PreviousState.Buttons.HasFlag( button ) && base.CurrentState.Buttons.HasFlag( button );
		}


		/// <summary>Returns a value indicating whether a button is released in the current state, but was pressed in the previous state.</summary>
		/// <param name="button">A gamepad button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is released in the current state and was pressed in the previous state; otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( GamePadButtons button )
		{
			return base.PreviousState.Buttons.HasFlag( button ) && !base.CurrentState.Buttons.HasFlag( button );
		}


		/// <summary>Gets or sets a value indicating whether the game controller is disabled.</summary>
		public bool Disabled
		{
			get { return isDisabled; }
			set { isDisabled = value; }
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


		/// <summary>Returns the <see cref="DisplayName"/> of this <see cref="GameController"/>.</summary>
		/// <returns>Returns the <see cref="DisplayName"/> of this <see cref="GameController"/>.</returns>
		public sealed override string ToString()
		{
			return this.DisplayName;
		}

	}

}