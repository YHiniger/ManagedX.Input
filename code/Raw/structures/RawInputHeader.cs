using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains the header information that is part of the raw input data.
	/// <para>This structure is equivalent to the native <code>RAWINPUTHEADER</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645571%28v=vs.85%29.aspx</remarks>
	[ManagedX.Design.Native( "WinUser.h", "RAWINPUTHEADER" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 16/24 bytes (x86/x64)
	internal struct RawInputHeader : IEquatable<RawInputHeader>
	{

		/// <summary>The type of raw input.</summary>
		internal InputDeviceType DeviceType;

		/// <summary>The size, in bytes, of the entire input packet of data.
		/// <para>This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array.</para>
		/// </summary>
		internal int Size;

		/// <summary>A handle to the device generating the raw input data.</summary>
		internal IntPtr DeviceHandle;

		/// <summary>The value passed in the WParam parameter of the WM_INPUT message.</summary>
		internal IntPtr WParameter;


		internal RawInputHeader( InputDeviceType deviceType, int structSize, IntPtr device, IntPtr param )
		{
			DeviceType = deviceType;
			Size = structSize;
			DeviceHandle = device;
			this.WParameter = param;
		}


		/// <summary>Returns a hash code for this <see cref="RawInputHeader"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputHeader"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)DeviceType ^ Size ^ DeviceHandle.GetHashCode() ^ WParameter.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputHeader"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputHeader"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputHeader"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawInputHeader other )
		{
			return
				( DeviceType == other.DeviceType ) &&
				( Size == other.Size ) &&
				( DeviceHandle == other.DeviceHandle ) &&
				( WParameter == other.WParameter );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputHeader"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInputHeader"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInputHeader ) && this.Equals( (RawInputHeader)obj );
		}

	}

}