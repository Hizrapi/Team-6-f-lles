using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CarApp.Model;

namespace CarApp.ViewModel
{
    public class FileCarRepository : ICarRepository
    {
        private readonly string filePath = "cars.txt";

        public IEnumerable<Car> GetAllCars()
        {
            if (!File.Exists(filePath))
                return new List<Car>();

            return File.ReadAllLines(filePath)
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new Car { LicensePlate = parts[0], Model = parts[1] };
                       })
                       .ToList();
        }

        // ✅ Tilføj GetCar metoden
        public Car GetCar(string licensePlate)
        {
            return GetAllCars().FirstOrDefault(c => c.LicensePlate == licensePlate);
        }

        public void AddCar(Car car)
        {
            File.AppendAllText(filePath, $"{car.LicensePlate},{car.Model}{Environment.NewLine}");
        }

        public void UpdateCar(Car car)
        {
            var cars = GetAllCars().ToList();
            var existingCar = cars.FirstOrDefault(c => c.LicensePlate == car.LicensePlate);
            if (existingCar != null)
            {
                existingCar.LicensePlate = car.LicensePlate;
                existingCar.Model = car.Model;
                SaveAllCars(cars);
            }
            
        }

        public void DeleteCar(string licensePlate)
        {
            var cars = GetAllCars().Where(c => c.LicensePlate != licensePlate).ToList();
            SaveAllCars(cars);
        }

        private void SaveAllCars(IEnumerable<Car> cars)
        {
            File.WriteAllLines(filePath, cars.Select(c => $"{c.LicensePlate},{c.Model}"));
        }
    }
}
