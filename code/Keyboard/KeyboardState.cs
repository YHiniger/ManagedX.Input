using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace ManagedX.Input
{

	/// <summary>Contains the state of a keyboard.</summary>
	public struct KeyboardState : IEquatable<KeyboardState>
	{

		internal byte[] Data;



		/// <summary>Gets a value indicating whether a key is down(=pressed).</summary>
		/// <param name="key">A <see cref="Key"/> value, except <see cref="Key.None"/>.</param>
		/// <exception cref="InvalidEnumArgumentException"/>
		public bool this[ Key key ] { get { return this.IsDown( key ); } }

		
		/// <summary>Returns a value indicating whether a key is down(=pressed).</summary>
		/// <param name="key">A <see cref="Key"/> value, except <see cref="Key.None"/>.</param>
		/// <returns>Returns true if the specified <paramref name="key"/> is down, otherwise returns false.</returns>
		/// <exception cref="InvalidEnumArgumentException"/>
		public bool IsDown( Key key )
		{
			const byte DownMask = 0x80;

			if( key == Key.None )
				throw new System.ComponentModel.InvalidEnumArgumentException( "key", (int)key, typeof( Key ) );

			return ( Data[ (int)key ] & DownMask ) == DownMask;
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		private bool GetToggleableKeyState( Key key )
		{
			const byte ToggleMask = 0x01;
			return ( Data[ (int)key ] & ToggleMask ) == ToggleMask;
		}


		/// <summary>Gets a value indicating whether the CapsLock toggle is active.</summary>
		public bool CapsLock { get { return this.GetToggleableKeyState( Key.CapsLock ); } }


		/// <summary>Gets a value indicating whether the NumLock toggle is active.</summary>
		public bool NumLock { get { return this.GetToggleableKeyState( Key.NumLock ); } }


		/// <summary>Gets a value indicating whether the ScrollLock toggle is active.</summary>
		public bool ScrollLock { get { return this.GetToggleableKeyState( Key.ScrollLock ); } }


		/// <summary>Returns a hash code for this <see cref="KeyboardState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="KeyboardState"/> structure.</returns>
		public override int GetHashCode()
		{
			return Data.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if this <see cref="KeyboardState"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1062" )]
		public bool Equals( KeyboardState other )
		{
			if( other.Data == null )
				return Data == null;

			for( int i = 0; i < 256; i++ )
				if( Data[ i ] != other.Data[ i ] )
					return false;

			return true;
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="KeyboardState"/> structure which equals this <see cref="KeyboardState"/> structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is KeyboardState ) && this.Equals( (KeyboardState)obj );
		}


		/// <summary>The empty <see cref="KeyboardState"/> structure.</summary>
		public static readonly KeyboardState Empty = new KeyboardState() { Data = new byte[ 256 ] };


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="keyboardState">A <see cref="KeyboardState"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( KeyboardState keyboardState, KeyboardState other )
		{
			return keyboardState.Equals( other );
		}

		
		/// <summary>Inequality comparer.</summary>
		/// <param name="keyboardState">A <see cref="KeyboardState"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( KeyboardState keyboardState, KeyboardState other )
		{
			return !keyboardState.Equals( other );
		}


		#endregion

	}

}