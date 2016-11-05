namespace ManagedX.Input.XInput
{

	/// <summary>Base class for <see cref="Vibration"/> effects.</summary>
	[System.Serializable]
	public abstract class VibrationEffect
	{


		/// <summary>Constructor.</summary>
		protected VibrationEffect()
		{
		}



		/// <summary>Gets a <see cref="Vibration"/> for a given time.</summary>
		/// <param name="time">The time, in milliseconds, of the requested <see cref="Vibration"/>.</param>
		/// <returns>Returns the requested <see cref="Vibration"/> on success, otherwise returns <see cref="Vibration.Zero"/>.</returns>
		public abstract Vibration this[ int time ] { get; }

	}

}