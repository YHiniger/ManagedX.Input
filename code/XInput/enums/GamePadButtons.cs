using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates XInput gamepad buttons.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_GAMEPAD_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[SuppressMessage( "Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags" )]
	[Flags]
	public enum GamePadButtons : short
	{
		
		/// <summary>No button.</summary>
		None = 0x0000,

		/// <summary>Directional pad up.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_DPAD_UP" )]
		DPadUp = 0x0001,

		/// <summary>Directional pad down.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_DPAD_DOWN" )]
		DPadDown = 0x0002,

		/// <summary>Directional pad left.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_DPAD_LEFT" )]
		DPadLeft = 0x0004,

		/// <summary>Directional pad right.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_DPAD_RIGHT" )]
		DPadRight = 0x0008,

		/// <summary>Start button.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_START" )]
		Start = 0x0010,

		/// <summary>Back button.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_BACK" )]
		Back = 0x0020,

		/// <summary>Left thumb.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_LEFT_THUMB" )]
		LeftThumb = 0x0040,

		/// <summary>Right thumb.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_RIGHT_THUMB" )]
		RightThumb = 0x0080,

		/// <summary>Left shoulder.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_LEFT_SHOULDER" )]
		LeftShoulder = 0x0100,

		/// <summary>Right shoulder.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_RIGHT_SHOULDER" )]
		RightShoulder = 0x0200,


		/// <summary>The "big" button.</summary>
		BigButton = 0x0800,


		/// <summary>The A button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		[Native( "XInput.h", "XINPUT_GAMEPAD_A" )]
		A = 0x1000,

		/// <summary>The B button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		[Native( "XInput.h", "XINPUT_GAMEPAD_B" )]
		B = 0x2000,

		/// <summary>The X button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		[Native( "XInput.h", "XINPUT_GAMEPAD_X" )]
		X = 0x4000,

		/// <summary>The Y button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		[Native( "XInput.h", "XINPUT_GAMEPAD_Y" )]
		Y = unchecked( (short)0x8000 )

	}

}