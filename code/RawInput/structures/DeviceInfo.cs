using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from any device.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO</code> structure.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645581%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 32 )]
	internal struct DeviceInfo : IEquatable<DeviceInfo>
	{

		[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 24 )]
		private struct Union
		{

			[FieldOffset( 0 )]
			internal KeyboardDeviceInfo Keyboard;	// Size = 24, Pack = 4

			[FieldOffset( 0 )]
			internal MouseDeviceInfo Mouse;			// Size = 16, Pack = 4

			[FieldOffset( 0 )]
			internal HumanInterfaceDeviceInfo HID;  // Size = 16, Pack = 4


			internal static readonly Union Empty;

		}



		internal readonly int StructSize;
		public readonly InputDeviceType DeviceType;
		private readonly Union info;



		private DeviceInfo( int structureSize )
		{
			StructSize = structureSize;
			DeviceType = InputDeviceType.Mouse;
			info = Union.Empty;
		}



		public MouseDeviceInfo MouseInfo => DeviceType == InputDeviceType.Mouse ? info.Mouse : MouseDeviceInfo.Empty;


		public KeyboardDeviceInfo KeyboardInfo => DeviceType == InputDeviceType.Keyboard ? info.Keyboard : KeyboardDeviceInfo.Empty;


		public HumanInterfaceDeviceInfo HumanInterfaceDeviceInfo => DeviceType == InputDeviceType.HumanInterfaceDevice ? info.HID : HumanInterfaceDeviceInfo.Empty;


		public override int GetHashCode()
		{
			var hashCode = StructSize ^ (int)DeviceType;
			
			if( DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ info.Keyboard.GetHashCode();

			if( DeviceType == InputDeviceType.Mouse )
				return hashCode ^ info.Mouse.GetHashCode();

			return hashCode ^ info.HID.GetHashCode();
		}


		public bool Equals( DeviceInfo other )
		{
			if( StructSize != other.StructSize || DeviceType != other.DeviceType )
				return false;

			if( DeviceType == InputDeviceType.Keyboard )
				return info.Keyboard.Equals( other.info.Keyboard );

			if( DeviceType == InputDeviceType.Mouse )
				return info.Mouse.Equals( other.info.Mouse );

			return info.HID.Equals( other.info.HID );
		}


		public override bool Equals( object obj )
		{
			return ( obj is DeviceInfo ) && this.Equals( (DeviceInfo)obj );
		}
		


		public static readonly DeviceInfo Default = new DeviceInfo( 32 );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="deviceInfo">A <see cref="DeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return deviceInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="deviceInfo">A <see cref="DeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return !deviceInfo.Equals( other );
		}

		#endregion Operators

	}

}