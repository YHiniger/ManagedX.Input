namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates controller subtypes available in XInput, used in the <see cref="Capabilities"/> structure.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_DEVSUBTYPE_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/hh405052%28v=vs.85%29.aspx
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/hh405050%28v=vs.85%29.aspx
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum DeviceSubType : byte
	{

		/// <summary>The controller type is unknown.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_UNKNOWN" )]
		Unknown = 0,

		/// <summary>Gamepad controller.
		/// Includes Left and Right Sticks, Left and Right Triggers, Directional Pad, and all standard buttons (A, B, X, Y, START, BACK, LB, RB, LSB, RSB).</summary>
		/// <remarks>The legacy version of XINPUT on Windows Vista (XInput 9.1.0) will always return a fixed subtype of <see cref="GamePad"/> regardless of attached device.</remarks>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_GAMEPAD" )]
		GamePad,

		/// <summary>Racing wheel controller.
		/// Left Stick X reports the wheel rotation, Right Trigger is the acceleration pedal, and Left Trigger is the brake pedal. Includes Directional Pad and most standard buttons (A, B, X, Y, START, BACK, LB, RB).
		/// LSB and RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_WHEEL" )]
		Wheel,

		/// <summary>Arcade stick controller.
		/// Includes a Digital Stick that reports as a DPAD (up, down, left, right), and most standard buttons (A, B, X, Y, START, BACK). The Left and Right Triggers are implemented as digital buttons and report either 0 or 0xFF.
		/// LB, LSB, RB, and RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_ARCADE_STICK" )]
		ArcadeStick,

		/// <summary>Flight stick controller.
		/// Includes a pitch and roll stick that reports as the Left Stick, a POV Hat which reports as the Right Stick, a rudder (handle twist or rocker) that reports as Left Trigger, and a throttle control as the Right Trigger. Includes support for a primary weapon (A), secondary weapon (B), and other standard buttons (X, Y, START, BACK).
		/// LB, LSB, RB, and RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_FLIGHT_STICK" )]
		FlightStick,

		/// <summary>Dance pad controller.
		/// Includes the Directional Pad and standard buttons (A, B, X, Y) on the pad, plus BACK and START.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_DANCE_PAD" )]
		DancePad,

		/// <summary>Guitar controller.
		/// The strum bar maps to DPAD (up and down), and the frets are assigned to A (green), B (red), Y (yellow), X (blue), and LB (orange).
		/// Right Stick Y is associated with a vertical orientation sensor; Right Stick X is the whammy bar.
		/// Includes support for BACK, START, DPAD (left, right).
		/// Left Trigger (pickup selector), Right Trigger, RB, LSB (fret modifier), RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_GUITAR" )]
		Guitar,

		/// <summary>Guitar controller.
		/// Supports a larger range of movement for the vertical orientation sensor.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE" )]
		GuitarAlternate,

		/// <summary>Drum controller.
		/// The drum pads are assigned to buttons: A for green (Floor Tom), B for red (Snare Drum), X for blue (Low Tom), Y for yellow (High Tom), and LB for the pedal (Bass Drum).
		/// Includes Directional-Pad, BACK, and START.
		/// RB, LSB, and RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_DRUM_KIT" )]
		DrumKit,

		/// <summary><see cref="GuitarBass"/> is identical to <see cref="Guitar"/>, with the distinct subtype to simplify setup.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_GUITAR_BASS" )]
		GuitarBass = 11,

		/// <summary>Arcade pad controller.
		/// Includes Directional Pad and most standard buttons (A, B, X, Y, START, BACK, LB, RB).
		/// The Left and Right Triggers are implemented as digital buttons and report either 0 or 0xFF.
		/// Left Stick, Right Stick, LSB, and RSB are optional.</summary>
		[Native( "XInput.h", "XINPUT_DEVSUBTYPE_ARCADE_PAD" )]
		ArcadePad = 19,

	}

}