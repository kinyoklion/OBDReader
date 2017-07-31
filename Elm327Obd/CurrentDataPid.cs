namespace OBDReader.Obd2
{
    /// <summary>
    /// List of PIDs for the "Current Data" mode.
    /// </summary>
    enum CurrentDataPid
    {
        SupportedPids1To20 = 0x00,
        SupportedPids21To40 = 0x20,
        SupportedPids41To60 = 0x40,
        SupportedPids61To80 = 0x60,
        SupportedPids81ToA0 = 0x80,
        DistanceSingeCodesCleared = 0x31,
        VehicleSpeed = 0x0D
    }

}
