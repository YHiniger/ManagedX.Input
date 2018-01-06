using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Scan code characteristics, used in the <see cref="RawKeyboard"/> structure.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645575(v=vs.85).aspx</remarks>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue" )]
	[Flags]
	public enum ScanCodeCharacteristics : short
	{

		/// <summary>The key is down.</summary>
		[Source( "WinUser.h", "RI_KEY_MAKE" )]
		Make = 0x0000,

		/// <summary>The key is up.</summary>
		[Source( "WinUser.h", "RI_KEY_BREAK" )]
		Break = 0x0001,

		/// <summary>The scan code has the 0xE0** prefix.</summary>
		[Source( "WinUser.h", "RI_KEY_E0" )]
		E0 = 0x0002,

		/// <summary>The scan code has the 0xE1** prefix.</summary>
		[Source( "WinUser.h", "RI_KEY_E1" )]
		E1 = 0x0004,


		/// <summary></summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LED" )]
		[Source( "WinUser.h", "RI_KEY_TERMSRV_SET_LED" )]
		TerminalServerSetLED = 0x0008,

		/// <summary></summary>
		[Source( "WinUser.h", "RI_KEY_TERMSRV_SHADOW" )]
		TerminalServerShadow = 0x0010,

	}

}