using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains information about the state of the mouse.
	/// <para>This structure is equivalent to the native <code>RAWMOUSE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645578%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWMOUSE" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct RawMouse : IEquatable<RawMouse>
	{

		/// <summary>The mouse state.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly RawMouseStateIndicators State;
		// NOTE - the native structure declares an USHORT, but due to the 4-byte alignment (packing), it adds a padding USHORT after the state flags.
		// So I defined RawMouseStateIndicators as a 32-bit signed integer to cover that padding USHORT.

		private readonly int buttons;

		/// <summary>The transition state of the mouse buttons.</summary>
		public RawMouseButtonStateIndicators ButtonsState => (RawMouseButtonStateIndicators)( buttons & 0x0000FFFF );

		/// <summary>If <see cref="ButtonsState"/> indicates <see cref="RawMouseButtonStateIndicators.Wheel"/>, gets the wheel delta.</summary>
		public int WheelDelta => buttons >> 16;

		/// <summary>The raw state of the mouse buttons.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int RawButtons;

		/// <summary>The motion in the X and Y directions.
		/// <para>This is signed relative motion or absolute motion, depending on the value of <see cref="State"/>.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Point Motion;

		/// <summary>Device-specific additional information for the event.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int ExtraInformation;



		/// <summary>Returns a hash code for this <see cref="RawMouse"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawMouse"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)State ^ buttons ^ RawButtons ^ Motion.GetHashCode() ^ ExtraInformation;
		}


		/// <summary>Returns a value indicating whether this <see cref="RawMouse"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawMouse"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawMouse other )
		{
			return
				( State == other.State ) &&
				( ButtonsState == other.ButtonsState ) &&
				( WheelDelta == other.WheelDelta ) &&
				( RawButtons == other.RawButtons ) &&
				Motion.Equals( other.Motion ) &&
				( ExtraInformation == other.ExtraInformation );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawMouse"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawMouse"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawMouse raw ) && this.Equals( raw );
		}

		
		/// <summary>The empty <see cref="RawMouse"/> structure.</summary>
		public static readonly RawMouse Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rawMouse">A <see cref="RawMouse"/> structure.</param>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawMouse rawMouse, RawMouse other )
		{
			return rawMouse.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rawMouse">A <see cref="RawMouse"/> structure.</param>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawMouse rawMouse, RawMouse other )
		{
			return !rawMouse.Equals( other );
		}

		#endregion Operators

	}

}