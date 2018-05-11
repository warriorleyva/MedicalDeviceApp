using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MedicalPhotonDevice
{
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage(Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException();

            BindingContext = patient;

            InitializeComponent();
        }
    }
}
