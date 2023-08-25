using System;
using System.Collections.Generic;
using HidSharp;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Core;

public class BloodyDevice
{
    private readonly PeripheralCaller _caller;
    private readonly Dictionary<int, Color> _colors = new(16);

    internal BloodyDevice(HidStream ctrlStream)
    {
        _caller = new PeripheralCaller(ctrlStream);

        _caller.SetDirect();
    }

    void SetColor(byte r, byte g, byte b)
    {
        var color = new Color(r, g, b);
        foreach (var (key, value) in _colors)
        {
            _colors[key] = color;
        }
    }

    internal void SetKeyColor(int key, byte r, byte g, byte b)
    {
        _colors[key] = new Color(r, g, b);
    }

    public bool Update()
    {
        _caller.Update(_colors);
        return true;
    }
}