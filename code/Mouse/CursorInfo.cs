using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Contains global cursor information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "CURSORINFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 20 or 24 bytes (x86 or x64)
	internal struct CursorInfo : IEquatable<CursorInfo>
	{

		private int structureSize;
		/// <summary>Indicates the state of the mouse cursor.</summary>
		public MouseCursorStateIndicators State;
		/// <summary>A handle to the mouse cursor.</summary>
		internal IntPtr Cursor;
		/// <summary>The screen coordinates of the mouse cursor.</summary>
		internal Point ScreenPosition;



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


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="CursorInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( CursorInfo other )
		{
			return ( structureSize == other.structureSize ) && ( State == other.State ) && ( Cursor == other.Cursor ) && ScreenPosition.Equals( other.ScreenPosition );
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="CursorInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is CursorInfo ci ) && this.Equals( ci );
		}



		/// <summary>The default <see cref="CursorInfo"/> structure.</summary>
		public static readonly CursorInfo Default = new CursorInfo( Marshal.SizeOf( typeof( CursorInfo ) ) );


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