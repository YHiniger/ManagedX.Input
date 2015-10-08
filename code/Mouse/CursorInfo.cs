using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx
	// WinUser.h


	/// <summary>Contains global cursor information.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct CursorInfo : IEquatable<CursorInfo>
	{


		/// <summary>Enumerates a mouse cursor states.</summary>
		[Flags]
		private enum StateFlags : int
		{

			/// <summary>The cursor is hidden.</summary>
			Hidden = 0x00000000,

			/// <summary>The cursor is showing.</summary>
			Showing = 0x00000001,

			/// <summary>Windows 8: The cursor is suppressed.
			/// This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.
			/// </summary>
			Suppressed = 0x00000002

		}



		private int structureSize;
		private StateFlags flags;
		private IntPtr cursor;
		private Point screenPosition;


		private CursorInfo( int structSize )
		{
			structureSize = structSize;
			flags = StateFlags.Hidden;
			cursor = IntPtr.Zero;
			screenPosition = Point.Zero;
		}


		/// <summary>Gets a value indicating whether the mouse cursor is hidden.</summary>
		public bool Hidden { get { return !flags.HasFlag( StateFlags.Showing ); } }
		
		/// <summary>Gets a value indicating whether the mouse cursor is suppressed.
		/// <para>Only available on Windows 8 and newer.</para>
		/// </summary>
		public bool Suppressed { get { return flags.HasFlag( StateFlags.Suppressed ); } }


		/// <summary>Gets a handle to the mouse cursor.</summary>
		public IntPtr Cursor { get { return cursor; } }


		/// <summary>Gets the screen coordinates of the mouse cursor.</summary>
		public Point ScreenPosition { get { return screenPosition; } }


		/// <summary>The null (and invalid) <see cref="CursorInfo"/> structure.</summary>
		private static readonly CursorInfo Empty = new CursorInfo();

		/// <summary>The default <see cref="CursorInfo"/> structure.</summary>
		public static readonly CursorInfo Default = new CursorInfo( Marshal.SizeOf( typeof( CursorInfo ) ) );


		/// <summary>Returns a hash code for this <see cref="CursorInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="CursorInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return structureSize ^ (int)flags ^ cursor.GetHashCode() ^ screenPosition.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="CursorInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="CursorInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="CursorInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( CursorInfo other )
		{
			return ( structureSize == other.structureSize ) && ( flags == other.flags ) && ( cursor == other.cursor ) && ( screenPosition == other.screenPosition );
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


		#endregion

	}

}