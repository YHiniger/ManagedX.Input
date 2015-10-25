using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645565%28v=vs.85%29.aspx


	/// <summary>Defines information for the raw input devices.
	/// <para>The native name of this structure is RAWINPUTDEVICE.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Pack = 4 )]
	internal struct RawInputDevice : IEquatable<RawInputDevice>
	{

		private int topLevelCollection; // combines UsagePage and Usage.
		internal RawInputDeviceRegistrationOptions flags;
		internal IntPtr targetWindowHandle;


		/// <summary>Private constructor.</summary>
		/// <param name="topLevelCollection">Top level collection Usage page and usage for the raw input device.</param>
		/// <param name="options">Mode flag that specifies how to interpret the information provided by <paramref name="topLevelCollection"/>. It can be zero (the default).
		/// <para>By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</para>
		/// </param>
		/// <param name="targetWindowHandle">A handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</param>
		private RawInputDevice( int topLevelCollection, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.topLevelCollection = topLevelCollection;
			this.flags = options;
			this.targetWindowHandle = targetWindowHandle;
		}


		/// <summary>Gets the top-level collection (usage page and usage) for the raw input device.</summary>
		public int TopLevelCollection { get { return topLevelCollection; } }


		/// <summary>Gets a value specifying how to interpret the information provided by <see cref="TopLevelCollection"/>. It can be zero (the default).
		/// <para>By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</para>
		/// </summary>
		public RawInputDeviceRegistrationOptions RegistrationOptions { get { return flags; } }
		

		/// <summary>Gets a handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</summary>
		public IntPtr TargetWindowHandle { get { return targetWindowHandle; } }


		/// <summary>Returns a hash code for this <see cref="RawInputDevice"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return topLevelCollection ^ (int)flags ^ targetWindowHandle.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputDevice"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawInputDevice other )
		{
			return
				( topLevelCollection == other.topLevelCollection ) &&
				( flags == other.flags ) && 
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
		public static readonly RawInputDevice Empty = new RawInputDevice();

		/// <summary>The default <see cref="RawInputDevice"/> structure for mice.</summary>
		internal static readonly RawInputDevice Mouse = new RawInputDevice( (int)Raw.TopLevelCollection.Mouse, RawInputDeviceRegistrationOptions.None, IntPtr.Zero );

		/// <summary>The default <see cref="RawInputDevice"/> structure for joysticks.</summary>
		internal static readonly RawInputDevice Joystick = new RawInputDevice( (int)Raw.TopLevelCollection.Joystick, RawInputDeviceRegistrationOptions.None, IntPtr.Zero );

		/// <summary>The default <see cref="RawInputDevice"/> structure for gamepads.</summary>
		internal static readonly RawInputDevice Gamepad = new RawInputDevice( (int)Raw.TopLevelCollection.Gamepad, RawInputDeviceRegistrationOptions.None, IntPtr.Zero );

		/// <summary>The default <see cref="RawInputDevice"/> structure for keyboards.</summary>
		internal static readonly RawInputDevice Keyboard = new RawInputDevice( (int)Raw.TopLevelCollection.Keyboard, RawInputDeviceRegistrationOptions.None, IntPtr.Zero );


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

		#endregion

	}

}