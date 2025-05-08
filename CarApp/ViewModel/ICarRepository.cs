using System.Collections.Generic;
using CarApp.Model;

namespace CarApp.ViewModel
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAllCars();
        Car GetCar(string licensePlate);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(string licensePlate);
    }
}

