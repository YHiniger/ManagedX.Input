using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Describes the state of an XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_GAMEPAD</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx</remarks>
	[Native( "XInput.h", "XINPUT_GAMEPAD" )]
	[StructLayout( LayoutKind.Sequential, Pack = 1, Size = 12 )]
	public struct GamePad : IEquatable<GamePad>
	{

		#region Constants

		/// <summary>Defines the default dead zone for the left thumbstick: 7849.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE" )]
		public const short DefaultLeftThumbDeadZone = 7849;

		/// <summary>Defines the default dead zone for the right thumbstick: 8689.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE" )]
		public const short DefaultRightThumbDeadZone = 8689;

		/// <summary>Defines the default threshold value used for triggers: 30.</summary>
		[Native( "XInput.h", "XINPUT_GAMEPAD_TRIGGER_THRESHOLD" )]
		public const byte DefaultTriggerThreshold = 30;

		#endregion Constants


		#region Static functions

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		private static float ToSingle( short value )
		{
			return value / ( value < 0 ? 32768.0f : 32767.0f );
		}

		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		private static short ToShort( float value )
		{
			if( value < 0.0f )
				return (short)( Math.Max( -32768.0f, value ) );
			else
				return (short)( Math.Min( 32767.0f, value ) );
		}


		private static void ApplyLinearThumbStickDeadZone( ref short component, short deadZone )
		{
			// assumes deadZone is zero or greater
			if( component < -deadZone )
				component = (short)( (float)( component + deadZone ) / ( 32768.0f - (float)deadZone ) * 32768.0f );
			else if( component > deadZone )
				component = (short)( (float)( component - deadZone ) / ( 32767.0f - (float)deadZone ) * 32767.0f );
			else
				component = 0;
		}

		private static void ApplyThumbStickDeadZone( ref short x, ref short y, short deadZone, DeadZoneMode deadZoneMode )
		{
			if( deadZoneMode == DeadZoneMode.Linear )
			{
				deadZone = Math.Abs( deadZone );
				ApplyLinearThumbStickDeadZone( ref x, deadZone );
				ApplyLinearThumbStickDeadZone( ref y, deadZone );
				return;
			}

			float h = (float)x;
			float v = (float)y;
			float dZ = (float)deadZone;
			
			float length = (float)Math.Sqrt( h * h + v * v );
			float scale;
			if( length <= dZ )
				scale = 0.0f;
			else
				scale = ( (float)( length - dZ ) / ( 32767.0f - dZ ) * 32767.0f ) / length;

			x = ToShort( h * scale );
			y = ToShort( v * scale );
		}

		#endregion Static functions



		private GamePadButtons buttons;
		private byte leftTrigger;
		private byte rightTrigger;
		private short leftThumbX;
		private short leftThumbY;
		private short rightThumbX;
		private short rightThumbY;



		/// <summary>Gets a value indicating which buttons are pressed.</summary>
		public GamePadButtons Buttons { get { return buttons; } }


		/// <summary>Returns a value indicating whether a button is pressed.</summary>
		/// <param name="button">A <see cref="GamePadButtons">button</see>.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed, otherwise returns false.</returns>
		public bool IsPressed( GamePadButtons button )
		{
			return ( button == GamePadButtons.None ) ? ( buttons == GamePadButtons.None ) : buttons.HasFlag( button );
		}


		/// <summary>Gets a value in the range [0,1] representing the state of the left trigger.</summary>
		public float LeftTrigger { get { return (float)leftTrigger / 255.0f; } }

		/// <summary>Gets a value in the range [0,1] representing the state of the right trigger.</summary>
		public float RightTrigger { get { return (float)rightTrigger / 255.0f; } }


		/// <summary>Gets a value in the range [-1,+1] representing the horizontal position of the left stick.</summary>
		public float LeftThumbX { get { return ToSingle( leftThumbX ); } }

		/// <summary>Gets a value in the range [-1,+1] representing the vertical position of the left stick.</summary>
		public float LeftThumbY { get { return ToSingle( leftThumbY ); } }


		/// <summary>Gets a value in the range [-1,+1] representing the horizontal position of the right stick.</summary>
		public float RightThumbX { get { return ToSingle( rightThumbX  ); } }

		/// <summary>Gets a value in the range [-1,+1] representing the vertical position of the right stick.</summary>
		public float RightThumbY { get { return ToSingle( rightThumbY ); } }


		#region Dead zone handling

		/// <summary>Applies a dead zone to the triggers.</summary>
		/// <param name="threshold">The triggers threshold; must be lower than 255; defaults to <see cref="DefaultTriggerThreshold"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void ApplyTriggersDeadZone( byte threshold )
		{
			if( threshold == 255 )
				throw new ArgumentOutOfRangeException( "threshold" );

			float range = 255.0f / ( 255.0f - (float)threshold );
			leftTrigger = (byte)( leftTrigger <= threshold ? 0.0f : (float)( leftTrigger - threshold ) * range );
			rightTrigger = (byte)( rightTrigger <= threshold ? 0.0f : (float)( rightTrigger - threshold ) * range );
		}

		/// <summary>Applies the default triggers dead zone.</summary>
		public void ApplyTriggersDeadZone()
		{
			this.ApplyTriggersDeadZone( DefaultTriggerThreshold );
		}


		/// <summary>Applies a dead zone to the thumbsticks.</summary>
		/// <param name="deadZoneMode">The kind of dead zone mode to apply.</param>
		/// <param name="leftStickDeadZone">The dead zone for the left stick; defaults to <see cref="DefaultLeftThumbDeadZone"/>.</param>
		/// <param name="rightStickDeadZone">The dead zone for the right stick; defaults to <see cref="DefaultRightThumbDeadZone"/>.</param>
		public void ApplyThumbSticksDeadZone( DeadZoneMode deadZoneMode, short leftStickDeadZone, short rightStickDeadZone )
		{
			if( deadZoneMode != DeadZoneMode.None )
			{
				ApplyThumbStickDeadZone( ref leftThumbX, ref leftThumbY, leftStickDeadZone, deadZoneMode );
				ApplyThumbStickDeadZone( ref rightThumbX, ref rightThumbY, rightStickDeadZone, deadZoneMode );
			}
		}

		/// <summary>Applies the default thumbsticks dead zones.</summary>
		/// <param name="deadZoneMode">The kind of dead zone mode to apply.</param>
		public void ApplyThumbSticksDeadZone( DeadZoneMode deadZoneMode )
		{
			this.ApplyThumbSticksDeadZone( deadZoneMode, DefaultLeftThumbDeadZone, DefaultRightThumbDeadZone );
		}

		#endregion Dead zone handling


		/// <summary>Returns a hash code for this <see cref="GamePad"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="GamePad"/> structure.</returns>
		public override int GetHashCode()
		{
			return buttons.GetHashCode() ^ leftTrigger.GetHashCode() ^ rightTrigger.GetHashCode() ^ leftThumbX.GetHashCode() ^ leftThumbY.GetHashCode() ^ rightThumbX.GetHashCode() ^ rightThumbY.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="GamePad"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="GamePad"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( GamePad other )
		{
			return ( other.buttons == buttons ) &&
				( other.leftTrigger == leftTrigger ) && ( other.rightTrigger == rightTrigger ) && 
				( other.leftThumbX == leftThumbX ) && ( other.leftThumbY == leftThumbY ) &&
				( other.rightThumbX == rightThumbX ) && ( other.rightThumbY == rightThumbY );
		}


		/// <summary>Returns a value indicating whether this <see cref="GamePad"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="GamePad"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is GamePad ) && this.Equals( (GamePad)obj );
		}


		/// <summary>The empty <see cref="GamePad"/> structure.</summary>
		public static readonly GamePad Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="gamePad">A <see cref="GamePad"/> structure.</param>
		/// <param name="other">A <see cref="GamePad"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( GamePad gamePad, GamePad other )
		{
			return gamePad.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="gamePad">A <see cref="GamePad"/> structure.</param>
		/// <param name="other">A <see cref="GamePad"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( GamePad gamePad, GamePad other )
		{
			return !gamePad.Equals( other );
		}

		#endregion Operators

	}

}