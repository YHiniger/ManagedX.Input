using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{

	// XInput.h
	// 


	/// <summary>Enumerates masks (XINPUT_KEYSTROKE_*) indicating the keyboard state at the time of the input event.</summary>
	[SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Undefined is the zero value." )]
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[Flags]
	public enum KeyStates : short
	{
		
		/// <summary>Undefined.</summary>
		Undefined = 0x0000,

		/// <summary>The key was pressed.</summary>
		Down = 0x0001,

		/// <summary>The key was released.</summary>
		Up = 0x0002,

		/// <summary>A repeat of a held key.</summary>
		Repeat = 0x0004

	}

}
