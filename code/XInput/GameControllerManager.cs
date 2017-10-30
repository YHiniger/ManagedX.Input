using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{

	/// <summary>Provides access to XInput game controllers.</summary>
	public static class GameControllerManager
	{

		private static XInputVersion version = GetXInputVersion();
		private static readonly List<GameController> gameControllers = Initialize();

	
		private static XInputVersion GetXInputVersion()
		{
#if XINPUT_14
			return XInputVersion.XInput14;
#elif XINPUT_13
			return XInputVersion.XInput13;
#else
			try
			{
				var osVersion = Environment.OSVersion;
				if( osVersion.Platform == PlatformID.Win32NT )
				{
					var windowsVersion = osVersion.Version;

					// Windows 8 or greater
					if( windowsVersion >= new Version( 6, 2 ) )
						return XInputVersion.XInput14;

					// Windows Vista or 7 (with DirectX End-User Runtime June 2010)
					return XInputVersion.XInput13;
				}
			}
			catch( InvalidOperationException )
			{
			}
			return XInputVersion.NotSupported;
#endif
		}


		private static List<GameController> Initialize()
		{
			var list = new List<GameController>( GameController.MaxControllerCount );
			try
			{
				if( version == XInputVersion.XInput14 )
				{
					for( var c = 0; c < GameController.MaxControllerCount; c++ )
						list.Add( new XInput14GameController( (GameControllerIndex)c ) );
				}
				else if( version == XInputVersion.XInput13 )
				{
					for( var c = 0; c < GameController.MaxControllerCount; c++ )
						list.Add( new XInput13GameController( (GameControllerIndex)c ) );
				}
			}
			catch( NotSupportedException )
			{
				list.Clear();
				version = XInputVersion.NotSupported;
			}
			return list;
		}



		/// <summary>Gets the version of the underlying XInput API.</summary>
		public static Version Version
		{
			get
			{
				if( version == XInputVersion.XInput14 )
					return new Version( 1, 4 );
				
				if( version == XInputVersion.XInput13 )
					return new Version( 1, 3 );

				return new Version( 0, 0 );
			}
		}


		/// <summary>Gets the file name of the underlying XInput library (ie: XInput1_4.dll); can be null if XInput is not supported.</summary>
		public static string LibraryFileName
		{
			get
			{
				if( version == XInputVersion.XInput14 )
					return XInput14GameController.LibraryName;

				if( version == XInputVersion.XInput13 )
					return XInput13GameController.LibraryName;

				return null;
			}
		}


		/// <summary>Returns an XInput <see cref="GameController"/> given its index.</summary>
		/// <param name="index">The index of the requested game controller.</param>
		/// <returns>Returns the XInput game controller associated with the specified <paramref name="index"/>.</returns>
		public static GameController GetController( GameControllerIndex index )
		{
			return gameControllers[ (int)index ];
		}


		/// <summary>Updates the state of all enabled XInput game controllers.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		public static void Update( TimeSpan time )
		{
			var cMax = gameControllers.Count;
			for( var c = 0; c < cMax; ++c )
			{
				var gameController = gameControllers[ c ];
				if( !gameController.Disabled )
					gameController.Update( time );
			}
		}

	}

}