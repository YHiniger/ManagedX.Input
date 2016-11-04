using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{

	/// <summary>A time-based sequence of <see cref="Vibration"/> structures.</summary>
	[Serializable]
	public sealed class VibrationSequence
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
				return obj is Keyframe && this.Equals( (Keyframe)obj );
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



		private static int KeyframeComparer( Keyframe frame, Keyframe other )
		{
			return frame.time.CompareTo( other.time );
		}



		private readonly List<Keyframe> keyframes;
		private int lastFrameTime;
		private bool loop;
		private bool sorted;



		/// <summary>Initializes a new <see cref="VibrationSequence"/>.</summary>
		public VibrationSequence()
		{
			keyframes = new List<Keyframe>();
		}



		/// <summary>Gets the time of the last vibration keyframe.</summary>
		public int LastFrameTime { get { return lastFrameTime; } }


		/// <summary>Gets or sets a value indicating whether this <see cref="VibrationSequence"/> is looping.</summary>
		public bool Loops
		{
			get { return loop; }
			set { loop = value; }
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
			var set = false;

			var prev = 0;

			for( var k = 0; k < keyframes.Count; ++k )
			{
				var current = keyframes[ k ];
				if( current.time == time )
				{
					keyframes[ k ] = keyframe;
					set = true;
					break;
				}

				if( current.time < time && current.time > prev )
					prev = current.time;
			}

			if( !set )
			{
				if( prev == keyframes.Count - 1 )
				{
					keyframes.Add( keyframe );
					if( time > lastFrameTime )
						lastFrameTime = time;
				}
				else
					keyframes.Insert( prev + 1, keyframe );
			}
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
						lastFrameTime = keyframes[ k - 1 ].time;
					return true;
				}
			}

			return false;
		}


		/// <summary>Returns an array containing the time of all defined keyframes.</summary>
		/// <returns>Returns an array containing the time of all defined keyframes.</returns>
		public int[] GetKeyframeTimes()
		{
			if( !sorted )
			{
				keyframes.Sort( KeyframeComparer );
				sorted = true;
			}

			var times = new int[ keyframes.Count ];
			for( var k = 0; k < times.Length; ++k )
				times[ k ] = keyframes[ k ].time;
			return times;
		}


		/// <summary>Removes all keyframes from this <see cref="VibrationSequence"/>.</summary>
		public void Clear()
		{
			keyframes.Clear();
		}


		/// <summary>Gets a <see cref="Vibration"/> for a given time.
		/// <para>The returned structure is interpolated from keyframes.</para>
		/// </summary>
		/// <param name="time">The time, in milliseconds, of the requested <see cref="Vibration"/> structure.</param>
		/// <returns>Returns the requested <see cref="Vibration"/> structure.</returns>
		public Vibration this[ int time ]
		{
			get
			{
				if( time < 0 )
					return Vibration.Zero;

				if( time > lastFrameTime )
				{
					if( !loop )
						return Vibration.Zero;
					time %= lastFrameTime;
				}

				Vibration prev, next;
				int prevTime, nextTime;

				prev = next = Vibration.Zero;
				prevTime = 0;
				nextTime = int.MaxValue;

				for( var k = 0; k < keyframes.Count; ++k )
				{
					var current = keyframes[ k ];
					if( current.time == time )
						return current.vibration;

					if( current.time < time && current.time > prevTime )
					{
						prevTime = current.time;
						prev = current.vibration;
					}

					if( current.time > time && current.time < nextTime )
					{
						nextTime = current.time;
						next = current.vibration;
					}
				}

				return Vibration.Lerp( prev, next, (float)( time - prevTime ) / (float)( nextTime - prevTime ) );
			}
		}

	}

}