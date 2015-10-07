using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	// http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_vibration%28v=vs.85%29.aspx
	// XInput.h


	/// <summary>An XInput VIBRATION structure.</summary>
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 4 )]
	public struct Vibration : IEquatable<Vibration>
	{

		private ushort leftMotorSpeed;
		private ushort rightMotorSpeed;


		#region Constructors


		/// <summary>Instantiates a new <see cref="Vibration"/> structure.</summary>
		/// <param name="leftMotorSpeed">The speed of the left motor.</param>
		/// <param name="rightMotorSpeed">The speed of the right motor.</param>
		[CLSCompliant( false )]
		public Vibration( ushort leftMotorSpeed, ushort rightMotorSpeed )
		{
			this.leftMotorSpeed = leftMotorSpeed;
			this.rightMotorSpeed = rightMotorSpeed;
		}


		/// <summary>Instantiates a new <see cref="Vibration"/> structure.</summary>
		/// <param name="leftMotorSpeed">The speed of the left motor, in the range [0,1].</param>
		/// <param name="rightMotorSpeed">The speed of the right motor, in the range [0,1].</param>
		public Vibration( float leftMotorSpeed, float rightMotorSpeed )
			: this( FloatToUShort( leftMotorSpeed ), FloatToUShort( rightMotorSpeed ) )
		{
		}


		#endregion


		/// <summary>Gets or sets the left motor speed, in the range [0,1].</summary>
		public float LeftMotorSpeed
		{
			get { return UShortToFloat( leftMotorSpeed ); }
			set { leftMotorSpeed = FloatToUShort( value ); }
		}


		/// <summary>Gets or sets the right motor speed, in the range [0,1].</summary>
		public float RightMotorSpeed
		{
			get { return UShortToFloat( rightMotorSpeed ); }
			set { rightMotorSpeed = FloatToUShort( value ); }
		}



		/// <summary>Returns a hash code for this <see cref="Vibration"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vibration"/> structure.</returns>
		public override int GetHashCode()
		{
			return ( ( (uint)leftMotorSpeed ) | ( ( (uint)rightMotorSpeed ) << 16 ) ).GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Vibration"/> structure is equivalent to another structure of the same type.</summary>
		/// <param name="other">A vibration structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> Vibration structure has the same motor speeds as this structure, otherwise returns false.</returns>
		public bool Equals( Vibration other )
		{
			return ( leftMotorSpeed == other.leftMotorSpeed ) && ( rightMotorSpeed == other.rightMotorSpeed );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vibration"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vibration"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Vibration ) && this.Equals( (Vibration)obj );
		}


		/// <summary>Returns a string representing this <see cref="Vibration"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="Vibration"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "XInput.Vibration ({0:X4}/{1:X4})", this.leftMotorSpeed, this.rightMotorSpeed );
		}




		/// <summary>The zero <see cref="Vibration"/> structure.</summary>
		public static readonly Vibration Zero = new Vibration();

		/// <summary>Full throttle vibration.</summary>
		public static readonly Vibration FullThrottle = new Vibration( ushort.MaxValue, ushort.MaxValue );



		#region Static methods


		private static float UShortToFloat( ushort value )
		{
			return (float)value / 65535.0f;
		}

		private static ushort FloatToUShort( float value )
		{
			if( float.IsNaN( value ) || float.IsNegativeInfinity( value ) || value <= 0.0f )
				return 0;

			if( float.IsPositiveInfinity( value ) || value >= 1.0f )
				return 65535;

			return (ushort)( value * 65535.0f );
		}



		/// <summary>Returns the result of the addition of two <see cref="Vibration"/> structures.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns the result of the addition of the two <see cref="Vibration"/> structures.</returns>
		public static Vibration Add( Vibration vibration, Vibration other )
		{
			int left = (int)vibration.leftMotorSpeed + (int)other.leftMotorSpeed;
			int right = (int)vibration.rightMotorSpeed + (int)other.rightMotorSpeed;
			return new Vibration( (ushort)Math.Min( left, 65535 ), (ushort)Math.Min( right, 65535 ) );
		}


		/// <summary></summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns></returns>
		public static Vibration Multiply( Vibration vibration, Vibration other )
		{
			return new Vibration( (ushort)( vibration.LeftMotorSpeed * (float)other.leftMotorSpeed ), (ushort)( vibration.RightMotorSpeed * (float)other.rightMotorSpeed ) );
		}

		/// <summary></summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="factor"></param>
		/// <returns></returns>
		public static Vibration Multiply( Vibration vibration, float factor )
		{
			bool isZero = vibration.Equals( Zero );
			
			if( isZero || float.IsNaN( factor ) || float.IsNegativeInfinity( factor ) || factor <= 0.0f )
				return Zero;

			if( float.IsPositiveInfinity( factor ) && !isZero )
				return FullThrottle;

			return new Vibration( vibration.LeftMotorSpeed * factor, vibration.RightMotorSpeed * factor );
		}


		/// <summary>Performs a linear interpolation between two <see cref="Vibration"/> structures.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="amount"></param>
		/// <returns></returns>
		public static Vibration Lerp( Vibration vibration, Vibration other, float amount )
		{
			return new Vibration( 
				vibration.LeftMotorSpeed * ( 1.0f - amount ) + other.LeftMotorSpeed * amount, 
				vibration.RightMotorSpeed * ( 1.0f - amount ) + other.RightMotorSpeed * amount
			);
		}


		#endregion


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Vibration vibration, Vibration other )
		{
			return vibration.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Vibration vibration, Vibration other )
		{
			return !vibration.Equals( other );
		}


		/// <summary></summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns></returns>
		public static Vibration operator +( Vibration vibration, Vibration other )
		{
			return Add( vibration, other );
		}


		/// <summary></summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns></returns>
		public static Vibration operator *( Vibration vibration, Vibration other )
		{
			return Multiply( vibration, other );
		}

		/// <summary></summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static Vibration operator *( Vibration vibration, float value )
		{
			return Multiply( vibration, value );
		}

		/// <summary></summary>
		/// <param name="value"></param>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns></returns>
		public static Vibration operator *( float value, Vibration vibration )
		{
			return Multiply( vibration, value );
		}

		#endregion


	}

}