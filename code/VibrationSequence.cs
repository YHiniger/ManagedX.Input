using System;
using System.Collections.Generic;


namespace XInput.Design
{

	/// <summary>Contains a sequence of <see cref="Vibration"/> structures.</summary>
	public sealed class VibrationSequence
	{

		private static int CompareFramesByTime( KeyValuePair<int, Vibration> frame, KeyValuePair<int, Vibration> other )
		{
			return frame.Key.CompareTo( other.Key );
		}


		private int lastKeyFrameTime;
		private Dictionary<int, Vibration> keyframes;	// time (in milliseconds, relative to the sequence start time) --> state (index?)
		private bool sorted;


		/// <summary>Instantiates a new <see cref="VibrationSequence"/>.</summary>
		public VibrationSequence()
		{
			lastKeyFrameTime = 0;
			keyframes = new Dictionary<int, Vibration>();
		}



		/// <summary>Gets a <see cref="Vibration"/> structure for a frame, given its time.</summary>
		/// <param name="time">The frame time, in milliseconds.</param>
		/// <returns></returns>
		public Vibration this[ int time ]
		{
			get
			{
				if( time < 0 )
					throw new ArgumentOutOfRangeException( "time" );

				Vibration state;
				if( keyframes.TryGetValue( time, out state ) )
					return state;

				if( time > lastKeyFrameTime )
					return Vibration.Zero;

				if( !sorted )
					this.Sort();

				var prevFrame = new KeyValuePair<int, Vibration>( 0, Vibration.Zero );
				var nextFrame = new KeyValuePair<int, Vibration>( lastKeyFrameTime, keyframes[ lastKeyFrameTime ] );
				
				int frameTime;
				foreach( var frame in keyframes )
				{
					frameTime = frame.Key;

					if( frameTime < time && frameTime >= prevFrame.Key )
						prevFrame = frame;

					if( frameTime > time && frameTime <= nextFrame.Key )
						nextFrame = frame;
					// NOTE - if keyframes are sorted (as they should), we can leave the loop as soon as we found the next frame.
				}

				var amount = (float)( time - prevFrame.Key ) / (float)( nextFrame.Key - prevFrame.Key );
				return Vibration.Lerp( prevFrame.Value, nextFrame.Value, amount );
			}
		}


		/// <summary>Adds a frame to this <see cref="VibrationSequence"/>. If a frame is already present, the two frames are cumulated.</summary>
		/// <param name="time">The frame time, in milliseconds.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure representing the frame state.</param>
		public void Add( int time, Vibration vibration )
		{
			if( time < 0 )
				throw new ArgumentOutOfRangeException( "time" );

			Vibration frame;
			if( keyframes.TryGetValue( time, out frame ) )
				keyframes[ time ] = Vibration.Add( frame, vibration );
			else
			{
				keyframes.Add( time, vibration );
				sorted = false;
			}

			lastKeyFrameTime = Math.Max( lastKeyFrameTime, time );
		}

		
		/// <summary>Clears this <see cref="VibrationSequence"/>.</summary>
		public void Clear()
		{
			keyframes.Clear();
			lastKeyFrameTime = -1;
			sorted = true;
		}


		/// <summary>Sorts keyframes by time.</summary>
		public void Sort()
		{
			if( sorted )
				return;

			var frames = new KeyValuePair<int, Vibration>[ keyframes.Count ];
			int f = 0;
			foreach( var keyframe in keyframes )
				frames[ f++ ] = keyframe;
			keyframes.Clear();

			Array.Sort<KeyValuePair<int, Vibration>>( frames, CompareFramesByTime );
			foreach( var keyframe in frames )
				keyframes.Add( keyframe.Key, keyframe.Value );
			sorted = true;
		}

	}

}
