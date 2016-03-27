using System;


namespace ManagedX.Input.XInput
{
	using Design;


	/// <summary>Represents an XInput (1.3 or 1.4) controller, as a managed input device.</summary>
	public abstract class GameController : InputDevice<GamePad, GamePadButtons>
	{

		/// <summary>Defines the maximum number of controllers supported by XInput: 4.</summary>
		[Win32.Native( "XInput.h", "XUSER_MAX_COUNT" )]
		public const int MaxControllerCount = 4;



		private DeadZoneMode deadZoneMode;
		private bool isDisabled;



		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		internal GameController( GameControllerIndex controllerIndex )
			: base( (int)controllerIndex )
		{
			deadZoneMode = DeadZoneMode.Circular;
		}



		/// <summary>Gets the friendly name of this <see cref="GameController"/>.</summary>
		public sealed override string DisplayName { get { return Properties.Resources.GameController + " " + (int)this.Index; } }


		/// <summary>Gets a value indicating the type of this input device.</summary>
		public sealed override InputDeviceType DeviceType { get { return InputDeviceType.HumanInterfaceDevice; } }


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


		/// <summary>Raised when the game controller is connected.</summary>
		public event EventHandler Connected;


		/// <summary>Raises the <see cref="Connected"/> or Disconnected event.</summary>
		protected sealed override void OnDisconnectedChanged()
		{
			if( base.IsDisconnected )
				base.OnDisconnectedChanged();
			else
			{
				var connectedEvent = this.Connected;
				if( connectedEvent != null )
					connectedEvent( this, EventArgs.Empty );
			}
		}


		/// <summary>Gets or sets a value indicating the type of dead zone to apply.</summary>
		public DeadZoneMode DeadZoneMode
		{
			get { return deadZoneMode; }
			set { deadZoneMode = value; }
		}


		/// <summary>Gets or sets a value indicating whether the game controller is disabled.</summary>
		public bool Disabled
		{
			get { return isDisabled; }
			set { isDisabled = value; }
		}


		#region Device info

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

		#endregion Device info


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public abstract bool SetVibration( Vibration vibration );
		

		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and Xbox 360/One).</para>
		/// </summary>
		public abstract Keystroke Keystroke { get; }


		///// <summary>Initializes the game controller.
		///// <para>This method is called by derived constructors, and by Update when required (ie: checking whether the controller is connected).</para>
		///// </summary>
		///// <param name="time">The time elapsed since the application start.</param>
		//protected override void Reset( ref TimeSpan time )
		//{
		//	base.Reset( ref time );
		//}


		/// <summary>Returns the state of the controller.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns the state of the controller.</returns>
		protected abstract override GamePad GetState();


		/// <summary>Gets the index of this <see cref="InputDevice"/>.</summary>
		new public GameControllerIndex Index { get { return (GameControllerIndex)base.Index; } }

	}

}