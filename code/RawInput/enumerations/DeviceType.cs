namespace ManagedX.Input.Raw
{
	using Win32;


	/// <summary>Device types used by <see cref="DeviceBroadcastHeader"/>.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363246(v=vs.85).aspx</remarks>
	internal enum DeviceType : int
	{

		/// <summary>OEM- or IHV-defined device type. This structure is a <see cref="DeviceBroadcastOEM"/> structure.</summary>
		[Source( "DBT.h", "DBT_DEVTYP_OEM" )]
		OEM = 0,


		/// <summary>Logical volume. This structure is a DEV_BROADCAST_VOLUME structure.</summary>
		[Source( "DBT.h", "DBT_DEVTYP_VOLUME" )]
		Volume = 2,

		/// <summary>Port device (serial or parallel). This structure is a DEV_BROADCAST_PORT structure.</summary>
		[Source( "DBT.h", "DBT_DEVTYP_PORT" )]
		Port = 3,


		/// <summary>Class of devices. This structure is a DEV_BROADCAST_DEVICEINTERFACE structure.</summary>
		[Source( "DBT.h", "DBT_DEVTYP_DEVICEINTERFACE" )]
		DeviceInterface = 5,

		/// <summary>File system handle. This structure is a DEV_BROADCAST_HANDLE structure.</summary>
		[Source( "DBT.h", "DBT_DEVTYP_HANDLE" )]
		Handle = 6,

	}
	
}