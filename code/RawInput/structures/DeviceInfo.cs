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
	[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 32 )]
	internal struct DeviceInfo : IEquatable<DeviceInfo>
	{

		[FieldOffset( 0 )]
		internal readonly int StructSize;
		[FieldOffset( 4 )]
		public readonly InputDeviceType DeviceType;
		[FieldOffset( 8 )]
		private readonly KeyboardDeviceInfo keyboard;   // Size = 24, Pack = 4
		[FieldOffset( 8 )]
		private readonly MouseDeviceInfo mouse;         // Size = 16, Pack = 4
		[FieldOffset( 8 )]
		private readonly HumanInterfaceDeviceInfo hid;  // Size = 16, Pack = 4



		private DeviceInfo( int structureSize )
		{
			StructSize = structureSize;
			DeviceType = InputDeviceType.Mouse;
			keyboard = KeyboardDeviceInfo.Empty;
			mouse = MouseDeviceInfo.Empty;
			hid = HumanInterfaceDeviceInfo.Empty;
		}



		public MouseDeviceInfo MouseInfo => DeviceType == InputDeviceType.Mouse ? mouse : MouseDeviceInfo.Empty;


		public KeyboardDeviceInfo KeyboardInfo => DeviceType == InputDeviceType.Keyboard ? keyboard : KeyboardDeviceInfo.Empty;


		public HumanInterfaceDeviceInfo HumanInterfaceDeviceInfo => DeviceType == InputDeviceType.HumanInterfaceDevice ? hid : HumanInterfaceDeviceInfo.Empty;


		public override int GetHashCode()
		{
			var hashCode = StructSize ^ (int)DeviceType;

			if( DeviceType == InputDeviceType.Mouse )
				return hashCode ^ mouse.GetHashCode();

			if( DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ keyboard.GetHashCode();

			return hashCode ^ hid.GetHashCode();
		}


		public bool Equals( DeviceInfo other )
		{
			if( StructSize != other.StructSize || DeviceType != other.DeviceType )
				return false;

			if( DeviceType == InputDeviceType.Mouse )
				return mouse.Equals( other.mouse );

			if( DeviceType == InputDeviceType.Keyboard )
				return keyboard.Equals( other.keyboard );

			return hid.Equals( other.hid );
		}


		public override bool Equals( object obj )
		{
			return obj is DeviceInfo info && this.Equals( info );
		}
		


		public static readonly DeviceInfo Default = new DeviceInfo( 32 );


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return deviceInfo.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return !deviceInfo.Equals( other );
		}

		#endregion Operators

	}

}