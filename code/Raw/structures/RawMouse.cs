using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	// https://msdn.microsoft.com/en-us/library/windows/desktop/ms645578%28v=vs.85%29.aspx


	/// <summary>Contains information about the state of the mouse.
	/// <para>The native name of this structure is RAWMOUSE.</para>
	/// </summary>
	[StructLayout( LayoutKind.Sequential, Size = 22 )]
	public struct RawMouse : IEquatable<RawMouse>
	{

		private RawMouseStates flags;
		private RawMouseButtonStates buttonFlags;
		private short buttonData;
		private int rawButtons;
		private int lastX;
		private int lastY;
		private int extraInformation;



		/// <summary>Gets the mouse state.</summary>
		public RawMouseStates State { get { return flags; } }


		/// <summary>Gets the transition state of the mouse buttons.</summary>
		public RawMouseButtonStates ButtonsState { get { return buttonFlags; } }
		

		/// <summary>If <see cref="buttonFlags"/> is <see cref="RawMouseButtonStates.Wheel"/>, gets a signed value indicating the wheel delta.</summary>
		public short WheelDelta { get { return buttonData; } }
		

		/// <summary>Gets the raw state of the mouse buttons.</summary>
		public int Buttons { get { return rawButtons; } }


		/// <summary>Gets the motion in the X direction.
		/// <para>This is signed relative motion or absolute motion, depending on the value of <see cref="State"/>.</para>
		/// </summary>
		public int LastX { get { return lastX; } }
		

		/// <summary>Gets the motion in the Y direction.
		/// <para>This is signed relative motion or absolute motion, depending on the value of <see cref="State"/>.</para>
		/// </summary>
		public int LastY { get { return lastY; } }
		

		/// <summary>Gets the device-specific additional information for the event.</summary>
		public int ExtraInformation { get { return extraInformation; } }


		/// <summary>Returns a hash code for this <see cref="RawMouse"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="RawMouse"/> structure.</returns>
		public override int GetHashCode()
		{
			return (int)flags ^ (int)buttonFlags ^ (int)buttonData ^ rawButtons ^ lastX ^ lastY ^ extraInformation;
		}


		/// <summary>Returns a value indicating whether this <see cref="RawMouse"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if this <see cref="RawMouse"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( RawMouse other )
		{
			return
				( flags == other.flags ) &&
				( buttonFlags == other.buttonFlags ) &&
				( buttonData == other.buttonData ) &&
				( rawButtons == other.rawButtons ) &&
				( lastX == other.lastX ) &&
				( lastY == other.lastY ) &&
				( extraInformation == other.extraInformation );
		}


		/// <summary>Returns a value indicating whether this <see cref="RawMouse"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="RawMouse"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is RawMouse ) && this.Equals( (RawMouse)obj );
		}

		
		/// <summary>The empty <see cref="RawMouse"/> structure.</summary>
		public static readonly RawMouse Empty = new RawMouse();


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="rawMouse">A <see cref="RawMouse"/> structure.</param>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( RawMouse rawMouse, RawMouse other )
		{
			return rawMouse.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="rawMouse">A <see cref="RawMouse"/> structure.</param>
		/// <param name="other">A <see cref="RawMouse"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( RawMouse rawMouse, RawMouse other )
		{
			return !rawMouse.Equals( other );
		}

		#endregion

	}

}