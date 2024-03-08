using System;
using Bloody.NET;
using RGB.NET.Core;
using RGB.NET.Devices.Bloody.Keyboard;

namespace RGB.NET.Devices.Bloody;

public class BloodyKeyboardUpdateQueue(IDeviceUpdateTrigger updateTrigger, BloodyKeyboard keyboard)
    : UpdateQueue(updateTrigger)
{
    protected override bool Update(in ReadOnlySpan<(object key, Color color)> dataSet)
    {
        foreach (var (key, color) in dataSet)
        {
            if (!KeyboardLedMap.LedMap.TryGetValue((LedId)key, out var bloodyKey)) continue;
            var clr = System.Drawing.Color.FromArgb(255, (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
            keyboard.SetKeyColor(bloodyKey, clr);
        }

        return keyboard.Update();
    }
}