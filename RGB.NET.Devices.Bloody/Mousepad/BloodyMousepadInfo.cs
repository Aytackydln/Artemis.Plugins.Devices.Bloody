using System.Collections.Generic;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Mousepad;

public class BloodyMousepadInfo : BloodyDeviceInfo
{
    private static readonly Dictionary<object, LedId> Mapping = new()
    {
        [MousepadLed.L1] = LedId.Mousepad1,
        [MousepadLed.L2] = LedId.Mousepad2,
        [MousepadLed.L3] = LedId.Mousepad3,
        [MousepadLed.L4] = LedId.Mousepad4,
        [MousepadLed.L5] = LedId.Mousepad5,
        [MousepadLed.L6] = LedId.Mousepad6,
        [MousepadLed.L7] = LedId.Mousepad7,
        [MousepadLed.L8] = LedId.Mousepad8,
        [MousepadLed.L9] = LedId.Mousepad9,
        [MousepadLed.L10] = LedId.Mousepad10,
    };
        
    public override RGBDeviceType DeviceType => RGBDeviceType.Mousepad;
    public override string DeviceName => "Bloody Mousepad";
    public override string Model => "MP-60R";
    public override object LayoutMetadata { get; set; }
        
    internal override Dictionary<object, LedId> KeyMapping => Mapping;
}