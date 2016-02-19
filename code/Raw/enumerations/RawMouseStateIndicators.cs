using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.Raw
{

	/// <summary>Enumerates raw mouse state flags.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "MoveRelative is the zero value." )]
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[Flags]
	public enum RawMouseStateIndicators : short
	{

		/// <summary>Mouse movement data is relative to the last mouse position.</summary>
		MoveRelative = 0x0000,

		/// <summary>Mouse movement data is based on absolute position.</summary>
		MoveAbsolute = 0x0001,

		/// <summary>Mouse coordinates are mapped to the virtual desktop (for a multiple monitor system).</summary>
		VirtualDesktop = 0x0002,

		/// <summary>Mouse attributes changed; application needs to query the mouse attributes.</summary>
		AttributesChanged = 0x0004
	
	}

}