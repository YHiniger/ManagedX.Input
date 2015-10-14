using System;
using System.Collections.Generic;


namespace ManagedX.Input.Design
{

	/// <summary>Defines properties and methods to properly implement XInput support as a managed service.</summary>
	public interface IXInput : IEnumerable<IXInputController>
	{

		/// <summary>Gets the version of the underlying XInput API:
		/// <list type="bullet">
		/// <item><description>1.3 (requires Windows Vista or newer)</description></item>
		/// <item><description>1.4 (requires Windows 8 or newer)</description></item>
		/// <item><description>1.5 (requires Windows 10)</description></item>
		/// </list>
		/// </summary>
		Version Version { get; }


		/// <summary>Gets the name of the XInput library (DLL): XInput1_3.dll, XInput1_4.dll, etc.
		/// <para>For debugging purpose.</para>
		/// </summary>
		string LibraryFileName { get; }


		/// <summary>Gets the maximum number of game controllers supported by the underlying XInput API.</summary>
		int SupportedControllerCount { get; }


		/// <summary>Gets or sets a value indicating whether XInput is disabled; defaults to false.</summary>
		bool Suspended { get; set; }

		
		/// <summary>Gets an XInput controller given its index.</summary>
		/// <param name="index">The index of the desired XInput controller.
		/// <para>On Windows 10, the maximum controller index is <see cref="GameControllerIndex.Eight"/>, otherwise, it's <see cref="GameControllerIndex.Four"/>.</para>
		/// </param>
		/// <returns>Returns the XInput controller associated with the specified controller <paramref name="index"/>.</returns>
		IXInputController this[ GameControllerIndex index ] { get; }


		/// <summary>Updates the state of all connected XInput controllers.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		void Update( TimeSpan time );
		// TODO - move to IUpdateable ?

	}

}