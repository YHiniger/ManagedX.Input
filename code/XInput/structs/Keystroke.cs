using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// THINKABOUTME - remove unicodeChar from GetHashCode and Equals, to allow user-implemented (and localized) support.


namespace ManagedX.Input.XInput
{

	/// <summary>Specifies keystroke data returned by XInputGetKeystroke.
	/// <para>This structure is equivalent to the <code>XINPUT_KEYSTROKE</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_keystroke%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "XInput.h", "XINPUT_KEYSTROKE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 8 )]
	public struct Keystroke : IEquatable<Keystroke>
	{

		/// <summary>Gets the virtual-key code of the key, button, or stick movement.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly VirtualKeyCode VirtualKey;
		
		/// <summary>This member is unused and the value is zero.</summary>
		internal char unicodeChar;

		/// <summary>A value indicating the keyboard state at the time of the input event.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly KeyStates State;

		private readonly byte userIndex;

		/// <summary>The HID code corresponding to the input. If there is no corresponding HID code, this value is zero.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly byte HidCode;



		/// <summary>Gets the index of the signed-in gamer associated with the device.</summary>
		public GameControllerIndex UserIndex => (GameControllerIndex)userIndex;


		// TODO - add a Char property ?

		
		/// <summary>Returns a hash code for this <see cref="Keystroke"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Keystroke"/> structure.</returns>
		public override int GetHashCode()
		{
			return VirtualKey.GetHashCode() ^ unicodeChar.GetHashCode() ^ State.GetHashCode() ^ userIndex.GetHashCode() ^ HidCode.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Keystroke"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if this <see cref="Keystroke"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( Keystroke other )
		{
			return ( VirtualKey == other.VirtualKey ) && ( unicodeChar == other.unicodeChar ) && ( State == other.State ) && ( userIndex == other.userIndex ) && ( HidCode == other.HidCode );
		}

		
		/// <summary>Returns a value indicating whether this <see cref="Keystroke"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Keystroke"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Keystroke ks && this.Equals( ks );
		}
		

		/// <summary>The empty <see cref="Keystroke"/> structure.</summary>
		public static readonly Keystroke Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="keystroke">A <see cref="Keystroke"/> structure.</param>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( Keystroke keystroke, Keystroke other )
		{
			return keystroke.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="keystroke">A <see cref="Keystroke"/> structure.</param>
		/// <param name="other">A <see cref="Keystroke"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( Keystroke keystroke, Keystroke other )
		{
			return !keystroke.Equals( other );
		}

		#endregion Operators

	}

}
