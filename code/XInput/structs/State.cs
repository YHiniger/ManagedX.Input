using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.XInput
{

	/// <summary>Contains the state of an XInput controller.
	/// <para>This structure is equivalent to the native <code>XINPUT_STATE</code> structure (defined in XInput.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/microsoft.directx_sdk.reference.xinput_state%28v=vs.85%29.aspx</remarks>
	[Win32.Native( "XInput.h", "XINPUT_STATE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct State : IEquatable<State>
	{

		/// <summary>The state packet number.
		/// <para>The packet number indicates whether there have been any changes in the state of the controller.</para>
		/// If the packet number [...] is the same in sequentially returned <see cref="State"/> structures, the controller state has not changed.
		/// </summary>
		public int PacketNumber;
		private Gamepad state;



		/// <summary>Gets an <see cref="Gamepad"/> structure containing the state of an XInput Controller.</summary>
		public Gamepad GamePadState { get { return state; } }


		/// <summary>Returns a hash code for this <see cref="State"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="State"/> structure.</returns>
		public override int GetHashCode()
		{
			return PacketNumber ^ state.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="State"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="State"/> structure.</param>
		/// <returns>Returns true if the <paramref name="other"/> structure equals this <see cref="State"/> structure, otherwise returns false.</returns>
		public bool Equals( State other )
		{
			return ( PacketNumber == other.PacketNumber ) || state.Equals( other.state );
		}


		/// <summary>Returns a value indicating whether this <see cref="State"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object; if null, it is replaced with the <see cref="Empty"/> structure.</param>
		/// <returns>Returns true if the specified object is a <see cref="State"/> structure equal to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is State ) && this.Equals( (State)obj );
		}


		/// <summary>The empty <see cref="State"/>.</summary>
		public static readonly State Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="state">A <see cref="State"/> structure.</param>
		/// <param name="other">A <see cref="State"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( State state, State other )
		{
			return state.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="state">A <see cref="State"/> structure.</param>
		/// <param name="other">A <see cref="State"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( State state, State other )
		{
			return !state.Equals( other );
		}

		#endregion Operators

	}

}