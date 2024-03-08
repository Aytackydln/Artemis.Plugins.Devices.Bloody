using Artemis.Core.DeviceProviders;
using Artemis.Core.Services;
using RGB.NET.Core;

namespace Artemis.Plugins.Devices.Bloody;

public class BloodyDeviceProvider : DeviceProvider
{
    private readonly IDeviceService _rgbService;

    public BloodyDeviceProvider(IDeviceService rgbService)
    {
        _rgbService = rgbService;
        CreateMissingLedsSupported = true;
        RemoveExcessiveLedsSupported = true;

        CanDetectLogicalLayout = false;
        CanDetectPhysicalLayout = false;
    }

    public override void Enable()
    {
        _rgbService.AddDeviceProvider(this);
    }

    public override void Disable()
    {
        _rgbService.RemoveDeviceProvider(this);
        RgbDeviceProvider.Dispose();
    }

    public override IRGBDeviceProvider RgbDeviceProvider => RGB.NET.Devices.Bloody.BloodyDeviceFactory.Instance;
}