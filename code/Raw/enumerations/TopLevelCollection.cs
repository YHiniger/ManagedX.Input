namespace ManagedX.Input.Raw
{

	/// <summary>Enumerates Usage values for UsagePage = 1.</summary>
	internal enum TopLevelCollectionPage1Usage : ushort
	{
		None = 0,
		Pointer = 1,
		Mouse = 2,
		// 3 ?
		Joystick = 4,
		Gamepad = 5,
		Keyboard = 6,
		Keypad = 7,
		SystemControl = 0x80
	}


	/// <summary>Enumerates some top-level collection (TLC) UsagePage and Usage combinations.</summary>
	internal enum TopLevelCollection : int
	{
		
		/// <summary>Undefined.</summary>
		None = 0,


		#region UsagePage = 1

		/// <summary>Pointers.</summary>
		Pointer = 1 | ( TopLevelCollectionPage1Usage.Pointer << 16 ),

		/// <summary>Mice.</summary>
		Mouse = 1 | ( TopLevelCollectionPage1Usage.Mouse << 16 ),

		/// <summary>Joysticks.</summary>
		Joystick = 1 | ( TopLevelCollectionPage1Usage.Joystick << 16 ),

		/// <summary>Gamepads.</summary>
		Gamepad = 1 | ( TopLevelCollectionPage1Usage.Gamepad << 16 ),

		/// <summary>Keyboards.</summary>
		Keyboard = 1 | ( TopLevelCollectionPage1Usage.Keyboard << 16 ),

		/// <summary>Keypads.</summary>
		Keypad = 1 | ( TopLevelCollectionPage1Usage.Keypad << 16 ),

		/// <summary>System controls.</summary>
		SystemControl = 1 | ( TopLevelCollectionPage1Usage.SystemControl << 16 ),

		#endregion // UsagePage = 1

	}

}