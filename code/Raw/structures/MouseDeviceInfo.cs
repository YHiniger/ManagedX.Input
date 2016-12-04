using System;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified mouse.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_MOUSE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645589%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO_MOUSE" )]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	internal struct MouseDeviceInfo : IEquatable<MouseDeviceInfo>
	{

		private int id;
		private int buttonCount;
		private int sampleRate;
		[MarshalAs( UnmanagedType.Bool )]
		private bool hasHorizontalWheel;



		/// <summary>Gets the identifier of the mouse device.</summary>
		public int Id { get { return id; } }


		/// <summary>Gets the number of buttons for the mouse.</summary>
		public int ButtonCount { get { return buttonCount; } }
		

		/// <summary>Gets the number of data points per second.
		/// <para>This information may not be applicable for every mouse device.</para>
		/// </summary>
		public int SampleRate { get { return sampleRate; } }


		/// <summary>Gets a value indicating whether the mouse has a wheel for horizontal scrolling.
		/// <para>This member is only supported starting with Windows Vista.</para>
		/// </summary>
		public bool HasHorizontalWheel { get { return hasHorizontalWheel; } }


		/// <summary>Returns a hash code for this <see cref="MouseDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MouseDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return id ^ buttonCount ^ sampleRate ^ ( hasHorizontalWheel ? -1 : 0 );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseDeviceInfo"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if this <see cref="MouseDeviceInfo"/> structure and the <paramref name="other"/> structure are equal, otherwise returns false.</returns>
		public bool Equals( MouseDeviceInfo other )
		{
			return
				( id == other.id ) &&
				( buttonCount == other.buttonCount ) &&
				( sampleRate == other.sampleRate ) &&
				( hasHorizontalWheel == other.hasHorizontalWheel );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MouseDeviceInfo"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MouseDeviceInfo ) && this.Equals( (MouseDeviceInfo)obj );
		}



		/// <summary>The empty <see cref="MouseDeviceInfo"/> structure.</summary>
		public static readonly MouseDeviceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="mouseInfo">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return mouseInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="mouseInfo">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return !mouseInfo.Equals( other );
		}

		#endregion Operators

	}

}