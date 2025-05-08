using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CarApp.Model;
using System;
using CarApp.Utilities;

namespace CarApp.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private string licensePlate;
        private string model;
        private Car selectedCar;
        private Trip selectedTrip;
        private DateTime startDate = DateTime.Now;
        private DateTime endDate = DateTime.Now;
        private double distance;

        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<Trip> Trips { get; set; }

        public string LicensePlate
        {
            get => licensePlate;
            set { licensePlate = value; OnPropertyChanged(nameof(LicensePlate)); }
        }

        public string Model
        {
            get => model;
            set { model = value; OnPropertyChanged(nameof(Model)); }
        }

        public Car SelectedCar
        {
            get => selectedCar;
            set { selectedCar = value; OnPropertyChanged(nameof(SelectedCar)); LoadTrips(); }
        }

        public Trip SelectedTrip
        {
            get => selectedTrip;
            set { selectedTrip = value; OnPropertyChanged(nameof(SelectedTrip)); }
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

        public ICommand AddCarCommand { get; }
        public ICommand EditCarCommand { get; }
        public ICommand DeleteCarCommand { get; }
        public ICommand AddTripCommand { get; }
        public ICommand EditTripCommand { get; }
        public ICommand DeleteTripCommand { get; }

        public CarViewModel()
        {
            Cars = new ObservableCollection<Car>();
            Trips = new ObservableCollection<Trip>();

            AddCarCommand = new RelayCommand(AddCar);
            EditCarCommand = new RelayCommand(EditCar);
            DeleteCarCommand = new RelayCommand(DeleteCar);
            AddTripCommand = new RelayCommand(AddTrip);
            EditTripCommand = new RelayCommand(EditTrip);
            DeleteTripCommand = new RelayCommand(DeleteTrip);
        }

        private void AddCar()
        {
            if (string.IsNullOrWhiteSpace(LicensePlate) || string.IsNullOrWhiteSpace(Model)) return;

            Cars.Add(new Car { LicensePlate = LicensePlate, Model = Model });
            LicensePlate = string.Empty;
            Model = string.Empty;
        }

        private void EditCar()
        {
            if (SelectedCar == null) return;
            SelectedCar.LicensePlate = LicensePlate;
            SelectedCar.Model = Model;
        }

        private void DeleteCar()
        {
            if (SelectedCar == null) return;
            Cars.Remove(SelectedCar);
            Trips.Clear();
        }

        private void AddTrip()
        {
            if (SelectedCar == null) return;
            Trips.Add(new Trip
            {
                CarRegNr = SelectedCar.LicensePlate,
                StartDate = StartDate,
                EndDate = EndDate,
                Distance = Distance
            });
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Distance = 0;
        }

        private void EditTrip()
        {
            if (SelectedTrip == null) return;
            SelectedTrip.StartDate = StartDate;
            SelectedTrip.EndDate = EndDate;
            SelectedTrip.Distance = Distance;
        }

        private void DeleteTrip()
        {
            if (SelectedTrip == null) return;
            Trips.Remove(SelectedTrip);
        }

        private void LoadTrips()
        {
            Trips.Clear();
            if (SelectedCar == null) return;

            // Eksempel: Her kunne du hente trips fra en database eller fil
            Trips.Add(new Trip { CarRegNr = SelectedCar.LicensePlate, StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now, Distance = 50 });
            Trips.Add(new Trip { CarRegNr = SelectedCar.LicensePlate, StartDate = DateTime.Now.AddHours(-5), EndDate = DateTime.Now.AddHours(-3), Distance = 100 });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
