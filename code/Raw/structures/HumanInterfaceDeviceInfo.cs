using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified Human Interface Device (HID).
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_HID</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645584%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinUser.h", "RID_DEVICE_INFO_HID" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct HumanInterfaceDeviceInfo : IEquatable<HumanInterfaceDeviceInfo>
	{

		/// <summary>The vendor identifier for the HID.</summary>
		internal int VendorId;
		/// <summary>The product identifier for the HID.</summary>
		internal int ProductId;
		/// <summary>The version number for the HID.</summary>
		internal int VersionNumber;
		/// <summary>The top-level collection (TLC usage page and usage) for the device.</summary>
		internal TopLevelCollectionUsage TopLevelCollection;



		/// <summary>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return VendorId ^ ProductId ^ VersionNumber ^ (int)TopLevelCollection;
		}


		/// <summary>Returns a value indicating whether this <see cref="HumanInterfaceDeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="HumanInterfaceDeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( HumanInterfaceDeviceInfo other )
		{
			return
				( VendorId == other.VendorId ) &&
				( ProductId == other.ProductId ) &&
				( VersionNumber == other.VersionNumber ) &&
				( TopLevelCollection == other.TopLevelCollection );
		}


		/// <summary>Returns a value indicating whether this <see cref="HumanInterfaceDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="HumanInterfaceDeviceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is HumanInterfaceDeviceInfo ) && this.Equals( (HumanInterfaceDeviceInfo)obj );
		}


		/// <summary>The empty <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		public static readonly HumanInterfaceDeviceInfo Empty;


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