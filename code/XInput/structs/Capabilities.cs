using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Describes the capabilities of a connected XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_CAPABILITIES</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_capabilities(v=vs.85).aspx</remarks>
	[Win32.Native( "XInput.h", "XINPUT_CAPABILITIES" )]
	[StructLayout( LayoutKind.Sequential, Pack = 1, Size = 20 )]
	public struct Capabilities : IEquatable<Capabilities>
	{
	
		private DeviceType type;
		private DeviceSubType subType;
		private Caps caps;				// capability flags
		private GamePad gamePad;		// describes available controller features and control resolutions
		private Vibration vibration;	// describes available vibration functionality and resolutions



		/// <summary>Gets the <see cref="DeviceType">type</see> of the associated controller.</summary>
		public DeviceType ControllerType { get { return type; } }


		/// <summary>Gets the <see cref="DeviceSubType">subtype</see> of the associated controller.</summary>
		public DeviceSubType ControllerSubType { get { return subType; } }


		/// <summary>Returns a value indicating whether a <see cref="Caps"/> is present.</summary>
		/// <param name="capability">A <see cref="Caps"/>.</param>
		/// <returns>Returns true if the specified <paramref name="capability"/> is set, otherwise returns false.</returns>
		public bool IsSet( Caps capability )
		{
			return ( capability == Caps.None ) ? ( caps == Caps.None ) : caps.HasFlag( capability );
		}


		/// <summary>Indicates whether the game controller has the specified button (or an equivalent).</summary>
		/// <param name="button">A <see cref="GamePadButtons">gamepad button</see>.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is present on the game controller, otherwise returns false.</returns>
		public bool HasButton( GamePadButtons button )
		{
			return gamePad.IsPressed( button );
		}


		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has support for the horizontal axis of the left stick.</summary>
		public bool HasLeftThumbStickX { get { return gamePad.LeftThumbX != 0.0f; } }

		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has support for the vertical axis of the left stick.</summary>
		public bool HasLeftThumbStickY { get { return gamePad.LeftThumbY != 0.0f; } }


		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has support for the horizontal axis of the right stick.</summary>
		public bool HasRightThumbStickX { get { return gamePad.RightThumbX != 0.0f; } }

		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has support for the vertical axis of the right stick.</summary>
		public bool HasRightThumbStickY { get { return gamePad.RightThumbY != 0.0f; } }


		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has a left-side (analogic) trigger.</summary>
		public bool HasLeftTrigger { get { return gamePad.LeftTrigger != 0.0f; } }

		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has a right-side (analogic) trigger.</summary>
		public bool HasRightTrigger { get { return gamePad.RightTrigger != 0.0f; } }


		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has a left motor (for vibration).</summary>
		public bool HasLeftMotor { get { return vibration.LeftMotorSpeed != 0.0f; } }

		/// <summary>Gets a value indicating whether the game controller associated with this <see cref="Capabilities"/> structure has a right motor (for vibration).</summary>
		public bool HasRightMotor { get { return vibration.RightMotorSpeed != 0.0f; } }


		/// <summary>Returns a hash code for this <see cref="Capabilities"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Capabilities"/> structure.</returns>
		public override int GetHashCode()
		{
			return ( (int)type | ( (int)subType << 8 ) | ( (int)caps << 16 ) ) ^ gamePad.GetHashCode() ^ vibration.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Capabilities"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if this <see cref="Capabilities"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( Capabilities other )
		{
			return ( type == other.type ) && ( subType == other.subType ) && ( caps == other.caps ) && gamePad.Equals( other.gamePad ) && vibration.Equals( other.vibration );
		}


		/// <summary>Returns a value indicating whether this <see cref="Capabilities"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object; if null, it is replaced with the <see cref="Empty"/> structure.</param>
		/// <returns>Returns true if the specified object is a <see cref="Capabilities"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is Capabilities ) && this.Equals( (Capabilities)obj );
		}



		/// <summary>The empty <see cref="Capabilities"/> structure.</summary>
		public static readonly Capabilities Empty = new Capabilities();


		#region Operators


		/// <summary>Equality comparer.</summary>
		/// <param name="caps">A <see cref="Capabilities"/> structure.</param>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( Capabilities caps, Capabilities other )
		{
			return caps.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="caps">A <see cref="Capabilities"/> structure.</param>
		/// <param name="other">A <see cref="Capabilities"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( Capabilities caps, Capabilities other )
		{
			return !caps.Equals( other );
		}


		#endregion


	}

}