using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from any device.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO</code> structure.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645581%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 32 )]
	internal struct DeviceInfo : IEquatable<DeviceInfo>
	{

		[StructLayout( LayoutKind.Explicit, Pack = 4, Size = 24 )]
		private struct Info
		{

			[FieldOffset( 0 )]
			internal KeyboardDeviceInfo Keyboard;

			[FieldOffset( 0 )]
			internal MouseDeviceInfo Mouse;

			[FieldOffset( 0 )]
			internal HumanInterfaceDeviceInfo HID;


			internal static readonly Info Empty;

		}



		internal readonly int StructSize;
		/// <summary>The device type.</summary>
		internal readonly InputDeviceType DeviceType;
		private readonly Info info;



		private DeviceInfo( int structureSize )
		{
			StructSize = structureSize;
			DeviceType = InputDeviceType.Mouse;
			info = Info.Empty;
		}



		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.Mouse"/>, gets information about the mouse.</summary>
		public MouseDeviceInfo? MouseInfo
		{
			get
			{
				if( DeviceType == InputDeviceType.Mouse )
					return info.Mouse;
				return null;
			}
		}


		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.Keyboard"/>, gets information about the keyboard.</summary>
		public KeyboardDeviceInfo? KeyboardInfo
		{
			get
			{
				if( DeviceType == InputDeviceType.Keyboard )
					return info.Keyboard;
				return null;
			}
		}


		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.HumanInterfaceDevice"/>, gets information about the HID.</summary>
		public HumanInterfaceDeviceInfo? HumanInterfaceDeviceInfo
		{
			get
			{
				if( DeviceType == InputDeviceType.HumanInterfaceDevice )
					return info.HID;
				return null;
			}
		}


		/// <summary>Returns a hash code for this <see cref="DeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			var hashCode = StructSize ^ (int)DeviceType;
			
			if( DeviceType == InputDeviceType.Keyboard )
				return hashCode ^ info.Keyboard.GetHashCode();

			if( DeviceType == InputDeviceType.Mouse )
				return hashCode ^ info.Mouse.GetHashCode();

			return hashCode ^ info.HID.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A this <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="DeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
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


		/// <summary>Returns a value indicating whether this <see cref="DeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DeviceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DeviceInfo ) && this.Equals( (DeviceInfo)obj );
		}
		


		/// <summary>The default <see cref="DeviceInfo"/> structure.</summary>
		public static readonly DeviceInfo Default = new DeviceInfo( Marshal.SizeOf( typeof( DeviceInfo ) ) );


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="deviceInfo">A <see cref="DeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equal, otherwise returns false.</returns>
		public static bool operator ==( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return deviceInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="deviceInfo">A <see cref="DeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( DeviceInfo deviceInfo, DeviceInfo other )
		{
			return !deviceInfo.Equals( other );
		}

		#endregion Operators

	}

}