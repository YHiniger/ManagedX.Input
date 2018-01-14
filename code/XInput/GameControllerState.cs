using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;


namespace ManagedX.Input
{
	using XInput;


	/// <summary>Contains the state of a <see cref="GameController"/>.</summary>
	[System.Diagnostics.DebuggerStepThrough]
	public struct GameControllerState : IInputDeviceState<GameControllerButtons>, IEquatable<GameControllerState>
	{

		private const float MaxAbsThumbStickPosition = short.MaxValue;


		private static void ApplyLinearDeadZone( ref Vector2 stick, float deadZone )
		{
			if( stick.X < -deadZone )
				stick.X += deadZone;
			else if( stick.X > deadZone )
				stick.X -= deadZone;
			else
				stick.X = 0.0f;

			if( stick.Y < -deadZone )
				stick.Y += deadZone;
			else if( stick.Y > deadZone )
				stick.Y -= deadZone;
			else
				stick.Y = 0.0f;

			stick /= 1.0f - deadZone;

			stick.X = stick.X.Clamp( -1.0f, +1.0f );
			stick.Y = stick.Y.Clamp( -1.0f, +1.0f );
		}


		private static void ApplyCircularDeadZone( ref Vector2 stick, float deadZone )
		{
			var length = stick.Length;
			var deadZoneRadius = (float)Math.Sqrt( deadZone * deadZone * 2.0f );
			if( length <= deadZoneRadius )
				stick = Vector2.Zero;
			else
			{
				stick *= ( length - deadZoneRadius ) / ( ( 1.0f - deadZoneRadius ) * length * length );
				stick.X = stick.X.Clamp( -1.0f, +1.0f );
				stick.Y = stick.Y.Clamp( -1.0f, +1.0f );
			}
		}



		/// <summary>Indicates which buttons are pressed.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly GameControllerButtons Buttons;

		/// <summary>A value, within the range [0,1], representing the state of the left trigger.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float LeftTrigger;

		/// <summary>A value, within the range [0,1], representing the state of the right trigger.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public float RightTrigger;

		/// <summary>A <see cref="Vector2"/> representing the position, normalized within the range [-1,+1], of the left stick.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector2 LeftThumbStick;

		/// <summary>A <see cref="Vector2"/> representing the position, normalized within the range [-1,+1], of the right stick.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Vector2 RightThumbStick;



		internal GameControllerState( ref Gamepad state )
		{
			Buttons = (GameControllerButtons)state.Buttons;
			LeftTrigger = state.LeftTrigger;
			RightTrigger = state.RightTrigger;
			LeftThumbStick = state.LeftThumb;
			RightThumbStick = state.RightThumb;
		}



		/// <summary>Gets a value indicating whether a button is pressed.</summary>
		/// <param name="button">A gamepad button.</param>
		/// <returns>Returns true if the specified button is pressed, otherwise returns false.</returns>
		public bool this[ GameControllerButtons button ] => button == GameControllerButtons.None ? Buttons == GameControllerButtons.None : Buttons.HasFlag( button );


		#region Dead zone handling

		/// <summary>Applies a dead zone to the triggers.</summary>
		/// <param name="threshold">The triggers threshold, within the range [0,1]; defaults to <see cref="GameController.DefaultTriggerThreshold"/> / 255 .</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		internal void ApplyTriggersDeadZone( float threshold )
		{
			var range = 1.0f / ( 1.0f - threshold );
			LeftTrigger = LeftTrigger <= threshold ? 0.0f : ( LeftTrigger - threshold ) * range;
			RightTrigger = RightTrigger <= threshold ? 0.0f : ( RightTrigger - threshold ) * range;
		}


		/// <summary>Applies a dead zone to the thumbsticks.</summary>
		/// <param name="deadZoneMode">The kind of dead zone mode to apply.</param>
		/// <param name="leftStickDeadZone">The dead zone for the left stick; defaults to <see cref="GameController.DefaultLeftThumbDeadZone"/> / 32767.</param>
		/// <param name="rightStickDeadZone">The dead zone for the right stick; defaults to <see cref="GameController.DefaultRightThumbDeadZone"/> / 32767.</param>
		internal void ApplyThumbSticksDeadZone( DeadZoneMode deadZoneMode, float leftStickDeadZone, float rightStickDeadZone )
		{
			if( deadZoneMode != DeadZoneMode.None )
			{
				if( deadZoneMode == DeadZoneMode.Linear )
				{
					ApplyLinearDeadZone( ref LeftThumbStick, leftStickDeadZone );
					ApplyLinearDeadZone( ref RightThumbStick, rightStickDeadZone );
				}
				else if( deadZoneMode == DeadZoneMode.Circular )
				{
					ApplyCircularDeadZone( ref LeftThumbStick, leftStickDeadZone );
					ApplyCircularDeadZone( ref RightThumbStick, rightStickDeadZone );
				}
			}
		}

		#endregion Dead zone handling


		/// <summary>Returns a hash code for this <see cref="GameControllerState"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="GameControllerState"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)Buttons ^ LeftTrigger.GetHashCode() ^ RightTrigger.GetHashCode() ^ LeftThumbStick.GetHashCode() ^ RightThumbStick.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="GameControllerState"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="GameControllerState"/> structure.</param>
		/// <returns>Returns true if this structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( GameControllerState other )
		{
			return
				( Buttons == other.Buttons ) &&
				( LeftTrigger == other.LeftTrigger ) &&
				( RightTrigger == other.RightTrigger ) &&
				LeftThumbStick.Equals( other.LeftThumbStick ) &&
				RightThumbStick.Equals( other.RightThumbStick );
		}


		/// <summary>Returns a value indicating whether this <see cref="GameControllerState"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="GameControllerState"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return obj is GameControllerState state && this.Equals( state );
		}



		/// <summary>The empty <see cref="GameControllerState"/> structure.</summary>
		public static readonly GameControllerState Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="gamepad">A <see cref="GameControllerState"/> structure.</param>
		/// <param name="other">A <see cref="GameControllerState"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( GameControllerState gamepad, GameControllerState other )
		{
			return gamepad.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="gamepad">A <see cref="GameControllerState"/> structure.</param>
		/// <param name="other">A <see cref="GameControllerState"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( GameControllerState gamepad, GameControllerState other )
		{
			return !gamepad.Equals( other );
		}

		#endregion Operators

	}

}