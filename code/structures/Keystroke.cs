using System;
using System.Runtime.InteropServices;

// THINKABOUTME - remove unicodeChar from GetHashCode and Equals, to allow user-implemented (and localized) support.


namespace ManagedX.Input.XInput
{

	// XInput.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_keystroke%28v=vs.85%29.aspx


	/// <summary>Specifies keystroke data returned by XInputGetKeystroke.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 8 )]
	public struct Keystroke : IEquatable<Keystroke>
	{

		private VirtualKeyCode virtualKey;
		/// <summary>This member is unused and the value is zero.</summary>
		internal char unicodeChar;
		private KeyStates flags;
		private byte userIndex;
		private byte hidCode;


		/// <summary>Gets the virtual-key code of the key, button, or stick movement.</summary>
		public VirtualKeyCode VirtualKey { get { return virtualKey; } }

		/// <summary>Gets a value indicating the keyboard state at the time of the input event.</summary>
		public KeyStates State { get { return flags; } }


		/// <summary>Gets a value indicating whether a state flag is present.</summary>
		/// <param name="state">A <see cref="KeyStates"/> value.</param>
		/// <returns>Returns true if the specified <paramref name="state"/> is present, otherwise returns false.</returns>
		public bool IsSet( KeyStates state )
		{
			return ( flags & state ) == state;
		}


		/// <summary>Gets the index of the signed-in gamer associated with the device.
		/// <para>Can be a value in the range [0,3] ([0,7] on Windows 10).</para>
		/// </summary>
		public byte UserIndex { get { return userIndex; } }

		/// <summary>Gets the HID code corresponding to the input. If there is no corresponding HID code, this value is zero.</summary>
		public byte HidCode { get { return hidCode; } }

		
		// TODO - add a Char property

		
		/// <summary>Returns a hash code for this <see cref="Keystroke"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Keystroke"/> structure.</returns>
		public override int GetHashCode()
		{
			return virtualKey.GetHashCode() ^ unicodeChar.GetHashCode() ^ flags.GetHashCode() ^ userIndex.GetHashCode() ^ hidCode.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Keystroke"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if this <see cref="Keystroke"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( Keystroke other )
		{
			return ( virtualKey == other.virtualKey ) && ( unicodeChar == other.unicodeChar ) && ( flags == other.flags ) && ( userIndex == other.userIndex ) && ( hidCode == other.hidCode );
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Keystroke"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Keystroke"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Keystroke ) && ( this.Equals( (Keystroke)obj ) );
		}
		

		/// <summary>The empty <see cref="Keystroke"/> structure.</summary>
		public static readonly Keystroke Empty = new Keystroke();


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="keystroke">A <see cref="Keystroke"/> structure.</param>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Keystroke keystroke, Keystroke other )
		{
			return keystroke.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="keystroke">A <see cref="Keystroke"/> structure.</param>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Keystroke keystroke, Keystroke other )
		{
			return !keystroke.Equals( other );
		}


		#endregion

	}

}
