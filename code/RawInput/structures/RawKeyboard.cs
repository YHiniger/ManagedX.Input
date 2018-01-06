using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains information about the state of the keyboard.
	/// <para>This structure is equivalent to the native <code>RAWKEYBOARD</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645575%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWKEYBOARD" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct RawKeyboard : IEquatable<RawKeyboard>
	{

		/// <summary>Scan code from the key depression.
		/// <para>The scan code for keyboard overrun is KEYBOARD_OVERRUN_MAKE_CODE.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly short MakeCode;

		/// <summary>Flags for scan code information.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly ScanCodeCharacteristics MakeCodeInfo;

		private readonly short reserved; // must be 0, I believe it's used for proper packing.

		/// <summary>Windows message compatible <see cref="VirtualKeyCode"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly short VirtualKey;

		/// <summary>The corresponding window message, for example WM_KEYDOWN, WM_SYSKEYDOWN, and so forth.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Message;

		/// <summary>Device-specific additional information for the event.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int ExtraInformation;



		/// <summary>Returns a hash code for this <see cref="RawKeyboard"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawKeyboard"/> structure.</returns>
		public override int GetHashCode()
		{
			return unchecked((int)MakeCode | ( (int)MakeCodeInfo << 16 ) ^ ( (int)reserved | ( (int)VirtualKey << 16 ) ) ) ^ Message ^ ExtraInformation;
		}


		/// <summary>Returns a value indicating whether this <see cref="RawKeyboard"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawKeyboard"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawKeyboard other )
		{
			return
				( MakeCode == other.MakeCode ) &&
				( MakeCodeInfo == other.MakeCodeInfo ) &&
				( reserved == other.reserved ) &&
				( VirtualKey == other.VirtualKey ) &&
				( Message == other.Message ) &&
				( ExtraInformation == other.ExtraInformation );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawKeyboard"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawKeyboard"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawKeyboard kbd ) && this.Equals( kbd );
		}



		/// <summary>The empty <see cref="RawKeyboard"/> structure.</summary>
		public static readonly RawKeyboard Empty;


		#region Operators

		/// <summary>Equality operator.</summary>
		/// <param name="rawKeyboard">A <see cref="RawKeyboard"/> structure.</param>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawKeyboard rawKeyboard, RawKeyboard other )
		{
			return rawKeyboard.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="rawKeyboard">A <see cref="RawKeyboard"/> structure.</param>
		/// <param name="other">A <see cref="RawKeyboard"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawKeyboard rawKeyboard, RawKeyboard other )
		{
			return !rawKeyboard.Equals( other );
		}

		#endregion Operators

	}

}