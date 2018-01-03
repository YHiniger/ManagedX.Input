using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains the header information that is part of the raw input data.
	/// <para>This structure is equivalent to the native <code>RAWINPUTHEADER</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645571%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUTHEADER" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 16/24 bytes (x86/x64)
	internal struct RawInputHeader : IEquatable<RawInputHeader>
	{

		/// <summary>The type of raw input.</summary>
		public readonly InputDeviceType DeviceType;

		/// <summary>The size, in bytes, of the entire input packet of data.
		/// <para>This includes RAWINPUT plus possible extra input reports in the RAWHID variable length array.</para>
		/// </summary>
		private readonly int size;

		/// <summary>A handle to the device generating the raw input data.</summary>
		public readonly IntPtr DeviceHandle;

		/// <summary>The value passed in the WParam parameter of the WM_INPUT message.</summary>
		public readonly IntPtr Parameter;



		internal RawInputHeader( InputDeviceType deviceType, int structSize, IntPtr device, IntPtr param )
		{
			DeviceType = deviceType;
			size = structSize;
			DeviceHandle = device;
			Parameter = param;
		}



		public override int GetHashCode()
		{
			return (int)DeviceType ^ size ^ DeviceHandle.GetHashCode() ^ Parameter.GetHashCode();
		}


		public bool Equals( RawInputHeader other )
		{
			return
				( DeviceType == other.DeviceType ) &&
				( size == other.size ) &&
				( DeviceHandle == other.DeviceHandle ) &&
				( Parameter == other.Parameter );
		}


		public override bool Equals( object obj )
		{
			return ( obj is RawInputHeader header ) && this.Equals( header );
		}


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawInputHeader header, RawInputHeader other )
		{
			return header.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawInputHeader header, RawInputHeader other )
		{
			return !header.Equals( other );
		}

		#endregion Operators

	}

}