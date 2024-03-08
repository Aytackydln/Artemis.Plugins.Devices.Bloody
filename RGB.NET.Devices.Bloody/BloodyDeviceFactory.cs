using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Bloody.NET;
using HidSharp;
using RGB.NET.Core;
using RGB.NET.Devices.Bloody.Core;
using RGB.NET.Devices.Bloody.Keyboard;
using RGB.NET.Devices.Bloody.Mouse;
using RGB.NET.Devices.Bloody.Mousepad;

namespace RGB.NET.Devices.Bloody;

public class BloodyDeviceFactory : AbstractRGBDeviceProvider
{
    private static Lazy<BloodyDeviceFactory> _lazyInstance = new(() => new BloodyDeviceFactory(),LazyThreadSafetyMode.ExecutionAndPublication);
    public static BloodyDeviceFactory Instance => _lazyInstance.Value;

    private BloodyDeviceFactory() : base(0.2)
    {
    }

    private readonly Dictionary<PeripheralType, BloodyDeviceInfo> _deviceInfos = new()
    {
        { PeripheralType.Mouse, new BloodyMouseInfo() },
        { PeripheralType.Mousepad, new BloodyMousepadInfo() },
    };

    protected override void InitializeSDK()
    {
        // nothing
    }

    protected override IEnumerable<IRGBDevice> LoadDevices()
    {
        foreach (var productId in BloodyConstants.DeviceIds.Keys)
        {
            if (Initialize(productId, out var dev))
            {
                yield return dev;
            }
        }
        var keyboard = BloodyKeyboard.Initialize();
        if (keyboard != null)
        {
            yield return new BloodyRgbNetKeyboard(new BloodyKeyboardInfo(), new BloodyKeyboardUpdateQueue(GetUpdateTrigger(), keyboard));
        }
    }

    private bool Initialize(int productId, out BloodyPeripheral peripheral)
    {
        var devices =
            DeviceList.Local.GetHidDevices(BloodyConstants.VendorId, productId); //Find device with given VID PID

        try
        {
            var ctrlDevice = devices.First(d => d.GetMaxFeatureReportLength() > 50);

            var ctrlStream = ctrlDevice.Open();
            var type = BloodyConstants.DeviceIds.GetValueOrDefault(productId, PeripheralType.Unknown);
            var bd = new BloodyMouseDevice(ctrlStream);
            var bp = new BloodyPeripheral(
                _deviceInfos[type],
                new BloodyMouseUpdateQueue(GetUpdateTrigger(), bd)
            );
            peripheral = bp;
            return true;
        }
        catch
        {
            peripheral = null;
            return false;
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _lazyInstance = new(() => new BloodyDeviceFactory(),LazyThreadSafetyMode.ExecutionAndPublication);
    }
}