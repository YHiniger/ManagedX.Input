using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Serves as a standard header for information related to a device event reported through the WM_DEVICECHANGE message.
	/// <para>The members of the <see cref="DeviceBroadcastHeader"/> structure are contained in each device management structure. To determine which structure you have received through WM_DEVICECHANGE, treat the structure as a <see cref="DeviceBroadcastHeader"/> structure and check its DeviceType member.</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363246(v=vs.85).aspx</remarks>
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public abstract class DeviceBroadcastHeader
	{

		/// <summary>The size of this structure, in bytes.
		/// <para>If this is a user-defined event, this member must be the size of this header, plus the size of the variable-length data in the _DEV_BROADCAST_USERDEFINED structure.</para>
		/// </summary>
		private readonly int size;

		/// <summary>The device type, which determines the event-specific information that follows the first three members.</summary>
		internal readonly DeviceType DeviceType;

		private readonly int reserved;



		internal DeviceBroadcastHeader( int size, DeviceType deviceType )
		{
			this.size = size;
			DeviceType = deviceType;
			reserved = 0;
		}

	}

}