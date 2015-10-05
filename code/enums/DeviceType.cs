namespace ManagedX.Input.XInput
{

	// XInput.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities%28v=vs.85%29.aspx


	/// <summary>Enumerates XInput game controller device types (XINPUT_DEVTYPE_*), used in the <see cref="Capabilities"/> structure.</summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	public enum DeviceType : byte
	{

		/// <summary>Undefined.</summary>
		Undefined = 0,

		/// <summary>Game controller.</summary>
		GamePad = 1,

		// ? = 2,

		/// <summary>No documentation.</summary>
		BigButtonPad = 3

	}

}