using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CarApp.Model;
using CarApp.Utilities;

namespace CarApp.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private readonly ICarRepository _repository;
        private string licensePlate;
        private string model;
        private Car selectedCar;

        public ObservableCollection<Car> Cars { get; set; }
        public ICommand AddCarCommand { get; }
        public ICommand EditCarCommand { get; }
        public ICommand DeleteCarCommand { get; }

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
            set
            {
                selectedCar = value;
                if (selectedCar != null)
                {
                    LicensePlate = selectedCar.LicensePlate;
                    Model = selectedCar.Model;
                }
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        public CarViewModel()
        {
            _repository = new FileCarRepository();
            Cars = new ObservableCollection<Car>(_repository.GetAllCars());
            AddCarCommand = new RelayCommand(AddCar);
            EditCarCommand = new RelayCommand(EditCar, () => SelectedCar != null);
            DeleteCarCommand = new RelayCommand(DeleteCar, () => SelectedCar != null);
        }

        private void AddCar()
        {
            var newCar = new Car { LicensePlate = LicensePlate, Model = Model };
            _repository.AddCar(newCar);
            Cars.Add(newCar);
            ClearFields();
        }

        private void EditCar()
        {
            if (SelectedCar == null) return;

            // Find den eksisterende bil i listen
            var existingCar = Cars.FirstOrDefault(c => c.LicensePlate == SelectedCar.LicensePlate);
            if (existingCar != null)
            {
                existingCar.LicensePlate = LicensePlate;
                existingCar.Model = Model;

                // Opdaterer i filen
                _repository.UpdateCar(existingCar);

                // Opdaterer listen (forcerer UI opdatering)
                int index = Cars.IndexOf(existingCar);
                Cars[index] = new Car { LicensePlate = existingCar.LicensePlate, Model = existingCar.Model };
                OnPropertyChanged(nameof(Cars));
            }

            // Rydder inputfelterne
            LicensePlate = string.Empty;
            Model = string.Empty;
            SelectedCar = null;
        }


        private void DeleteCar()
        {
            if (SelectedCar == null) return;
            _repository.DeleteCar(SelectedCar.LicensePlate);
            Cars.Remove(SelectedCar);
            ClearFields();
        }

        private void RefreshCars()
        {
            Cars.Clear();
            foreach (var car in _repository.GetAllCars())
                Cars.Add(car);
        }

        private void ClearFields()
        {
            LicensePlate = string.Empty;
            Model = string.Empty;
            SelectedCar = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
