using System;


namespace ManagedX.Input.Raw
{

	/// <summary>A raw HID.
	/// <para>This class does not currently work properly.</para>
	/// </summary>
	public sealed class RawHumanInterfaceDevice : RawInputDevice<RawHID, int>
	{


		private HumanInterfaceDeviceInfo info;



		[System.Diagnostics.CodeAnalysis.SuppressMessage( "Microsoft.Design", "CA1045:DoNotPassTypesByReference" )]
		internal RawHumanInterfaceDevice( int controllerIndex, ref RawInputDeviceDescriptor descriptor )
			: base( controllerIndex, ref descriptor )
		{
			var time = TimeSpan.Zero;
			this.Reset( ref time );
		}



		#region Device info

		/// <summary>Gets the vendor identifier of the HID.</summary>
		public int VendorId { get { return info.VendorId; } }


		/// <summary>Gets the product identifier of the HID.</summary>
		public int ProductId { get { return info.ProductId; } }


		/// <summary>Gets the version number for the HID.</summary>
		public int Version { get { return info.VersionNumber; } }


		/// <summary>Gets the top-level collection (TLC usage page and usage) for the HID.</summary>
		public TopLevelCollectionUsage TopLevelCollection { get { return info.TopLevelCollection; } }

		#endregion Device info


		/// <summary>Retrieves the device state and returns it.
		/// <para>This method is called by Reset and Update.</para>
		/// </summary>
		/// <returns>Returns a <see cref="RawHID"/> structure representing the current state of the device.</returns>
		protected sealed override RawHID GetState()
		{
			return RawHID.Empty;
		}


		/// <summary>Returns a value indicating whether a button is pressed in the current state and released in the previous state.</summary>
		/// <param name="button">A button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is pressed in the current state and released in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenPressed( int button )
		{
			return false;
		}


		/// <summary>Returns a value indicating whether a button is released in the current state and pressed in the previous state.</summary>
		/// <param name="button">A button.</param>
		/// <returns>Returns true if the specified <paramref name="button"/> is released in the current state and pressed in the previous state, otherwise returns false.</returns>
		public sealed override bool HasJustBeenReleased( int button )
		{
			return false;
		}


		/// <summary>Resets the state and information about this <see cref="RawHumanInterfaceDevice"/>.</summary>
		/// <param name="time">The time elapsed since the start of the application.</param>
		protected sealed override void Reset( ref TimeSpan time )
		{
			base.Reset( ref time );

			var deviceInfo = base.Info.HumanInterfaceDeviceInfo;
			if( deviceInfo != null && deviceInfo.HasValue )
				info = deviceInfo.Value;
			else
				info = HumanInterfaceDeviceInfo.Empty;
		}

	}

}