using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Describes the format of the raw input from a Human Interface Device (HID).
	/// <para>This structure is equivalent to the native <code>RAWHID</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645549%28v=vs.85%29.aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
	[Win32.Source( "WinUser.h", "RAWHID" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 12 (x86) or 16 (x64) bytes
	public struct RawHID : IEquatable<RawHID>
	{

		/// <summary>The size, in bytes, of each HID input in <see cref="RawData"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Size;

		/// <summary>The number of HID inputs in <see cref="RawData"/>.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Count;

		/// <summary>The raw input data.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly IntPtr RawData;



		/// <summary>Returns the raw input data, as an array of bytes.</summary>
		public byte[] GetData()
		{
			var data = new byte[ Size * Count ];
			Marshal.Copy( RawData, data, 0, data.Length );
			return data;
		}


		/// <summary>Returns a hash code for this <see cref="RawHID"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawHID"/> structure.</returns>
		public override int GetHashCode()
		{
			return Size ^ Count ^ RawData.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawHID"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawHID"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawHID other )
		{
			return
				( Size == other.Size ) &&
				( Count == other.Count ) &&
				( RawData == other.RawData );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawHID"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawHID"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawHID hid ) && this.Equals( hid );
		}



		/// <summary>The empty <see cref="RawHID"/> structure.</summary>
		public static readonly RawHID Empty;


		#region Operators

		/// <summary>Equality operator.</summary>
		/// <param name="rawHID">A <see cref="RawHID"/> structure.</param>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawHID rawHID, RawHID other )
		{
			return rawHID.Equals( other );
		}


		/// <summary>Inequality operator.</summary>
		/// <param name="rawHID">A <see cref="RawHID"/> structure.</param>
		/// <param name="other">A <see cref="RawHID"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HID" )]
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawHID rawHID, RawHID other )
		{
			return !rawHID.Equals( other );
		}

		#endregion Operators

	}

}