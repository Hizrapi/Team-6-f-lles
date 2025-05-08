using System.ComponentModel;

namespace CarApp.Model
{
    public class Car : INotifyPropertyChanged
    {
        private string licensePlate;
        private string model;

        public string LicensePlate
        {
            get => licensePlate;
            set
            {
                if (licensePlate != value)
                {
                    licensePlate = value;
                    OnPropertyChanged(nameof(LicensePlate));
                }
            }
        }

        public string Model
        {
            get => model;
            set
            {
                if (model != value)
                {
                    model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }
        }

        // Implementerer INotifyPropertyChanged for UI-opdatering
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
