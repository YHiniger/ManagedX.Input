using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Enumerates raw mouse state flags.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "MoveRelative is the zero value." )]
	[Flags]
	public enum RawMouseStateIndicators : int
	{

		/// <summary>Mouse movement data is relative to the last mouse position.</summary>
		[Source( "WinUser.h", "MOUSE_MOVE_RELATIVE" )]
		MoveRelative = 0x0000,

		/// <summary>Mouse movement data is based on absolute position.</summary>
		[Source( "WinUser.h", "MOUSE_MOVE_ABSOLUTE" )]
		MoveAbsolute = 0x0001,

		/// <summary>Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).</summary>
		[Source( "WinUser.h", "MOUSE_VIRTUAL_DESKTOP" )]
		VirtualDesktop = 0x0002,

		/// <summary>Mouse attributes changed; application needs to query the mouse attributes.</summary>
		[Source( "WinUser.h", "MOUSE_ATTRIBUTES_CHANGED" )]
		AttributesChanged = 0x0004,

		/// <summary>Do not coalesce mouse moves.</summary>
		[Source( "WinUser.h", "MOUSE_MOVE_NOCOALESCE" )]
		NoCoalesce = 0x0008,

	}

}