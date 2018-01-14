using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


namespace ManagedX.Input
{

	/// <summary>A mouse state.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public struct MouseState : IInputDeviceState<MouseButton>, IEquatable<MouseState>
	{

		/// <summary>The raw mouse motion.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Point Motion;

		/// <summary>The wheels' delta.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Point Wheels;

		internal MouseButtons Buttons;

		

		/// <summary>Gets a value indicating whether a button is down.</summary>
		/// <param name="button">A mouse button.</param>
		/// <returns>Returns true if the button is down, otherwise returns false.</returns>
		public bool this[ MouseButton button ] => Buttons.HasFlag( (MouseButtons)( 1 << (int)button ) );


		/// <summary>Returns a hash code for this <see cref="MouseState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MouseState"/> structure.</returns>
		public override int GetHashCode()
		{
			return Motion.GetHashCode() ^ Wheels.GetHashCode() ^ (int)Buttons;
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( MouseState other )
		{
			return
				Motion.Equals( other.Motion ) &&
				Wheels.Equals( other.Wheels ) &&
				( Buttons == other.Buttons );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MouseState"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is MouseState state && this.Equals( state );
		}



		/// <summary>The empty <see cref="MouseState"/> structure.</summary>
		public static readonly MouseState Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="mouseState">A <see cref="MouseState"/> structure.</param>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( MouseState mouseState, MouseState other )
		{
			return mouseState.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="mouseState">A <see cref="MouseState"/> structure.</param>
		/// <param name="other">A <see cref="MouseState"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( MouseState mouseState, MouseState other )
		{
			return !mouseState.Equals( other );
		}

		#endregion Operators

	}

}