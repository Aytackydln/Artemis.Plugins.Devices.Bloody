using System.Collections.Generic;

namespace RGB.NET.Devices.Bloody.Core
{
    public static class BloodyConstants
    {
        internal static readonly Dictionary<int, PeripheralType> DeviceIds = new()
        {
            [0x37EA] = PeripheralType.Mouse,        //W60
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
}