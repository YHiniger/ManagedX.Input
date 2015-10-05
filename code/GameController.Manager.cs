using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{
	using Design;


	partial class GameController
	{

		/// <summary>A singleton class providing access to XInput; implements the <see cref="IXInput"/> interface.</summary>
		private sealed class Manager : IXInput
		{


			private Dictionary<GameControllerIndex, GameController> controllers;
			private bool suspended;


			#region Constructor / Destructor

			/// <summary>Internal constructor.</summary>
			internal Manager()
			{
				controllers = new Dictionary<GameControllerIndex, GameController>( NativeMethods.MaxControllerCount );

				foreach( GameControllerIndex index in Enum.GetValues( typeof( GameControllerIndex ) ) )
					controllers.Add( index, new GameController( index ) );
			}


			/// <summary>Destructor.</summary>
			~Manager()
			{
				if( controllers != null )
				{
					controllers.Clear();
					controllers = null;
				}
			}

			#endregion


			/// <summary>Gets XInput version (ie: 1.3, 1.4, etc).</summary>
			public Version Version { get { return NativeMethods.Version; } }


			/// <summary>Gets XInput library name (ie: XInput1_3.dll, XInput1_4.dll, etc).</summary>
			public string LibraryFileName
			{
				get { return NativeMethods.LibraryName; }
			}


			/// <summary>Gets or sets a value indicating whether XInput is suspended.</summary>
			/// <remarks>Calls XInputEnable with the <code>enable</code> parameter set to the specified value.</remarks>
			public bool Suspended
			{
				get { return suspended; }
				set
				{ 
					NativeMethods.XInputEnable( suspended = value );
					// FIXME - only supported on Windows 8 and greater !
				}
			}


			/// <summary>Gets an XInput controller given its index.</summary>
			/// <param name="index">A <see cref="GameControllerIndex"/> value.</param>
			/// <returns>Returns the <see cref="IXInputController"/> associated with the specified <paramref name="index"/>.</returns>
			public IXInputController this[ GameControllerIndex index ] { get { return controllers[ index ]; } }


			/// <summary>Updates XInput controllers.</summary>
			/// <param name="time">The time elapsed since the start of the application.</param>
			public void Update( TimeSpan time )
			{
				foreach( GameController controller in controllers.Values )
					controller.Update( time );
			}


			/// <summary></summary>
			/// <returns></returns>
			public IEnumerator<IXInputController> GetEnumerator()
			{
				return controllers.Values.GetEnumerator();
			}

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return controllers.Values.GetEnumerator();
			}


			/// <summary>Returns a string in the form "XInput v{version_number}".</summary>
			/// <returns>Returns a string in the form "XInput v{version_number}".</returns>
			public sealed override string ToString()
			{
				return string.Format( System.Globalization.CultureInfo.InvariantCulture, "XInput v{0} ({1})", NativeMethods.Version, NativeMethods.LibraryName );
			}

		}

	}

}