using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input
{

	/// <summary>Contains the state of a keyboard.</summary>
	[StructLayout( LayoutKind.Sequential, Size = 256 )]
	public struct KeyboardState : IInputDeviceState<Key>, IEquatable<KeyboardState>
	{

		internal const byte KeyDownMask = 0x80;



		[MarshalAs( UnmanagedType.ByValArray, SizeConst = 256 )]
		internal readonly byte[] Data;



		internal KeyboardState( byte[] data )
		{
			Data = data ?? throw new ArgumentNullException( "data" );
		}



		/// <summary>Gets a value indicating whether a key is down(=pressed).</summary>
		/// <param name="key">A <see cref="Key"/> value, except <see cref="Key.None"/>.</param>
		/// <exception cref="InvalidEnumArgumentException"/>
		public bool this[ Key key ] => ( this.Data[ (int)key ] & KeyDownMask ) != 0;


		/// <summary>Returns a value indicating whether a key is down(=pressed).</summary>
		/// <param name="button">A <see cref="Key"/> value, except <see cref="Key.None"/>.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is down, otherwise returns false.</returns>
		/// <exception cref="InvalidEnumArgumentException"/>
		public bool IsPressed( Key button )
		{
			return ( this.Data[ (int)button ] & KeyDownMask ) != 0;
		}


		/// <summary>Returns a hash code for this <see cref="KeyboardState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="KeyboardState"/> structure.</returns>
		public override int GetHashCode()
		{
			return Data == null ? 0 : Data.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if this <see cref="KeyboardState"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1062" )]
		public bool Equals( KeyboardState other )
		{
			if( Data == null )
				return other.Data == null;

			for( var i = 0; i < 256; ++i )
				if( Data[ i ] != other.Data[ i ] )
					return false;

			return true;
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="KeyboardState"/> structure which equals this <see cref="KeyboardState"/> structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is KeyboardState ks ) && this.Equals( ks );
		}


		/// <summary>The empty <see cref="KeyboardState"/> structure.</summary>
		public static readonly KeyboardState Empty = new KeyboardState( new byte[ 256 ] );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="keyboardState">A <see cref="KeyboardState"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( KeyboardState keyboardState, KeyboardState other )
		{
			return keyboardState.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="keyboardState">A <see cref="KeyboardState"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardState"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( KeyboardState keyboardState, KeyboardState other )
		{
			return !keyboardState.Equals( other );
		}

		#endregion Operators

	}

}