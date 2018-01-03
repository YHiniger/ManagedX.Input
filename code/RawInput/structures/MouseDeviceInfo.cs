using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified mouse.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_MOUSE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645589%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO_MOUSE" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct MouseDeviceInfo : IEquatable<MouseDeviceInfo>
	{

		/// <summary>The identifier of the mouse device.</summary>
		public readonly int Id;

		/// <summary>The number of buttons for the mouse.</summary>
		public readonly int ButtonCount;

		/// <summary>The number of data points per second.
		/// <para>This information may not be applicable for every mouse device.</para>
		/// </summary>
		public readonly int SampleRate;

		/// <summary>Indicates whether the mouse has a wheel for horizontal scrolling.</summary>
		[MarshalAs( UnmanagedType.Bool )]
		public readonly bool HasHorizontalWheel;




		public override int GetHashCode()
		{
			return Id ^ ButtonCount ^ SampleRate ^ ( HasHorizontalWheel ? -1 : 0 );
		}


		public bool Equals( MouseDeviceInfo other )
		{
			return
				( Id == other.Id ) &&
				( ButtonCount == other.ButtonCount ) &&
				( SampleRate == other.SampleRate ) &&
				( HasHorizontalWheel == other.HasHorizontalWheel );
		}


		public override bool Equals( object obj )
		{
			return ( obj is MouseDeviceInfo info ) && this.Equals( info );
		}



		public static readonly MouseDeviceInfo Empty;


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return mouseInfo.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return !mouseInfo.Equals( other );
		}

		#endregion Operators

	}

}