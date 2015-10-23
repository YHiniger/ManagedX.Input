namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a mouse, as a managed input device (see <see cref="IInputDevice"/>).</summary>
	public interface IMouse : IInputDevice<MouseState, MouseButton>
	{
		
		/// <summary>Gets or sets the cumulated wheel value.</summary>
		int WheelValue { get; set; }


		///// <summary>Gets or sets a value indicating the state of the mouse cursor.</summary>
		//MouseCursorOptions CursorState { get; set; }


		///// <summary>Sets the mouse cursor position.</summary>
		///// <param name="position">The new cursor position.</param>
		//void SetCursorPosition( Point position );

	}

}