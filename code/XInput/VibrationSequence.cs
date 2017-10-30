using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	/// <summary>A time-based sequence of <see cref="Vibration"/> keyframes.</summary>
	[Serializable]
	public sealed class VibrationSequence : VibrationEffect
	{

		[Serializable]
		private struct Keyframe : IEquatable<Keyframe>, IComparable<Keyframe>
		{

			internal int time;
			internal Vibration vibration;



			internal Keyframe( int time, Vibration vibration )
			{
				this.time = time;
				this.vibration = vibration;
			}



			public int CompareTo( Keyframe other )
			{
				return time.CompareTo( other.time );
			}


			public bool Equals( Keyframe other )
			{
				return time == other.time && vibration.Equals( other.vibration );
			}


			public override bool Equals( object obj )
			{
				return obj is Keyframe kf && this.Equals( kf );
			}


			public override int GetHashCode()
			{
				return time ^ vibration.GetHashCode();
			}


			public override string ToString()
			{
				return string.Format( System.Globalization.CultureInfo.InvariantCulture, "{{Time: {0}, Vibration: {1}}}", time, vibration );
			}

		}



		private readonly List<Keyframe> keyframes;



		/// <summary>Initializes a new <see cref="VibrationSequence"/>.</summary>
		public VibrationSequence()
			: base()
		{
			keyframes = new List<Keyframe>();
		}



		/// <summary>Gets a <see cref="Vibration"/> for a given time.
		/// <para>The returned structure is interpolated from keyframes.</para>
		/// </summary>
		/// <param name="time">The time, in milliseconds, of the requested <see cref="Vibration"/> structure.</param>
		/// <returns>Returns the requested <see cref="Vibration"/> structure.</returns>
		public sealed override Vibration this[ int time ]
		{
			get
			{
				if( time < 0 )
					return Vibration.Zero;

				if( time > base.Duration )
				{
					if( !base.IsRepeating )
						return Vibration.Zero;
					time %= base.Duration + 1;
				}

				var pre = new Keyframe( -1, Vibration.Zero );
				var post = new Keyframe( int.MaxValue, Vibration.Zero );

				for( var k = 0; k < keyframes.Count; ++k )
				{
					var current = keyframes[ k ];
					if( current.time == time )
						return current.vibration;

					if( current.time < time && current.time > pre.time )
						pre = current;

					if( current.time > time && current.time < post.time )
						post = current;
				}

				return Vibration.Lerp( pre.vibration, post.vibration, (float)( time - pre.time ) / (float)( post.time - pre.time ) );
			}
		}


		/// <summary>Adds a keyframe to this <see cref="VibrationSequence"/>.</summary>
		/// <param name="time">The time, in milliseconds, of the keyframe; must be greater than or equal to 0.</param>
		/// <param name="vibration">The <see cref="Vibration"/> structure associated with the keyframe.</param>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public void Add( int time, Vibration vibration )
		{
			if( time < 0 )
				throw new ArgumentOutOfRangeException( "time" );

			var keyframe = new Keyframe( time, vibration );

			var prevFrameIndex = -1;
			var prevTime = -1;

			for( var k = 0; k < keyframes.Count; ++k )
			{
				var current = keyframes[ k ];
				if( current.time == time )
				{
					keyframes[ k ] = keyframe;
					return;
				}

				if( current.time < time && current.time > prevTime )
				{
					prevTime = current.time;
					prevFrameIndex = k;
				}
			}

			if( keyframes.Count == 0 || prevFrameIndex == keyframes.Count - 1 )
			{
				keyframes.Add( keyframe );
				if( time > base.Duration )
					base.Duration = time;
			}
			else
				keyframes.Insert( prevFrameIndex + 1, keyframe );
		}


		/// <summary>Removes a keyframe from this <see cref="VibrationSequence"/>.</summary>
		/// <param name="time">The time of the keyframe to remove; must be greater than or equal to 0.</param>
		/// <returns>Returns true if the specified keyframe was present and has been removed, otherwise returns false.</returns>
		/// <exception cref="ArgumentOutOfRangeException"/>
		public bool Remove( int time )
		{
			if( time < 0 )
				throw new ArgumentOutOfRangeException( "time" );

			for( var k = 0; k < keyframes.Count; ++k )
			{
				if( keyframes[ k ].time == time )
				{
					keyframes.RemoveAt( k );
					if( k > 0 && k == keyframes.Count )
						base.Duration = keyframes[ k - 1 ].time;
					return true;
				}
			}

			return false;
		}


		/// <summary>Removes all keyframes from this <see cref="VibrationSequence"/>.</summary>
		public void Clear()
		{
			keyframes.Clear();
			base.Duration = 0;
		}


		/// <summary>Returns an array containing the time of all defined keyframes.</summary>
		/// <returns>Returns an array containing the time of all defined keyframes.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Keyframe" )]
		public int[] GetKeyframeTimes()
		{
			var times = new int[ keyframes.Count ];
			for( var k = 0; k < times.Length; ++k )
				times[ k ] = keyframes[ k ].time;
			return times;
		}


		/// <summary>Returns a value indicating whether a keyframe has been defined for the specified time.</summary>
		/// <param name="time">Time, in milliseconds; must be greater than or equal to 0.</param>
		/// <returns>Returns true if a keyframe has been defined for the specified <paramref name="time"/>, otherwise returns false.</returns>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Keyframe" )]
		public bool IsKeyframe( int time )
		{
			if( time < 0 )
				throw new ArgumentOutOfRangeException( "time" );

			for( var k = 0; k < keyframes.Count; ++k )
			{
				if( keyframes[ k ].time == time )
					return true;
			}
			return false;
		}

	}

}