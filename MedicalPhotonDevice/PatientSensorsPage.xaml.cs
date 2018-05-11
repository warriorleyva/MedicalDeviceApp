using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;


using Xamarin.Forms;

namespace MedicalPhotonDevice
{
    public partial class PatientSensorsPage : ContentPage
    {
        private readonly int _patientId;

        private string bmpValue;
        private string spo2Value;
        private string tempValue;

        private const string Url = "https://api.particle.io/v1/devices/4b0047000751353530373132";

        private HttpClient PhotonHttpClient = new HttpClient();

        private async void GetBMP()
        {
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
            };
            var content = new FormUrlEncodedContent(body);
            var response = await PhotonHttpClient.PostAsync(Url + "/BPM", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var photonValues = JsonConvert.DeserializeObject<BMP>(responseContent);
            BMP_label.Text = String.Format("{0} BMP", photonValues.ReturnValue);
            bmpValue = BMP_label.Text;
        }

        private async void GetSPO2()
        {
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
            };
            var content = new FormUrlEncodedContent(body);
            var response = await PhotonHttpClient.PostAsync(Url + "/Spo2", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var photonValues = JsonConvert.DeserializeObject<SPO2>(responseContent);
            SPO2_label.Text = String.Format("{0} %", photonValues.ReturnValue);
            spo2Value = SPO2_label.Text;
        }

        private async void GetTemp()
        {
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
            };
            var content = new FormUrlEncodedContent(body);
            var response = await PhotonHttpClient.PostAsync(Url + "/Tmp", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var photonValues = JsonConvert.DeserializeObject<Temperature>(responseContent);
            Temp_label.Text = String.Format("{0:F1} °C", (photonValues.ReturnValue) / 10.0);
            tempValue = Temp_label.Text;
        }
        
         

        public PatientSensorsPage(int patientId)
        {
            //if (patient == null)
            //  throw new ArgumentNullException();

            //BindingContext = patient;

            _patientId = patientId;

            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await ShowValuesAsync();
            base.OnAppearing();
        }

        private async Task ShowValuesAsync()
        {
            while (true)
            {
                await Task.Delay(1000);
                GetBMP();
                GetSPO2();
                GetTemp();
            }
        }

        async void Accept_Clicked(object sender, System.EventArgs e)
        {
            var app = Application.Current as App;
            
            var sensorsRecord = new SensorsRecord
            {
                Bmp = bmpValue,
                Spo2 = spo2Value,
                Temp = tempValue,
                PatientId = _patientId
            };
            
            await app.connection.InsertAsync(sensorsRecord);
        }
    }
}
