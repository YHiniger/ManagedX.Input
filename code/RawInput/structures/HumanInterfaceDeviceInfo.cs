using System;
using System.Diagnostics.CodeAnalysis;
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
	public struct HumanInterfaceDeviceInfo : IEquatable<HumanInterfaceDeviceInfo>
	{

		/// <summary>The vendor identifier for the HID.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int VendorId;

		/// <summary>The product identifier for the HID.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int ProductId;

		/// <summary>The version number for the HID.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int VersionNumber;

		/// <summary>The top-level collection (TLC usage page and usage) for the device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly TopLevelCollectionUsage TopLevelCollection;



		/// <summary>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="HumanInterfaceDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return VendorId ^ ProductId ^ VersionNumber ^ (int)TopLevelCollection;
		}


		/// <summary>Returns a value indicating whether this <see cref="HumanInterfaceDeviceInfo"/> structure is equivalent to another <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, false otherwise.</returns>
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
		/// <returns>Returns true if the specified object is a <see cref="HumanInterfaceDeviceInfo"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is HumanInterfaceDeviceInfo info ) && this.Equals( info );
		}



		/// <summary>The empty <see cref="HumanInterfaceDeviceInfo"/> structure.</summary>
		public static readonly HumanInterfaceDeviceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="hidInfo">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return hidInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="hidInfo">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( HumanInterfaceDeviceInfo hidInfo, HumanInterfaceDeviceInfo other )
		{
			return !hidInfo.Equals( other );
		}

		#endregion Operators

	}

}