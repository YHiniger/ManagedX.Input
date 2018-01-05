using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Contains information about a OEM-defined device type.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/aa363247(v=vs.85).aspx</remarks>
	[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OEM" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public sealed class DeviceBroadcastOEM : DeviceBroadcastHeader
	{

		/// <summary>The OEM-specific identifier for the device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Identifier;

		/// <summary>The OEM-specific function value. Possible values depend on the device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int SuppFunc;



		internal DeviceBroadcastOEM()
			: base( 20, DeviceType.OEM )
		{
		}

	}

}