using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Describes the state of an XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_GAMEPAD</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx</remarks>
	[Source( "XInput.h", "XINPUT_GAMEPAD" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	internal struct Gamepad
	{

		private const float MaxAbsThumbStickPosition = short.MaxValue;


		/// <summary>A value indicating which buttons are pressed.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly GamepadButtons Buttons;
		private readonly ushort triggers;
		private readonly short leftThumbX;
		private readonly short leftThumbY;
		private readonly short rightThumbX;
		private readonly short rightThumbY;



		/// <summary>Gets a value, within the range [0,1], representing the state of the left trigger.</summary>
		public float LeftTrigger => ( triggers & 0xFF ) / 255.0f;


		/// <summary>Gets a value, within the range [0,1], representing the state of the right trigger.</summary>
		public float RightTrigger => ( triggers >> 8 ) / 255.0f;


		/// <summary>Gets a <see cref="Vector2"/> representing the position, normalized within the range [-1,+1], of the left stick.</summary>
		public Vector2 LeftThumb => new Vector2( leftThumbX / MaxAbsThumbStickPosition, leftThumbY / MaxAbsThumbStickPosition );


		/// <summary>Gets a <see cref="Vector2"/> representing the position, normalized within the range [-1,+1], of the right stick.</summary>
		public Vector2 RightThumb => new Vector2( rightThumbX / MaxAbsThumbStickPosition, rightThumbY / MaxAbsThumbStickPosition );

	}

}