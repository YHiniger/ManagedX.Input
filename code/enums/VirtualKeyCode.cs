using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h

	
	/// <summary></summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum VirtualKeyCode : short
	{

		/// <summary></summary>
		None = 0x0000,

		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = 0x5800,
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = 0x5801,
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = 0x5802,
		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = 0x5803,

		/// <summary></summary>
		RightShoulder = 0x5804,
		/// <summary></summary>
		LeftShoulder = 0x5805,

		/// <summary></summary>
		LeftTrigger = 0x5806,
		/// <summary></summary>
		RightTrigger = 0x5807,

		/// <summary></summary>
		DPadUp = 0x5810,
		/// <summary></summary>
		DPadDown = 0x5811,
		/// <summary></summary>
		DPadLeft = 0x5812,
		/// <summary></summary>
		DPadRight = 0x5813,

		/// <summary></summary>
		Start = 0x5814,
		/// <summary></summary>
		Back = 0x5815,

		/// <summary></summary>
		LeftThumb = 0x5816,
		/// <summary></summary>
		RightThumb = 0x5817,

		/// <summary></summary>
		LeftThumbUp = 0x5820,
		/// <summary></summary>
		LeftThumbDown = 0x5821,
		/// <summary></summary>
		LeftThumbRight = 0x5822,
		/// <summary></summary>
		LeftThumbLeft = 0x5823,
		/// <summary></summary>
		LeftThumbUpLeft = 0x5824,
		/// <summary></summary>
		LeftThumbUpRight = 0x5825,
		/// <summary></summary>
		LeftThumbDownRight = 0x5826,
		/// <summary></summary>
		LeftThumbDownLeft = 0x5827,

		/// <summary></summary>
		RightThumbUp = 0x5830,
		/// <summary></summary>
		RightThumbDown = 0x5831,
		/// <summary></summary>
		RightThumbRight = 0x5832,
		/// <summary></summary>
		RightThumbLeft = 0x5833,
		/// <summary></summary>
		RightThumbUpLeft = 0x5834,
		/// <summary></summary>
		RightThumbUpRight = 0x5835,
		/// <summary></summary>
		RightThumbDownRight = 0x5836,
		/// <summary></summary>
		RightThumbDownLeft = 0x5837

	}

}
