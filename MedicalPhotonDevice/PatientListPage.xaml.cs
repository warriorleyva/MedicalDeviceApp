using System;
using SQLite;
using System.Collections.Generic;
using MedicalPhotonDevice.Persistence;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace MedicalPhotonDevice
{
    public partial class PatientListPage : ContentPage
    {
        public PatientListPage()
        {
            InitializeComponent();

            var app = Application.Current as App;
            app.connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            app.connection.CreateTableAsync<Patient>().Wait();
            app.connection.CreateTableAsync<SensorsRecord>().Wait();

        }

        protected override async void OnAppearing()
        {
            var app = Application.Current as App;
            var patientsList = await app.connection.Table<Patient>().ToListAsync();
            app.patients = new ObservableCollection<Patient>(patientsList);
            patientsListView.ItemsSource = app.patients;
            base.OnAppearing();
        }

        async void PersonAdd_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PatientPage());
        }

        async void Patient_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            
            var patient = e.SelectedItem as Patient;
            var response = await DisplayActionSheet("Opciones", "Cancelar", "Borrar", "Datos del Paciente", "Historial de Salud");
            
			if (response == "Datos del Paciente")
            {
                await Navigation.PushAsync(new PatientDetailPage(patient));
                patientsListView.SelectedItem = null;  
            }

			else if (response == "Historial de Salud")
            {
                await Navigation.PushAsync(new PatientHealthHistory(patient));
                patientsListView.SelectedItem = null; 
            }

            else if (response == "Borrar")
            {
                var alertResponse = await DisplayAlert("CUIDADO", "Está seguro que desea borrarlo?", "Sí", "No");
                if (alertResponse == true)
                {
                    var app = Application.Current as App;
                    await app.connection.DeleteAsync(patient);
                    app.patients.Remove(patient);
                }
                else
                {
                    patientsListView.SelectedItem = null;  
                }
            }

            else if (response == "Cancelar")
            {
                patientsListView.SelectedItem = null;  
            }
                
        }
    }
}
