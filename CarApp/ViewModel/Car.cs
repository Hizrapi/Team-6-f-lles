using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApp.ViewModel
{
   
    
    
        public class Car : INotifyPropertyChanged
        {
            private readonly ICarRepository _repository;    

            // Felter til at gemme input fra tekstfelterne
            private string licensePlate;
            private string model;

            // Property til registreringsnummer
            public string LicensePlate
            {
                get => licensePlate;
                set
                {
                    licensePlate = value;
                    OnPropertyChanged(nameof(LicensePlate));
                }
            }

            // Property til bilens modelnavn
            public string Model
            {
                get => model;
                set
                {
                    model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }

            // Liste over tilføjede biler
            public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();

            // Kommando til at tilføje bil via UI-knap
            public ICommand AddCarCommand { get; }

            // Constructor, hvor vi sætter hvad AddCarCommand skal gøre
            public Car(ICarRepository repository)
            {
                _repository = repository;
                Cars = new ObservableCollection<Car>(_repository.GetAllCars());
                AddCarCommand = new RelayCommand(AddCar, CanAddCar);
            }

            // Den metode der bliver kaldt når brugeren trykker på "Tilføj bil"-knappen
            private void AddCar()
            {
                var newCar = new Car
                {
                    LicensePlate = LicensePlate,
                    Model = Model
                };

                _repository.AddCar(newCar);
                Cars.Add(newCar);
                
                LicensePlate = string.Empty;
                Model = string.Empty;
            }

            // Afgør om knappen må være aktiv – begge felter skal være udfyldt
            private bool CanAddCar()
            {
                return !string.IsNullOrWhiteSpace(LicensePlate) && !string.IsNullOrWhiteSpace(Model);
            }

            // Event til at informere UI om ændringer
            public event PropertyChangedEventHandler PropertyChanged;

            // Hjælper-metode der kaldes når en property ændres
            private void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

}
