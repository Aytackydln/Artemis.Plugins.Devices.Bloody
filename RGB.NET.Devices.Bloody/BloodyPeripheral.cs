﻿using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody;

public sealed class BloodyPeripheral : AbstractRGBDevice<BloodyDeviceInfo>
{
    private readonly BloodyDeviceInfo _deviceInfo;
        
    internal BloodyPeripheral(BloodyDeviceInfo deviceInfo, IUpdateQueue updateQueue)
        : base(deviceInfo, updateQueue)
    {
        _deviceInfo = deviceInfo;
        InitializeLayout();
    }

    private void InitializeLayout()
    {
        var x = 0;
        foreach (var key in _deviceInfo.KeyMapping.Keys)
        {
            if (!_deviceInfo.KeyMapping.TryGetValue(key, out var ledId))
            {
                continue;
            }

            AddLed(ledId, new Point(x, 0), new Size(19), key);
            x += 20;
        }
    }
}