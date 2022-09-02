using System;
using RGB.NET.Core;
using RGB.NET.Devices.Bloody.Core;

namespace RGB.NET.Devices.Bloody
{
    public class BloodyUpdateQueue : UpdateQueue
    {
        private readonly BloodyDevice _bloodyDevice;

        public BloodyUpdateQueue(IDeviceUpdateTrigger updateTrigger, BloodyDevice bloodyPeripheral) : base(updateTrigger)
        {
            _bloodyDevice = bloodyPeripheral;
        }

        protected override void Update(in ReadOnlySpan<(object key, Color color)> dataSet)
        {
            foreach ((object key, Color color) item in dataSet)
            {
                _bloodyDevice.SetKeyColor((int)item.key, item.color.GetR(), item.color.GetG(), item.color.GetB());
            }

            _bloodyDevice.Update();
        }
    }
}