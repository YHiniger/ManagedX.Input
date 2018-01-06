using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains the raw input from a device.
	/// <para>This structure is equivalent to the native <code>RAWINPUT</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645562(v=vs.85).aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUT" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 40 (x86) or 48 (x64) bytes
	internal struct RawInput : IEquatable<RawInput>
	{
		
		[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 24 )]
		private struct Union
		{

			[FieldOffset( 0 )]
			internal readonly RawMouse Mouse;		// Size = 24

			[FieldOffset( 0 )]
			internal readonly RawKeyboard Keyboard;	// Size = 16

			[FieldOffset( 0 )]
			internal readonly RawHID HID;			// Size = 12 (x86) or 16 (x64) bytes

		}



		internal readonly RawInputHeader Header;
		// Since the header structure size depends on the platform, we can't use the FieldOffset attribute to specify the data field's position. 
		private readonly Union data;



		public RawMouse Mouse => Header.DeviceType == InputDeviceType.Mouse ? data.Mouse : RawMouse.Empty;


		public RawKeyboard Keyboard => Header.DeviceType == InputDeviceType.Keyboard ? data.Keyboard : RawKeyboard.Empty;


		public RawHID HumanInterfaceDevice => Header.DeviceType == InputDeviceType.HumanInterfaceDevice ? data.HID : RawHID.Empty;


		public override int GetHashCode()
		{
			var hashCode = Header.GetHashCode();
			
			if( Header.DeviceType == InputDeviceType.Mouse )
				return hashCode ^ data.Mouse.GetHashCode();

			if( Header.DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ data.Keyboard.GetHashCode();

			return hashCode ^ data.HID.GetHashCode();
		}


		public bool Equals( RawInput other )
		{
			if( !Header.Equals( other.Header ) )
				return false;

			if( Header.DeviceType == InputDeviceType.Mouse )
				return data.Mouse.Equals( other.data.Mouse );

			if( Header.DeviceType == InputDeviceType.Keyboard )
				return data.Keyboard.Equals( other.data.Keyboard );

			return data.HID.Equals( other.data.HID );
		}


		public override bool Equals( object obj )
		{
			return ( obj is RawInput ri ) && this.Equals( ri );
		}



		public static readonly RawInput Empty;


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawInput rawInput, RawInput other )
		{
			return rawInput.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawInput rawInput, RawInput other )
		{
			return !rawInput.Equals( other );
		}

		#endregion Operators

	}

}