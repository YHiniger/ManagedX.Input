namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Enumerates commands used by the GetRawInputDeviceInfo function.</summary>
	internal enum GetInfoCommand : int
	{

		/// <summary>Data points to the previously parsed data.</summary>
		[Source( "WinUser.h", "RIDI_PREPARSEDDATA" )]
		PreParsedData = 0x20000005,

		/// <summary>Data points to a string that contains the device name.
		/// <para>For this command only, the value in size is the character count (not the byte count).</para>
		/// </summary>
		[Source( "WinUser.h", "RIDI_DEVICENAME" )]
		DeviceName = 0x20000007,

		/// <summary>Data points to a <see cref="DeviceInfo"/> structure.</summary>
		[Source( "WinUser.h", "RIDI_DEVICEINFO" )]
		DeviceInfo = 0x2000000B

	}

}