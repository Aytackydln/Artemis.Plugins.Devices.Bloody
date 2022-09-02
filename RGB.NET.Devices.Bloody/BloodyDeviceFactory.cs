using System.Collections.Generic;
using System.Linq;
using HidSharp;
using RGB.NET.Core;
using RGB.NET.Devices.Bloody.Core;
using RGB.NET.Devices.Bloody.Mouse;
using RGB.NET.Devices.Bloody.Mousepad;

namespace RGB.NET.Devices.Bloody
{
    public class BloodyDeviceFactory : AbstractRGBDeviceProvider
    {
        private static BloodyDeviceFactory _instance;
        public static BloodyDeviceFactory Instance => _instance ??= new BloodyDeviceFactory();
        
        private const int VendorId = 0x09DA;

        private readonly Dictionary<PeripheralType, BloodyDeviceInfo> _deviceInfos = new()
            {
                {PeripheralType.Mouse, new BloodyMouseInfo()},
                {PeripheralType.Mousepad, new BloodyMousepadInfo()},
            };

        protected override void InitializeSDK()
        {
            // nothing
        }

        protected override IEnumerable<IRGBDevice> LoadDevices()
        {
            List<BloodyPeripheral> devices = new List<BloodyPeripheral>();
            foreach (int productId in BloodyConstants.DeviceIds.Keys)
            {
                if (Initialize(productId, out var dev))
                {
                    devices.Add(dev);
                }
            }

            return devices;
        }

        private bool Initialize(int productId, out BloodyPeripheral peripheral)
        {
            var devices = DeviceList.Local.GetHidDevices(VendorId, productId); //Find device with given VID PID

            try
            {
                HidDevice ctrlDevice = devices.First(d => d.GetMaxFeatureReportLength() > 50);

                HidStream ctrlStream = ctrlDevice.Open();
                PeripheralType type = BloodyConstants.DeviceIds.GetValueOrDefault(productId, PeripheralType.Unknown);
                BloodyDevice bd = new BloodyDevice(ctrlStream);
                BloodyPeripheral bp = new BloodyPeripheral(
                    _deviceInfos[type],
                    new BloodyUpdateQueue(GetUpdateTrigger(), bd)
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
    }
}