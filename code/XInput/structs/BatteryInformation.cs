using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Contains information on <see cref="BatteryType"/> and <see cref="BatteryLevel"/>.
	/// <para>This structure is equivalent to the native <code>XINPUT_BATTERY_INFORMATION</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_battery_information%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "XInput.h", "XINPUT_BATTERY_INFORMATION" )]
	[StructLayout( LayoutKind.Sequential, Pack = 1, Size = 2 )]
	public struct BatteryInformation : IEquatable<BatteryInformation>
	{

		private BatteryType type;
		private BatteryLevel level;



		/// <summary>Gets the type of battery.</summary>
		public BatteryType BatteryType { get { return type; } }


		/// <summary>Gets the charge state of the battery; only valid for wireless devices with a known <see cref="BatteryType"/>.</summary>
		public BatteryLevel BatteryLevel { get { return level; } }


		/// <summary>Returns a hash code for this <see cref="BatteryInformation"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="BatteryInformation"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)type | ( (int)level << 8 );
		}


		/// <summary>Returns a value indicating whether this <see cref="BatteryInformation"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if this <see cref="BatteryInformation"/> structure and the <paramref name="other"/> structure have the same battery type and level, otherwise returns false.</returns>
		public bool Equals( BatteryInformation other )
		{
			return ( type == other.type ) && ( level == other.level );
		}


		/// <summary>Returns a value indicating whether this <see cref="BatteryInformation"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="BatteryInformation"/> structure which equals this structure.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is BatteryInformation ) && this.Equals( (BatteryInformation)obj );
		}


		/// <summary>Returns a string representing this <see cref="BatteryInformation"/> structure.</summary>
		/// <returns>Returns a string representing this <see cref="BatteryInformation"/> structure.</returns>
		public override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Type: {0}, Level: {1}}}", type, level );
		}



		/// <summary>The "disconnected" <see cref="BatteryInformation"/> structure.</summary>
		public static readonly BatteryInformation Disconnected;


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="batteryInfo">A <see cref="BatteryInformation"/> structure.</param>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( BatteryInformation batteryInfo, BatteryInformation other )
		{
			return batteryInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="batteryInfo">A <see cref="BatteryInformation"/> structure.</param>
		/// <param name="other">A <see cref="BatteryInformation"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( BatteryInformation batteryInfo, BatteryInformation other )
		{
			return !batteryInfo.Equals( other );
		}


		#endregion

	}

}