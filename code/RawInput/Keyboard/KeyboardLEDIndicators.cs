using System;


namespace ManagedX.Input
{

	/// <summary><see cref="Keyboard"/> LED indicators.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LED", Justification = "LED = Light-Emitting Diode" )]
	[Flags]
	public enum KeyboardLEDIndicators : int
	{

		/// <summary>No LED indicators.</summary>
		None = 0x00000000,

		/// <summary>Num lock LED is active.</summary>
		NumLock = 0x00000001,

		/// <summary>Caps lock LED is active.</summary>
		CapsLock = 0x00000002,

		/// <summary>Scroll lock LED is active.</summary>
		ScrollLock = 0x00000004

	}

}