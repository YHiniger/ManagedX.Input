using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Contains global cursor information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinUser.h", "CURSORINFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 20 or 24 bytes (x86 or x64)
	internal struct CursorInfo : IEquatable<CursorInfo>
	{

		private int structureSize;
		public MouseCursorStateIndicators CursorState;
		private IntPtr cursor;
		/// <summary>The screen coordinates of the mouse cursor.</summary>
		internal Point ScreenPosition;



		private CursorInfo( int structSize )
		{
			structureSize = structSize;
			CursorState = MouseCursorStateIndicators.Hidden;
			cursor = IntPtr.Zero;
			ScreenPosition = Point.Zero;
		}



		/// <summary>Gets a value indicating the state of the mouse cursor.</summary>
		public MouseCursorStateIndicators State { get { return CursorState; } }

		
		/// <summary>Gets a handle to the mouse cursor.</summary>
		public IntPtr Cursor { get { return cursor; } }


		/// <summary>The default <see cref="CursorInfo"/> structure.</summary>
		public static readonly CursorInfo Default = new CursorInfo( Marshal.SizeOf( typeof( CursorInfo ) ) );


		/// <summary>Returns a hash code for this <see cref="CursorInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="CursorInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return structureSize ^ (int)CursorState ^ cursor.GetHashCode() ^ ScreenPosition.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="CursorInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( CursorInfo other )
		{
			return ( structureSize == other.structureSize ) && ( CursorState == other.CursorState ) && ( cursor == other.cursor ) && ScreenPosition.Equals( other.ScreenPosition );
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="CursorInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is CursorInfo ) && this.Equals( (CursorInfo)obj );
		}


		#region Operators
		
		/// <summary>Equality comparer.</summary>
		/// <param name="cursorInfo">A <see cref="CursorInfo"/> structure.</param>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( CursorInfo cursorInfo, CursorInfo other )
		{
			return cursorInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="cursorInfo">A <see cref="CursorInfo"/> structure.</param>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( CursorInfo cursorInfo, CursorInfo other )
		{
			return !cursorInfo.Equals( other );
		}

		#endregion Operators

	}

}