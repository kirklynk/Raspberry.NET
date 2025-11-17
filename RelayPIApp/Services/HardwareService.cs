using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Gpio;
using System.Net;
using System.Runtime.CompilerServices;

namespace RelayPIApp.Services
{
    public class HardwareService : INotifyPropertyChanged
    {
        private readonly GpioController? _gpioController;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Pin> Pins { get; }

        public HardwareService()
        {
            Pins = new ObservableCollection<Pin>
            {
                RelayPIApp.Services.Pins.Pin2,
                RelayPIApp.Services.Pins.Pin3,
                RelayPIApp.Services.Pins.Pin4,
                RelayPIApp.Services.Pins.Pin5,
                RelayPIApp.Services.Pins.Pin6,
                RelayPIApp.Services.Pins.Pin7,
                RelayPIApp.Services.Pins.Pin8,
                RelayPIApp.Services.Pins.Pin9,
                RelayPIApp.Services.Pins.Pin10,
                RelayPIApp.Services.Pins.Pin11,
                RelayPIApp.Services.Pins.Pin12,
                RelayPIApp.Services.Pins.Pin13,
                RelayPIApp.Services.Pins.Pin14,
                RelayPIApp.Services.Pins.Pin15,
                RelayPIApp.Services.Pins.Pin16,
                RelayPIApp.Services.Pins.Pin17,
                RelayPIApp.Services.Pins.Pin18,
                RelayPIApp.Services.Pins.Pin19,
                RelayPIApp.Services.Pins.Pin20,
                RelayPIApp.Services.Pins.Pin21,
                RelayPIApp.Services.Pins.Pin22,
                RelayPIApp.Services.Pins.Pin23,
                RelayPIApp.Services.Pins.Pin24,
                RelayPIApp.Services.Pins.Pin25,
                RelayPIApp.Services.Pins.Pin26,
                RelayPIApp.Services.Pins.Pin27
            };
            foreach (var pin in Pins)
            {
                pin.PropertyChanged += (sender, args) => OnPropertyChanged(nameof(pin));
            }

#if !DEBUG
            _gpioController = new GpioController();
           
            Console.WriteLine("HardwareService: GPIO pins initialized.");
#endif
            var pin4 = Pins.First(p => p.Number == 4);
            pin4.Name = "Relay 1";
            UpdatePinUsage(pin4, true); // Example: Set pin 4 as not in use initially
            var pin6 = Pins.First(p => p.Number == 6);
            pin6.Name = "Relay 2";
            UpdatePinUsage(pin6, true); // Example: Set pin 6 as not in use initially
            var pin22 = Pins.First(p => p.Number == 22);
            pin22.Name = "Relay 3";
            UpdatePinUsage(pin22, true); // Example: Set pin 22 as not in use initially
            var pin26 = Pins.First(p => p.Number == 26);
            pin26.Name = "Relay 4";
            UpdatePinUsage(pin26, true); // Example: Set pin 26 as not in use initially
        }

        public void UpdatePinUsage(Pin pin, bool inUse)
        {
            if (inUse)
            {
                pin.GpioPin = _gpioController?.OpenPin(pin.Number, pin.Mode == PinMode.Output ? PinMode.Output : PinMode.Input);
                pin.InUse = true;

            }
            else
            {
                if (pin.GpioPin != null)
                {
                    pin.GpioPin.Write(PinValue.Low);
                    pin.GpioPin.Close();
                    pin.GpioPin.Dispose();
                    pin.GpioPin = null;
                }
                pin.InUse = false;
            }
            OnPropertyChanged(nameof(pin));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public record Pin() : INotifyPropertyChanged
    {
        public int Order { get; internal set; } = 0;
        public int Number { get; init; }
        public PinMode Mode { get; init; } = PinMode.Output;
        public bool InUse { get; set; }

        public string? Name { get => field; set { field = value; OnPropertyChanged(); } }
        public bool State { get => GpioPin?.Read() == PinValue.High; set { GpioPin?.Write(value ? PinValue.High : PinValue.Low); OnPropertyChanged(); } }

        internal GpioPin? GpioPin { get; set; } = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Pins
    {
        public static readonly Pin Pin2 = new() { Order = 0, Number = 2, Mode = PinMode.Output, Name = "Gpio 2" };
        public static readonly Pin Pin3 = new() { Order = 1, Number = 3, Mode = PinMode.Output, Name = "Gpio 3" };
        public static readonly Pin Pin4 = new() {Order = 2, Number = 4, Mode = PinMode.Output, Name = "Gpio 4" };
        public static readonly Pin Pin5 = new() {Order = 3, Number = 5, Mode = PinMode.Output, Name = "Gpio 5" };
        public static readonly Pin Pin6 = new() {Order = 4, Number = 6, Mode = PinMode.Output, Name = "Gpio 6" };
        public static readonly Pin Pin7 = new() {Order = 5, Number = 7, Mode = PinMode.Output, Name = "Gpio 7" };
        public static readonly Pin Pin8 = new() {Order = 6, Number = 8, Mode = PinMode.Output, Name = "Gpio 8" };
        public static readonly Pin Pin9 = new() {Order = 7, Number = 9, Mode = PinMode.Output, Name = "Gpio 9" };
        public static readonly Pin Pin10 = new() {Order= 8, Number = 10, Mode = PinMode.Output, Name = "Gpio 10" };
        public static readonly Pin Pin11 = new() {Order= 9, Number = 11, Mode = PinMode.Output, Name = "Gpio 11" };
        public static readonly Pin Pin12 = new() {Order= 10, Number = 12, Mode = PinMode.Output, Name = "Gpio 12" };
        public static readonly Pin Pin13 = new() {Order= 11, Number = 13, Mode = PinMode.Output, Name = "Gpio 13" };
        public static readonly Pin Pin14 = new() {Order= 12, Number = 14, Mode = PinMode.Output, Name = "Gpio 14" };
        public static readonly Pin Pin15 = new() {Order= 13, Number = 15, Mode = PinMode.Output, Name = "Gpio 15" };
        public static readonly Pin Pin16 = new() {Order= 14, Number = 16, Mode = PinMode.Output, Name = "Gpio 16" };
        public static readonly Pin Pin17 = new() {Order= 15, Number = 17, Mode = PinMode.Output, Name = "Gpio 17" };
        public static readonly Pin Pin18 = new() {Order= 16, Number = 18, Mode = PinMode.Output, Name = "Gpio 18" };
        public static readonly Pin Pin19 = new() {Order= 17, Number = 19, Mode = PinMode.Output, Name = "Gpio 19" };
        public static readonly Pin Pin20 = new() {Order= 18, Number = 20, Mode = PinMode.Output, Name = "Gpio 20" };
        public static readonly Pin Pin21 = new() {Order= 19, Number = 21, Mode = PinMode.Output, Name = "Gpio 21" };
        public static readonly Pin Pin22 = new() {Order= 20, Number = 22, Mode = PinMode.Output, Name = "Gpio 22" };
        public static readonly Pin Pin23 = new() {Order= 21, Number = 23, Mode = PinMode.Output, Name = "Gpio 23" };
        public static readonly Pin Pin24 = new() {Order= 22, Number = 24, Mode = PinMode.Output, Name = "Gpio 24" };
        public static readonly Pin Pin25 = new() {Order= 23, Number = 25, Mode = PinMode.Output, Name = "Gpio 25" };
        public static readonly Pin Pin26 = new() {Order= 24, Number = 26, Mode = PinMode.Output, Name = "Gpio 26" };
        public static readonly Pin Pin27 = new() {Order= 25, Number = 27, Mode = PinMode.Output, Name = "Gpio 27" };
    }
}
