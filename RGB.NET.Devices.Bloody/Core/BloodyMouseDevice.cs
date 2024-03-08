using System.Collections.Generic;
using HidSharp;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Core;

public class BloodyMouseDevice
{
    private readonly PeripheralHidCaller _hidCaller;
    private readonly Dictionary<int, Color> _colors = new(16);

    internal BloodyMouseDevice(HidStream ctrlStream)
    {
        _hidCaller = new PeripheralHidCaller(ctrlStream);

        _hidCaller.SetDirect();
    }

    internal void SetKeyColor(int key, byte r, byte g, byte b)
    {
        _colors[key] = new Color(r, g, b);
    }

    public bool Update()
    {
        _hidCaller.Update(_colors);
        return true;
    }
}