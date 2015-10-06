using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities%28v=vs.85%29.aspx


	/// <summary>Enumerates mask values (XINPUT_CAPS_*) used in the <see cref="Capabilities"/> structure.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[Flags]
	public enum Caps : short
	{

		/// <summary>None.</summary>
		None = 0x0000,

		/// <summary>The device supports force feedback functionality.
		/// <para>Note that these force-feedback features beyond rumble are not currently supported through XInput on Windows.</para>
		/// </summary>
		ForceFeedbackSupported = 0x0001,

		/// <summary>The device is wireless.
		/// <para>Only supported on Windows 8.</para>
		/// </summary>
		Wireless = 0x0002,

		/// <summary>The device has an integrated voice device.</summary>
		VoiceSupported = 0x0004,

		/// <summary>The device supports plug-in modules.
		/// <para>Note that plug-in modules like the text input device (TID) are not supported currently through XInput on Windows.</para>
		/// </summary>
		PluginModuleDeviceSupported = 0x0008,

		/// <summary>The device lacks menu navigation buttons (START, BACK, DPAD).</summary>
		NoNavigation = 0x0010

	}


}