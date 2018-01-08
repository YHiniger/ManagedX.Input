namespace ManagedX.Input
{

	/// <summary>Defines a method which indicates whether a key or button is pressed.</summary>
	/// <typeparam name="TButton">An enumeration representing the input device keys or buttons.</typeparam>
	public interface IInputDeviceState<in TButton>
		where TButton : struct
	{

		/// <summary>Gets a value indicating whether a key or button is pressed.</summary>
		/// <param name="button">A key or button.</param>
		/// <returns>Returns true if the specified key or button is pressed, false otherwise.</returns>
		bool IsPressed( TButton button );

	}

}