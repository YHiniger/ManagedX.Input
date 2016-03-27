using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Describes the format of the raw input from a Human Interface Device (HID).
	/// <para>This structure is equivalent to the native <code>RAWHID</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645549%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
	[Win32.Native( "WinUser.h", "RAWHID" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 12 (x86) or 16 (x64) bytes
	public struct RawHID : IEquatable<RawHID>
	{

		private int size;
		private int count;
		private IntPtr rawData;


		/// <summary>Gets the size, in bytes, of each HID input in <see cref="rawData"/>.</summary>
		public int Size { get { return size; } }
		
		/// <summary>Gets the number of HID inputs in <see cref="rawData"/>.</summary>
		public int Count { get { return count; } }
		
		/// <summary>Returns the raw input data, as an array of bytes.</summary>
		public byte[] GetData()
		{
			var data = new byte[ size * count ];
			Marshal.Copy( rawData, data, 0, data.Length );
			return data;
		}


		/// <summary>Returns a hash code for this <see cref="RawHID"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawHID"/> structure.</returns>
		public override int GetHashCode()
		{
			return size ^ count ^ rawData.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawHID"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawHID"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawHID other )
		{
			return
				( size == other.size ) &&
				( count == other.count ) &&
				( rawData == other.rawData );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawHID"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawHID"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawHID ) && this.Equals( (RawHID)obj );
		}


		/// <summary>The empty <see cref="RawHID"/> structure.</summary>
		public static readonly RawHID Empty = new RawHID();


		#region Operators

		/// <summary>Equality operator.</summary>
		/// <param name="rawHID">A <see cref="RawHID"/> structure.</param>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static bool operator ==( RawHID rawHID, RawHID other )
		{
			return rawHID.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="rawHID">A <see cref="RawHID"/> structure.</param>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		public static bool operator !=( RawHID rawHID, RawHID other )
		{
			return !rawHID.Equals( other );
		}

		#endregion

	}

}
