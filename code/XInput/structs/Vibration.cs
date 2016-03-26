using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>An XInput VIBRATION structure.
	/// <para>This structure is equivalent to the native <code>XINPUT_VIBRATION</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_vibration%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "XInput.h", "XINPUT_VIBRATION" )]
	[Serializable]
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
			: this( ToUShort( leftMotorSpeed ), ToUShort( rightMotorSpeed ) )
		{
		}


		#endregion


		/// <summary>Gets or sets the left motor speed, in the range [0,1].</summary>
		public float LeftMotorSpeed
		{
			get { return ToFloat( leftMotorSpeed ); }
			set { leftMotorSpeed = ToUShort( value ); }
		}


		/// <summary>Gets or sets the right motor speed, in the range [0,1].</summary>
		public float RightMotorSpeed
		{
			get { return ToFloat( rightMotorSpeed ); }
			set { rightMotorSpeed = ToUShort( value ); }
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
		public static readonly Vibration Zero;

		/// <summary>Full throttle <see cref="Vibration"/>.</summary>
		public static readonly Vibration FullThrottle = new Vibration( ushort.MaxValue, ushort.MaxValue );



		#region Static methods (Add, Multiply, Lerp)


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		private static float ToFloat( ushort value )
		{
			return (float)value / 65535.0f;
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		private static ushort ToUShort( float value )
		{
			if( float.IsNaN( value ) || float.IsNegativeInfinity( value ) || value <= 0.0f )
				return 0;

			if( float.IsPositiveInfinity( value ) || value >= 1.0f )
				return 65535;

			return (ushort)( value * 65535.0f );
		}


		/// <summary>Calculates the sum of two <see cref="Vibration"/>s.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vibration"/> structure initialized with the sum (clamped to the valid range) of the two specified values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Add( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			result.leftMotorSpeed = (ushort)Math.Min( (uint)vibration.leftMotorSpeed + (uint)other.leftMotorSpeed, (uint)ushort.MaxValue );
			result.rightMotorSpeed = (ushort)Math.Min( (uint)vibration.rightMotorSpeed + (uint)other.rightMotorSpeed, (uint)ushort.MaxValue );
		}

		/// <summary>Returns the sum of two <see cref="Vibration"/> structures.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> initialized with the sum of the two specified <see cref="Vibration"/> values.</returns>
		public static Vibration Add( Vibration vibration, Vibration other )
		{
			Vibration result;
			result.leftMotorSpeed = (ushort)Math.Min( (uint)vibration.leftMotorSpeed + (uint)other.leftMotorSpeed, (uint)ushort.MaxValue );
			result.rightMotorSpeed = (ushort)Math.Min( (uint)vibration.rightMotorSpeed + (uint)other.rightMotorSpeed, (uint)ushort.MaxValue );
			return result;
		}


		/// <summary>Calculates the difference between two <see cref="Vibration"/>s.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vibration"/> structure initialized with the difference (clamped to the valid range) of the two specified vibrations.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Subtract( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			result.leftMotorSpeed = (ushort)XMath.Clamp( (int)vibration.leftMotorSpeed - (int)other.leftMotorSpeed, 0, (int)ushort.MaxValue );
			result.rightMotorSpeed = (ushort)XMath.Clamp( (int)vibration.rightMotorSpeed - (int)other.rightMotorSpeed, 0, (int)ushort.MaxValue );
		}

		/// <summary>Calculates the difference between two <see cref="Vibration"/>s.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initialized with the difference (clamped to the valid range) of the two specified vibrations.</returns>
		public static Vibration Subtract( Vibration vibration, Vibration other )
		{
			Vibration result;
			result.leftMotorSpeed = (ushort)XMath.Clamp( (int)vibration.leftMotorSpeed - (int)other.leftMotorSpeed, 0, (int)ushort.MaxValue );
			result.rightMotorSpeed = (ushort)XMath.Clamp( (int)vibration.rightMotorSpeed - (int)other.rightMotorSpeed, 0, (int)ushort.MaxValue );
			return result;
		}


		/// <summary>Calculates the product of two <see cref="Vibration"/> values.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vibration"/> initialized with the product of the two specified values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			const uint Max = 65535u * 65535u;
			result.leftMotorSpeed = (ushort)( (uint)vibration.leftMotorSpeed * (uint)other.leftMotorSpeed / Max );
			result.rightMotorSpeed = (ushort)( (uint)vibration.rightMotorSpeed * (uint)other.rightMotorSpeed / Max );
		}

		/// <summary>Multiplies two <see cref="Vibration"/> values.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns the scaled <see cref="Vibration"/>.</returns>
		public static Vibration Multiply( Vibration vibration, Vibration other )
		{
			const uint Max = 65535u * 65535u;
			Vibration result;
			result.leftMotorSpeed = (ushort)( (uint)vibration.leftMotorSpeed * (uint)other.leftMotorSpeed / Max );
			result.rightMotorSpeed = (ushort)( (uint)vibration.rightMotorSpeed * (uint)other.rightMotorSpeed / Max );
			return result;
		}


		/// <summary>Multiplies a <see cref="Vibration"/> with a value.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite, positive single-precision floating-point value.</param>
		/// <param name="result">Returns the scaled <see cref="Vibration"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vibration vibration, float value, out Vibration result )
		{
			result.leftMotorSpeed = (ushort)XMath.Clamp( (float)vibration.leftMotorSpeed * value, 0.0f, 65535.0f );
			result.rightMotorSpeed  = (ushort)XMath.Clamp( (float)vibration.rightMotorSpeed * value, 0.0f, 65535.0f );
		}

		/// <summary>Multiplies a <see cref="Vibration"/> with a value.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite, positive single-precision floating-point value.</param>
		/// <returns>Returns the scaled <see cref="Vibration"/>.</returns>
		public static Vibration Multiply( Vibration vibration, float value )
		{
			Vibration result;
			result.leftMotorSpeed = (ushort)XMath.Clamp( (float)vibration.leftMotorSpeed * value, 0.0f, 65535.0f );
			result.rightMotorSpeed = (ushort)XMath.Clamp( (float)vibration.rightMotorSpeed * value, 0.0f, 65535.0f );
			return result;
		}



		/// <summary>Performs a linear interpolation between two <see cref="Vibration"/> structures.</summary>
		/// <param name="source">A <see cref="Vibration"/> structure.</param>
		/// <param name="target">A <see cref="Vibration"/> structure.</param>
		/// <param name="amount">The weight factor.</param>
		/// <param name="result">Receives the interpolated <see cref="Vibration"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "1#", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Lerp( ref Vibration source, ref Vibration target, float amount, out Vibration result )
		{
			result.leftMotorSpeed = (ushort)XMath.Lerp( (float)source.leftMotorSpeed, (float)target.leftMotorSpeed, amount );
			result.rightMotorSpeed = (ushort)XMath.Lerp( (float)source.rightMotorSpeed, (float)target.rightMotorSpeed, amount );
		}

		/// <summary>Performs a linear interpolation between two <see cref="Vibration"/> structures.</summary>
		/// <param name="source">A <see cref="Vibration"/> structure.</param>
		/// <param name="target">A <see cref="Vibration"/> structure.</param>
		/// <param name="amount">The weight factor.</param>
		/// <returns>Returns the interpolated <see cref="Vibration"/>.</returns>
		public static Vibration Lerp( Vibration source, Vibration target, float amount )
		{
			Vibration result;
			result.leftMotorSpeed = (ushort)XMath.Lerp( (float)source.leftMotorSpeed, (float)target.leftMotorSpeed, amount );
			result.rightMotorSpeed = (ushort)XMath.Lerp( (float)source.rightMotorSpeed, (float)target.rightMotorSpeed, amount );
			return result;
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


		/// <summary>Addition operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the sum of the specified values.</returns>
		public static Vibration operator +( Vibration vibration, Vibration other )
		{
			Vibration sum;
			Add( ref vibration, ref other, out sum );
			return sum;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the difference between the two specified vibrations.</returns>
		public static Vibration operator -( Vibration vibration, Vibration other )
		{
			Vibration result;
			Subtract( ref vibration, ref other, out result );
			return result;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		public static Vibration operator *( Vibration vibration, Vibration other )
		{
			Vibration result;
			Multiply( ref vibration, ref other, out result );
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		public static Vibration operator *( Vibration vibration, float value )
		{
			Vibration result;
			Multiply( ref vibration, value, out result );
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		public static Vibration operator *( float value, Vibration vibration )
		{
			Vibration result;
			Multiply( ref vibration, value, out result );
			return result;
		}

		#endregion

	}

}