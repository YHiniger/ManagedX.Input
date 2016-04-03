using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using Win32;
	using VKeyCode = Input.VirtualKeyCode;


	/// <summary>Enumerates XInput virtual key codes.
	/// <para>This enumeration is a subset of the <see cref="Input.VirtualKeyCode"/> enumeration,
	/// and is equivalent to the native <code>VK_PAD_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum VirtualKeyCode : short
	{

		/// <summary>No key.</summary>
		None = VKeyCode.None,


		/// <summary>The A (cross) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		[Native( "XInput.h", "VK_PAD_A" )]
		A = VKeyCode.GamepadA,
		
		/// <summary>The B (circle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		[Native( "XInput.h", "VK_PAD_B" )]
		B = VKeyCode.GamepadB,
		
		/// <summary>The X (square) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		[Native( "XInput.h", "VK_PAD_X" )]
		X = VKeyCode.GamepadX,
		
		/// <summary>The Y (triangle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		[Native( "XInput.h", "VK_PAD_Y" )]
		Y = VKeyCode.GamepadY,


		/// <summary>The right bumper (R1).</summary>
		[Native( "XInput.h", "VK_PAD_RSHOULDER" )]
		RightShoulder = VKeyCode.GamepadRightShoulder,
		
		/// <summary>The left bumper (L1).</summary>
		[Native( "XInput.h", "VK_PAD_LSHOULDER" )]
		LeftShoulder = VKeyCode.GamepadLeftShoulder,


		/// <summary>The left trigger (L2).</summary>
		[Native( "XInput.h", "VK_PAD_LTRIGGER" )]
		LeftTrigger = VKeyCode.GamepadLeftTrigger,
		
		/// <summary>The right trigger (R2).</summary>
		[Native( "XInput.h", "VK_PAD_RTRIGGER" )]
		RightTrigger = VKeyCode.GamepadRightTrigger,


		/// <summary>The directional pad up.</summary>
		[Native( "XInput.h", "VK_PAD_DPAD_UP" )]
		DPadUp = VKeyCode.GamepadDPadUp,
		
		/// <summary>The directional pad down.</summary>
		[Native( "XInput.h", "VK_PAD_DPAD_DOWN" )]
		DPadDown = VKeyCode.GamepadDPadDown,
		
		/// <summary>The directional pad left.</summary>
		[Native( "XInput.h", "VK_PAD_DPAD_LEFT" )]
		DPadLeft = VKeyCode.GamepadDPadLeft,
		
		/// <summary>The directional pad right.</summary>
		[Native( "XInput.h", "VK_PAD_DPAD_RIGHT" )]
		DPadRight = VKeyCode.GamepadDPadRight,


		/// <summary>The Start button.</summary>
		[Native( "XInput.h", "VK_PAD_START" )]
		Start = VKeyCode.GamepadStart,
		
		/// <summary>The Back (Select) button.</summary>
		[Native( "XInput.h", "VK_PAD_BACK" )]
		Back = VKeyCode.GamepadBack,

		
		/// <summary>The left stick button.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_PRESS" )]
		LeftThumb = VKeyCode.GamepadLeftThumb,
		
		/// <summary>The right stick button.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_PRESS" )]
		RightThumb = VKeyCode.GamepadRightThumb,

		
		/// <summary>The left stick up direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_UP" )]
		LeftThumbUp = VKeyCode.GamepadLeftThumbUp,
		
		/// <summary>The left stick down direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_DOWN" )]
		LeftThumbDown = VKeyCode.GamepadLeftThumbDown,
		
		/// <summary>The left stick right direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_RIGHT" )]
		LeftThumbRight = VKeyCode.GamepadLeftThumbRight,
		
		/// <summary>The left stick left direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_LEFT" )]
		LeftThumbLeft = VKeyCode.GamepadLeftThumbLeft,
		
		/// <summary>The left stick up left direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_UPLEFT" )]
		LeftThumbUpLeft = VKeyCode.GamepadLeftThumbUpLeft,

        /// <summary>The left stick up right direction.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "UpRight")]
        [Native( "XInput.h", "VK_PAD_LTHUMB_UPRIGHT" )]
		LeftThumbUpRight = VKeyCode.GamepadLeftThumbUpRight,

        /// <summary>The left stick down right direction.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DownRight")]
        [Native( "XInput.h", "VK_PAD_LTHUMB_DOWNRIGHT" )]
		LeftThumbDownRight = VKeyCode.GamepadLeftThumbDownRight,
		
		/// <summary>The left stick down left direction.</summary>
		[Native( "XInput.h", "VK_PAD_LTHUMB_DOWNLEFT" )]
		LeftThumbDownLeft = VKeyCode.GamepadLeftThumbDownLeft,

		
		/// <summary>The right stick up direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_UP" )]
		RightThumbUp = VKeyCode.GamepadRightThumbUp,
		
		/// <summary>The right stick down direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_DOWN" )]
		RightThumbDown = VKeyCode.GamepadRightThumbDown,
		
		/// <summary>The right stick right direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_RIGHT" )]
		RightThumbRight = VKeyCode.GamepadRightThumbRight,
		
		/// <summary>The right stick left direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_LEFT" )]
		RightThumbLeft = VKeyCode.GamepadRightThumbLeft,
		
		/// <summary>The right stick up left direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_UPLEFT" )]
		RightThumbUpLeft = VKeyCode.GamepadRightThumbUpLeft,

        /// <summary>The right stick up right direction.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "UpRight")]
        [Native( "XInput.h", "VK_PAD_RTHUMB_UPRIGHT" )]
		RightThumbUpRight = VKeyCode.GamepadRightThumbUpRight,

        /// <summary>The right stick down right direction.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DownRight")]
        [Native( "XInput.h", "VK_PAD_RTHUMB_DOWNRIGHT" )]
		RightThumbDownRight = VKeyCode.GamepadRightThumbDownRight,
		
		/// <summary>The right stick down left direction.</summary>
		[Native( "XInput.h", "VK_PAD_RTHUMB_DOWNLEFT" )]
		RightThumbDownLeft = VKeyCode.GamepadRightThumbDownLeft,

	}

}