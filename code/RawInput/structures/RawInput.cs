using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// Important: "Pack = 4" causes the structure size to be rounded the nearest greater multiple of 4.
	// Since it's applied to the private Data structure, its actual size is 24, instead of 22.
	// Of course, this increases the size of the RawInput structure to 40 or 48 bytes (instead of 38 or 46 bytes), which can then safely use the same packing.


	/// <summary>Contains the raw input from a device.
	/// <para>This structure is equivalent to the native <code>RAWINPUT</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645562(v=vs.85).aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUT" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 2 )] // Size = 40 (x86) or 48 (x64) byte
	internal struct RawInput : IEquatable<RawInput>
	{

		[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 24 )]
		private struct Data
		{

			[FieldOffset( 0 )]
			internal readonly RawMouse Mouse;		// Size = 24, Pack = 4

			[FieldOffset( 0 )]
			internal readonly RawKeyboard Keyboard;	// Size = 16, Pack = 2

			[FieldOffset( 0 )]
			internal readonly RawHID HID;			// Size = 12 or 16, Pack = 4

		}



		internal readonly RawInputHeader Header; // TODO - turn RawInputHeader into a class, as well as this structure, and inherit from RawInputHeader ?
		private readonly Data data;



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