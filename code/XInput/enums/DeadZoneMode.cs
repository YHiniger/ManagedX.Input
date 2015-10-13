namespace ManagedX.Input.XInput
{
	
	/// <summary>Enumerates dead zone modes.</summary>
	public enum DeadZoneMode : int
	{

		/// <summary>No dead zone; this value is not recommended. (see https://msdn.microsoft.com/en-us/library/windows/desktop/ee417001%28v=vs.85%29.aspx#dead_zone ).</summary>
		None,
		
		/// <summary>Axis-dependent dead zone.</summary>
		Linear,
		
		/// <summary>Circular (radial?) dead zone.</summary>
		Circular

	}

}