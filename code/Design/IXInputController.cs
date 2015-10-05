namespace ManagedX.Input.Design
{
	using XInput;


	/// <summary>Defines properties and methods to implement an XInput controller; inherits from <see cref="IInputDevice&lt;TState,TButton&gt;"/>.</summary>
	public interface IXInputController : IInputDevice<GamePad, GamePadButtons>
	{

		/// <summary>Gets the controller index.</summary>
		new GameControllerIndex Index { get; }


		/// <summary>Gets the information about the controller capabilities.</summary>
		Capabilities Capabilities { get; }


		/// <summary>Gets information about the controller battery.</summary>
		BatteryInformation BatteryInfo { get; }


#if XINPUT_1_4 || XBOX_360 || XBOX_ONE

		/// <summary>Gets information about keystrokes.</summary>
		Keystroke Keystroke { get; }

#endif


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns></returns>
		bool SetVibration( Vibration vibration );

//#if XINPUT_1_5

//		/// <summary>Sends vibration states to the controller; requires XInput 1.5 (Windows X only ?).</summary>
//		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
//		/// <param name="triggers">A <see cref="Vibration"/> structure for the triggers; requires an Xbox One controller.</param>
//		/// <returns></returns>
//		/// <remarks>Only supported in XInput 1.5?</remarks>
//		bool SetVibration( Vibration vibration, Vibration triggers );

//#endif

	}

}
