using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines information for the raw input devices.
	/// <para>This structure is equivalent to the native <code>RAWINPUTDEVICE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645565%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUTDEVICE" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )] // Size = 12 (x86) or 16 (x64) bytes
	internal struct RawInputDevice : IEquatable<RawInputDevice>
	{

		/// <summary>The top-level collection (usage page and usage) for the raw input device.</summary>
		public readonly TopLevelCollectionUsage TopLevelCollection;

		// combines UsagePage and Usage.
		/// <summary>Specifies how to interpret the information provided by <see cref="TopLevelCollection"/>. It can be zero (the default).
		/// <para>By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</para>
		/// </summary>
		public readonly RawInputDeviceRegistrationOptions RegistrationOptions;

		/// <summary>A handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</summary>
		public readonly IntPtr TargetWindowHandle;



		internal RawInputDevice( TopLevelCollectionUsage topLevelCollection, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.TopLevelCollection = topLevelCollection;
			this.RegistrationOptions = options;
			this.TargetWindowHandle = targetWindowHandle;
		}



		public override int GetHashCode()
		{
			return TopLevelCollection.GetHashCode() ^ RegistrationOptions.GetHashCode() ^ TargetWindowHandle.GetHashCode();
		}


		public bool Equals( RawInputDevice other )
		{
			return
				( TopLevelCollection == other.TopLevelCollection ) &&
				( RegistrationOptions == other.RegistrationOptions ) && 
				( TargetWindowHandle == other.TargetWindowHandle );
		}


		public override bool Equals( object obj )
		{
			return ( obj is RawInputDevice rid ) && this.Equals( rid );
		}



		/// <summary>The empty <see cref="RawInputDevice"/> structure.</summary>
		public static readonly RawInputDevice Empty;


		#region Operators

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( RawInputDevice rawInputDevice, RawInputDevice other )
		{
			return rawInputDevice.Equals( other );
		}


		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( RawInputDevice rawInputDevice, RawInputDevice other )
		{
			return !rawInputDevice.Equals( other );
		}

		#endregion Operators

	}

}