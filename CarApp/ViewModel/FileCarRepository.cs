using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.ViewModel
{
    public class FileCarRepository : ICarRepository
    {
        private readonly string filePath = "cars.txt";

        public List<Car> GetAllCars()
        {
            if (!File.Exists(filePath))
                return new List<Car>();

            return File.ReadAllLines(filePath).Select(Car.FromString).ToList();
        }

        public Car GetCar(string licensePlate)
        {
            return GetAllCars().FirstOrDefault(c => c.LicensePlate == licensePlate);
        }

        public void AddCar(Car car)
        {
            File.AppendAllText(filePath, car + Environment.NewLine);
        }

        public void UpdateCar(Car car)
        {
            var cars = GetAllCars();
            var index = cars.FindIndex(c => c.LicensePlate == car.LicensePlate);
            if (index >= 0)
            {
                cars[index] = car;
                SaveAllCars(cars);
            }
        }

        public void DeleteCar(String licensePlate)
        {
            var cars = GetAllCars().Where(c => c.LicensePlate != licensePlate).ToList();
            SaveAllCars(cars);
        }

        private void SaveAllCars(List<Car> cars)
        {
            File.WriteAllLines(filePath, cars.Select(c => c.ToString()));
        }

    }
}
