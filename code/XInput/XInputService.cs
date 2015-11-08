using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{
	using Design;


	/// <summary>Provides access to XInput; implements the <see cref="IXInput"/> interface.</summary>
	internal sealed class XInputService : IXInput
	{

		/// <summary>Defines the maximum number of controllers supported by XInput: 4.</summary>
		public const int MaxControllerCount = 4;

		
		/// <summary>The managed XInput service.</summary>
		internal static readonly XInputService Instance = new XInputService();


		private Version apiVersion;
		private string libraryFileName;
		private List<GameController> controllers;


		#region Constructor, destructor

		private XInputService()
		{
			var windowsVersion = Environment.OSVersion.Version;
			// assumes OSVersion.Platform == PlatformID.Win32NT

			APIVersion version;
			if( windowsVersion >= new Version( 10, 0 ) )		
			{
				// Windows 10
				version = APIVersion.XInput15;
				apiVersion = new Version( 1, 5 );
				libraryFileName = SafeNativeMethods.LibraryName15;
			}
			else if( windowsVersion >= new Version( 6, 2 ) )	
			{
				// Windows 8/8.x
				version = APIVersion.XInput14;
				apiVersion = new Version( 1, 4 );
				libraryFileName = SafeNativeMethods.LibraryName14;
			}
			else												
			{
				// Windows Vista/7
				version = APIVersion.XInput13;
				apiVersion = new Version( 1, 3 );
				libraryFileName = SafeNativeMethods.LibraryName13;
			}

			controllers = new List<GameController>( MaxControllerCount );
			for( var index = 0; index < MaxControllerCount; index++ )
				controllers.Add( new GameController( (GameControllerIndex)index, version ) );
		}


		/// <summary>Destructor.</summary>
		~XInputService()
		{
			if( controllers != null )
			{
				controllers.Clear();
				controllers = null;
			}
		}

		#endregion



		/// <summary>Gets the version of the underlying XInput API (ie: 1.3, 1.4, etc).</summary>
		public Version Version { get { return apiVersion; } }


		/// <summary>Gets the file name of the underlying XInput library (ie: XInput1_3.dll, XInput1_4.dll, etc).</summary>
		public string LibraryFileName { get { return string.Copy( libraryFileName ); } }


		/// <summary>Gets an XInput controller given its index.</summary>
		/// <param name="index">A <see cref="GameControllerIndex"/> value.</param>
		/// <returns>Returns the <see cref="IXInputController"/> associated with the specified <paramref name="index"/>.</returns>
		public IXInputController this[ GameControllerIndex index ] { get { return controllers[ (int)index ]; } }


		/// <summary>Updates the state of all supported XInput controllers.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		public void Update( TimeSpan time )
		{
			for( var c = 0; c < controllers.Count; c++ )
				if( !controllers[ c ].Disabled )
					controllers[ c ].Update( time );
		}


		/// <summary>Returns an enumerator which iterates through the list of controllers.</summary>
		/// <returns>Returns an enumerator which iterates through the list of controllers.</returns>
		public IEnumerator<IXInputController> GetEnumerator()
		{
			return controllers.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return controllers.GetEnumerator();
		}


		/// <summary>Returns a string in the form "XInput {<see cref="Version"/>}".</summary>
		/// <returns>Returns a string in the form "XInput {<see cref="Version"/>}".</returns>
		public sealed override string ToString()
		{
			return string.Format( System.Globalization.CultureInfo.InvariantCulture, "XInput {0}", apiVersion );
		}

	}

}