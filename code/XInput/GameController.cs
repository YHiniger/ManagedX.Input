using System;


/*
 * IMPORTANT : legacy (XInput9_1_0) and older versions (1.2, 1.1 etc) aren't (and won't be) supported.
 * Xbox 360 and Xbox One are not supported.
 */


namespace ManagedX.Input.XInput
{
	using Design;
	using Win32;


	/// <summary>An XInput controller, as a managed input device (see <see cref="IInputDevice"/>, <see cref="IXInputController"/>).</summary>
	public abstract class GameController : InputDevice<GamePad, GamePadButtons>, IXInputController
	{

		/// <summary>Gets an interface to the managed XInput service.</summary>
		public static IXInput Service { get { return XInputService.Instance; } }


		private DeadZoneMode deadZoneMode;
		private bool isDisabled;



		/// <summary>Instantiates a new XInput <see cref="GameController"/>.</summary>
		/// <param name="controllerIndex">The game controller index.</param>
		internal GameController( GameControllerIndex controllerIndex )
			: base( controllerIndex )
		{
			deadZoneMode = DeadZoneMode.Circular;

			var zero = TimeSpan.Zero;
			this.Reset( ref zero );

			//isDisabled = base.IsDisconnected;
		}



		/// <summary>Gets the identifier of this <see cref="GameController"/>.</summary>
		public sealed override string DeviceIdentifier { get { return "XInput#" + (int)this.Index; } }


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


		/// <summary>Gets the <see cref="ManagedX.Input.XInput.Capabilities">capabilities</see> of this <see cref="GameController"/>.</summary>
		public abstract Capabilities Capabilities { get; }


		/// <summary>Gets <see cref="BatteryInformation">information</see> about the battery type and charge level.</summary>
		public abstract BatteryInformation BatteryInfo { get; }


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns false if the controller has no vibration support or is not connected, otherwise returns true.</returns>
		public abstract bool SetVibration( Vibration vibration );
		

		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and Xbox 360/One).</para>
		/// </summary>
		public abstract Keystroke Keystroke { get; }


		///// <summary>Initializes the game controller.
		///// <para>This method is called by the constructor, and by Update when required (ie: checking whether the controller is connected).</para>
		///// </summary>
		///// <param name="time">The time elapsed since the application start.</param>
		//protected abstract override void Reset( ref TimeSpan time );


		/// <summary>Returns the state of the controller.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns the state of the controller.</returns>
		protected abstract override GamePad GetState();


		/// <summary>Gets the sound rendering and sound capture audio device IDs associated with the headset connected to this controller.
		/// <para>Not supported through XInput 1.3 (Windows Vista/7).</para>
		/// </summary>
		public abstract AudioDeviceIds AudioDeviceIds { get; }


		/// <summary>Gets the sound rendering and sound capture device GUIDs associated with the headset connected to this controller.
		/// <para>Only supported through XInput 1.3, deprecated.</para>
		/// </summary>
		public abstract DSoundAudioDeviceGuids DSoundAudioDeviceGuids { get; }


		/// <summary>Returns the <see cref="DisplayName"/> of this <see cref="GameController"/>.</summary>
		/// <returns>Returns the <see cref="DisplayName"/> of this <see cref="GameController"/>.</returns>
		public sealed override string ToString()
		{
			return this.DisplayName;
		}

	}

}