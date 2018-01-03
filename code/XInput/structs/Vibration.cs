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
	[Win32.Source( "XInput.h", "XINPUT_VIBRATION" )]
	[System.Diagnostics.DebuggerStepThrough]
	[Serializable]
	[StructLayout( LayoutKind.Sequential, Pack = 2, Size = 4 )]
	public struct Vibration : IEquatable<Vibration>
	{

		internal ushort leftMotorSpeed;
		internal ushort rightMotorSpeed;



		#region Constructors

		/// <summary>Initializes a new <see cref="Vibration"/> structure.</summary>
		/// <param name="leftMotorSpeed">The speed of the left motor.</param>
		/// <param name="rightMotorSpeed">The speed of the right motor.</param>
		[CLSCompliant( false )]
		public Vibration( ushort leftMotorSpeed, ushort rightMotorSpeed )
		{
			this.leftMotorSpeed = leftMotorSpeed;
			this.rightMotorSpeed = rightMotorSpeed;
		}


		/// <summary>Initializes a new <see cref="Vibration"/> structure.</summary>
		/// <param name="leftMotorSpeed">The speed of the left motor, within the range [0,1].</param>
		/// <param name="rightMotorSpeed">The speed of the right motor, within the range [0,1].</param>
		public Vibration( float leftMotorSpeed, float rightMotorSpeed )
			: this( ToUShort( leftMotorSpeed ), ToUShort( rightMotorSpeed ) )
		{
		}

		#endregion Constructors



		/// <summary>Gets or sets the left motor speed, normalized within the range [0,1].</summary>
		public float LeftMotorSpeed
		{
			get => ToFloat( leftMotorSpeed );
			set => leftMotorSpeed = ToUShort( value );
		}


		/// <summary>Gets or sets the right motor speed, normalized within the range [0,1].</summary>
		public float RightMotorSpeed
		{
			get => ToFloat( rightMotorSpeed );
			set => rightMotorSpeed = ToUShort( value );
		}



		/// <summary>Returns a hash code for this <see cref="Vibration"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Vibration"/> structure.</returns>
		public override int GetHashCode()
		{
			return unchecked( (int)leftMotorSpeed | (int)rightMotorSpeed << 16 );
		}


		/// <summary>Returns a value indicating whether this <see cref="Vibration"/> structure is equivalent to another <see cref="Vibration"/> structure.</summary>
		/// <param name="other">A vibration structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> <see cref="Vibration"/> structure has the same motor speeds as this structure, otherwise returns false.</returns>
		public bool Equals( Vibration other )
		{
			return this.GetHashCode() == other.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Vibration"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Vibration"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Vibration vib && this.Equals( vib );
		}


		/// <summary>Returns a string representing this <see cref="Vibration"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="Vibration"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Left: {0}, Right: {1}}}", this.LeftMotorSpeed, this.RightMotorSpeed );
		}



		/// <summary>The zero <see cref="Vibration"/> structure.</summary>
		public static readonly Vibration Zero;


		#region Static methods

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
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Add( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			result = new Vibration(
				(ushort)Math.Min( (uint)vibration.leftMotorSpeed + (uint)other.leftMotorSpeed, (uint)ushort.MaxValue ),
				(ushort)Math.Min( (uint)vibration.rightMotorSpeed + (uint)other.rightMotorSpeed, (uint)ushort.MaxValue )
			);
		}

		/// <summary>Returns the sum of two <see cref="Vibration"/> structures.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> initialized with the sum of the two specified <see cref="Vibration"/> values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration Add( Vibration vibration, Vibration other )
		{
			return new Vibration(
				(ushort)Math.Min( (uint)vibration.leftMotorSpeed + (uint)other.leftMotorSpeed, (uint)ushort.MaxValue ),
				(ushort)Math.Min( (uint)vibration.rightMotorSpeed + (uint)other.rightMotorSpeed, (uint)ushort.MaxValue )
			);
		}


		/// <summary>Calculates the difference between two <see cref="Vibration"/>s.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vibration"/> structure initialized with the difference (clamped to the valid range) of the two specified vibrations.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Subtract( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			result = new Vibration(
				(ushort)XMath.Clamp( (int)vibration.leftMotorSpeed - (int)other.leftMotorSpeed, 0, (int)ushort.MaxValue ),
				(ushort)XMath.Clamp( (int)vibration.rightMotorSpeed - (int)other.rightMotorSpeed, 0, (int)ushort.MaxValue )
			);
		}

		/// <summary>Calculates the difference between two <see cref="Vibration"/>s.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initialized with the difference (clamped to the valid range) of the two specified vibrations.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration Subtract( Vibration vibration, Vibration other )
		{
			return new Vibration(
				(ushort)XMath.Clamp( (int)vibration.leftMotorSpeed - (int)other.leftMotorSpeed, 0, (int)ushort.MaxValue ),
				(ushort)XMath.Clamp( (int)vibration.rightMotorSpeed - (int)other.rightMotorSpeed, 0, (int)ushort.MaxValue )
			);
		}


		/// <summary>Calculates the product of two <see cref="Vibration"/> values.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <param name="result">Receives a <see cref="Vibration"/> initialized with the product of the two specified values.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vibration vibration, ref Vibration other, out Vibration result )
		{
			result = new Vibration(
				(ushort)( (uint)vibration.leftMotorSpeed * (uint)other.leftMotorSpeed / uint.MaxValue ),
				(ushort)( (uint)vibration.rightMotorSpeed * (uint)other.rightMotorSpeed / uint.MaxValue )
			);
		}

		/// <summary>Multiplies two <see cref="Vibration"/> values.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns the scaled <see cref="Vibration"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration Multiply( Vibration vibration, Vibration other )
		{
			return new Vibration(
				(ushort)( (uint)vibration.leftMotorSpeed * (uint)other.leftMotorSpeed / uint.MaxValue ),
				(ushort)( (uint)vibration.rightMotorSpeed * (uint)other.rightMotorSpeed / uint.MaxValue )
			);
		}


		/// <summary>Multiplies a <see cref="Vibration"/> with a value.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite, positive single-precision floating-point value.</param>
		/// <param name="result">Returns the scaled <see cref="Vibration"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Multiply( ref Vibration vibration, float value, out Vibration result )
		{
			result = new Vibration(
				(ushort)XMath.Clamp( (float)vibration.leftMotorSpeed * value, 0.0f, 65535.0f ),
				(ushort)XMath.Clamp( (float)vibration.rightMotorSpeed * value, 0.0f, 65535.0f )
			);
		}

		/// <summary>Multiplies a <see cref="Vibration"/> with a value.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite, positive single-precision floating-point value.</param>
		/// <returns>Returns the scaled <see cref="Vibration"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration Multiply( Vibration vibration, float value )
		{
			return new Vibration(
				(ushort)XMath.Clamp( (float)vibration.leftMotorSpeed * value, 0.0f, 65535.0f ),
				(ushort)XMath.Clamp( (float)vibration.rightMotorSpeed * value, 0.0f, 65535.0f )
			);
		}



		/// <summary>Performs a linear interpolation between two <see cref="Vibration"/> structures.</summary>
		/// <param name="source">A <see cref="Vibration"/> structure.</param>
		/// <param name="target">A <see cref="Vibration"/> structure.</param>
		/// <param name="amount">The weight factor.</param>
		/// <param name="result">Receives the interpolated <see cref="Vibration"/>.</param>
		[SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Performance matters." )]
		[SuppressMessage( "Microsoft.Design", "CA1021:AvoidOutParameters", Justification = "Performance matters." )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static void Lerp( ref Vibration source, ref Vibration target, float amount, out Vibration result )
		{
			result = new Vibration(
				(ushort)XMath.Lerp( (float)source.leftMotorSpeed, (float)target.leftMotorSpeed, amount ),
				(ushort)XMath.Lerp( (float)source.rightMotorSpeed, (float)target.rightMotorSpeed, amount )
			);
		}

		/// <summary>Performs a linear interpolation between two <see cref="Vibration"/> structures.</summary>
		/// <param name="source">A <see cref="Vibration"/> structure.</param>
		/// <param name="target">A <see cref="Vibration"/> structure.</param>
		/// <param name="amount">The weight factor.</param>
		/// <returns>Returns the interpolated <see cref="Vibration"/>.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration Lerp( Vibration source, Vibration target, float amount )
		{
			return new Vibration(
				(ushort)XMath.Lerp( (float)source.leftMotorSpeed, (float)target.leftMotorSpeed, amount ),
				(ushort)XMath.Lerp( (float)source.rightMotorSpeed, (float)target.rightMotorSpeed, amount )
			);
		}

		#endregion Static methods


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( Vibration vibration, Vibration other )
		{
			return vibration.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( Vibration vibration, Vibration other )
		{
			return !vibration.Equals( other );
		}


		/// <summary>Addition operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the sum of the specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration operator +( Vibration vibration, Vibration other )
		{
			Add( ref vibration, ref other, out Vibration sum );
			return sum;
		}


		/// <summary>Subtraction operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the difference between the two specified vibrations.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration operator -( Vibration vibration, Vibration other )
		{
			Subtract( ref vibration, ref other, out Vibration result );
			return result;
		}


		/// <summary>Multiplication operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="other">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration operator *( Vibration vibration, Vibration other )
		{
			Multiply( ref vibration, ref other, out Vibration result );
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration operator *( Vibration vibration, float value )
		{
			Multiply( ref vibration, value, out Vibration result );
			return result;
		}

		/// <summary>Multiplication operator.</summary>
		/// <param name="value">A finite single-precision floating-point value.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		/// <returns>Returns a <see cref="Vibration"/> structure initializes with the product of the specified values.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static Vibration operator *( float value, Vibration vibration )
		{
			Multiply( ref vibration, value, out Vibration result );
			return result;
		}

		#endregion Operators

	}

}