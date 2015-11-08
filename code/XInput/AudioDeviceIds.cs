using System;
using System.Diagnostics.CodeAnalysis;


namespace ManagedX.Input.XInput
{
	
	/// <summary>Contains the Windows Core Audio device IDs of the headset rendering and capture devices.</summary>
	public struct AudioDeviceIds : IEquatable<AudioDeviceIds>
	{

		/// <summary>The Windows Core Audio device ID string for render (speakers); on Windows Vista and 7 (XInput 1.3), this is always null.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public string RenderDeviceId;

		/// <summary>The Windows Core Audio device ID string for capture (microphone); on Windows Vista and 7 (XInput 1.3), this is always null.</summary>
		[SuppressMessage( "Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields" )]
		public string CaptureDeviceId;

		

		/// <summary>Returns a hash code for this <see cref="AudioDeviceIds"/> structure.</summary>
		/// <returns>Returns a hash code for this <see cref="AudioDeviceIds"/> structure.</returns>
		public override int GetHashCode()
		{
			if( RenderDeviceId == null )
			{
				if( CaptureDeviceId == null )
					return 0;
				
				return CaptureDeviceId.GetHashCode();
			}

			if( CaptureDeviceId == null )
				return RenderDeviceId.GetHashCode();

			return RenderDeviceId.GetHashCode() ^ CaptureDeviceId.GetHashCode();
		}


		/// <summary>Returns a value indicating whether this <see cref="AudioDeviceIds"/> structure equals another structure of the same type.</summary>
		/// <param name="other">An <see cref="AudioDeviceIds"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public bool Equals( AudioDeviceIds other )
		{
			return ( this.RenderDeviceId == other.RenderDeviceId ) && ( this.CaptureDeviceId == other.CaptureDeviceId );
		}


		/// <summary>Returns a value indicating whether this <see cref="AudioDeviceIds"/> structure is equivalent to an object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>Returns true if the specified object is an <see cref="AudioDeviceIds"/> structure which equals this structure, otherwise returns false.</returns>
		public override bool Equals( object obj )
		{
			return ( obj is AudioDeviceIds ) && this.Equals( (AudioDeviceIds)obj );
		}



		/// <summary>The empty <see cref="AudioDeviceIds"/> structure.</summary>
		public static readonly AudioDeviceIds Empty;


		#region Operators

		/// <summary>Equality comparer.</summary>
		/// <param name="deviceIds">An <see cref="AudioDeviceIds"/> structure.</param>
		/// <param name="other">An <see cref="AudioDeviceIds"/> structure.</param>
		/// <returns>Returns true if the structures are equal, otherwise returns false.</returns>
		public static bool operator ==( AudioDeviceIds deviceIds, AudioDeviceIds other )
		{
			return deviceIds.Equals( other );
		}


		/// <summary>Inequality comparer.</summary>
		/// <param name="deviceIds">An <see cref="AudioDeviceIds"/> structure.</param>
		/// <param name="other">An <see cref="AudioDeviceIds"/> structure.</param>
		/// <returns>Returns true if the structures are not equal, otherwise returns false.</returns>
		public static bool operator !=( AudioDeviceIds deviceIds, AudioDeviceIds other )
		{
			return !deviceIds.Equals( other );
		}

		#endregion

	}

}