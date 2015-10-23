namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a mouse, as a managed input device (see <see cref="IInputDevice"/>).</summary>
	public interface IMouse : IInputDevice<MouseState, MouseButton>
	{
		
		/// <summary>Gets or sets the cumulated wheel value.</summary>
		int WheelValue { get; set; }

	}

}