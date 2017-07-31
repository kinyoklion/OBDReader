namespace OBDReader.Obd2
{
    /// <summary>
    /// Enumeration listing all of the available protocol versions.
    /// </summary>
    public enum Protocol
    {
        Automatic = 0,
        SaeJ1850Pwm,
        SaeJ1850Vpw,
        Iso9141V2,
        Iso14230V4Kwp,
        Iso14230V4KwpFastInit,
        Iso15765V4Can11Bit500Kbaud,
        Iso15765V4Can29Bit500Kbaud,
        Iso15765V4Can11Bit250Kbaud,
        Iso15765V4Can29Bit250Kbaud,
        SaeJ1939Can,
        User1Can,
        User2Can
    }
}
