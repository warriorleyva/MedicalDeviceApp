using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MedicalPhotonDevice
{
    public partial class PatientHealthHistory : ContentPage
	{
		private readonly int _patientId;

        public PatientHealthHistory(Patient patient)
		{
			if (patient == null)
                throw new ArgumentNullException();

			BindingContext = patient;
			_patientId = patient.Id;

            InitializeComponent();
        }

		protected override async void OnAppearing()
        {
			var app = Application.Current as App;
			var sensorsRecordList = await app.connection.Table<SensorsRecord>().Where(s => s.PatientId == _patientId).ToListAsync();
			app.sensorsRecords = new ObservableCollection<SensorsRecord>(sensorsRecordList);
			sensorsRecordsListView.ItemsSource = app.sensorsRecords;

			//var app = Application.Current as App;
			//var sensorsRecordList = await app.connection.Table<SensorsRecord>().ToListAsync();
			//app.sensorsRecords = new ObservableCollection<SensorsRecord>(sensorsRecordList);
			//sensorsRecordsListView.ItemsSource = app.sensorsRecords;
            base.OnAppearing();
        }

		async void SensorsRecord_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
                return;

            var sensorRecord = e.SelectedItem as SensorsRecord;

			await Navigation.PushAsync(new PatientHealthDetail(sensorRecord));
                sensorsRecordsListView.SelectedItem = null; 
		}

		async void SensorRecordAdd_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new PatientSensorsPage(_patientId));
		}
    }
}
