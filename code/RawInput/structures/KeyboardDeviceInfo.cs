using System;
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
	internal struct KeyboardDeviceInfo : IEquatable<KeyboardDeviceInfo>
	{

		/// <summary>The type of the keyboard.</summary>
		public readonly int KeyboardType;

		/// <summary>The subtype of the keyboard.</summary>
		public readonly int KeyboardSubtype;

		/// <summary>The scan code mode.</summary>
		public readonly int Mode;

		/// <summary>The number of function keys on the keyboard.</summary>
		public readonly int FunctionKeyCount;

		/// <summary>The number of LED indicators on the keyboard.</summary>
		public readonly int IndicatorCount;

		/// <summary>The total number of keys on the keyboard.</summary>
		public readonly int TotalKeyCount;


		
		public override int GetHashCode()
		{
			return KeyboardType ^ KeyboardSubtype ^ Mode ^ FunctionKeyCount ^ IndicatorCount ^ TotalKeyCount;
		}


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


		public override bool Equals( object obj )
		{
			return obj is KeyboardDeviceInfo info && this.Equals( info );
		}



		public static readonly KeyboardDeviceInfo Empty;


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return keyboardInfo.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( KeyboardDeviceInfo keyboardInfo, KeyboardDeviceInfo other )
		{
			return !keyboardInfo.Equals( other );
		}

		#endregion Operators

	}

}