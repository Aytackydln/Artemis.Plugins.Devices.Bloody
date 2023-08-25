using System.Collections.Generic;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody;

public abstract class BloodyDeviceInfo : IRGBDeviceInfo
{
    public abstract RGBDeviceType DeviceType { get; }
    public string Manufacturer => "Bloody";
    public abstract string DeviceName { get; }
    public abstract string Model { get; }
    public abstract object LayoutMetadata { get; set; }
        
    internal abstract Dictionary<object, LedId> KeyMapping { get; }
}