using System;
using System.Collections.Generic;
using HidSharp;
using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Core;

public class PeripheralHidCaller(HidStream ctrlStream)
{
    private static readonly byte[] ColorPacketHeader = [0x07, 0x03, 0x06, 0x02, 0x00, 0x00, 0x00, 0x00];

    private readonly byte[] _keyColors = new byte[16 * 3];

    public void SetDirect()
    {
        byte[] a = [0x07, 0x03, 0x06, 0x01];
        byte[] b = [0x07, 0x03, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01];

        var packet = new byte[64];
        try
        {
            a.CopyTo(packet, 0);
            ctrlStream.SetFeature(packet);
            b.CopyTo(packet, 0);
            ctrlStream.SetFeature(packet);
        }
        catch
        {
            Disconnect();
        }
    }

    public void Disconnect()
    {
        ctrlStream?.Close();
    }

    public void SetKeyColor(int key, Color clr)
    {
        var offset = key * 3;
        _keyColors[offset + 0] = clr.GetR();
        _keyColors[offset + 1] = clr.GetG();
        _keyColors[offset + 2] = clr.GetB();
    }

    public void Update(Dictionary<int, Color> keyColors)
    {
        SetColors(keyColors);
        WriteColorBuffer();
    }

    private void SetColors(Dictionary<int, Color> keyColors)
    {
        foreach (var (key, color) in keyColors)
            SetKeyColor(key, color);
    }

    private void WriteColorBuffer()
    {
        var packet = new byte[64];
        try
        {
            ColorPacketHeader.CopyTo(packet, 0);
            Array.Copy(_keyColors, 0, packet, 8, 16 * 3);
            ctrlStream.SetFeature(packet);
        }
        catch
        {
            Disconnect();
        }
    }
}