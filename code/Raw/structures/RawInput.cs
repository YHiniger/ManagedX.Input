using System;
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
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 40 (x86) or 48 (x64) byte
	internal struct RawInput : IEquatable<RawInput>
	{

		[StructLayout( LayoutKind.Explicit, Pack = 4 )] // Size = 24 bytes (22 rounded to the nearest greater multiple of 4, due to the packing)
		private struct Data
		{

			[FieldOffset( 0 )]
			internal readonly RawMouse Mouse;

			[FieldOffset( 0 )]
			internal readonly RawKeyboard Keyboard;

			[FieldOffset( 0 )]
			internal readonly RawHID HID;

		}



		internal readonly RawInputHeader Header; // 16/24 bytes (x86/x64)
		private readonly Data data;



		/// <summary>When the DeviceType of the <see cref="Header"/> is <see cref="InputDeviceType.Mouse"/>, gets ...</summary>
		public RawMouse? Mouse => Header.DeviceType == InputDeviceType.Mouse ? new RawMouse?( data.Mouse ) : null;


		/// <summary>When the DeviceType of the <see cref="Header"/> is <see cref="InputDeviceType.Keyboard"/>, gets ...</summary>
		public RawKeyboard? Keyboard => Header.DeviceType == InputDeviceType.Keyboard ? new RawKeyboard?( data.Keyboard ) : null;


		/// <summary>When the DeviceType of the <see cref="Header"/> is <see cref="InputDeviceType.HumanInterfaceDevice"/>, gets ...</summary>
		public RawHID? HumanInterfaceDevice => Header.DeviceType == InputDeviceType.HumanInterfaceDevice ? new RawHID?( data.HID ) : null;


		/// <summary>Returns a hash code for this <see cref="RawInput"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInput"/> structure.</returns>
		public override int GetHashCode()
		{
			var hashCode = Header.GetHashCode();
			
			if( Header.DeviceType == InputDeviceType.Mouse )
				return hashCode ^ data.Mouse.GetHashCode();

			if( Header.DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ data.Keyboard.GetHashCode();

			return hashCode ^ data.HID.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInput"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInput"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInput"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
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


		/// <summary>Returns a value indicating whether this <see cref="RawInput"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInput"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInput ri ) && this.Equals( ri );
		}



		/// <summary>The empty <see cref="RawInput"/> structure.</summary>
		public static readonly RawInput Empty;


		#region Operators

		/// <summary>Equality operator.</summary>
		/// <param name="rawInput">A <see cref="RawInput"/> structure.</param>
		/// <param name="other">A <see cref="RawInput"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RawInput rawInput, RawInput other )
		{
			return rawInput.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="rawInput">A <see cref="RawInput"/> structure.</param>
		/// <param name="other">A <see cref="RawInput"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RawInput rawInput, RawInput other )
		{
			return !rawInput.Equals( other );
		}

		#endregion Operators

	}

}