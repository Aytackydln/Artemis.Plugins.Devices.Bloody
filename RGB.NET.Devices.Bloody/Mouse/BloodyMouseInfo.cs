using System.Collections.Generic;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Mouse;

public class BloodyMouseInfo : BloodyDeviceInfo
{
    private static readonly Dictionary<object, LedId> Mapping = new()
    {
        [MouseLeds.L1] = LedId.Mouse1,
        [MouseLeds.L2] = LedId.Mouse2,
        [MouseLeds.L3] = LedId.Mouse3,
        [MouseLeds.L4] = LedId.Mouse4,
        [MouseLeds.L5] = LedId.Mouse5,
        [MouseLeds.L6] = LedId.Mouse6,
        [MouseLeds.L7] = LedId.Mouse7,
        [MouseLeds.L8] = LedId.Mouse8,
        [MouseLeds.L9] = LedId.Mouse9,
        [MouseLeds.L10] = LedId.Mouse10,
        [MouseLeds.L12] = LedId.Mouse12,    //scroll
        [MouseLeds.Logo] = LedId.Mouse11,
        [MouseLeds.L11] = LedId.Logo,
        [MouseLeds.L13] = LedId.Mouse13,
        [MouseLeds.L14] = LedId.Mouse14,
        [MouseLeds.L15] = LedId.Mouse14,
    };
        
    public override RGBDeviceType DeviceType => RGBDeviceType.Mouse;
    public override string DeviceName => "Bloody Mouse";
    public override string Model => "W60";
    public override object LayoutMetadata { get; set; }

    internal override Dictionary<object, LedId> KeyMapping => Mapping;
}