using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates XInput gamepad buttons.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_GAMEPAD_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx</remarks>
	[Flags]
	internal enum GamepadButtons : ushort
	{

		/// <summary>No buttons specified.</summary>
		None = 0x0000,

		/// <summary>Directional pad up.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_DPAD_UP" )]
		DPadUp = 0x0001,

		/// <summary>Directional pad down.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_DPAD_DOWN" )]
		DPadDown = 0x0002,

		/// <summary>Directional pad left.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_DPAD_LEFT" )]
		DPadLeft = 0x0004,

		/// <summary>Directional pad right.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_DPAD_RIGHT" )]
		DPadRight = 0x0008,

		/// <summary>Start button.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_START" )]
		Start = 0x0010,

		/// <summary>Back button.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_BACK" )]
		Back = 0x0020,

		/// <summary>Left thumb.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_LEFT_THUMB" )]
		LeftThumb = 0x0040,

		/// <summary>Right thumb.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_RIGHT_THUMB" )]
		RightThumb = 0x0080,

		/// <summary>Left shoulder.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_LEFT_SHOULDER" )]
		LeftShoulder = 0x0100,

		/// <summary>Right shoulder.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_RIGHT_SHOULDER" )]
		RightShoulder = 0x0200,


		/// <summary>The "big" button.</summary>
		BigButton = 0x0800,


		/// <summary>The A button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		[Source( "XInput.h", "XINPUT_GAMEPAD_A" )]
		A = 0x1000,

		/// <summary>The B button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		[Source( "XInput.h", "XINPUT_GAMEPAD_B" )]
		B = 0x2000,

		/// <summary>The X button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		[Source( "XInput.h", "XINPUT_GAMEPAD_X" )]
		X = 0x4000,

		/// <summary>The Y button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		[Source( "XInput.h", "XINPUT_GAMEPAD_Y" )]
		Y = 0x8000

	}

}