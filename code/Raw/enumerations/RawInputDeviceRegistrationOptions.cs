using System;


namespace ManagedX.Input.Raw
{
	
	/// <summary>Enumerates options used when registering a raw input device.
	/// <para>This enumeration is equivalent to the native RIDEV_* constants.</para>
	/// </summary>
	[Flags]
	public enum RawInputDeviceRegistrationOptions : int
	{

		/// <summary>No option specified.</summary>
		None = 0x00000000,

		/// <summary>If set, this removes the top level collection (TLC) from the inclusion list.
		/// <para>This tells the operating system to stop reading from a device which matches the top level collection.</para>
		/// If Remove is set and the TargetWindowHandle member is not set to NULL, then parameter validation will fail.
		/// </summary>
		Remove = 0x00000001,

		/// <summary>If set, this specifies the top level collections (TLC) to exclude when reading a complete usage page.
		/// This flag only affects a TLC whose usage page is already specified with <see cref="RawInputDeviceRegistrationOptions.PageOnly"/>.
		/// </summary>
		Exclude = 0x00000010,

		/// <summary>If set, this specifies all devices whose top level collection (TLC) is from the specified UsagePage; note that Usage must be zero.
		/// To exclude a particular top level collection, use <see cref="RawInputDeviceRegistrationOptions.Exclude"/>.
		/// </summary>
		PageOnly = 0x00000020,

		/// <summary>If set, this prevents any devices specified by UsagePage or Usage from generating legacy messages.
		/// <para>This is only for the mouse and keyboard.</para>
		/// If this value is set for a mouse or a keyboard, the system does not generate any legacy message for that device for the application.
		/// For example, if the mouse TLC is set with NoLegacy, WM_LBUTTONDOWN and related legacy mouse messages are not generated.
		/// Likewise, if the keyboard TLC is set with NoLegacy, WM_KEYDOWN and related legacy keyboard messages are not generated.
		/// </summary>
		NoLegacy = Exclude | PageOnly,

		/// <summary>If set, this enables the caller to receive the input even when the caller is not in the foreground; note that TargetWindowHandle must be specified.</summary>
		InputSink = 0x00000100,

		/// <summary>If set, the mouse button click does not activate the other window.</summary>
		CaptureMouse = 0x00000200,

		/// <summary>If set, the application-defined keyboard device hotkeys are not handled.
		/// <para>However, the system hotkeys (for example, ALT+TAB and CTRL+ALT+DEL) are still handled. By default, all keyboard hotkeys are handled.</para>
		/// NoHotKeys can be specified even if <see cref="RawInputDeviceRegistrationOptions.NoLegacy"/> is not specified and TargetWindowHandle is NULL.
		/// </summary>
		NoHotKeys = CaptureMouse,

		/// <summary>If set, the application command keys are handled.
		/// AppKeys can be specified only if <see cref="RawInputDeviceRegistrationOptions.NoLegacy"/> is specified for a keyboard device.</summary>
		AppKeys = 0x00000400,

		/// <summary>If set, this enables the caller to receive input in the background only if the foreground application does not process it.
		/// <para>In other words, if the foreground application is not registered for raw input, then the background application that is registered will receive the input.</para>
		/// </summary>
		ExInputSink = 0x00001000,

		/// <summary>If set, this enables the caller to receive WindowMessage.InputDeviceChange (0x00FE) notifications for device arrival (WParam = 1) and device removal (WParam = 2).</summary>
		DevNotify = 0x00002000

	}

}