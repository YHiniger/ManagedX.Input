using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified Human Interface Device (HID).
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_HID</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645584%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO_HID" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct HumanInterfaceDeviceInfo : IEquatable<HumanInterfaceDeviceInfo>
	{

		/// <summary>The vendor identifier for the HID.</summary>
		public readonly int VendorId;

		/// <summary>The product identifier for the HID.</summary>
		public readonly int ProductId;

		/// <summary>The version number for the HID.</summary>
		public readonly int VersionNumber;

		/// <summary>The top-level collection (TLC usage page and usage) for the device.</summary>
		internal readonly TopLevelCollectionUsage TopLevelCollection;



		public override int GetHashCode()
		{
			return VendorId ^ ProductId ^ VersionNumber ^ (int)TopLevelCollection;
		}


		public bool Equals( HumanInterfaceDeviceInfo other )
		{
			return
				( VendorId == other.VendorId ) &&
				( ProductId == other.ProductId ) &&
				( VersionNumber == other.VersionNumber ) &&
				( TopLevelCollection == other.TopLevelCollection );
		}


		public override bool Equals( object obj )
		{
			return ( obj is HumanInterfaceDeviceInfo info ) && this.Equals( info );
		}


		public static readonly HumanInterfaceDeviceInfo Empty;


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return hidInfo.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return !hidInfo.Equals( other );
		}

		#endregion Operators

	}

}