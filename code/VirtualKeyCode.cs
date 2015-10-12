using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{

	
	/// <summary>Enumerates virtual key codes (including XInput ones).</summary>
	public enum VirtualKeyCode : int
	{

		/// <summary>No key/button.</summary>
		None = 0x0000,


		#region Mouse

		/// <summary>The left mouse button.</summary>
		MouseLeft = 0x0001,

		/// <summary>The right mouse button.</summary>
		MouseRight = 0x0002,

		/// <summary>The middle mouse button.</summary>
		MouseMiddle = 0x0004,

		/// <summary>The extended mouse button 1.</summary>
		MouseX1 = 0x0005,

		/// <summary>The extended mouse button 2.</summary>
		MouseX2 = 0x0006,

		#endregion

		
		#region Keyboard

		// TODO !

		#endregion


		#region XInput controller (Gamepad*)

		// XInput.h

		/// <summary>The A (cross) button.</summary>
		GamepadA = 0x5800,
		
		/// <summary>The B (circle) button.</summary>
		GamepadB = 0x5801,
		
		/// <summary>The X (square) button.</summary>
		GamepadX = 0x5802,
		
		/// <summary>The Y (triangle) button.</summary>
		GamepadY = 0x5803,


		/// <summary>The right bumper (R1).</summary>
		GamepadRightShoulder = 0x5804,
		
		/// <summary>The left bumper (L1).</summary>
		GamepadLeftShoulder = 0x5805,


		/// <summary>The left trigger (L2).</summary>
		GamepadLeftTrigger = 0x5806,
		
		/// <summary>The right trigger (R2).</summary>
		GamepadRightTrigger = 0x5807,


		/// <summary>The directional pad up.</summary>
		GamepadDPadUp = 0x5810,
		
		/// <summary>The directional pad down.</summary>
		GamepadDPadDown = 0x5811,
		
		/// <summary>The directional pad left.</summary>
		GamepadDPadLeft = 0x5812,
		
		/// <summary>The directional pad right.</summary>
		GamepadDPadRight = 0x5813,


		/// <summary>The Start button.</summary>
		GamepadStart = 0x5814,
		
		/// <summary>The Back (Select) button.</summary>
		GamepadBack = 0x5815,

		
		/// <summary>The left stick button.</summary>
		GamepadLeftThumb = 0x5816,
		
		/// <summary>The right stick button.</summary>
		GamepadRightThumb = 0x5817,

		
		/// <summary>The left stick up direction.</summary>
		GamepadLeftThumbUp = 0x5820,
		
		/// <summary>The left stick down direction.</summary>
		GamepadLeftThumbDown = 0x5821,
		
		/// <summary>The left stick right direction.</summary>
		GamepadLeftThumbRight = 0x5822,
		
		/// <summary>The left stick left direction.</summary>
		GamepadLeftThumbLeft = 0x5823,
		
		/// <summary>The left stick up left direction.</summary>
		GamepadLeftThumbUpLeft = 0x5824,
		
		/// <summary>The left stick up right direction.</summary>
		GamepadLeftThumbUpRight = 0x5825,
		
		/// <summary>The left stick down right direction.</summary>
		GamepadLeftThumbDownRight = 0x5826,
		
		/// <summary>The left stick down left direction.</summary>
		GamepadLeftThumbDownLeft = 0x5827,

		
		/// <summary>The right stick up direction.</summary>
		GamepadRightThumbUp = 0x5830,
		
		/// <summary>The right stick down direction.</summary>
		GamepadRightThumbDown = 0x5831,
		
		/// <summary>The right stick right direction.</summary>
		GamepadRightThumbRight = 0x5832,
		
		/// <summary>The right stick left direction.</summary>
		GamepadRightThumbLeft = 0x5833,
		
		/// <summary>The right stick up left direction.</summary>
		GamepadRightThumbUpLeft = 0x5834,
		
		/// <summary>The right stick up right direction.</summary>
		GamepadRightThumbUpRight = 0x5835,
		
		/// <summary>The right stick down right direction.</summary>
		GamepadRightThumbDownRight = 0x5836,
		
		/// <summary>The right stick down left direction.</summary>
		GamepadRightThumbDownLeft = 0x5837

		#endregion

	}

}