using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using VKeyCode = ManagedX.Input.VirtualKeyCode;


	// XInput.h

	
	/// <summary>Enumerates XInput virtual key codes.
	/// <para>This enumeration is equivalent to the native <code>BATTERY_DEVTYPE_*</code> constants (defined in XInput.h).</para>
	/// This enumeration is a subset of the <see cref="ManagedX.Input.VirtualKeyCode"/> enumeration.
	/// </summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum VirtualKeyCode : short
	{

		/// <summary>No key.</summary>
		None = VKeyCode.None,


		/// <summary>The A (cross) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = VKeyCode.GamepadA,
		
		/// <summary>The B (circle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = VKeyCode.GamepadB,
		
		/// <summary>The X (square) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = VKeyCode.GamepadX,
		
		/// <summary>The Y (triangle) button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = VKeyCode.GamepadY,


		/// <summary>The right bumper (R1).</summary>
		RightShoulder = VKeyCode.GamepadRightShoulder,
		
		/// <summary>The left bumper (L1).</summary>
		LeftShoulder = VKeyCode.GamepadLeftShoulder,


		/// <summary>The left trigger (L2).</summary>
		LeftTrigger = VKeyCode.GamepadLeftTrigger,
		
		/// <summary>The right trigger (R2).</summary>
		RightTrigger = VKeyCode.GamepadRightTrigger,


		/// <summary>The directional pad up.</summary>
		DPadUp = VKeyCode.GamepadDPadUp,
		
		/// <summary>The directional pad down.</summary>
		DPadDown = VKeyCode.GamepadDPadDown,
		
		/// <summary>The directional pad left.</summary>
		DPadLeft = VKeyCode.GamepadDPadLeft,
		
		/// <summary>The directional pad right.</summary>
		DPadRight = VKeyCode.GamepadDPadRight,


		/// <summary>The Start button.</summary>
		Start = VKeyCode.GamepadStart,
		
		/// <summary>The Back (Select) button.</summary>
		Back = VKeyCode.GamepadBack,

		
		/// <summary>The left stick button.</summary>
		LeftThumb = VKeyCode.GamepadLeftThumb,
		
		/// <summary>The right stick button.</summary>
		RightThumb = VKeyCode.GamepadRightThumb,

		
		/// <summary>The left stick up direction.</summary>
		LeftThumbUp = VKeyCode.GamepadLeftThumbUp,
		
		/// <summary>The left stick down direction.</summary>
		LeftThumbDown = VKeyCode.GamepadLeftThumbDown,
		
		/// <summary>The left stick right direction.</summary>
		LeftThumbRight = VKeyCode.GamepadLeftThumbRight,
		
		/// <summary>The left stick left direction.</summary>
		LeftThumbLeft = VKeyCode.GamepadLeftThumbLeft,
		
		/// <summary>The left stick up left direction.</summary>
		LeftThumbUpLeft = VKeyCode.GamepadLeftThumbUpLeft,
		
		/// <summary>The left stick up right direction.</summary>
		LeftThumbUpRight = VKeyCode.GamepadLeftThumbUpRight,
		
		/// <summary>The left stick down right direction.</summary>
		LeftThumbDownRight = VKeyCode.GamepadLeftThumbDownRight,
		
		/// <summary>The left stick down left direction.</summary>
		LeftThumbDownLeft = VKeyCode.GamepadLeftThumbDownLeft,

		
		/// <summary>The right stick up direction.</summary>
		RightThumbUp = VKeyCode.GamepadRightThumbUp,
		
		/// <summary>The right stick down direction.</summary>
		RightThumbDown = VKeyCode.GamepadRightThumbDown,
		
		/// <summary>The right stick right direction.</summary>
		RightThumbRight = VKeyCode.GamepadRightThumbRight,
		
		/// <summary>The right stick left direction.</summary>
		RightThumbLeft = VKeyCode.GamepadRightThumbLeft,
		
		/// <summary>The right stick up left direction.</summary>
		RightThumbUpLeft = VKeyCode.GamepadRightThumbUpLeft,
		
		/// <summary>The right stick up right direction.</summary>
		RightThumbUpRight = VKeyCode.GamepadRightThumbUpRight,
		
		/// <summary>The right stick down right direction.</summary>
		RightThumbDownRight = VKeyCode.GamepadRightThumbDownRight,
		
		/// <summary>The right stick down left direction.</summary>
		RightThumbDownLeft = VKeyCode.GamepadRightThumbDownLeft

	}

}