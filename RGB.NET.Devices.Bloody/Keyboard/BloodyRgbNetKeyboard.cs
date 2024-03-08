using RGB.NET.Core;

namespace RGB.NET.Devices.Bloody.Keyboard;

public class BloodyRgbNetKeyboard : AbstractRGBDevice<BloodyKeyboardInfo>
{
    public BloodyRgbNetKeyboard(BloodyKeyboardInfo deviceInfo, IUpdateQueue updateQueue) : base(deviceInfo, updateQueue)
    {
        InitializeLayout();
    }

    private void InitializeLayout()
    {
        var x = 0;
        foreach (var key in KeyboardLedMap.LedMap.Keys)
        {
            AddLed(key, new Point(x, 0), new Size(19), key);
            x += 20;
        }
    }
}