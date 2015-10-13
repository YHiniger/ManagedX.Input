using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	
	/// <summary>Contains the GUID of the headset sound rendering and capture devices.</summary>
	public struct DSoundAudioDeviceGuids : IEquatable<DSoundAudioDeviceGuids>
	{

		/// <summary>The <see cref="Guid"/> of the headset sound rendering device; on Windows 8 and greater, this is always an empty GUID.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Guid RenderDeviceGuid;

		/// <summary>The <see cref="Guid"/> of the headset sound capture device; on Windows 8 and greater, this is always an empty GUID.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public Guid CaptureDeviceGuid;


		/// <summary>Returns a hash code for this <see cref="DSoundAudioDeviceGuids"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="DSoundAudioDeviceGuids"/> structure.</returns>
		public override int GetHashCode()
		{
			return RenderDeviceGuid.GetHashCode() ^ CaptureDeviceGuid.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="DSoundAudioDeviceGuids"/> structure equals another structure of the same type.</summary>
		/// <param name="other">A <see cref="DSoundAudioDeviceGuids"/> structure.</param>
		/// <returns>Returns true if this <see cref="DSoundAudioDeviceGuids"/> structure equals the <paramref name="other"/> structure, otherwise returns false.</returns>
		public bool Equals( DSoundAudioDeviceGuids other )
		{
			return ( this.RenderDeviceGuid == other.RenderDeviceGuid ) && ( this.CaptureDeviceGuid == other.CaptureDeviceGuid );
		}

		/// <summary>Returns a value indicating whether this <see cref="DSoundAudioDeviceGuids"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is a <see cref="DSoundAudioDeviceGuids"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is DSoundAudioDeviceGuids ) && this.Equals( (DSoundAudioDeviceGuids)obj );
		}


		/// <summary>The empty <see cref="DSoundAudioDeviceGuids"/> structure.</summary>
		public static readonly DSoundAudioDeviceGuids Empty = new DSoundAudioDeviceGuids();


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="guids">A <see cref="DSoundAudioDeviceGuids"/> structure.</param>
		/// <param name="other">A <see cref="DSoundAudioDeviceGuids"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( DSoundAudioDeviceGuids guids, DSoundAudioDeviceGuids other )
		{
			return guids.Equals( other );
		}

		/// <summary>Inequality comparer.</summary>
		/// <param name="guids">A <see cref="DSoundAudioDeviceGuids"/> structure.</param>
		/// <param name="other">A <see cref="DSoundAudioDeviceGuids"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( DSoundAudioDeviceGuids guids, DSoundAudioDeviceGuids other )
		{
			return !guids.Equals( other );
		}

		#endregion

	}

}