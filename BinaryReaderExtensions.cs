using System;
using System.IO;


namespace ManagedX.Input.XInput
{

	/// <summary>Provides an extension method to read a <see cref="Vibration"/> structure from a stream using a <see cref="BinaryReader"/>.</summary>
	public static class BinaryReaderExtensions
	{

		/// <summary>Reads a <see cref="Vibration"/> structure from a stream and returns it.</summary>
		/// <param name="reader">A reader; must not be null.</param>
		/// <returns>Returns the read <see cref="Vibration"/> structure.</returns>
		public static Vibration ReadVibration( this BinaryReader reader )
		{
			if( reader == null )
				throw new ArgumentNullException( "reader" );

			Vibration vibration;
			vibration.leftMotorSpeed = reader.ReadUInt16();
			vibration.rightMotorSpeed = reader.ReadUInt16();
			return vibration;
		}

	}

}