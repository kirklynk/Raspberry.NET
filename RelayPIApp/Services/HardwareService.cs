using System.Device.Gpio;

namespace RelayPIApp.Services
{
    public class HardwareService
    {
        private readonly GpioController? _gpioController;
        private readonly GpioPin? _relay6;
        private readonly GpioPin? _relay4;
        private readonly GpioPin? _relay22;
        private readonly GpioPin? _relay26;

        public bool Relay1 { get => _relay4?.Read() == PinValue.High; set { _relay4?.Write(value ? PinValue.High : PinValue.Low); Console.WriteLine($"Relay 1 clicked"); } }
        public bool Relay2 { get => _relay6?.Read() == PinValue.High; set { _relay6?.Write(value ? PinValue.High : PinValue.Low); Console.WriteLine($"Relay 2 clicked"); } }
        public bool Relay3 { get => _relay22?.Read() == PinValue.High; set { _relay22?.Write(value ? PinValue.High : PinValue.Low); Console.WriteLine($"Relay 3 clicked"); } }
        public bool Relay4 { get => _relay26?.Read() == PinValue.High; set { _relay26?.Write(value ? PinValue.High : PinValue.Low); Console.WriteLine($"Relay 4 clicked"); } }

        public HardwareService()
        {
#if !DEBUG
                _gpioController = new GpioController();
                _relay6 = _gpioController.OpenPin(6);
                _relay4 = _gpioController.OpenPin(4);
                _relay22 = _gpioController.OpenPin(22);
                _relay26 = _gpioController.OpenPin(26);
#endif
        }
    }
}
