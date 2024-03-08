using System;
using RGB.NET.Core;
using RGB.NET.Devices.Bloody.Core;

namespace RGB.NET.Devices.Bloody;

public class BloodyMouseUpdateQueue(IDeviceUpdateTrigger updateTrigger, BloodyMouseDevice bloodyMousePeripheral) : UpdateQueue(updateTrigger)
{
    protected override bool Update(in ReadOnlySpan<(object key, Color color)> dataSet)
    {
        foreach (var item in dataSet)
        {
            bloodyMousePeripheral.SetKeyColor((int)item.key, item.color.GetR(), item.color.GetG(), item.color.GetB());
        }

        return bloodyMousePeripheral.Update();
    }
}