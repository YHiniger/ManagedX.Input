using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645584%28v=vs.85%29.aspx


	/// <summary>Defines the raw input data coming from the specified Human Interface Device (HID).
	/// <para>The native name of this structure is RID_DEVICE_INFO_HID.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct HumanInterfaceDeviceInfo : IEquatable<HumanInterfaceDeviceInfo>
	{

		private int vendorId;
		private int productId;
		private int versionNumber;
		private short usagePage;	// For more info about usage page and usage id, see:
		private short usage;		// https://msdn.microsoft.com/en-us/library/windows/hardware/ff543477%28v=vs.85%29.aspx
		// DirectInput Gamepad: 1, 5 (shared access)
		// Consumer Audio Control: 12, 1 (shared access)
		// Joystick (1, 4) and System Control (1, 0x80) have also shared access; all other have exclusive access.


		/// <summary>The vendor identifier for the HID.</summary>
		public int VendorId { get { return vendorId; } }

		/// <summary>The product identifier for the HID.</summary>
		public int ProductId { get { return productId; } }

		/// <summary>The version number for the HID.</summary>
		public int VersionNumber { get { return versionNumber; } }

		/// <summary>The top-level collection Usage Page for the device.</summary>
		public short UsagePage { get { return usagePage; } }

		/// <summary>The top-level collection Usage for the device.</summary>
		public short Usage { get { return usage; } }


		/// <summary>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			var buffer = new byte[ 4 ];
			Array.Copy( BitConverter.GetBytes( usagePage ), 0, buffer, 0, 2 );
			Array.Copy( BitConverter.GetBytes( usage ), 0, buffer, 2, 2 );
			return vendorId ^ productId ^ versionNumber ^ BitConverter.ToInt32( buffer, 0 );
		}


		/// <summary>Returns a value indicating whether this <see cref="HumanInterfaceDeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="HumanInterfaceDeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( HumanInterfaceDeviceInfo other )
		{
			return
				( vendorId == other.vendorId ) &&
				( productId == other.productId ) &&
				( versionNumber == other.versionNumber ) &&
				( usagePage == other.usagePage ) &&
				( usage == other.usage );
		}


		/// <summary>Returns a value indicating whether this <see cref="HumanInterfaceDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="HumanInterfaceDeviceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is HumanInterfaceDeviceInfo ) && this.Equals( (HumanInterfaceDeviceInfo)obj );
		}


		/// <summary>The empty <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		public static readonly HumanInterfaceDeviceInfo Empty = new HumanInterfaceDeviceInfo();


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="hidInfo">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equal, otherwise returns false.</returns>
		public static bool operator ==( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return hidInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="hidInfo">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return !hidInfo.Equals( other );
		}
		
		#endregion

	}

}