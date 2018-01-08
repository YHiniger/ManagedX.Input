using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Describes the state of an XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_GAMEPAD</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_gamepad%28v=vs.85%29.aspx</remarks>
	[Source( "XInput.h", "XINPUT_GAMEPAD" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 12 )]
	public struct Gamepad : IInputDeviceState<GamepadButtons>, IEquatable<Gamepad>
	{

		#region Constants

		/// <summary>Defines the default dead zone for the left thumbstick: 7849.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE" )]
		public const short DefaultLeftThumbDeadZone = 7849;

		/// <summary>Defines the default dead zone for the right thumbstick: 8689.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE" )]
		public const short DefaultRightThumbDeadZone = 8689;

		/// <summary>Defines the default threshold value used for triggers: 30.</summary>
		[Source( "XInput.h", "XINPUT_GAMEPAD_TRIGGER_THRESHOLD" )]
		public const byte DefaultTriggerThreshold = 30;


		//private const short MaxThumbStickPosition = short.MaxValue;
		//private const short MinThumbStickPosition = -MaxThumbStickPosition;

		#endregion Constants


		#region Static functions

		private static void ApplyLinearThumbStickDeadZone( ref short component, short deadZone )
		{
			// assumes deadZone is zero or greater
			if( component < -deadZone )
				component = (short)( (float)( component + deadZone ) / ( 32767.0f - (float)deadZone ) * 32767.0f );
			else if( component > deadZone )
				component = (short)( (float)( component - deadZone ) / ( 32767.0f - (float)deadZone ) * 32767.0f );
			else
				component = 0;
		}


		private static void ApplyCircularThumbStickDeadZone( ref short x, ref short y, short deadZone )
		{
			var h = (float)x;
			var v = (float)y;
			var dz = (float)deadZone;

			var length = (float)Math.Sqrt( h * h + v * v );
			float scale;
			if( length <= dz )
				scale = 0.0f;
			else
				scale = ( (float)( length - dz ) / ( 32767.0f - dz ) * 32767.0f ) / length;

			x = (short)( h * scale ).Clamp( -32767.0f, 32767.0f );
			y = (short)( v * scale ).Clamp( -32767.0f, 32767.0f );
		}

		#endregion Static functions



		/// <summary>A value indicating which buttons are pressed.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly GamepadButtons Buttons;
		private ushort triggers;
		private short leftThumbX;
		private short leftThumbY;
		private short rightThumbX;
		private short rightThumbY;



		/// <summary>Returns a value indicating whether a button is pressed.</summary>
		/// <param name="button">A <see cref="GamepadButtons"/>.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed, otherwise returns false.</returns>
		public bool IsPressed( GamepadButtons button )
		{
			return ( button == GamepadButtons.None ) ? ( Buttons == GamepadButtons.None ) : Buttons.HasFlag( button );
		}


		/// <summary>Gets a value, within the range [0,1], representing the state of the left trigger.</summary>
		public float LeftTrigger => ( triggers & 0xFF ) / 255.0f;


		/// <summary>Gets a value, within the range [0,1], representing the state of the right trigger.</summary>
		public float RightTrigger => ( triggers >> 8 ) / 255.0f;


		/// <summary>Gets a <see cref="Vector2"/> representing the position, within the range [-1,+1], of the left stick.</summary>
		public Vector2 LeftThumb => new Vector2( leftThumbX / 32767.0f, leftThumbY / 32767.0f );


		/// <summary>Gets a <see cref="Vector2"/> representing the position, within the range [-1,+1], of the right stick.</summary>
		public Vector2 RightThumb => new Vector2( rightThumbX / 32767.0f, rightThumbY / 32767.0f );


		#region Dead zone handling

		/// <summary>Applies a dead zone to the triggers.</summary>
		/// <param name="threshold">The triggers threshold; must be lower than 255; defaults to <see cref="DefaultTriggerThreshold"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void ApplyTriggersDeadZone( byte threshold )
		{
			if( threshold == 255 )
				throw new ArgumentOutOfRangeException( "threshold" );

			var range = 255.0f / ( 255.0f - threshold );
			var lTrigger = (byte)( triggers & 0xFF );
			var rTrigger = (byte)( triggers >> 8 );
			lTrigger = (byte)( lTrigger <= threshold ? 0.0f : (float)( lTrigger - threshold ) * range );
			rTrigger = (byte)( rTrigger <= threshold ? 0.0f : (float)( rTrigger - threshold ) * range );
			triggers = (ushort)( ( rTrigger << 8 ) + lTrigger );
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
			leftStickDeadZone = Math.Abs( leftStickDeadZone );
			rightStickDeadZone = Math.Abs( rightStickDeadZone );
			if( deadZoneMode == DeadZoneMode.Linear )
			{
				ApplyLinearThumbStickDeadZone( ref leftThumbX, leftStickDeadZone );
				ApplyLinearThumbStickDeadZone( ref leftThumbY, leftStickDeadZone );

				ApplyLinearThumbStickDeadZone( ref rightThumbX, rightStickDeadZone );
				ApplyLinearThumbStickDeadZone( ref rightThumbY, rightStickDeadZone );
			}
			else if( deadZoneMode == DeadZoneMode.Circular )
			{
				ApplyCircularThumbStickDeadZone( ref leftThumbX, ref leftThumbY, leftStickDeadZone );
				ApplyCircularThumbStickDeadZone( ref rightThumbX, ref rightThumbY, rightStickDeadZone );
			}
		}


		/// <summary>Applies the default thumbsticks dead zones.</summary>
		/// <param name="deadZoneMode">The kind of dead zone mode to apply.</param>
		public void ApplyThumbSticksDeadZone( DeadZoneMode deadZoneMode )
		{
			if( deadZoneMode == DeadZoneMode.None )
				return;

			this.ApplyThumbSticksDeadZone( deadZoneMode, DefaultLeftThumbDeadZone, DefaultRightThumbDeadZone );
		}

		#endregion Dead zone handling


		/// <summary>Returns a hash code for this <see cref="Gamepad"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="Gamepad"/> structure.</returns>
		public override int GetHashCode()
		{
			return Buttons.GetHashCode() ^ triggers.GetHashCode() ^ leftThumbX.GetHashCode() ^ leftThumbY.GetHashCode() ^ rightThumbX.GetHashCode() ^ rightThumbY.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="Gamepad"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="Gamepad"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( Gamepad other )
		{
			return
				( Buttons == other.Buttons ) && ( triggers == other.triggers ) &&
				( leftThumbX == other.leftThumbX ) && ( leftThumbY == other.leftThumbY ) &&
				( rightThumbX == other.rightThumbX ) && ( rightThumbY == other.rightThumbY );
		}


		/// <summary>Returns a value indicating whether this <see cref="Gamepad"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="Gamepad"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is Gamepad pad && this.Equals( pad );
		}



		/// <summary>The empty <see cref="Gamepad"/> structure.</summary>
		public static readonly Gamepad Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="gamepad">A <see cref="Gamepad"/> structure.</param>
		/// <param name="other">A <see cref="Gamepad"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( Gamepad gamepad, Gamepad other )
		{
			return gamepad.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="gamepad">A <see cref="Gamepad"/> structure.</param>
		/// <param name="other">A <see cref="Gamepad"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( Gamepad gamepad, Gamepad other )
		{
			return !gamepad.Equals( other );
		}

		#endregion Operators

	}

}