using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.ViewModel
{
    public interface ICarRepository
    {
        List<Car> GetAllCars();
        Car? GetCar(string licensePlate);
        void AddCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(String LicePlate);

    }
}
