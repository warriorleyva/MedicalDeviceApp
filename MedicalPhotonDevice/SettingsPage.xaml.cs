using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicalPhotonDevice
{
    public partial class SettingsPage : ContentPage
    {
		private const string Url = "https://api.particle.io/v1/devices/2c0022001951353337343731";

        private HttpClient PhotonHttpClient = new HttpClient();

        private void FilterPercentPost()
        {
            var app = Application.Current as App;
            app.FilterValue = Filter_stepper.Value;

            var filterPorcent = Filter_stepper.Value.ToString();
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", filterPorcent),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/LP", content);
        }

        private void BmpOffsetPost()
        {
            var app = Application.Current as App;
            app.BmpValue = Bmp_stepper.Value;

            var bmpOffset = Bmp_stepper.Value.ToString();
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", bmpOffset),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/BMPO", content);
        }

        private void Spo2OffsetPost()
        {
            var app = Application.Current as App;
            app.Spo2Value = Spo2_stepper.Value;

            var spo2Offset = Spo2_stepper.Value.ToString();
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", spo2Offset),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/Spo2O", content);
        }

        private void TemperatureOffsetPost()
        {
            var app = Application.Current as App;
            app.TemperatureValue = Temp_stepper.Value;

            var temperatureOffset = ((Temp_stepper.Value) * 10).ToString();
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", temperatureOffset),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/TmpO", content);
        }

        public SettingsPage()
        {
            InitializeComponent();

            var app = Application.Current as App;
            IRCurrent_picker.SelectedIndex = app.IRCurrenValue;
            LedCurrent_picker.SelectedIndex = app.LedCurrenValue;
            Filter_stepper.Value = app.FilterValue;
            Bmp_stepper.Value = app.BmpValue;
            Spo2_stepper.Value = app.Spo2Value;
            Temp_stepper.Value = app.TemperatureValue;
        }

        protected override void OnDisappearing()
        {
            Application.Current.SavePropertiesAsync();
            base.OnDisappearing();
        }

        private void IRCurrent_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var app = Application.Current as App;
            app.IRCurrenValue = IRCurrent_picker.SelectedIndex;

            var irCurrentIndex = IRCurrent_picker.SelectedIndex.ToString();
            //DisplayAlert("Elegido", irCurrentIndex, "ok", "Cancel");
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", irCurrentIndex),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/IR", content);
        }

        private void LedCurrent_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var app = Application.Current as App;
            app.LedCurrenValue = LedCurrent_picker.SelectedIndex;

            var ledCurrentIndex = LedCurrent_picker.SelectedIndex.ToString();
            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("access_token", "0f4384bd65ba35fac75ae7ea740214655b485ea4"),
                new KeyValuePair<string, string>("args", ledCurrentIndex),

            };
            var content = new FormUrlEncodedContent(body);
            PhotonHttpClient.PostAsync(Url + "/Red", content);
        }

        private void Offset_OnClicked(object sender, EventArgs e)
        {
            FilterPercentPost();
            BmpOffsetPost();
            Spo2OffsetPost();
            TemperatureOffsetPost();
        }
    }
}
