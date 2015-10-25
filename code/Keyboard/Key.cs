using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input
{

	/// <summary>Enumerates keyboard keys using their <see cref="VirtualKeyCode"/>.</summary>
	public enum Key : int
	{

		/// <summary>No key.</summary>
		None = VirtualKeyCode.None,


		/// <summary>The CANCEL key.</summary>
		Cancel = VirtualKeyCode.Cancel,

		
		/// <summary>The BACKSPACE key.</summary>
		Back = VirtualKeyCode.Back,
		
		/// <summary>The TAB key.</summary>
		Tab = VirtualKeyCode.Tab,
		
		/// <summary>The LINE FEED key.</summary>
		LineFeed = VirtualKeyCode.LineFeed,
		
		/// <summary>The CLEAR key.</summary>
		Clear = VirtualKeyCode.Clear,
		
		/// <summary>The ENTER or RETURN key.</summary>
		Enter = VirtualKeyCode.Enter,
		

		/// <summary>The SHIFT key.</summary>
		ShiftKey = VirtualKeyCode.ShiftKey,
		
		/// <summary>The CTRL key.</summary>
		ControlKey = VirtualKeyCode.ControlKey,
		
		/// <summary>The ALT key.</summary>
		Menu = VirtualKeyCode.Menu,
		
		/// <summary>The PAUSE key.</summary>
		Pause = VirtualKeyCode.Pause,
		
		/// <summary>The CAPS LOCK key.</summary>
		CapsLock = VirtualKeyCode.CapsLock,

		
		/// <summary>The IME Kana or Hangul mode key.</summary>
		KanaMode = VirtualKeyCode.KanaMode,
		
		/// <summary>The IME Junja mode key.</summary>
		JunjaMode = VirtualKeyCode.JunjaMode,
		
		/// <summary>The IME final mode key.</summary>
		FinalMode = VirtualKeyCode.FinalMode,
		
		/// <summary>The IME Hanja or Kanji mode key.</summary>
		HanjaMode = VirtualKeyCode.HanjaMode,


		/// <summary>The ESC key.</summary>
		Escape = VirtualKeyCode.Escape,
		

		/// <summary>The IME Convert key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEConvert = VirtualKeyCode.IMEConvert,
		
		/// <summary>The IME Nonconvert key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMENonconvert = VirtualKeyCode.IMENonconvert,
		
		/// <summary>The IME Accept key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEAccept = VirtualKeyCode.IMEAccept,
		
		/// <summary>The IME mode modification key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "IME" )]
		IMEModeChange = VirtualKeyCode.IMEModeChange,
		
		
		/// <summary>The SPACEBAR key.</summary>
		Space = VirtualKeyCode.Space,
		
		/// <summary>The PAGE UP key.</summary>
		PageUp = VirtualKeyCode.PageUp,
		
		/// <summary>The PAGE DOWN key.</summary>
		PageDown = VirtualKeyCode.PageDown,
		
		/// <summary>The END key.</summary>
		End = VirtualKeyCode.End,
		
		/// <summary>The HOME key.</summary>
		Home = VirtualKeyCode.Home,
		

		/// <summary>The LEFT ARROW key.</summary>
		Left = VirtualKeyCode.Left,
		
		/// <summary>The UP ARROW key.</summary>
		Up = VirtualKeyCode.Up,
		
		/// <summary>The RIGHT ARROW key.</summary>
		Right = VirtualKeyCode.Right,
		
		/// <summary>The DOWN ARROW key.</summary>
		Down = VirtualKeyCode.Down,
		

		/// <summary>The SELECT key.</summary>
		Select = VirtualKeyCode.Select,
		
		/// <summary>The PRINT key.</summary>
		Print = VirtualKeyCode.Print,
		
		/// <summary>The EXECUTE key.</summary>
		Execute = VirtualKeyCode.Execute,
		
		/// <summary>The PRINT SCREEN key.</summary>
		PrintScreen = VirtualKeyCode.PrintScreen,
		
		/// <summary>The INS key.</summary>
		Insert = VirtualKeyCode.Insert,
		
		/// <summary>The DEL key.</summary>
		Delete = VirtualKeyCode.Delete,
		
		/// <summary>The HELP key.</summary>
		Help = VirtualKeyCode.Help,
		
		/// <summary>The 0 key.</summary>
		D0 = VirtualKeyCode.D0,
		
		/// <summary>The 1 key.</summary>
		D1 = VirtualKeyCode.D1,
		
		/// <summary>The 2 key.</summary>
		D2 = VirtualKeyCode.D2,
		
		/// <summary>The 3 key.</summary>
		D3 = VirtualKeyCode.D3,
		
		/// <summary>The 4 key.</summary>
		D4 = VirtualKeyCode.D4,
		
		/// <summary>The 5 key.</summary>
		D5 = VirtualKeyCode.D5,

		/// <summary>The 6 key.</summary>
		D6 = VirtualKeyCode.D6,
		
		/// <summary>The 7 key.</summary>
		D7 = VirtualKeyCode.D7,
		
		/// <summary>The 8 key.</summary>
		D8 = VirtualKeyCode.D8,
		
		/// <summary>The 9 key.</summary>
		D9 = VirtualKeyCode.D9,
		
		
		/// <summary>The A key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A" )]
		A = VirtualKeyCode.A,
		
		/// <summary>The B key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B" )]
		B = VirtualKeyCode.B,
		
		/// <summary>The C key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C" )]
		C = VirtualKeyCode.C,
		
		/// <summary>The D key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D" )]
		D = VirtualKeyCode.D,
		
		/// <summary>The E key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "E" )]
		E = VirtualKeyCode.E,
		
		/// <summary>The F key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F" )]
		F = VirtualKeyCode.F,
		
		/// <summary>The G key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G" )]
		G = VirtualKeyCode.G,
		
		/// <summary>The H key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "H" )]
		H = VirtualKeyCode.H,
		
		/// <summary>The I key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "I" )]
		I = VirtualKeyCode.I,
		
		/// <summary>The J key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "J" )]
		J = VirtualKeyCode.J,
		
		/// <summary>The K key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "K" )]
		K = VirtualKeyCode.K,
		
		/// <summary>The L key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L" )]
		L = VirtualKeyCode.L,
		
		/// <summary>The M key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M" )]
		M = VirtualKeyCode.M,
		
		/// <summary>The N key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "N" )]
		N = VirtualKeyCode.N,
		
		/// <summary>The O key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "O" )]
		O = VirtualKeyCode.O,
		
		/// <summary>The P key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P" )]
		P = VirtualKeyCode.P,
		
		/// <summary>The Q key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Q" )]
		Q = VirtualKeyCode.Q,
		
		/// <summary>The R key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R" )]
		R = VirtualKeyCode.R,
		
		/// <summary>The S key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "S" )]
		S = VirtualKeyCode.S,
		
		/// <summary>The T key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "T" )]
		T = VirtualKeyCode.T,
		
		/// <summary>The U key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U" )]
		U = VirtualKeyCode.U,
		
		/// <summary>The V key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "V" )]
		V = VirtualKeyCode.V,
		
		/// <summary>The W key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W" )]
		W = VirtualKeyCode.W,
		
		/// <summary>The X key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X" )]
		X = VirtualKeyCode.X,
		
		/// <summary>The Y key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y" )]
		Y = VirtualKeyCode.Y,
		
		/// <summary>The Z key.</summary>
		[SuppressMessage( "Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z" )]
		Z = VirtualKeyCode.Z,
		
		
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		LWin = VirtualKeyCode.LWin,
		
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		RWin = VirtualKeyCode.RWin,
		
		/// <summary>The application key (Microsoft Natural Keyboard).</summary>
		Apps = VirtualKeyCode.Apps,
		
		/// <summary>The SLEEP key.</summary>
		Sleep = VirtualKeyCode.Sleep,
		
		
		/// <summary>The 0 key on the numeric keypad.</summary>
		NumPad0 = VirtualKeyCode.NumPad0,
		
		/// <summary>The 1 key on the numeric keypad.</summary>
		NumPad1 = VirtualKeyCode.NumPad1,

		/// <summary>The 2 key on the numeric keypad.</summary>
		NumPad2 = VirtualKeyCode.NumPad2,

		/// <summary>The 3 key on the numeric keypad.</summary>
		NumPad3 = VirtualKeyCode.NumPad3,

		/// <summary>The 4 key on the numeric keypad.</summary>
		NumPad4 = VirtualKeyCode.NumPad4,

		/// <summary>The 5 key on the numeric keypad.</summary>
		NumPad5 = VirtualKeyCode.NumPad5,

		/// <summary>The 6 key on the numeric keypad.</summary>
		NumPad6 = VirtualKeyCode.NumPad6,

		/// <summary>The 7 key on the numeric keypad.</summary>
		NumPad7 = VirtualKeyCode.NumPad7,

		/// <summary>The 8 key on the numeric keypad.</summary>
		NumPad8 = VirtualKeyCode.NumPad8,

		/// <summary>The 9 key on the numeric keypad.</summary>
		NumPad9 = VirtualKeyCode.NumPad9,
		
		/// <summary>The MULTIPLY key.</summary>
		Multiply = VirtualKeyCode.Multiply,
		
		/// <summary>The ADD key.</summary>
		Add = VirtualKeyCode.Add,
		
		/// <summary>The SEPARATOR key.</summary>
		Separator = VirtualKeyCode.Separator,
		
		/// <summary>The SUBTRACT key.</summary>
		Subtract = VirtualKeyCode.Subtract,
		
		/// <summary>The DECIMAL key.</summary>
		Decimal = VirtualKeyCode.Decimal,
		
		/// <summary>The DIVIDE key.</summary>
		Divide = VirtualKeyCode.Divide,
		
		
		/// <summary>The F1 key.</summary>
		F1 = VirtualKeyCode.F1,
		
		/// <summary>The F2 key.</summary>
		F2 = VirtualKeyCode.F2,
		
		/// <summary>The F3 key.</summary>
		F3 = VirtualKeyCode.F3,
		
		/// <summary>The F4 key.</summary>
		F4 = VirtualKeyCode.F4,
		
		/// <summary>The F5 key.</summary>
		F5 = VirtualKeyCode.F5,
		
		/// <summary>The F6 key.</summary>
		F6 = VirtualKeyCode.F6,
		
		/// <summary>The F7 key.</summary>
		F7 = VirtualKeyCode.F7,
		
		/// <summary>The F8 key.</summary>
		F8 = VirtualKeyCode.F8,
		
		/// <summary>The F9 key.</summary>
		F9 = VirtualKeyCode.F9,
		
		/// <summary>The F10 key.</summary>
		F10 = VirtualKeyCode.F10,
		
		/// <summary>The F11 key.</summary>
		F11 = VirtualKeyCode.F11,
		
		/// <summary>The F12 key.</summary>
		F12 = VirtualKeyCode.F12,
		
		/// <summary>The F13 key.</summary>
		F13 = VirtualKeyCode.F13,
		
		/// <summary>The F14 key.</summary>
		F14 = VirtualKeyCode.F14,
		
		/// <summary>The F15 key.</summary>
		F15 = VirtualKeyCode.F15,
		
		/// <summary>The F16 key.</summary>
		F16 = VirtualKeyCode.F16,
		
		/// <summary>The F17 key.</summary>
		F17 = VirtualKeyCode.F17,
		
		/// <summary>The F18 key.</summary>
		F18 = VirtualKeyCode.F18,
		
		/// <summary>The F19 key.</summary>
		F19 = VirtualKeyCode.F19,
		
		/// <summary>The F20 key.</summary>
		F20 = VirtualKeyCode.F20,
		
		/// <summary>The F21 key.</summary>
		F21 = VirtualKeyCode.F21,
		
		/// <summary>The F22 key.</summary>
		F22 = VirtualKeyCode.F22,
		
		/// <summary>The F23 key.</summary>
		F23 = VirtualKeyCode.F23,
		
		/// <summary>The F24 key.</summary>
		F24 = VirtualKeyCode.F24,

		
		/// <summary>The NUM LOCK key.</summary>
		NumLock = VirtualKeyCode.NumLock,

		/// <summary>The SCROLL LOCK key.</summary>
		ScrollLock = VirtualKeyCode.ScrollLock,


		/// <summary>The left SHIFT key.</summary>
		LShiftKey = VirtualKeyCode.LShiftKey,
		
		/// <summary>The right SHIFT key.</summary>
		RShiftKey = VirtualKeyCode.RShiftKey,
		
		/// <summary>The left CTRL key.</summary>
		LControlKey = VirtualKeyCode.LControlKey,
		
		/// <summary>The right CTRL key.</summary>
		RControlKey = VirtualKeyCode.RControlKey,
		
		/// <summary>The left ALT key.</summary>
		LMenu = VirtualKeyCode.LMenu,
		
		/// <summary>The right ALT key.</summary>
		RMenu = VirtualKeyCode.RMenu,
		
		
		/// <summary>The browser BACK key.</summary>
		BrowserBack = VirtualKeyCode.BrowserBack,
		
		/// <summary>The browser NEXT key.</summary>
		BrowserForward = VirtualKeyCode.BrowserForward,
		
		/// <summary>The browser REFRESH key.</summary>
		BrowserRefresh = VirtualKeyCode.BrowserRefresh,
		
		/// <summary>The browser STOP key.</summary>
		BrowserStop = VirtualKeyCode.BrowserStop,
		
		/// <summary>The browser SEARCH key.</summary>
		BrowserSearch = VirtualKeyCode.BrowserSearch,
		
		/// <summary>The browser FAVORITES key.</summary>
		BrowserFavorites = VirtualKeyCode.BrowserFavorites,
		
		/// <summary>The browser HOME key.</summary>
		BrowserHome = VirtualKeyCode.BrowserHome,
		
		/// <summary>The VOLUME MUTE key.</summary>
		VolumeMute = VirtualKeyCode.VolumeMute,
		
		/// <summary>The VOLUME DOWN key.</summary>
		VolumeDown = VirtualKeyCode.VolumeDown,
		
		/// <summary>The VOLUME UP key.</summary>
		VolumeUp = VirtualKeyCode.VolumeUp,
		
		/// <summary>The MEDIA NEXT TRACK key.</summary>
		MediaNextTrack = VirtualKeyCode.MediaNextTrack,
		
		/// <summary>The MEDIA PREVIOUS TRACK key.</summary>
		MediaPreviousTrack = VirtualKeyCode.MediaPreviousTrack,
		
		/// <summary>The MEDIA STOP key.</summary>
		MediaStop = VirtualKeyCode.MediaStop,
		
		/// <summary>The MEDIA PLAY/PAUSE key.</summary>
		MediaPlayPause = VirtualKeyCode.MediaPlayPause,
		
		/// <summary>The LAUNCH MAIL key.</summary>
		LaunchMail = VirtualKeyCode.LaunchMail,
		
		/// <summary>The SELECT MEDIA key.</summary>
		SelectMedia = VirtualKeyCode.SelectMedia,

		/// <summary>The LAUNCH APPLICATION #1 key.</summary>
		LaunchApplication1 = VirtualKeyCode.LaunchApplication1,
		
		/// <summary>The LAUNCH APPLICATION #2 key.</summary>
		LaunchApplication2 = VirtualKeyCode.LaunchApplication2,
		
		/// <summary>The OEM SEMICOLON key.</summary>
		OemSemicolon = VirtualKeyCode.OemSemicolon,
		
		/// <summary>The OEM PLUS key.</summary>
		OemPlus = VirtualKeyCode.OemPlus,
		
		/// <summary>The OEM COMMA key.</summary>
		OemComma = VirtualKeyCode.OemComma,

		/// <summary>The OEM MINUS key.</summary>
		OemMinus = VirtualKeyCode.OemMinus,

		/// <summary>The OEM PERIOD key.</summary>
		OemPeriod = VirtualKeyCode.OemPeriod,

		/// <summary>The OEM 2 key.</summary>
		Oem2 = VirtualKeyCode.Oem2,

		/// <summary>The OEM 3 key.</summary>
		Oem3 = VirtualKeyCode.Oem3,

		/// <summary>The OEM 4 key.</summary>
		Oem4 = VirtualKeyCode.Oem4,

		/// <summary>The OEM 5 key.</summary>
		Oem5 = VirtualKeyCode.Oem5,

		/// <summary>The OEM 6 key.</summary>
		Oem6 = VirtualKeyCode.Oem6,

		/// <summary>The OEM 7 key.</summary>
		Oem7 = VirtualKeyCode.Oem7,

		/// <summary>The OEM 8 key.</summary>
		Oem8 = VirtualKeyCode.Oem8,

		/// <summary>The OEM 102 key.</summary>
		Oem102 = VirtualKeyCode.Oem102,

		/// <summary>The PROCESS key.</summary>
		ProcessKey = VirtualKeyCode.ProcessKey,

		///// <summary>Permet de passer des caractères Unicode comme s'il s'agissait de séquences de touches.
		///// <para>La valeur de la touche Packet est le mot inférieur d'une valeur de clé virtuelle 32 bits utilisée pour les méthodes d'entrée autres qu'au clavier.</para>
		///// </summary>
		//Packet = VirtualKeyCode.Packet,

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
		Alt = VirtualKeyCode.Alt

	}

}