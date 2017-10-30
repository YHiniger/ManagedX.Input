using System;


namespace ManagedX.Input.XInput
{

	/// <summary>Base class for <see cref="Vibration"/> effects.</summary>
	[Serializable]
	public abstract class VibrationEffect
	{

		private bool isRepeating;
		private int duration;



		/// <summary>Constructor.</summary>
		protected VibrationEffect()
		{
		}



		/// <summary>Gets or sets a value indicating whether the vibration effect is repeating.</summary>
		public virtual bool IsRepeating
		{
			get => isRepeating;
			set => isRepeating = value;
		}


		/// <summary>Gets the duration, in milliseconds, of the vibration effect.
		/// <para>When <see cref="IsRepeating"/> is true, this is the duration of one cycle (period).</para>
		/// </summary>
		public int Duration
		{
			get => duration;
			protected set => duration = Math.Max( 0, value );
		}


		/// <summary>Gets a <see cref="Vibration"/> for a given time.</summary>
		/// <param name="time">The time, in milliseconds, of the requested <see cref="Vibration"/>.</param>
		/// <returns>Returns the requested <see cref="Vibration"/> on success, otherwise returns <see cref="Vibration.Zero"/>.</returns>
		public abstract Vibration this[ int time ] { get; }

	}

}