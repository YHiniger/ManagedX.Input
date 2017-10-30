namespace ManagedX.Input.XInput
{

	internal enum XInputVersion
	{

		/// <summary>XInput is not supported.</summary>
		NotSupported,

		/// <summary>XInput 1.3, for Windows 7; requires the DirectX End-User Runtime (June 2010).</summary>
		XInput13,
		
		/// <summary>XInput 1.4, available on Windows 8 and later.</summary>
		XInput14,

	}

}