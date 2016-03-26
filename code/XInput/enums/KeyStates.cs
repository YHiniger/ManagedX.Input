using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	using Win32;


	/// <summary>Enumerates masks indicating the keyboard state at the time of the input event.
	/// <para>This enumeration is equivalent to the native <code>XINPUT_KEYSTROKE_*</code> constants (defined in XInput.h).</para>
	/// </summary>
	/// <remarks></remarks>
	[SuppressMessage( "Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Undefined is the zero value." )]
	[SuppressMessage( "Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "Required by implementation." )]
	[Flags]
	public enum KeyStates : short
	{
		
		/// <summary>Undefined.</summary>
		Undefined = 0x0000,

		/// <summary>The key was pressed.</summary>
		[Native( "XInput.h", "XINPUT_KEYSTROKE_KEYDOWN" )]
		Down = 0x0001,

		/// <summary>The key was released.</summary>
		[Native( "XInput.h", "XINPUT_KEYSTROKE_KEYUP" )]
		Up = 0x0002,

		/// <summary>A repeat of a held key.</summary>
		[Native( "XInput.h", "XINPUT_KEYSTROKE_REPEAT" )]
		Repeat = 0x0004

	}

}