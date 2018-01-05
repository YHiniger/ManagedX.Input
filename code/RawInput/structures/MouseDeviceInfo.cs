using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace ManagedX.Input.Raw
{

	/// <summary>Defines the raw input data coming from the specified mouse.
	/// <para>This structure is equivalent to the native <code>RID_DEVICE_INFO_MOUSE</code> structure (defined in WinUser.h).</para>
	/// </summary>
	/// <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms645589%28v=vs.85%29.aspx</remarks>
	[Win32.Source( "WinUser.h", "RID_DEVICE_INFO_MOUSE" )]
	[System.Diagnostics.DebuggerStepThrough]
	[StructLayout( LayoutKind.Sequential, Pack = 4, Size = 16 )]
	public struct MouseDeviceInfo : IEquatable<MouseDeviceInfo>
	{

		/// <summary>The identifier of the mouse device.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int Id;

		/// <summary>The number of buttons for the mouse.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int ButtonCount;

		/// <summary>The number of data points per second.
		/// <para>This information may not be applicable for every mouse device.</para>
		/// </summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public readonly int SampleRate;

		/// <summary>Indicates whether the mouse has a wheel for horizontal scrolling.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		[MarshalAs( UnmanagedType.Bool )]
		public readonly bool HasHorizontalWheel;



		/// <summary>Returns a hash code for this <see cref="MouseDeviceInfo"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="MouseDeviceInfo"/> structure.</returns>
		public override int GetHashCode()
		{
			return Id ^ ButtonCount ^ SampleRate ^ ( HasHorizontalWheel ? -1 : 0 );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseDeviceInfo"/> structure is equivalent to another <see cref="MouseDeviceInfo"/> structure.</summary>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the structures are equivalent, false otherwise.</returns>
		public bool Equals( MouseDeviceInfo other )
		{
			return
				( Id == other.Id ) &&
				( ButtonCount == other.ButtonCount ) &&
				( SampleRate == other.SampleRate ) &&
				( HasHorizontalWheel == other.HasHorizontalWheel );
		}


		/// <summary>Returns a value indicating whether this <see cref="MouseDeviceInfo"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="MouseDeviceInfo"/> structure equivalent to this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is MouseDeviceInfo info ) && this.Equals( info );
		}



		/// <summary>The empty <see cref="MouseDeviceInfo"/> structure.</summary>
		public static readonly MouseDeviceInfo Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="mouseInfo">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator ==( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return mouseInfo.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="mouseInfo">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <param name="other">A <see cref="MouseDeviceInfo"/> structure.</param>
		/// <returns>Returns true if the specified structures are not equivalent, false otherwise.</returns>
		[MethodImpl( MethodImplOptions.AggressiveInlining )]
		public static bool operator !=( MouseDeviceInfo mouseInfo, MouseDeviceInfo other )
		{
			return !mouseInfo.Equals( other );
		}

		#endregion Operators

	}

}