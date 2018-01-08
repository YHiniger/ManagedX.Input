using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Describes the capabilities of a connected XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_CAPABILITIES</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities(v=vs.85).aspx</remarks>
	[Win32.Source( "XInput.h", "XINPUT_CAPABILITIES" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 20 )]
	public struct Capabilities : IEquatable<Capabilities>
	{

		/// <summary>The type of the XInput controller.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly DeviceType ControllerType;

		/// <summary>The sub-type of the XInput controller.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly DeviceSubtype ControllerSubtype;

		private Caps caps;				// capability flags
		private Gamepad gamePad;		// describes available controller features and control resolutions
		private Vibration vibration;	// describes available vibration functionality and resolutions



		#region Caps

		/// <summary>Gets a value indicating whether the XInput controller is a wireless device.
		/// <para>Only available on Windows 8 or newer.</para>
		/// </summary>
		public bool IsWireless => caps.HasFlag( Caps.Wireless );


		/// <summary>Gets a value indicating whether the XInput controller has an integrated voice device.</summary>
		public bool SupportsVoice => caps.HasFlag( Caps.VoiceSupported );


		/// <summary>Gets a value indicating whether the XInput controller supports PMDs.</summary>
		public bool SupportsPluginModuleDevice => caps.HasFlag( Caps.PluginModuleDeviceSupported );


		/// <summary>Gets a value indicating whether the XInput controller supports force feedback.</summary>
		public bool SupportsForceFeedback => caps.HasFlag( Caps.ForceFeedbackSupported );


		/// <summary>Gets a value indicating whether the XInput controller lacks menu navigation buttons (START, BACK, DPAD).</summary>
		public bool HasNoNavigation => caps.HasFlag( Caps.NoNavigation );

		#endregion Caps


		/// <summary>Indicates whether the XInput controller has the specified button (or an equivalent).</summary>
		/// <param name="button">A <see cref="GamepadButtons">gamepad button</see>.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is present on the XInput controller, otherwise returns false.</returns>
		public bool HasButton( GamepadButtons button )
		{
			return gamePad.IsPressed( button );
		}


		/// <summary>Gets a value indicating whether the XInput controller has support for the horizontal axis of the left stick.</summary>
		public bool HasLeftThumbStickX => gamePad.LeftThumb.X != 0.0f;

		/// <summary>Gets a value indicating whether the XInput controller has support for the vertical axis of the left stick.</summary>
		public bool HasLeftThumbStickY => gamePad.LeftThumb.Y != 0.0f;


		/// <summary>Gets a value indicating whether the XInput controller has support for the horizontal axis of the right stick.</summary>
		public bool HasRightThumbStickX => gamePad.RightThumb.X != 0.0f;

		/// <summary>Gets a value indicating whether the XInput controller has support for the vertical axis of the right stick.</summary>
		public bool HasRightThumbStickY => gamePad.RightThumb.Y != 0.0f;


		/// <summary>Gets a value indicating whether the XInput controller has a left-side (analogic) trigger.</summary>
		public bool HasLeftTrigger => gamePad.LeftTrigger != 0.0f;

		/// <summary>Gets a value indicating whether the XInput controller has a right-side (analogic) trigger.</summary>
		public bool HasRightTrigger => gamePad.RightTrigger != 0.0f;


		/// <summary>Gets a value indicating whether the XInput controller has a left motor (for vibration).</summary>
		public bool HasLeftMotor => vibration.LeftMotorSpeed != 0.0f;

		/// <summary>Gets a value indicating whether the XInput controller has a right motor (for vibration).</summary>
		public bool HasRightMotor => vibration.RightMotorSpeed != 0.0f;


		/// <summary>Returns a hash code for this <see cref="Capabilities"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Capabilities"/> structure.</returns>
		public override int GetHashCode()
		{
			return ( (int)ControllerType | ( (int)ControllerSubtype << 8 ) | ( (int)caps << 16 ) ) ^ gamePad.GetHashCode() ^ vibration.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Capabilities"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if this <see cref="Capabilities"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( Capabilities other )
		{
			return ( ControllerType == other.ControllerType ) && ( ControllerSubtype == other.ControllerSubtype ) && ( caps == other.caps ) && gamePad.Equals( other.gamePad ) && vibration.Equals( other.vibration );
		}


		/// <summary>Returns a value indicating whether this <see cref="Capabilities"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object; if null, it is replaced with the <see cref="Empty"/> structure.</param>
		/// <returns>Returns true if the specified object is a <see cref="Capabilities"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Capabilities c && this.Equals( c );
		}



		/// <summary>The empty <see cref="Capabilities"/> structure.</summary>
		public static readonly Capabilities Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="caps">A <see cref="Capabilities"/> structure.</param>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( Capabilities caps, Capabilities other )
		{
			return caps.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="caps">A <see cref="Capabilities"/> structure.</param>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( Capabilities caps, Capabilities other )
		{
			return !caps.Equals( other );
		}

		#endregion Operators

	}

}