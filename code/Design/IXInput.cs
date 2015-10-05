using System;
using System.Collections.Generic;


namespace ManagedX.Input.Design
{
	using XInput;


	/// <summary>Suggests properties and methods to implement managed XInput support.</summary>
	public interface IXInput : IEnumerable<IXInputController>
	{

		/// <summary>Gets the version of the underlying XInput: 1.3, 1.4, etc.</summary>
		Version Version { get; }


		/// <summary>Gets the name of the XInput library (DLL): XInput1_3.dll, XInput1_4.dll, etc.
		/// <para>For debugging purpose.</para>
		/// </summary>
		string LibraryFileName { get; }


		/// <summary>Gets or sets a value indicating whether XInput is disabled; defaults to false.</summary>
		/// <remarks>Calls XInputEnable with the <code>enable</code> parameter set to the specified value.</remarks>
		bool Suspended { get; set; }


		/// <summary>Gets an XInput controller given its index.</summary>
		/// <param name="index">The index of the desired XInput controller.</param>
		/// <returns>Returns the XInput controller associated with the specified <see cref="GameControllerIndex">controller <paramref name="index"/></see>; can't be null.</returns>
		IXInputController this[ GameControllerIndex index ] { get; }


		/// <summary>Updates all registered controllers.</summary>
		/// <param name="time">The time elapsed since the application start.</param>
		void Update( TimeSpan time );
		// TODO - move to IUpdateable ?

	}

}