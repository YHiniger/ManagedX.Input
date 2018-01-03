using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Contains information on <see cref="BatteryType"/> and <see cref="BatteryLevel"/>.
	/// <para>This structure is equivalent to the native <code>XINPUT_BATTERY_INFORMATION</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "XInput.h", "XINPUT_BATTERY_INFORMATION" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 1, Size = 2 )]
	public struct BatteryInformation : IEquatable<BatteryInformation>
	{

		/// <summary>The type of battery.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly BatteryType Type;

		/// <summary>The charge state of the battery; only valid for wireless devices with a known <see cref="BatteryType"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly BatteryLevel Level;



		/// <summary>Returns a hash code for this <see cref="BatteryInformation"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="BatteryInformation"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)Type | ( (int)Level << 8 );
		}


		/// <summary>Returns a value indicating whether this <see cref="BatteryInformation"/> structure is equivalent to another structure of the same type.</summary>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if this <see cref="BatteryInformation"/> structure and the <paramref name="other"/> structure have the same battery type and level, otherwise returns false.</returns>
		public bool Equals( BatteryInformation other )
		{
			return this.GetHashCode() == other.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="BatteryInformation"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="BatteryInformation"/> structure equivalent to this structure.</returns>
		public override bool Equals( object obj )
		{
			return obj is BatteryInformation info && this.Equals( info );
		}


		/// <summary>Returns a string representing this <see cref="BatteryInformation"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="BatteryInformation"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Type: {0}, Level: {1}}}", Type, Level );
		}



		/// <summary>The "disconnected" <see cref="BatteryInformation"/> structure.</summary>
		public static readonly BatteryInformation Disconnected;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="batteryInfo">A <see cref="BatteryInformation"/> structure.</param>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( BatteryInformation batteryInfo, BatteryInformation other )
		{
			return batteryInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="batteryInfo">A <see cref="BatteryInformation"/> structure.</param>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if the structures are not equivalent, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( BatteryInformation batteryInfo, BatteryInformation other )
		{
			return !batteryInfo.Equals( other );
		}

		#endregion Operators

	}

}