using System;
using System.Collections.Generic;
using SQLite;
using SQLite.Extensions;
using MedicalPhotonDevice.Persistence;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace MedicalPhotonDevice
{
    [Table("Patients")]
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SensorsRecord> SensorsRecords { get; set; }
    }

    [Table("SensorsRecords")]
    public class SensorsRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Bmp { get; set; }
        public string Spo2 { get; set; }
        public string Temp { get; set; }

        [ForeignKey(typeof(Patient))]
        public int PatientId { get; set; }
    }

    public partial class PatientPage : ContentPage
    {
        public PatientPage()
        {
            InitializeComponent();

            var app = Application.Current as App;
            app.connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            //app.connection.CreateTableAsync<Patient>().Wait();
        }

        

        async void Aceptar_Clicked(object sender, System.EventArgs e)
        {
            var app = Application.Current as App;
            var patient = new Patient
            {
                Name = Nombre.Text,
                LastName = Apellido.Text,
                Gender = (string)Genero.SelectedItem,
                Birthday = Nacimiento.Date
            };

            await app.connection.InsertAsync(patient);
            app.patients.Add(patient);
        }

    }
}
