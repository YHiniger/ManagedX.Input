using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645584(v=vs.85).aspx


	/// <summary>Defines the raw input data coming from any device.
	/// <para>The native name of this structure is RID_DEVICEINFO.</para>
	/// </summary>
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


			internal static readonly Info Empty = new Info();

		}


		private int structSize;
		private InputDeviceType type;
		private Info info;


		private DeviceInfo( int structureSize )
		{
			structSize = structureSize;
			type = InputDeviceType.Mouse;
			info = Info.Empty;
		}


		internal int StructureSize { get { return structSize; } }


		/// <summary>Gets the device type.</summary>
		public InputDeviceType Type { get { return type; } }


		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.Mouse"/>, gets information about the mouse.</summary>
		public MouseDeviceInfo? MouseInfo
		{
			get
			{
				if( type == InputDeviceType.Mouse )
					return info.Mouse;
				return null;
			}
		}


		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.Keyboard"/>, gets information about the keyboard.</summary>
		public KeyboardDeviceInfo? KeyboardInfo
		{
			get
			{
				if( type == InputDeviceType.Keyboard )
					return info.Keyboard;
				return null;
			}
		}


		/// <summary>When <see cref="InputDeviceType"/> is <see cref="InputDeviceType.HumanInterfaceDevice"/>, gets information about the HID.</summary>
		public HumanInterfaceDeviceInfo? HumanInterfaceDeviceInfo
		{
			get
			{
				if( type == InputDeviceType.HumanInterfaceDevice )
					return info.HID;
				return null;
			}
		}




		/// <summary>Returns a hash code for this <see cref="DeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			var hashCode = structSize ^ (int)type;
			
			if( type == InputDeviceType.Keyboard )
				return hashCode ^ info.Keyboard.GetHashCode();

			if( type == InputDeviceType.Mouse )
				return hashCode ^ info.Mouse.GetHashCode();

			return hashCode ^ info.HID.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A this <see cref="DeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="DeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( DeviceInfo other )
		{
			if( structSize != other.structSize || type != other.type )
				return false;

			if( type == InputDeviceType.Keyboard )
				return info.Keyboard.Equals( other.info.Keyboard );

			if( type == InputDeviceType.Mouse )
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
		
		#endregion

	}

}