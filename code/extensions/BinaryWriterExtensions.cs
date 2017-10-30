using System;
using System.IO;


namespace ManagedX.Input.XInput
{

	/// <summary>Provides an extension method to write a <see cref="Vibration"/> structure to a stream using a <see cref="BinaryWriter"/>.</summary>
	public static class BinaryWriterExtensions
	{

		/// <summary>Writes a <see cref="Vibration"/> structure to a stream.</summary>
		/// <param name="writer">The writer; must not be null.</param>
		/// <param name="vibration">A <see cref="Vibration"/> structure.</param>
		public static void Write( this BinaryWriter writer, Vibration vibration )
		{
			if( writer == null )
				throw new ArgumentNullException( "writer" );

			writer.Write( vibration.leftMotorSpeed );
			writer.Write( vibration.rightMotorSpeed );
		}

	}

}