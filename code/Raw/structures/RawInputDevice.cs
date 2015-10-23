using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645565%28v=vs.85%29.aspx


	/// <summary>Defines information for the raw input devices.
	/// <para>The native name of this structure is RAWINPUTDEVICE.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential )]
	internal struct RawInputDevice : IEquatable<RawInputDevice>
	{

		private short usagePage;
		private short usage;
		internal RawInputDeviceRegistrationOptions flags;
		internal IntPtr targetWindowHandle;


		/// <summary>Private constructor.</summary>
		/// <param name="usagePage">Top level collection Usage page for the raw input device.</param>
		/// <param name="usage">Top level collection Usage for the raw input device.</param>
		/// <param name="options">Mode flag that specifies how to interpret the information provided by <paramref name="usagePage"/> and <paramref name="usage"/>. It can be zero (the default).
		/// By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</param>
		/// <param name="targetWindowHandle">A handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</param>
		private RawInputDevice( short usagePage, short usage, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.usagePage = usagePage;
			this.usage = usage;
			this.flags = options;
			this.targetWindowHandle = targetWindowHandle;
		}


		/// <summary>Initializes a new <see cref="RawInputDevice"/> structure.</summary>
		/// <param name="hidInfo">A valid <see cref="HumanInterfaceDeviceInfo"/> structure.</param>
		/// <param name="options">Mode flag that specifies how to interpret the information provided by UsagePage and Usage. It can be zero (the default).
		/// By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.</param>
		/// <param name="targetWindowHandle">A handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</param>
		public RawInputDevice( HumanInterfaceDeviceInfo hidInfo, RawInputDeviceRegistrationOptions options, IntPtr targetWindowHandle )
		{
			this.usagePage = hidInfo.UsagePage;
			this.usage = hidInfo.Usage;
			this.flags = options;
			this.targetWindowHandle = targetWindowHandle;
		}


		/// <summary>Gets the top level collection Usage page for the raw input device.</summary>
		public short UsagePage { get { return usagePage; } }
		
		/// <summary>Gets the top level collection Usage for the raw input device.</summary>
		public short Usage { get { return usage; } }

		/// <summary>Gets a value specifying how to interpret the information provided by <see cref="UsagePage"/> and <see cref="Usage"/>. It can be zero (the default).
		/// By default, the operating system sends raw input from devices with the specified top level collection (TLC) to the registered application as long as it has the window focus.
		/// </summary>
		public RawInputDeviceRegistrationOptions RegistrationOptions { get { return flags; } }
		
		/// <summary>Gets a handle to the target window. If <see cref="IntPtr.Zero"/> it follows the keyboard focus.</summary>
		public IntPtr TargetWindowHandle { get { return targetWindowHandle; } }


		/// <summary>Returns a hash code for this <see cref="RawInputDevice"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawInputDevice"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)usagePage ^ (int)usage ^ (int)flags ^ targetWindowHandle.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this <see cref="RawInputDevice"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawInputDevice"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawInputDevice"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( RawInputDevice other )
		{
			return ( usagePage == other.usagePage ) && ( usage == other.usage ) && ( flags == other.flags ) && ( targetWindowHandle == other.targetWindowHandle );
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
		internal static readonly RawInputDevice Mouse = new RawInputDevice( MouseDeviceInfo.UsagePage, MouseDeviceInfo.Usage, RawInputDeviceRegistrationOptions.InputSink, IntPtr.Zero );

		/// <summary>The default <see cref="RawInputDevice"/> structure for the keyboard.</summary>
		internal static readonly RawInputDevice Keyboard = new RawInputDevice( KeyboardDeviceInfo.UsagePage, KeyboardDeviceInfo.Usage, RawInputDeviceRegistrationOptions.None, IntPtr.Zero );



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