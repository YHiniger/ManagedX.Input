using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines information for the raw input devices.
	/// <para>This structure is equivalent to the native <code>RAWINPUTDEVICE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645565%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "WinUser.h", "RAWINPUTDEVICE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RawInputDevice : IEquatable<RawInputDevice>
	{

		private TopLevelCollectionUsage topLevelCollection; // combines UsagePage and Usage.
		internal RawInputDeviceRegistrationOptions options;
		internal IntPtr targetWindowHandle;



		private RawInputDevice( TopLevelCollectionUsage topLevelCollection )
		{
			this.topLevelCollection = topLevelCollection;
			options = RawInputDeviceRegistrationOptions.None;
			targetWindowHandle = IntPtr.Zero;
		}


		internal RawInputDevice( TopLevelCollectionUsage topLevelCollection, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.topLevelCollection = topLevelCollection;
			this.options = options;
			this.targetWindowHandle = targetWindowHandle;
		}



		/// <summary>Gets the top-level collection (usage page and usage) for the raw input device.</summary>
		public TopLevelCollectionUsage TopLevelCollection { get { return topLevelCollection; } }


		/// <summary>Gets a value specifying how to interpret the information provided by <see cref="TopLevelCollection"/>. It can be zero (the default).
		/// <para>By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</para>
		/// </summary>
		public RawInputDeviceRegistrationOptions RegistrationOptions { get { return options; } }
		

		/// <summary>Gets a handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</summary>
		public IntPtr TargetWindowHandle { get { return targetWindowHandle; } }


		/// <summary>Returns a hash code for this <see cref="RawInputDevice"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return topLevelCollection.GetHashCode() ^ options.GetHashCode() ^ targetWindowHandle.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputDevice"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawInputDevice other )
		{
			return
				( topLevelCollection == other.topLevelCollection ) &&
				( options == other.options ) && 
				( targetWindowHandle == other.targetWindowHandle );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawInputDevice"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawInputDevice ) && this.Equals( (RawInputDevice)obj );
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