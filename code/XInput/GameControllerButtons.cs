using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{
	using XInput;


	/// <summary>XInput game controller buttons.</summary>
	[Flags]
	public enum GameControllerButtons : int
	{

		/// <summary>No buttons specified.</summary>
		None = GamepadButtons.None,

		/// <summary>Directional pad up.</summary>
		DPadUp = GamepadButtons.DPadUp,

		/// <summary>Directional pad down.</summary>
		DPadDown = GamepadButtons.DPadDown,

		/// <summary>Directional pad left.</summary>
		DPadLeft = GamepadButtons.DPadLeft,

		/// <summary>Directional pad right.</summary>
		DPadRight = GamepadButtons.DPadRight,

		/// <summary>Start button.</summary>
		Start = GamepadButtons.Start,

		/// <summary>Back button.</summary>
		Back = GamepadButtons.Back,

		/// <summary>Left thumb.</summary>
		LeftThumb = GamepadButtons.LeftThumb,

		/// <summary>Right thumb.</summary>
		RightThumb = GamepadButtons.RightThumb,

		/// <summary>Left shoulder.</summary>
		LeftShoulder = GamepadButtons.LeftShoulder,

		/// <summary>Right shoulder.</summary>
		RightShoulder = GamepadButtons.RightShoulder,


		/// <summary>The "big" button.</summary>
		BigButton = GamepadButtons.BigButton,


		/// <summary>The A button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = GamepadButtons.A,

		/// <summary>The B button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = GamepadButtons.B,

		/// <summary>The X button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = GamepadButtons.X,

		/// <summary>The Y button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = GamepadButtons.Y

	}

}