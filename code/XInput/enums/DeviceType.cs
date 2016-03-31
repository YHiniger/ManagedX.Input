namespace ManagedX.Input.XInput
{

	/// <summary>Enumerates XInput game controller device types, used in the <see cref="Capabilities"/> structure.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_DEVTYPE_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities%28v=vs.85%29.aspx</remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum DeviceType : byte
	{

		/// <summary>Undefined.</summary>
		Undefined = 0,

		/// <summary>Game controller.</summary>
		[Win32.Native( "XInput.h", "XINPUT_DEVTYPE_GAMEPAD" )]
		Gamepad = 1,

		// ? = 2,

		/// <summary>No documentation.</summary>
		BigButtonPad = 3,

	}

}