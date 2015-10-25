namespace ManagedX.Input.Design
{
	using XInput;


	/// <summary>Defines properties and methods to properly implement an XInput controller, as a managed input device (see <see cref="IInputDevice"/>).</summary>
	public interface IXInputController : IInputDevice<GamePad, GamePadButtons>
	{

		/// <summary>Gets information about the controller capabilities.</summary>
		Capabilities Capabilities { get; }


		/// <summary>Gets information about the controller battery.</summary>
		BatteryInformation BatteryInfo { get; }


		/// <summary>Gets information about keystrokes.</summary>
		Keystroke Keystroke { get; }


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true on success, otherwise returns false (ie: not connected, no vibration support, etc).</returns>
		bool SetVibration( Vibration vibration );

		//	/// <summary>Sends vibration states to the controller; requires XInput 1.5 (Windows 10 only ?).</summary>
		//	/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		//	/// <param name="triggers">A <see cref="Vibration"/> structure for the triggers; requires an Xbox One controller.</param>
		//	/// <returns>Returns true on sucess, otherwise returns false (ie: disconnected controller, no vibration support, etc).</returns>
		//	/// <remarks>Only supported in XInput 1.5 (to be confirmed, it might be exclusive to Xbox One...)</remarks>
		//	bool SetVibration( Vibration vibration, Vibration triggers );


		///// <summary>Gets the sound rendering and sound capture audio device IDs associated with the headset connected to this controller.
		///// <para>Not supported through XInput 1.3 (Windows Vista/7).</para>
		///// </summary>
		//AudioDeviceIds AudioDeviceIds { get; }

		
		///// <summary>Gets the sound rendering and sound capture device GUIDs associated with the headset connected to this controller.
		///// <para>Deprecated, only supported through XInput 1.3.</para>
		///// </summary>
		//DSoundAudioDeviceGuids DSoundAudioDeviceGuids { get; }

	}

}