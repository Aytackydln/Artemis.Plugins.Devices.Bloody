using Artemis.Core.DeviceProviders;
using Artemis.Core.Services;
using RGB.NET.Core;

namespace Artemis.Plugins.Devices.Bloody;

public class BloodyDeviceProvider : DeviceProvider
{
    private readonly IRgbService _rgbService;

    public BloodyDeviceProvider(IRgbService rgbService)
    {
        _rgbService = rgbService;
        CreateMissingLedsSupported = false;
        RemoveExcessiveLedsSupported = true;

        CanDetectLogicalLayout = false;
        CanDetectPhysicalLayout = false;
    }

    public override void Enable()
    {
        _rgbService.AddDeviceProvider(RgbDeviceProvider);
    }

    public override void Disable()
    {
        _rgbService.RemoveDeviceProvider(RgbDeviceProvider);
        RgbDeviceProvider.Dispose();
    }

    public override IRGBDeviceProvider RgbDeviceProvider => RGB.NET.Devices.Bloody.BloodyDeviceFactory.Instance;
}