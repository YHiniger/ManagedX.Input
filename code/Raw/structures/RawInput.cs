using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645562(v=vs.85).aspx

	// Important: "Pack = 4" causes the structure size to be rounded the nearest greater multiple of 4.
	// Since it's applied to the private Data structure, its actual size is 24, instead of 22.
	// Of course, this increases the size of the RawInput structure to 40 or 48 bytes (instead of 38 or 46 bytes), which can then safely use the same packing.

	
	/// <summary>Contains the raw input from a device.
	/// <para>The native name of this structure is RAWINPUT.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 40 (x86) or 48 (x64) byte
	internal struct RawInput : IEquatable<RawInput>
	{

		[StructLayout( LayoutKind.Explicit, Pack = 4 )] // Size = 24 bytes (22 rounded to the nearest greater multiple of 4, due to the packing)
		private struct Data
		{

			[FieldOffset( 0 )]
			internal RawMouse Mouse;

			[FieldOffset( 0 )]
			internal RawKeyboard Keyboard;

			[FieldOffset( 0 )]
			internal RawHID HID;

		}


		private RawInputHeader header; // 16/24 bytes (x86/x64)
		private Data data;



		/// <summary>Gets the type of the raw input device.</summary>
		public InputDeviceType DeviceType { get { return header.DeviceType; } }

		/// <summary>Gets a handle to the device generating the raw input data.</summary>
		public IntPtr DeviceHandle { get { return header.DeviceHandle; } }

		/// <summary>The value passed in the WParam parameter of the WM_INPUT message.</summary>
		public IntPtr WParam { get { return header.WParameter; } }


		/// <summary>When <see cref="DeviceType"/> is <see cref="InputDeviceType.Mouse"/>, gets ...</summary>
		public RawMouse? Mouse
		{
			get
			{
				if( header.DeviceType == InputDeviceType.Mouse )
					return data.Mouse;
				return null;
			}
		}

		/// <summary>When <see cref="DeviceType"/> is <see cref="InputDeviceType.Keyboard"/>, gets ...</summary>
		public RawKeyboard? Keyboard
		{
			get
			{
				if( header.DeviceType == InputDeviceType.Keyboard )
					return data.Keyboard;
				return null;
			}
		}

		/// <summary>When <see cref="DeviceType"/> is <see cref="InputDeviceType.HumanInterfaceDevice"/>, gets ...</summary>
		public RawHID? HumanInterfaceDevice
		{
			get
			{
				if( header.DeviceType == InputDeviceType.HumanInterfaceDevice )
					return data.HID;
				return null;
			}
		}


		/// <summary>Returns a hash code for this <see cref="RawInput"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInput"/> structure.</returns>
		public override int GetHashCode()
		{
			var hashCode = header.GetHashCode();
			
			if( header.DeviceType == InputDeviceType.Mouse )
				return hashCode ^ data.Mouse.GetHashCode();

			if( header.DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ data.Keyboard.GetHashCode();

			return hashCode ^ data.HID.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInput"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInput"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInput"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawInput other )
		{
			if( !header.Equals( other.header ) )
				return false;

			if( header.DeviceType == InputDeviceType.Mouse )
				return data.Mouse.Equals( other.data.Mouse );

			if( header.DeviceType == InputDeviceType.Keyboard )
				return data.Keyboard.Equals( other.data.Keyboard );

			return data.HID.Equals( other.data.HID );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInput"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInput"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInput ) && this.Equals( (RawInput)obj );
		}


		/// <summary>The empty <see cref="RawInput"/> structure.</summary>
		public static readonly RawInput Empty = new RawInput();


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

		#endregion

	}

}