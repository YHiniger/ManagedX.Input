using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Contains global cursor information.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "CURSORINFO" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 20 or 24 bytes (x86 or x64)
	internal struct CursorInfo : IEquatable<CursorInfo>
	{

		private readonly int structureSize;
		
		/// <summary>Indicates the state of the mouse cursor.</summary>
		internal MouseCursorStateIndicators State;
		
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



		public override int GetHashCode()
		{
			return structureSize ^ (int)State ^ Cursor.GetHashCode() ^ ScreenPosition.GetHashCode();
		}


		public bool Equals( CursorInfo other )
		{
			return ( structureSize == other.structureSize ) && ( State == other.State ) && ( Cursor == other.Cursor ) && ScreenPosition.Equals( other.ScreenPosition );
		}


		public override bool Equals( object obj )
		{
			return obj is CursorInfo info && this.Equals( info );
		}



		/// <summary>The default <see cref="CursorInfo"/> structure.</summary>
		public static readonly CursorInfo Default = new CursorInfo( Marshal.SizeOf( typeof( CursorInfo ) ) );


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( CursorInfo cursorInfo, CursorInfo other )
		{
			return cursorInfo.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( CursorInfo cursorInfo, CursorInfo other )
		{
			return !cursorInfo.Equals( other );
		}

		#endregion Operators

	}

}