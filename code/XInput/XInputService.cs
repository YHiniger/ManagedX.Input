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


		/// <summary>Returns the version of XInput to use.</summary>
		/// <returns>Returns a value indicating which version of XInput to use.</returns>
		private static XInputVersion GetXInputVersion()
		{
#if XINPUT_13
			return XInputVersion.XInput13;
#elif XINPUT_14
			return XInputVersion.XInput14;
#else
			try
			{
				var osVersion = Environment.OSVersion;
				if( osVersion.Platform != PlatformID.Win32NT )
					return XInputVersion.NotSupported;

				var windowsVersion = osVersion.Version;

				//// Windows 10
				//if( windowsVersion >= new Version( 10, 0 ) )
				//	return XInputVersion.XInput15;

				// Windows 8 or greater
				if( windowsVersion >= new Version( 6, 2 ) )
					return XInputVersion.XInput14;

				// Windows Vista or 7 (with DirectX End-User Runtime June 2010)
				return XInputVersion.XInput13;
			}
			catch( Exception )
			{
				return XInputVersion.NotSupported;
			}
#endif
		}



		private readonly XInputVersion xInputVersion;
		private readonly Version apiVersion;
		private readonly List<GameController> controllers;



		#region Constructor, destructor

		private XInputService()
		{
			xInputVersion = GetXInputVersion();
			
			if( xInputVersion == XInputVersion.XInput14 )
				apiVersion = new Version( 1, 4 );
			else												
				apiVersion = new Version( 1, 3 );

			controllers = new List<GameController>( MaxControllerCount );
			for( var index = 0; index < MaxControllerCount; index++ )
				controllers.Add( new GameController( (GameControllerIndex)index, xInputVersion ) );
		}


		/// <summary>Destructor.</summary>
		~XInputService()
		{
			if( controllers != null )
				controllers.Clear();
		}

		#endregion



		/// <summary>Gets the version of the underlying XInput API.</summary>
		public Version Version { get { return apiVersion; } }


		/// <summary>Gets the file name of the underlying XInput library (ie: XInput1_4.dll).</summary>
		public string LibraryFileName
		{
			get
			{
				if( xInputVersion == XInputVersion.XInput13 )
					return SafeNativeMethods.LibraryName13;
				
				return SafeNativeMethods.LibraryName14;
			}
		}


		/// <summary>Gets an XInput controller given its index.</summary>
		/// <param name="index">A <see cref="GameControllerIndex"/> value.</param>
		/// <returns>Returns the <see cref="IXInputController"/> associated with the specified <paramref name="index"/>.</returns>
		public IXInputController this[ GameControllerIndex index ] { get { return controllers[ (int)index ]; } }


		/// <summary>Updates the state of all (non disabled) XInput controllers.</summary>
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