using System;


namespace ManagedX.Input.XInput
{

	internal enum XInputVersion
	{

		/// <summary>XInput is not supported.</summary>
		NotSupported,

		/// <summary>XInput 1.3, for Windows Vista and 7; requires the DirectX End-User Runtime (June 2010).</summary>
		XInput13,
		
		/// <summary>XInput 1.4, available on Windows 8 and later.</summary>
		XInput14,

		/// <summary>XInput 1.5, available on Windows 10 and later.</summary>
		XInput15

	}

}