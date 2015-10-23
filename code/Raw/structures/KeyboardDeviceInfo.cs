using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645587%28v=vs.85%29.aspx


	/// <summary>Defines the raw input data coming from the specified keyboard.
	/// <para>The native name of this structure is RID_DEVICE_INFO_KEYBOARD.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct KeyboardDeviceInfo : IEquatable<KeyboardDeviceInfo>
	{

		/// <summary>Defines the usage page for the keyboard: 1.</summary>
		public const short UsagePage = 1;
		
		/// <summary>Defines the usage for the keyboard: 6.</summary>
		public const short Usage = 6;


		private int type;
		private int subType;
		private int mode;
		private int functionKeyCount;
		private int indicatorCount;
		private int totalKeyCount;


		/// <summary>Gets the type of the keyboard.</summary>
		public int KeyboardType { get { return type; } }
		
		/// <summary>Gets the subtype of the keyboard.</summary>
		public int KeyboardSubType { get { return subType; } }
		
		/// <summary>Gets the scan code mode.</summary>
		public int Mode { get { return mode; } }
		
		/// <summary>Gets the number of function keys on the keyboard.</summary>
		public int FunctionKeyCount { get { return functionKeyCount; } }
		
		/// <summary>Gets the number of LED indicators on the keyboard.</summary>
		public int IndicatorCount { get { return indicatorCount; } }

		/// <summary>Gets the total number of keys on the keyboard.</summary>
		public int TotalKeyCount { get { return totalKeyCount; } }

		
		/// <summary>Returns a hash code for this <see cref="KeyboardDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="KeyboardDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return type ^ subType ^ mode ^ functionKeyCount ^ indicatorCount ^ totalKeyCount;
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardDeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="KeyboardDeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( KeyboardDeviceInfo other )
		{
			return
				( type == other.type ) &&
				( subType == other.subType ) &&
				( mode == other.mode ) &&
				( functionKeyCount == other.functionKeyCount ) &&
				( indicatorCount == other.indicatorCount ) &&
				( totalKeyCount == other.totalKeyCount );
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="KeyboardDeviceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is KeyboardDeviceInfo ) && this.Equals( (KeyboardDeviceInfo)obj );
		}


		/// <summary>The empty <see cref="KeyboardDeviceInfo"/> structure.</summary>
		public static readonly KeyboardDeviceInfo Empty = new KeyboardDeviceInfo();


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="keyboardInfo">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equal, otherwise returns false.</returns>
		public static bool operator ==( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return keyboardInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="keyboardInfo">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return !keyboardInfo.Equals( other );
		}

		#endregion

	}

}