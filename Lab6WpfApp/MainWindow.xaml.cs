using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab6WpfApp
{
    class WeatherControl:DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;
        private string windDirection;
        public string WindSpeed { get; set; }
        enum precipitation
        {
            sunny=0,
            cloudy=1,
            rain=2,
            snow=3
        }
        public string WindDirection
        {
            get => windDirection;
            set => windDirection = value;
        }
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set=> SetValue(TemperatureProperty,value);
        }
        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(int),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                    new CoerceValueCallback(CoerceTemperature)),
                new ValidateValueCallback(ValidateTemperature));

        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if (v >= -50 && v <= 50)
                return true;
            else
                return false;
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v >= -50)
                return v;
            else
                return 0;
        }
        public WeatherControl(int temperature, string windDirection, string windSpeed)
        {
            this.Temperature = temperature;
            this.WindDirection = windDirection;
            this.WindSpeed = windSpeed;
        }
        public string Print()
        {
            return $"{Temperature} {WindDirection} {WindSpeed} {typeof(precipitation)}";
        }
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
