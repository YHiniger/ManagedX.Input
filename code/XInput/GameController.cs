using System;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Represents an XInput controller, as a managed input device.</summary>
	public abstract class GameController : InputDevice<GameControllerState, GameControllerButtons>
	{

		/// <summary>Defines the maximum number of controllers supported by XInput: 4.</summary>
		[Source( "XInput.h", "XUSER_MAX_COUNT" )]
		public const int MaxControllerCount = 4;


		#region Constants

		/// <summary>Defines the default threshold value used for triggers: 30.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_TRIGGER_THRESHOLD" )]
		public const byte DefaultTriggerThreshold = 30;

		/// <summary>Defines the default dead zone for the left thumbstick: 7849.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE" )]
		public const short DefaultLeftThumbDeadZone = 7849;

		/// <summary>Defines the default dead zone for the right thumbstick: 8689.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE" )]
		public const short DefaultRightThumbDeadZone = 8689;

		#endregion Constants


		private readonly GameControllerIndex index;
		private DeadZoneMode deadZoneMode;
		private byte triggersThreshold = DefaultTriggerThreshold;
		private short leftThumbstickDeadZone = DefaultLeftThumbDeadZone;
		private short rightThumbstickDeadZone = DefaultRightThumbDeadZone;



		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		internal GameController( GameControllerIndex controllerIndex )
			: base()
		{
			index = controllerIndex;
			
			this.Reset( TimeSpan.Zero );
		}



		/// <summary>Gets a value indicating the type of this input device.</summary>
		public sealed override InputDeviceType DeviceType => InputDeviceType.HumanInterfaceDevice;


		/// <summary>Gets the friendly name of this <see cref="GameController"/>.</summary>
		public sealed override string DisplayName => Properties.Resources.GameController + " " + ( (int)index + 1 );


		/// <summary>Raised when the game controller is connected.</summary>
		public event EventHandler Connected;


		/// <summary>Raises the <see cref="Connected"/> or Disconnected event.</summary>
		protected sealed override void OnDisconnected()
		{
			if( base.IsDisconnected )
				base.OnDisconnected();
			else
				this.Connected?.Invoke( this, EventArgs.Empty );
		}


		/// <summary>Gets the index of this <see cref="GameController"/>.</summary>
		public GameControllerIndex Index => index;


		/// <summary>Gets or sets a value indicating the type of dead zone to apply.</summary>
		public DeadZoneMode DeadZoneMode
		{
			get => deadZoneMode;
			set => deadZoneMode = value;
		}


		/// <summary>Gets or sets the threshold value for the triggers dead-zone; must be less than 255, defaults to <see cref="DefaultTriggerThreshold"/>.
		/// <para>This property is ignored when <see cref="DeadZoneMode"/> is set to <see cref="DeadZoneMode.None"/>.</para>
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public byte TriggersThreshold
		{
			get => triggersThreshold;
			set
			{
				if( value == 255 )
					throw new ArgumentOutOfRangeException( "value" );
				triggersThreshold = value;
			}
		}


		/// <summary>Gets or sets the threshold value for the left stick; must be less than 32767, and greater than -1. Defaults to <see cref="DefaultLeftThumbDeadZone"/>.</summary>
		public short LeftThumbstickDeadZone
		{
			get => leftThumbstickDeadZone;
			set
			{
				if( value < 0 || value == short.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				leftThumbstickDeadZone = value;
			}
		}


		/// <summary>Gets or sets the threshold value for the right stick; must be less than 32767, and greater than -1. Defaults to <see cref="DefaultRightThumbDeadZone"/>.</summary>
		public short RightThumbstickDeadZone
		{
			get => rightThumbstickDeadZone;
			set
			{
				if( value < 0 || value == short.MaxValue )
					throw new ArgumentOutOfRangeException( "value" );
				rightThumbstickDeadZone = value;
			}
		}


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public abstract bool SetVibration( Vibration vibration );
		

		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and XBOX 360 and newer).</para>
		/// </summary>
		public abstract Keystroke Keystroke { get; }


		#region Description

		/// <summary>Gets the capabilities of this <see cref="GameController"/>.</summary>
		public abstract Capabilities Capabilities { get; }


		/// <summary>Gets information about the battery type and charge level.</summary>
		public abstract BatteryInformation BatteryInfo { get; }


		/// <summary>Gets the sound rendering and sound capture audio device IDs associated with the headset connected to this controller.
		/// <para>Not supported through XInput 1.3: requires Windows 8 or newer.</para>
		/// </summary>
		public abstract AudioDeviceIds AudioDeviceIds { get; }


		/// <summary>Gets the sound rendering and sound capture device GUIDs associated with the headset connected to this controller.
		/// <para>Only supported through XInput 1.3, deprecated.</para>
		/// </summary>
		public abstract DSoundAudioDeviceGuids DSoundAudioDeviceGuids { get; }

		#endregion Description

	}

}