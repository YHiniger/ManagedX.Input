using System;
using System.Runtime.InteropServices;

// THINKABOUTME - remove unicodeChar from GetHashCode and Equals, to allow user-implemented (and localized) support.

namespace ManagedX.Input.XInput
{

	// XInput.h
	// https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_keystroke%28v=vs.85%29.aspx


	/// <summary>Specifies keystroke data returned by XInputGetKeystroke.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 1, Size = 8 )]
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
		/// <returns></returns>
		public bool IsSet( KeyStates state )
		{
			return ( flags & state ) == state;
		}


		/// <summary>Gets the index of the signed-in gamer associated with the device. Can be a value in the range [0,3].</summary>
		public byte UserIndex { get { return userIndex; } }

		/// <summary>Gets the HID code corresponding to the input. If there is no corresponding HID code, this value is zero.</summary>
		public byte HidCode { get { return hidCode; } }

		
		// TODO - add a Char property

		
		/// <summary></summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return virtualKey.GetHashCode() ^ unicodeChar.GetHashCode() ^ flags.GetHashCode() ^ userIndex.GetHashCode() ^ hidCode.GetHashCode();
		}


		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( Keystroke other )
		{
			return ( virtualKey == other.virtualKey ) && ( unicodeChar == other.unicodeChar ) && ( flags == other.flags ) && ( userIndex == other.userIndex ) && ( hidCode == other.hidCode );
		}

		
		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			if( obj == null )
				return this.Equals( Empty );

			return ( obj is Keystroke ) && ( this.Equals( (Keystroke)obj ) );
		}
		

		/// <summary>The empty <see cref="Keystroke"/> structure.</summary>
		public static readonly Keystroke Empty = new Keystroke();


		#region Operators


		/// <summary></summary>
		public static bool operator ==( Keystroke keystroke, Keystroke other )
		{
			return keystroke.Equals( other );
		}


		/// <summary></summary>
		public static bool operator !=( Keystroke keystroke, Keystroke other )
		{
			return !keystroke.Equals( other );
		}


		#endregion

	}

}
