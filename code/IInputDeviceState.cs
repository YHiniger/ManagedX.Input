namespace ManagedX.Input
{
	
	/// <summary>Defines a property which indicates whether a button/key is pressed.</summary>
	/// <typeparam name="TButton">An enumeration representing the input device buttons/keys.</typeparam>
	public interface IInputDeviceState<in TButton>
		where TButton : struct
	{

		/// <summary>Gets a value indicating whether a button/key is pressed.</summary>
		/// <param name="button">A button/key.</param>
		/// <returns>Returns true if the specified button/key is pressed, false otherwise.</returns>
		bool this[ TButton button ] { get; }


		///// <summary>Gets the time the state has been retrieved.</summary>
		//System.TimeSpan Time { get; }

	}

}