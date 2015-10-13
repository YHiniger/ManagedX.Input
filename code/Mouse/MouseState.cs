using System;


namespace ManagedX.Input
{

	/// <summary>A mouse state.</summary>
	[Serializable]
	public struct MouseState : IEquatable<MouseState>
	{

		/// <summary>Defines the number of buttons supported by this implementation.</summary>
		internal const int MaxSupportedButtonCount = 5;


		private Point position;
		private int wheel;
		private bool[] buttons;


		#region Constructors

		/// <summary>Initializes a new <see cref="MouseState"/> structure.</summary>
		/// <param name="position">The mouse cursor position.</param>
		/// <param name="wheel">The mouse wheel value.</param>
		/// <param name="buttons">An array of 5 boolean values indicating whether a button is pressed.</param>
		internal MouseState( ref Point position, int wheel, bool[] buttons )
		{
			this.position = position;
			this.wheel = wheel;
			this.buttons = buttons;
		}

		private MouseState( int buttonCount )
		{
			this.position = Point.Zero;
			this.wheel = 0;
			this.buttons = new bool[ buttonCount ];
		}

		#endregion
		

		/// <summary>Gets the mouse cursor position.</summary>
		public Point Position { get { return this.position; } }


		/// <summary>Gets the wheel value.</summary>
		public int Wheel { get { return wheel; } }


		/// <summary>Gets a value indicating whether a button is down.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the button is down, otherwise returns false.</returns>
		public bool this[ MouseButton button ] { get { return buttons[ (int)button ]; } }


		/// <summary>Returns a value indicating whether a button is down.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the button is down, otherwise returns false.</returns>
		public bool IsPressed( MouseButton button )
		{
			return buttons[ (int)button ];
		}


		/// <summary>Returns a hash code for this <see cref="MouseState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MouseState"/> structure.</returns>
		public override int GetHashCode()
		{
			return position.GetHashCode() ^ wheel ^ ( ( buttons == null ) ? 0 : buttons.GetHashCode() );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( MouseState other )
		{
			return position.Equals( other.position ) && ( wheel == other.wheel ) && ( buttons == other.buttons );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MouseState"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MouseState ) && this.Equals( (MouseState)obj );
		}



		/// <summary>The empty <see cref="MouseState"/> structure.</summary>
		public static readonly MouseState Empty = new MouseState( MaxSupportedButtonCount );


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