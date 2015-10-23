using System;


namespace ManagedX.Input.Raw
{
	
	/// <summary>Enumerates commands used by the GetRawInputData function.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue" )]
	internal enum GetDataCommand : int
	{

		/// <summary>Get the raw data from the <see cref="RawInput"/> structure.</summary>
		Input = 0x10000003,

		/// <summary>Get the header information from the <see cref="RawInput"/> structure.</summary>
		Header = 0x10000005

	}

}
