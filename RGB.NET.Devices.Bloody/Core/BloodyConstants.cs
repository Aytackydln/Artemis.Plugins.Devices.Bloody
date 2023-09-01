using System.Collections.Generic;

namespace RGB.NET.Devices.Bloody;

public static class BloodyConstants
{
    public const int VendorId = 0x09DA;
    
    internal static readonly Dictionary<int, PeripheralType> DeviceIds = new()
    {
        [0x37EA] = PeripheralType.Mouse,        //W60 - Pro
        [0x3666] = PeripheralType.Mouse,        //W60 - Max?
        [0x527A] = PeripheralType.Mouse,        //W60 - Max?
        [0xFA60] = PeripheralType.Mousepad,     //MP-50RS
        [0x356E] = PeripheralType.Mousepad,     //MP-50R
    };
}

public enum PeripheralType
{
    Mouse,
    Mousepad,
    Unknown,
}