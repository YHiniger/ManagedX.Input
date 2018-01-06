namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Commands used by the GetRawInputData function.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645596(v=vs.85).aspx</remarks>
	internal enum GetDataCommand : int
	{

		/// <summary>Get the raw data from the <see cref="RawInput"/> structure.</summary>
		[Source( "WinUser.h", "RID_INPUT" )]
		Input = 0x10000003,

		/// <summary>Get the header information from the <see cref="RawInput"/> structure.</summary>
		[Source( "WinUser.h", "RID_HEADER" )]
		Header = 0x10000005

	}

}