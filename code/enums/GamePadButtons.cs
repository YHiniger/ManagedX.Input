using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx


	/// <summary>Enumerates XInput gamepad buttons.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[SuppressMessage( "Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags" )]
	[Flags]
	public enum GamePadButtons : short
	{
		
		/// <summary>No button.</summary>
		None = 0x0000,

		/// <summary>Directional pad up.</summary>
		DPadUp = 0x0001,

		/// <summary>Directional pad down.</summary>
		DPadDown = 0x0002,

		/// <summary>Directional pad left.</summary>
		DPadLeft = 0x0004,

		/// <summary>Directional pad right.</summary>
		DPadRight = 0x0008,

		/// <summary>Start button.</summary>
		Start = 0x0010,

		/// <summary>Back button.</summary>
		Back = 0x0020,

		/// <summary>Left thumb.</summary>
		LeftThumb = 0x0040,

		/// <summary>Right thumb.</summary>
		RightThumb = 0x0080,

		/// <summary>Left shoulder.</summary>
		LeftShoulder = 0x0100,

		/// <summary>Right shoulder.</summary>
		RightShoulder = 0x0200,


		/// <summary>The "big" button.</summary>
		BigButton = 0x0800,


		/// <summary>The A button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = 0x1000,

		/// <summary>The B button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = 0x2000,

		/// <summary>The X button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = 0x4000,

		/// <summary>The Y button.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = unchecked( (short)0x8000 )

	}

}