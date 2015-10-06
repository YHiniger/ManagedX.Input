using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h

	
	/// <summary>Enumerates XInput virtual key codes.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum VirtualKeyCode : short
	{

		/// <summary>No key.</summary>
		None = 0x0000,

		/// <summary>The A (cross) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = 0x5800,
		/// <summary>The B (circle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = 0x5801,
		/// <summary>The X (square) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = 0x5802,
		/// <summary>The Y (triangle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = 0x5803,

		/// <summary>The right shoulder (R1).</summary>
		RightShoulder = 0x5804,
		/// <summary>The left shoulder (L1).</summary>
		LeftShoulder = 0x5805,

		/// <summary>The left trigger (L2).</summary>
		LeftTrigger = 0x5806,
		/// <summary>The right trigger (R2).</summary>
		RightTrigger = 0x5807,

		/// <summary>The directional pad up.</summary>
		DPadUp = 0x5810,
		/// <summary>The directional pad down.</summary>
		DPadDown = 0x5811,
		/// <summary>The directional pad left.</summary>
		DPadLeft = 0x5812,
		/// <summary>The directional pad right.</summary>
		DPadRight = 0x5813,

		/// <summary>The Start button.</summary>
		Start = 0x5814,
		/// <summary>The Back (Select) button.</summary>
		Back = 0x5815,

		/// <summary>The left thumb.</summary>
		LeftThumb = 0x5816,
		/// <summary>The right thumb.</summary>
		RightThumb = 0x5817,

		/// <summary>The left thumbstick up direction.</summary>
		LeftThumbUp = 0x5820,
		/// <summary>The left thumbstick down direction.</summary>
		LeftThumbDown = 0x5821,
		/// <summary>The left thumbstick right direction.</summary>
		LeftThumbRight = 0x5822,
		/// <summary>The left thumbstick left direction.</summary>
		LeftThumbLeft = 0x5823,
		/// <summary>The left thumbstick up left direction.</summary>
		LeftThumbUpLeft = 0x5824,
		/// <summary>The left thumbstick up right direction.</summary>
		LeftThumbUpRight = 0x5825,
		/// <summary>The left thumbstick down right direction.</summary>
		LeftThumbDownRight = 0x5826,
		/// <summary>The left thumbstick down left direction.</summary>
		LeftThumbDownLeft = 0x5827,

		/// <summary>The right thumbstick up direction.</summary>
		RightThumbUp = 0x5830,
		/// <summary>The right thumbstick down direction.</summary>
		RightThumbDown = 0x5831,
		/// <summary>The right thumbstick right direction.</summary>
		RightThumbRight = 0x5832,
		/// <summary>The right thumbstick left direction.</summary>
		RightThumbLeft = 0x5833,
		/// <summary>The right thumbstick up left direction.</summary>
		RightThumbUpLeft = 0x5834,
		/// <summary>The right thumbstick up right direction.</summary>
		RightThumbUpRight = 0x5835,
		/// <summary>The right thumbstick down right direction.</summary>
		RightThumbDownRight = 0x5836,
		/// <summary>The right thumbstick down left direction.</summary>
		RightThumbDownLeft = 0x5837

	}

}