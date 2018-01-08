using System;


namespace ManagedX.Input.XInput
{

	/// <summary>Represents an XInput (1.3 or greater) controller, as a managed input device.</summary>
	public abstract class GameController : InputDevice<Gamepad, GamepadButtons>
	{

		/// <summary>Defines the maximum number of controllers supported by XInput: 4.</summary>
		[Win32.Source( "XInput.h", "XUSER_MAX_COUNT" )]
		public const int MaxControllerCount = 4;


		private readonly GameControllerIndex index;
		private DeadZoneMode deadZoneMode;



		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		internal GameController( GameControllerIndex controllerIndex )
			: base()
		{
			index = controllerIndex;
			deadZoneMode = DeadZoneMode.Circular;
			
			this.Reset( TimeSpan.Zero );
		}



		/// <summary>Gets a value indicating the type of this input device.</summary>
		public sealed override InputDeviceType DeviceType => InputDeviceType.HumanInterfaceDevice;


		/// <summary>Gets the friendly name of this <see cref="GameController"/>.</summary>
		public sealed override string DisplayName => Properties.Resources.GameController + " " + ( (int)index + 1 );


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public abstract bool SetVibration( Vibration vibration );
		

		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and XBOX 360 and newer).</para>
		/// </summary>
		public abstract Keystroke Keystroke { get; }


		/// <summary>Gets the index of this <see cref="GameController"/>.</summary>
		public GameControllerIndex Index => index;


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


		/// <summary>Gets or sets a value indicating the type of dead zone to apply.</summary>
		public DeadZoneMode DeadZoneMode
		{
			get => deadZoneMode;
			set => deadZoneMode = value;
		}


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