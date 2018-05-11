using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MedicalPhotonDevice
{
    public partial class PatientHealthDetail : ContentPage
    {
        public PatientHealthDetail(SensorsRecord sensorsRecord)
        {
			if (sensorsRecord == null)
                throw new ArgumentNullException();

            BindingContext = sensorsRecord;
            InitializeComponent();
        }
    }
}
