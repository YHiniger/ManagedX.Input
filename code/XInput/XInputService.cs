using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{
	using Design;


	/// <summary>Provides access to XInput; implements the <see cref="IXInput"/> interface.</summary>
	internal sealed class XInputService : IXInput
	{

		/// <summary>Defines the maximum number of controllers supported by XInput: 8 (on Windows 10).</summary>
		public const int MaxControllerCount = 8;

		
		/// <summary>The managed XInput service.</summary>
		internal static readonly XInputService Instance = new XInputService();


		/// <summary>Sets the reporting state of XInput.</summary>
		/// <param name="enable">If set to false, XInput will only send neutral data in response to GetState (all buttons up, axes centered, and triggers at 0).
		/// SetState calls will be registered but not sent to the device.
		/// Sending true will restore reading and writing functionality to normal.
		/// </param>
		private delegate void EnableProc( bool enable );


		private Version apiVersion;
		private string libraryFileName;
		private int supportedControllerCount;
		private EnableProc enableProc;
		private bool suspended;
		private List<GameController> controllers;


		#region Constructor, destructor

		/// <summary>Private constructor.</summary>
		private XInputService()
		{
			var windowsVersion = Environment.OSVersion.Version;
			// assumes OSVersion.Platform == PlatformID.Win32NT

			APIVersion version;
			if( windowsVersion >= new Version( 10, 0 ) )		// Windows 10
				version = this.Setup15();
			else if( windowsVersion >= new Version( 6, 2 ) )	// Windows 8/8.x
				version = this.Setup14();
			else												// Windows Vista/7
				version = this.Setup13();

			controllers = new List<GameController>( MaxControllerCount );
			for( int index = 0; index < MaxControllerCount; index++ )
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


		#region Setup*

		private APIVersion Setup15()
		{
			apiVersion = new Version( 1, 5 );
			libraryFileName = NativeMethods.LibraryName15;
			supportedControllerCount = 8;
			enableProc = NativeMethods.XInput15Enable;
			return APIVersion.XInput15;
		}

		private APIVersion Setup14()
		{
			apiVersion = new Version( 1, 4 );
			libraryFileName = NativeMethods.LibraryName14;
			supportedControllerCount = 4;
			enableProc = NativeMethods.XInput14Enable;
			return APIVersion.XInput14;
		}

		private APIVersion Setup13()
		{
			apiVersion = new Version( 1, 3 );
			libraryFileName = NativeMethods.LibraryName13;
			supportedControllerCount = 4;
			enableProc = NativeMethods.XInput13Enable;
			return APIVersion.XInput13;
		}

		#endregion // Setup*


		/// <summary>Gets the version of the underlying XInput API (ie: 1.3, 1.4, etc).</summary>
		public Version Version { get { return apiVersion; } }


		/// <summary>Gets the file name of the underlying XInput library (ie: XInput1_3.dll, XInput1_4.dll, etc).</summary>
		public string LibraryFileName { get { return string.Copy( libraryFileName ); } }


		///// <summary>Sets the reporting state of XInput.</summary>
		///// <param name="enable">If set to false, XInput will only send neutral data in response to <see cref="XInputGetState"/> (all buttons up, axes centered, and triggers at 0).
		///// <see cref="XInputSetState"/> calls will be registered but not sent to the device.
		///// Sending any value other than false will restore reading and writing functionality to normal.
		///// </param>
		/// <summary>Gets or sets a value indicating whether XInput is suspended.</summary>
		/// <remarks>Calls XInputEnable with the <code>enable</code> parameter set to the specified value.</remarks>
		public bool Suspended
		{
			get { return suspended; }
			set { enableProc( suspended = value ); }
		}


		/// <summary>Gets the maximum number of game controllers supported by XInput: 8 on Windows 10, otherwise 4.</summary>
		public int SupportedControllerCount { get { return supportedControllerCount; } }


		/// <summary>Gets an XInput controller given its index.</summary>
		/// <param name="index">A <see cref="GameControllerIndex"/> value.</param>
		/// <returns>Returns the <see cref="IXInputController"/> associated with the specified <paramref name="index"/>.</returns>
		public IXInputController this[ GameControllerIndex index ] { get { return controllers[ (int)index ]; } }


		/// <summary>Updates the state of all supported XInput controllers.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		public void Update( TimeSpan time )
		{
			foreach( var controller in controllers )
				controller.Update( time );
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