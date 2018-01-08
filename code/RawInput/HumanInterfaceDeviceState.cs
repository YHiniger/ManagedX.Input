using System;


namespace ManagedX.Input.Raw
{

	/// <summary></summary>
	public struct HumanInterfaceDeviceState : IInputDeviceState<int>, IEquatable<HumanInterfaceDeviceState>
	{

		internal byte[] data;



		internal HumanInterfaceDeviceState( byte[] data )
		{
			this.data = data ?? throw new ArgumentNullException( "data" );
		}



		/// <summary></summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			var hashCode = 0;
			for( var d = 0; d < data.Length / 4; d += 4 )
				hashCode ^= data[ d ] | ( data[ d + 1 ] << 8 ) | ( data[ d + 2 ] << 16 ) | ( data[ d + 3 ] << 24 );

			var temp = 0;
			for( var d = 0; d < data.Length % 4; ++d )
				temp = data[ d ] << ( 8 * d );

			return hashCode ^ temp;
		}


		/// <summary></summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool IsPressed( int button )
		{
			return false;
		}


		/// <summary></summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals( HumanInterfaceDeviceState other )
		{
			if( data == null )
				return other.data == null;

			if( other.data == null || data.Length != other.data.Length )
				return false;

			for( var d = 0; d < data.Length; ++d )
			{
				if( data[ d ] != other.data[ d ] )
					return false;
			}

			return true;
		}


		/// <summary></summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals( object obj )
		{
			return obj is HumanInterfaceDeviceState state && this.Equals( state );
		}

	}

}