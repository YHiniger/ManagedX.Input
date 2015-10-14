namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement a keyboard, as a managed input device (see <see cref="IInputDevice"/>).</summary>
	public interface IKeyboard : IInputDevice<KeyboardState, Key>
	{
		
		// TODO - void SetState( bool capsLock, bool numLock, bool scrollLock );

	}

}