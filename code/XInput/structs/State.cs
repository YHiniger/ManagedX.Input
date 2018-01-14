using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Contains the state of an XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_STATE</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_state%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "XInput.h", "XINPUT_STATE" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct State
	{

		/// <summary>The state packet number.
		/// <para>The packet number indicates whether there have been any changes in the state of the controller.</para>
		/// If the packet number [...] is the same in sequentially returned <see cref="State"/> structures, the controller state has not changed.
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int PacketNumber;

		/// <summary>A <see cref="Gamepad"/> structure containing the state of an XInput Controller.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Gamepad GamePadState;

	}

}