namespace ManagedX.Input.Raw
{

	/// <summary>Enumerates some top-level collection (TLC) UsagePage and Usage combinations.</summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/hardware/ff538842%28v=vs.85%29.aspx</remarks>
	public enum TopLevelCollectionUsage : int
	{
		
		/// <summary>Undefined.</summary>
		None = 0,


		#region Usage page 1

		/// <summary>Pointers.</summary>
		Pointer = ( 1 << 16 ) | 1,

		/// <summary>Mice.</summary>
		Mouse = ( 2 << 16 ) | 1,

		/// <summary>Joysticks.</summary>
		Joystick = ( 4 << 16 ) | 1,

		/// <summary>Gamepads.</summary>
		Gamepad = ( 5 << 16 ) | 1,

		/// <summary>Keyboards.</summary>
		Keyboard = ( 6 << 16 ) | 1,

		/// <summary>Keypads.</summary>
		Keypad = ( 7 << 16 ) | 1,

		/// <summary>System controls.</summary>
		SystemControl = ( 0x80 << 16 ) | 1,

		#endregion Usage page 1


		#region Usage page 12

		/// <summary></summary>
		ConsumerAudioControl = ( 1 << 16 ) | 12,

		#endregion Usage page 12

	}

}