using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Keyboard;

public class BloodyKeyboardInfo : IRGBDeviceInfo
{
    public string Manufacturer => "Bloody";
    public RGBDeviceType DeviceType => RGBDeviceType.Keyboard;
    public string DeviceName => "Bloody Keyboard";
    public string Model => "Unknown";
    public object LayoutMetadata { get; set; }
}