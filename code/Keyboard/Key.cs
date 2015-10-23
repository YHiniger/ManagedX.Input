using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{
	// TODO - make this a subset of the VirtualKeyCode enumeration:

	/// <summary>Enumerates keyboard keys using their <see cref="VirtualKeyCode"/>.</summary>
	public enum Key : int
	{

		/// <summary>No key.</summary>
		None = VirtualKeyCode.None,


		/// <summary>The CANCEL key.</summary>
		Cancel = 3,

		
		/// <summary>The BACKSPACE key.</summary>
		Back = 8,
		
		/// <summary>The TAB key.</summary>
		Tab = 9,
		
		/// <summary>The LINE FEED key.</summary>
		LineFeed = 10,
		
		/// <summary>The CLEAR key.</summary>
		Clear = 12,
		
		/// <summary>The ENTER or RETURN key.</summary>
		Enter = 13,
		

		/// <summary>The SHIFT key.</summary>
		ShiftKey = 16,
		
		/// <summary>The CTRL key.</summary>
		ControlKey = 17,
		
		/// <summary>The ALT key.</summary>
		Menu = 18,
		
		/// <summary>The PAUSE key.</summary>
		Pause = 19,
		
		/// <summary>The CAPS LOCK key.</summary>
		CapsLock = 20,

		
		/// <summary>The IME Kana mode key.</summary>
		KanaMode = 21,
		
		/// <summary>The IME Hangul mode key.</summary>
		HangulMode = 21,
		
		/// <summary>The IME Junja mode key.</summary>
		JunjaMode = 23,
		
		/// <summary>The IME final mode key.</summary>
		FinalMode = 24,
		
		/// <summary>The IME Hanja mode key.</summary>
		HanjaMode = 25,
		
		/// <summary>The IME Kanji mode key.</summary>
		KanjiMode = 25,



		/// <summary>The ESC key.</summary>
		Escape = 27,
		

		/// <summary>The IME Convert key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEConvert = 28,
		
		/// <summary>The IME Nonconvert key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMENonconvert = 29,
		
		/// <summary>The IME Accept key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEAccept = 30,
		
		/// <summary>The IME mode modification key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEModeChange = 31,
		
		
		/// <summary>The SPACEBAR key.</summary>
		Space = 32,
		
		/// <summary>The PAGE UP key.</summary>
		PageUp = 33,
		
		/// <summary>The PAGE DOWN key.</summary>
		PageDown = 34,
		
		/// <summary>The END key.</summary>
		End = 35,
		
		/// <summary>The HOME key.</summary>
		Home = 36,
		

		/// <summary>The LEFT ARROW key.</summary>
		Left = 37,
		
		/// <summary>The UP ARROW key.</summary>
		Up = 38,
		
		/// <summary>The RIGHT ARROW key.</summary>
		Right = 39,
		
		/// <summary>The DOWN ARROW key.</summary>
		Down = 40,
		

		/// <summary>The SELECT key.</summary>
		Select = 41,
		
		/// <summary>The PRINT key.</summary>
		Print = 42,
		
		/// <summary>The EXECUTE key.</summary>
		Execute = 43,
		
		/// <summary>The PRINT SCREEN key.</summary>
		PrintScreen = 44,
		
		/// <summary>The INS key.</summary>
		Insert = 45,
		
		/// <summary>The DEL key.</summary>
		Delete = 46,
		
		/// <summary>The HELP key.</summary>
		Help = 47,
		
		/// <summary>The 0 key.</summary>
		D0 = 48,
		
		/// <summary>The 1 key.</summary>
		D1 = 49,
		
		/// <summary>The 2 key.</summary>
		D2 = 50,
		
		/// <summary>The 3 key.</summary>
		D3 = 51,
		
		/// <summary>The 4 key.</summary>
		D4 = 52,
		
		/// <summary>The 5 key.</summary>
		D5 = 53,

		/// <summary>The 6 key.</summary>
		D6 = 54,
		
		/// <summary>The 7 key.</summary>
		D7 = 55,
		
		/// <summary>The 8 key.</summary>
		D8 = 56,
		
		/// <summary>The 9 key.</summary>
		D9 = 57,
		
		
		/// <summary>The A key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = 65,
		
		/// <summary>The B key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = 66,
		
		/// <summary>The C key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C" )]
		C = 67,
		
		/// <summary>The D key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D" )]
		D = 68,
		
		/// <summary>The E key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "E" )]
		E = 69,
		
		/// <summary>The F key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F" )]
		F = 70,
		
		/// <summary>The G key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G" )]
		G = 71,
		/// <summary>The H key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "H" )]
		H = 72,
		/// <summary>The I key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "I" )]
		I = 73,
		/// <summary>The J key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "J" )]
		J = 74,
		/// <summary>The K key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "K" )]
		K = 75,
		/// <summary>The L key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L" )]
		L = 76,
		/// <summary>The M key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M" )]
		M = 77,
		/// <summary>The N key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "N" )]
		N = 78,
		/// <summary>The O key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "O" )]
		O = 79,
		/// <summary>The P key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P" )]
		P = 80,
		/// <summary>The Q key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Q" )]
		Q = 81,
		/// <summary>The R key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R" )]
		R = 82,
		/// <summary>The S key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "S" )]
		S = 83,
		/// <summary>The T key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "T" )]
		T = 84,
		/// <summary>The U key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U" )]
		U = 85,
		/// <summary>The V key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "V" )]
		V = 86,
		/// <summary>The W key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W" )]
		W = 87,
		/// <summary>The X key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = 88,
		/// <summary>The Y key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = 89,
		/// <summary>The Z key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z" )]
		Z = 90,
		
		
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		LWin = 91,
		
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		RWin = 92,
		
		/// <summary>The application key (Microsoft Natural Keyboard).</summary>
		Apps = 93,
		
		/// <summary>The SLEEP key.</summary>
		Sleep = 95,
		
		
		/// <summary>The 0 key on the numeric keypad.</summary>
		NumPad0 = 96,
		
		/// <summary>The 1 key on the numeric keypad.</summary>
		NumPad1 = 97,

		/// <summary>The 2 key on the numeric keypad.</summary>
		NumPad2 = 98,

		/// <summary>The 3 key on the numeric keypad.</summary>
		NumPad3 = 99,

		/// <summary>The 4 key on the numeric keypad.</summary>
		NumPad4 = 100,

		/// <summary>The 5 key on the numeric keypad.</summary>
		NumPad5 = 101,

		/// <summary>The 6 key on the numeric keypad.</summary>
		NumPad6 = 102,

		/// <summary>The 7 key on the numeric keypad.</summary>
		NumPad7 = 103,

		/// <summary>The 8 key on the numeric keypad.</summary>
		NumPad8 = 104,

		/// <summary>The 9 key on the numeric keypad.</summary>
		NumPad9 = 105,
		
		/// <summary>The MULTIPLY key.</summary>
		Multiply = 106,
		
		/// <summary>The ADD key.</summary>
		Add = 107,
		
		/// <summary>The SEPARATOR key.</summary>
		Separator = 108,
		
		/// <summary>The SUBTRACT key.</summary>
		Subtract = 109,
		
		/// <summary>The DECIMAL key.</summary>
		Decimal = 110,
		
		/// <summary>The DIVIDE key.</summary>
		Divide = 111,
		
		
		/// <summary>The F1 key.</summary>
		F1 = 112,
		
		/// <summary>The F2 key.</summary>
		F2 = 113,
		
		/// <summary>The F3 key.</summary>
		F3 = 114,
		
		/// <summary>The F4 key.</summary>
		F4 = 115,
		
		/// <summary>The F5 key.</summary>
		F5 = 116,
		
		/// <summary>The F6 key.</summary>
		F6 = 117,
		
		/// <summary>The F7 key.</summary>
		F7 = 118,
		
		/// <summary>The F8 key.</summary>
		F8 = 119,
		
		/// <summary>The F9 key.</summary>
		F9 = 120,
		
		/// <summary>The F10 key.</summary>
		F10 = 121,
		
		/// <summary>The F11 key.</summary>
		F11 = 122,
		
		/// <summary>The F12 key.</summary>
		F12 = 123,
		
		/// <summary>The F13 key.</summary>
		F13 = 124,
		
		/// <summary>The F14 key.</summary>
		F14 = 125,
		
		/// <summary>The F15 key.</summary>
		F15 = 126,
		
		/// <summary>The F16 key.</summary>
		F16 = 127,
		
		/// <summary>The F17 key.</summary>
		F17 = 128,
		
		/// <summary>The F18 key.</summary>
		F18 = 129,
		
		/// <summary>The F19 key.</summary>
		F19 = 130,
		
		/// <summary>The F20 key.</summary>
		F20 = 131,
		
		/// <summary>The F21 key.</summary>
		F21 = 132,
		
		/// <summary>The F22 key.</summary>
		F22 = 133,
		
		/// <summary>The F23 key.</summary>
		F23 = 134,
		
		/// <summary>The F24 key.</summary>
		F24 = 135,

		
		/// <summary>The NUM LOCK key.</summary>
		NumLock = 144,

		/// <summary>The SCROLL LOCK key.</summary>
		ScrollLock = 145,

		/// <summary>The left SHIFT key.</summary>
		LShiftKey = 160,
		
		/// <summary>The right SHIFT key.</summary>
		RShiftKey = 161,
		
		/// <summary>The left CTRL key.</summary>
		LControlKey = 162,
		
		/// <summary>The right CTRL key.</summary>
		RControlKey = 163,
		
		/// <summary>The left ALT key.</summary>
		LMenu = 164,
		
		/// <summary>The right ALT key.</summary>
		RMenu = 165,
		
		
		/// <summary>The browser BACK key.</summary>
		BrowserBack = 166,
		
		/// <summary>The browser NEXT key.</summary>
		BrowserForward = 167,
		
		/// <summary>The browser REFRESH key.</summary>
		BrowserRefresh = 168,
		
		/// <summary>The browser STOP key.</summary>
		BrowserStop = 169,
		
		/// <summary>The browser SEARCH key.</summary>
		BrowserSearch = 170,
		
		/// <summary>The browser FAVORITES key.</summary>
		BrowserFavorites = 171,
		
		/// <summary>The browser HOME key.</summary>
		BrowserHome = 172,
		
		/// <summary>The VOLUME MUTE key.</summary>
		VolumeMute = 173,
		
		/// <summary>The VOLUME DOWN key.</summary>
		VolumeDown = 174,
		
		/// <summary>The VOLUME UP key.</summary>
		VolumeUp = 175,
		
		/// <summary>The MEDIA NEXT TRACK key.</summary>
		MediaNextTrack = 176,
		
		/// <summary>The MEDIA PREVIOUS TRACK key.</summary>
		MediaPreviousTrack = 177,
		
		/// <summary>The MEDIA STOP key.</summary>
		MediaStop = 178,
		
		/// <summary>The MEDIA PLAY/PAUSE key.</summary>
		MediaPlayPause = 179,
		
		/// <summary>The LAUNCH MAIL key.</summary>
		LaunchMail = 180,
		
		/// <summary>The SELECT MEDIA key.</summary>
		SelectMedia = 181,

		/// <summary>The LAUNCH APPLICATION #1 key.</summary>
		LaunchApplication1 = 182,
		
		/// <summary>The LAUNCH APPLICATION #2 key.</summary>
		LaunchApplication2 = 183,
		
		/// <summary>The OEM SEMICOLON key.</summary>
		OemSemicolon = 186,
		
		/// <summary>The OEM 1 key.</summary>
		Oem1 = 186,
		
		/// <summary>The OEM PLUS key.</summary>
		OemPlus = 187,
		
		/// <summary>The OEM COMMA key.</summary>
		OemComma = 188,

		/// <summary>The OEM MINUS key.</summary>
		OemMinus = 189,

		/// <summary>The OEM PERIOD key.</summary>
		OemPeriod = 190,

		/// <summary>The OEM 2 key.</summary>
		Oem2 = 191,

		/// <summary>The OEM 3 key.</summary>
		Oem3 = 192,

		/// <summary>The OEM 4 key.</summary>
		Oem4 = 219,

		/// <summary>The OEM 5 key.</summary>
		Oem5 = 220,

		/// <summary>The OEM 6 key.</summary>
		Oem6 = 221,

		/// <summary>The OEM 7 key.</summary>
		Oem7 = 222,

		/// <summary>The OEM 8 key.</summary>
		Oem8 = 223,

		/// <summary>The OEM 102 key.</summary>
		Oem102 = 226,

		/// <summary>The PROCESS key.</summary>
		ProcessKey = VirtualKeyCode.ProcessKey,

		/// <summary>Permet de passer des caractères Unicode comme s'il s'agissait de séquences de touches.
		/// <para>La valeur de la touche Packet est le mot inférieur d'une valeur de clé virtuelle 32 bits utilisée pour les méthodes d'entrée autres qu'au clavier.</para>
		/// </summary>
		Packet = VirtualKeyCode.Packet,

		/// <summary>The ATTN key.</summary>
		Attn = VirtualKeyCode.Attn,

		/// <summary>The CRSEL key.</summary>
		Crsel = VirtualKeyCode.Crsel,

		/// <summary>The EXSEL key.</summary>
		Exsel = VirtualKeyCode.Exsel,

		/// <summary>The ERASE EOF key.</summary>
		EraseEof = VirtualKeyCode.EraseEof,

		/// <summary>The PLAY key.</summary>
		Play = VirtualKeyCode.Play,

		/// <summary>The ZOOM key.</summary>
		Zoom = VirtualKeyCode.Zoom,

		///// <summary>Reserved for future use.</summary>
		//NoName = 252,

		/// <summary>The PA1 key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Pa" )]
		Pa1 = VirtualKeyCode.Pa1,

		/// <summary>The CLEAR key.</summary>
		OemClear = VirtualKeyCode.OemClear,


		/// <summary>The SHIFT modifier key.</summary>
		Shift = VirtualKeyCode.Shift,

		/// <summary>The CTRL modifier key.</summary>
		Control = VirtualKeyCode.Control,

		/// <summary>The ALT modifier key.</summary>
		Alt = VirtualKeyCode.Alt,


		//#region BitMasks

		// FIXME - this is not part of this enumeration !

		///// <summary>Le masque de bits pour extraire un code de touche à partir d'une valeur de touche.</summary>
		//KeyCode = 65535,

		///// <summary>Le masque de bits pour extraire les modificateurs à partir d'une valeur de touche.</summary>
		//Modifiers = -65536

		//#endregion

	}

}