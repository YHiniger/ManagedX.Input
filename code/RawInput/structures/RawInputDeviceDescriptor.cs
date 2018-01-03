using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains information about a raw input device.
	/// <para>This structure is equivalent to the native <code>RAWINPUTDEVICELIST</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645568(v=vs.85).aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUTDEVICELIST" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential )] // Size = 8 (x86) or 12 (x64) bytes
	public struct RawInputDeviceDescriptor : IEquatable<RawInputDeviceDescriptor>
	{

		/// <summary>A handle to the raw input device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly IntPtr DeviceHandle;

		/// <summary>The device type.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly InputDeviceType DeviceType;



		/// <summary>Returns a hash code for this <see cref="RawInputDeviceDescriptor"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputDeviceDescriptor"/> structure.</returns>
		public override int GetHashCode()
		{
			return DeviceHandle.GetHashCode() ^ DeviceType.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDeviceDescriptor"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputDeviceDescriptor"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawInputDeviceDescriptor other )
		{
			return ( DeviceHandle == other.DeviceHandle ) && ( DeviceType == other.DeviceType );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDeviceDescriptor"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInputDeviceDescriptor"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInputDeviceDescriptor desc ) && this.Equals( desc );
		}



		/// <summary>The empty <see cref="RawInputDeviceDescriptor"/> structure.</summary>
		public static readonly RawInputDeviceDescriptor Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="descriptor">A <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <param name="other">A <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawInputDeviceDescriptor descriptor, RawInputDeviceDescriptor other )
		{
			return descriptor.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="descriptor">A <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <param name="other">A <see cref="RawInputDeviceDescriptor"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawInputDeviceDescriptor descriptor, RawInputDeviceDescriptor other )
		{
			return !descriptor.Equals( other );
		}

		#endregion Operators

	}

}