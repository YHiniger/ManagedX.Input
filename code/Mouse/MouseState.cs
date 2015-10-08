using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{

	/// <summary>A mouse state.</summary>
	public struct MouseState : IEquatable<MouseState>
	{

		private int x;
		private int y;
		private int wheel;
		private bool[] buttons;


		/// <summary>Initializes a new <see cref="MouseState"/> structure.</summary>
		/// <param name="x">The horizontal position of the mouse cursor.</param>
		/// <param name="y">The vertical position of the mouse cursor.</param>
		/// <param name="wheel">The mouse wheel value.</param>
		/// <param name="buttons">An array of 5 boolean values indicating whether a button is pressed.</param>
		/// <exception cref="ArgumentNullException"/>
		/// <exception cref="ArgumentException"/>
		internal MouseState( int x, int y, int wheel, bool[] buttons )
		{
#if DEBUG
			if( buttons == null )
				throw new ArgumentNullException( "buttons" );
			
			if( buttons.Length != 5 )
				throw new ArgumentException( "Mouse buttons state array must be 5 bool-long.", "buttons" );
#endif	
			this.x = x;
			this.y = y;
			this.wheel = wheel;
			this.buttons = buttons;
		}


		/// <summary>Gets the horizontal mouse position, in pixels.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		public int X { get { return x; } }

		/// <summary>Gets the vertical mouse position, in pixels.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		public int Y { get { return y; } }


		/// <summary>Gets the wheel value.</summary>
		public int Wheel { get { return wheel; } }


		/// <summary>Gets a value indicating whether a button is down.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the button is down, otherwise returns false.</returns>
		public bool this[ MouseButton button ] { get { return buttons[ (int)button ]; } }


		/// <summary>Returns a hash code for this <see cref="MouseState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MouseState"/> structure.</returns>
		public override int GetHashCode()
		{
			return x ^ y ^ wheel ^ buttons.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( MouseState other )
		{
			return ( x == other.x ) && ( y == other.y ) && ( wheel == other.wheel ) && ( buttons == other.buttons );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MouseState"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MouseState ) && this.Equals( (MouseState)obj );
		}



		/// <summary>The empty <see cref="MouseState"/> structure.</summary>
		public static readonly MouseState Empty = new MouseState( 0, 0, 0, new bool[ 5 ] );


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="mouseState">A <see cref="MouseState"/> structure.</param>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( MouseState mouseState, MouseState other )
		{
			return mouseState.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="mouseState">A <see cref="MouseState"/> structure.</param>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( MouseState mouseState, MouseState other )
		{
			return !mouseState.Equals( other );
		}


		#endregion

	}

}