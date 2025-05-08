using System;
using System.ComponentModel;

namespace CarApp.Model
{
    public class Trip : INotifyPropertyChanged
    {
        private DateTime startDate;
        private DateTime endDate;
        private double distance;
        private string carRegNr;

        public string CarRegNr
        {
            get => carRegNr;
            set { carRegNr = value; OnPropertyChanged(nameof(CarRegNr)); }
        }

        public DateTime StartDate
        {
            get => startDate;
            set { startDate = value; OnPropertyChanged(nameof(StartDate)); }
        }

        public DateTime EndDate
        {
            get => endDate;
            set { endDate = value; OnPropertyChanged(nameof(EndDate)); }
        }

        public double Distance
        {
            get => distance;
            set { distance = value; OnPropertyChanged(nameof(Distance)); }
        }

        // Implementerer INotifyPropertyChanged for UI-opdatering
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
