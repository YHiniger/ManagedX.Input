namespace ManagedX.Input
{
	using Win32;


	/// <summary>Enumerates mouse cursor states.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "There is: Hidden." )]
	[Native( "WinUser.h" )]
	[System.Flags]
	public enum MouseCursorStateIndicators : int
	{

		/// <summary>The cursor is hidden.</summary>
		Hidden = 0x00000000,

		/// <summary>The cursor is showing.</summary>
		[Native( "WinUser.h", "CURSOR_SHOWING" )]
		Showing = 0x00000001,

		/// <summary>Windows 8: The cursor is suppressed.
		/// <para>This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.</para>
		/// </summary>
		[Native( "WinUser.h", "CURSOR_SUPPRESSED" )]
		Suppressed = 0x00000002

	}

}