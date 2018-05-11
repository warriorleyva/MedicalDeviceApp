using Xamarin.Forms;
using SQLite;
using System;
using System.Collections.ObjectModel;

namespace MedicalPhotonDevice
{
    public partial class App : Application
    {
        public SQLiteAsyncConnection connection;
        public ObservableCollection<Patient> patients;
        public ObservableCollection<SensorsRecord> sensorsRecords;

        private const string IRCurrenValueKey = "IRCurrenValue";
        private const string LedCurrentValueKey = "LedCurrentValue";
        private const string FilterValueKey = "FilterValue";
        private const string BmpValueKey = "BmpValue";
        private const string Spo2ValueKey = "Spo2Value";
        private const string TemperatureValueKey = "TemperatureValue";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#00695C")
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public int IRCurrenValue
        {
            get
            {
                if (Properties.ContainsKey(IRCurrenValueKey))
                {
                    return (int)Properties[IRCurrenValueKey];
                }
                else
                {
                    return -1;
                }
            }
            set { Properties[IRCurrenValueKey] = value; }
        }

        public int LedCurrenValue
        {
            get
            {
                if (Properties.ContainsKey(LedCurrentValueKey))
                {
                    return (int)Properties[LedCurrentValueKey];
                }
                else
                {
                    return -1;
                }
            }
            set { Properties[LedCurrentValueKey] = value; }
        }

        public double FilterValue
        {
            get
            {
                if (Properties.ContainsKey(FilterValueKey))
                {
                    return (double)Properties[FilterValueKey];
                }
                else
                {
                    return 80;
                }
            }
            set { Properties[FilterValueKey] = value; }
        }

        public double BmpValue
        {
            get
            {
                if (Properties.ContainsKey(BmpValueKey))
                {
                    return (double)Properties[BmpValueKey];
                }
                else
                {
                    return 0;
                }
            }
            set { Properties[BmpValueKey] = value; }
        }

        public double Spo2Value
        {
            get
            {
                if (Properties.ContainsKey(Spo2ValueKey))
                {
                    return (double)Properties[Spo2ValueKey];
                }
                else
                {
                    return 0;
                }
            }
            set { Properties[Spo2ValueKey] = value; }
        }

        public double TemperatureValue
        {
            get
            {
                if (Properties.ContainsKey(TemperatureValueKey))
                {
                    return (double)Properties[TemperatureValueKey];
                }
                else
                {
                    return 0;
                }
            }
            set { Properties[TemperatureValueKey] = value; }
        }
    }
}
