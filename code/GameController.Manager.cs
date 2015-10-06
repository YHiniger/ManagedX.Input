using System;
using System.Collections.Generic;


namespace ManagedX.Input.XInput
{
	using Design;


	partial class GameController
	{

		/// <summary>A singleton class providing access to XInput; implements the <see cref="IXInput"/> interface.</summary>
		private sealed class XInputService : IXInput
		{


			private Dictionary<GameControllerIndex, GameController> controllers;
			private bool suspended;


			#region Constructor / Destructor

			/// <summary>Internal constructor.</summary>
			internal XInputService()
			{
				controllers = new Dictionary<GameControllerIndex, GameController>( NativeMethods.MaxControllerCount );

				foreach( GameControllerIndex index in Enum.GetValues( typeof( GameControllerIndex ) ) )
					controllers.Add( index, new GameController( index ) );
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


			/// <summary>Gets version of the underlying XInput API (ie: 1.3, 1.4, etc).</summary>
			public Version Version { get { return NativeMethods.Version; } }


			/// <summary>Gets XInput library name (ie: XInput1_3.dll, XInput1_4.dll, etc).</summary>
			public string LibraryFileName { get { return NativeMethods.LibraryName; } }


			/// <summary>Gets or sets a value indicating whether XInput is suspended.</summary>
			/// <remarks>Calls XInputEnable with the <code>enable</code> parameter set to the specified value.</remarks>
			public bool Suspended
			{
				get { return suspended; }
				set { NativeMethods.XInputEnable( suspended = value ); }
			}


			/// <summary>Gets an XInput controller given its index.</summary>
			/// <param name="index">A <see cref="GameControllerIndex"/> value.</param>
			/// <returns>Returns the <see cref="IXInputController"/> associated with the specified <paramref name="index"/>.</returns>
			public IXInputController this[ GameControllerIndex index ] { get { return controllers[ index ]; } }


			/// <summary>Updates XInput controllers.</summary>
			/// <param name="time">The time elapsed since the start of the application.</param>
			public void Update( TimeSpan time )
			{
				foreach( var controller in controllers.Values )
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


			/// <summary>Returns a string in the form "XInput {<see cref="Version"/>}".</summary>
			/// <returns>Returns a string in the form "XInput {<see cref="Version"/>}".</returns>
			public sealed override string ToString()
			{
				return string.Format( System.Globalization.CultureInfo.InvariantCulture, "XInput {0}", NativeMethods.Version );
			}

		}

	}

}