namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates mask values used in the <see cref="Capabilities"/> structure.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_CAPS_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[System.Flags]
	public enum Caps : short
	{

		/// <summary>None.</summary>
		None = 0x0000,

		/// <summary>The device supports force feedback functionality.
		/// <para>Note that these force-feedback features beyond rumble are not currently supported through XInput on Windows.</para>
		/// </summary>
		[Native( "XInput.h", "XINPUT_CAPS_FFB_SUPPORTED" )]
		ForceFeedbackSupported = 0x0001,

		/// <summary>The device is wireless.
		/// <para>Only supported on Windows 8.</para>
		/// </summary>
		[Native( "XInput.h", "XINPUT_CAPS_WIRELESS" )]
		Wireless = 0x0002,

		/// <summary>The device has an integrated voice device.</summary>
		[Native( "XInput.h", "XINPUT_CAPS_VOICE_SUPPORTED" )]
		VoiceSupported = 0x0004,

		/// <summary>The device supports plug-in modules.
		/// <para>Note that plug-in modules like the text input device (TID) are not supported currently through XInput on Windows.</para>
		/// </summary>
		[Native( "XInput.h", "XINPUT_CAPS_PMD_SUPPORTED" )]
		PluginModuleDeviceSupported = 0x0008,

		/// <summary>The device lacks menu navigation buttons (START, BACK, DPAD).</summary>
		[Native( "XInput.h", "XINPUT_CAPS_NO_NAVIGATION" )]
		NoNavigation = 0x0010,

	}

}