using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains information about the state of the keyboard.
	/// <para>This structure is equivalent to the native <code>RAWKEYBOARD</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645575%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinUser.h", "RAWKEYBOARD" )]
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 16 )]
	public struct RawKeyboard : IEquatable<RawKeyboard>
	{

		private short makeCode;
		private short flags;
		private short reserved;	// must be 0
		private short vKey;
		private int message;
		private int extraInformation;



		/// <summary>Gets the scan code from the key depression.
		/// <para>The scan code for keyboard overrun is KEYBOARD_OVERRUN_MAKE_CODE.</para>
		/// </summary>
		public short MakeCode { get { return makeCode; } }
		

		/// <summary>Gets the flags for scan code information.</summary>
		public short MakeCodeInformations { get { return flags; } }
		

		/// <summary>Gets the Windows message compatible virtual-key code.</summary>
		public short VirtualKey { get { return vKey; } }


		/// <summary>Gets the corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth.</summary>
		public int Message { get { return message; } }
		

		/// <summary>Gets the device-specific additional information for the event.</summary>
		public int ExtraInformation { get { return extraInformation; } }

		

		/// <summary>Returns a hash code for this <see cref="RawKeyboard"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawKeyboard"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)makeCode ^ (int)flags ^ (int)reserved ^ (int)vKey ^ message ^ extraInformation;
		}


		/// <summary>Returns a value indicating whether this <see cref="RawKeyboard"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawKeyboard"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawKeyboard other )
		{
			return
				( makeCode == other.makeCode ) &&
				( flags == other.flags ) &&
				( reserved == other.reserved ) &&
				( vKey == other.vKey ) &&
				( message == other.message ) &&
				( extraInformation == other.extraInformation );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawKeyboard"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawKeyboard"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawKeyboard ) && this.Equals( (RawKeyboard)obj );
		}



		/// <summary>The empty <see cref="RawKeyboard"/> structure.</summary>
		public static readonly RawKeyboard Empty;


		#region Operators
		
		/// <summary>Equality operator.</summary>
		/// <param name="rawKeyboard">A <see cref="RawKeyboard"/> structure.</param>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RawKeyboard rawKeyboard, RawKeyboard other )
		{
			return rawKeyboard.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="rawKeyboard">A <see cref="RawKeyboard"/> structure.</param>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RawKeyboard rawKeyboard, RawKeyboard other )
		{
			return !rawKeyboard.Equals( other );
		}

		#endregion Operators

	}

}