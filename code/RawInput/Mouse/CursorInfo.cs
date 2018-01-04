using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Contains global (mouse) cursor information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "CURSORINFO" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 20 or 24 bytes (x86 or x64)
	public struct CursorInfo : IEquatable<CursorInfo>
	{

		private readonly int structureSize;

		/// <summary>The state of the mouse cursor.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly MouseCursorStateIndicators State;

		/// <summary>A handle to the mouse cursor.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly IntPtr Cursor;

		/// <summary>The screen coordinates of the mouse cursor.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly Point ScreenPosition;



		private CursorInfo( int structSize )
		{
			structureSize = structSize;
			State = MouseCursorStateIndicators.Hidden;
			Cursor = IntPtr.Zero;
			ScreenPosition = Point.Zero;
		}



		/// <summary>Returns a hash code for this <see cref="CursorInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="CursorInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return structureSize ^ (int)State ^ Cursor.GetHashCode() ^ ScreenPosition.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure is equivalent to another <see cref="CursorInfo"/> structure.</summary>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, false otherwise.</returns>
		public bool Equals( CursorInfo other )
		{
			return ( structureSize == other.structureSize ) && ( State == other.State ) && ( Cursor == other.Cursor ) && ScreenPosition.Equals( other.ScreenPosition );
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="CursorInfo"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is CursorInfo info && this.Equals( info );
		}



		/// <summary>The default <see cref="CursorInfo"/> structure.</summary>
		public static readonly CursorInfo Default = new CursorInfo( Marshal.SizeOf<CursorInfo>() );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="cursorInfo">A <see cref="CursorInfo"/> structure.</param>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( CursorInfo cursorInfo, CursorInfo other )
		{
			return cursorInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="cursorInfo">A <see cref="CursorInfo"/> structure.</param>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( CursorInfo cursorInfo, CursorInfo other )
		{
			return !cursorInfo.Equals( other );
		}

		#endregion Operators

	}

}