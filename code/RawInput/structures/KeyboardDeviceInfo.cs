using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified keyboard.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_KEYBOARD</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645587%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO_KEYBOARD" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 24 )]
	public struct KeyboardDeviceInfo : IEquatable<KeyboardDeviceInfo>
	{

		/// <summary>The type of the keyboard.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int KeyboardType;

		/// <summary>The subtype of the keyboard.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int KeyboardSubtype;

		/// <summary>The scan code mode.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Mode;

		/// <summary>The number of function keys on the keyboard.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int FunctionKeyCount;

		/// <summary>The number of LED indicators on the keyboard.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int IndicatorCount;

		/// <summary>The total number of keys on the keyboard.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int TotalKeyCount;



		/// <summary>Returns a hash code for this <see cref="KeyboardDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="KeyboardDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return KeyboardType ^ KeyboardSubtype ^ Mode ^ FunctionKeyCount ^ IndicatorCount ^ TotalKeyCount;
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardDeviceInfo"/> structure is equivalent to another <see cref="KeyboardDeviceInfo"/> structure.</summary>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, false otherwise.</returns>
		public bool Equals( KeyboardDeviceInfo other )
		{
			return
				( KeyboardType == other.KeyboardType ) &&
				( KeyboardSubtype == other.KeyboardSubtype ) &&
				( Mode == other.Mode ) &&
				( FunctionKeyCount == other.FunctionKeyCount ) &&
				( IndicatorCount == other.IndicatorCount ) &&
				( TotalKeyCount == other.TotalKeyCount );
		}


		/// <summary>Returns a value indicating whether this <see cref="KeyboardDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="KeyboardDeviceInfo"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is KeyboardDeviceInfo info && this.Equals( info );
		}



		/// <summary>The empty <see cref="KeyboardDeviceInfo"/> structure.</summary>
		public static readonly KeyboardDeviceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="keyboardInfo">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return keyboardInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="keyboardInfo">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="KeyboardDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return !keyboardInfo.Equals( other );
		}

		#endregion Operators

	}

}