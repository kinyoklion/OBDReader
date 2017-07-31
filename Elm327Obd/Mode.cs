namespace OBDReader.Obd2
{
    /// <summary>
    /// Enumeration of modes supported by OBD II.
    /// </summary>
    enum Mode
    {
        CurrentData = 0x01,
        FreezeFramData = 0x02,
        StoredDiagnosticTroubleCodes = 0x03,
        ClearDiagnosticTroubleCodes = 0x04,
        TestResultsNonCan = 0x05,
        TestResultsCan = 0x06,
        ShowPendingDiagnosticTroubleCodes = 0x07,
        ControlOperationOfOnBoardSystems = 0x08,
        RequestVehicleInformation = 0x09,
        PermanentDtcs = 0x0A
    }
}
