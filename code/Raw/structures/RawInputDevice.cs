using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines information for the raw input devices.
	/// <para>This structure is equivalent to the native <code>RAWINPUTDEVICE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645565%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RAWINPUTDEVICE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RawInputDevice : IEquatable<RawInputDevice>
	{

		/// <summary>The top-level collection (usage page and usage) for the raw input device.</summary>
		internal readonly TopLevelCollectionUsage TopLevelCollection;

		// combines UsagePage and Usage.
		/// <summary>Specifies how to interpret the information provided by <see cref="TopLevelCollection"/>. It can be zero (the default).
		/// <para>By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</para>
		/// </summary>
		internal readonly RawInputDeviceRegistrationOptions RegistrationOptions;

		/// <summary>A handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</summary>
		internal readonly IntPtr TargetWindowHandle;



		private RawInputDevice( TopLevelCollectionUsage topLevelCollection )
		{
			this.TopLevelCollection = topLevelCollection;
			RegistrationOptions = RawInputDeviceRegistrationOptions.None;
			TargetWindowHandle = IntPtr.Zero;
		}


		internal RawInputDevice( TopLevelCollectionUsage topLevelCollection, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.TopLevelCollection = topLevelCollection;
			this.RegistrationOptions = options;
			this.TargetWindowHandle = targetWindowHandle;
		}



		/// <summary>Returns a hash code for this <see cref="RawInputDevice"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return TopLevelCollection.GetHashCode() ^ RegistrationOptions.GetHashCode() ^ TargetWindowHandle.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputDevice"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawInputDevice other )
		{
			return
				( TopLevelCollection == other.TopLevelCollection ) &&
				( RegistrationOptions == other.RegistrationOptions ) && 
				( TargetWindowHandle == other.TargetWindowHandle );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInputDevice"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInputDevice rid ) && this.Equals( rid );
		}



		/// <summary>The empty <see cref="RawInputDevice"/> structure.</summary>
		public static readonly RawInputDevice Empty;



		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rawInputDevice">A <see cref="RawInputDevice"/> structure.</param>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RawInputDevice rawInputDevice, RawInputDevice other )
		{
			return rawInputDevice.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rawInputDevice">A <see cref="RawInputDevice"/> structure.</param>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RawInputDevice rawInputDevice, RawInputDevice other )
		{
			return !rawInputDevice.Equals( other );
		}

		#endregion Operators

	}

}