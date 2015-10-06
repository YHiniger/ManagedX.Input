namespace ManagedX.Input.Design
{
	using XInput;


	/// <summary>Defines properties and methods to properly implement an XInput controller; inherits from <see cref="IInputDevice&lt;TState,TButton&gt;"/>.</summary>
	public interface IXInputController : IInputDevice<GamePad, GamePadButtons>
	{

		/// <summary>Gets the controller index.</summary>
		new GameControllerIndex Index { get; }


		/// <summary>Gets the information about the controller capabilities.</summary>
		Capabilities Capabilities { get; }


		/// <summary>Gets information about the controller battery.</summary>
		BatteryInformation BatteryInfo { get; }


		/// <summary>Gets information about keystrokes.
		/// <para>Only available on Windows 8 and newer (and Xbox 360/One).</para>
		/// </summary>
		Keystroke Keystroke { get; }


		/// <summary>Sends a vibration state to the controller.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true on success, otherwise returns false (ie: not connected, no vibration support, etc).</returns>
		bool SetVibration( Vibration vibration );

	}


	///// <summary>Defines properties and methods to properly implement an XInput 1.3 controller.</summary>
	//internal interface IXInputController13 : IXInputController
	//{

	//	bool GetDSoundAudioDeviceGuids( out System.Guid dSoundRenderGuid, out System.Guid dSoundCaptureGuid );

	//}


	///// <summary>Defines properties and methods to properly implement an XInput 1.4 controller.</summary>
	//internal interface IXInputController14 : IXInputController
	//{

	//	bool GetAudioDeviceIds( out string renderDeviceId, out string captureDeviceId );

	//}


	///// <summary>Defines properties and methods to properly implement an XInput 1.5 controller.</summary>
	//internal interface IXInputController15 : IXInputController14
	//{

	//	/// <summary>Sends vibration states to the controller; requires XInput 1.5 (Windows 10 only ?).</summary>
	//	/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
	//	/// <param name="triggers">A <see cref="Vibration"/> structure for the triggers; requires an Xbox One controller.</param>
	//	/// <returns>Returns true on sucess, otherwise returns false (ie: disconnected controller, no vibration support, etc).</returns>
	//	/// <remarks>Only supported in XInput 1.5 (to be confirmed, it might be exclusive to Xbox...)</remarks>
	//	bool SetVibration( Vibration vibration, Vibration triggers );

	//}

}
