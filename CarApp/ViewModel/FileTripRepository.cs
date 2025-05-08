using CarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarApp.ViewModel.Interface1;
using System.IO;

namespace CarApp.ViewModel
{
    public class FileTripRepository : ITripRepository
    {
        private readonly string filePath = "trips.txt";

        public IEnumerable<Trip> GetAllTrips()
        {
            if (!File.Exists(filePath))
                return new List<Trip>();

            return File.ReadAllLines(filePath)
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new Trip
                           {
                               CarRegNr = parts[0],
                               StartDate = DateTime.Parse(parts[1]),
                               EndDate = DateTime.Parse(parts[2]),
                               Distance = double.Parse(parts[3])
                           };
                       })
                       .ToList();
        }

        public void AddTrip(Trip trip)
        {
            File.AppendAllText(filePath, $"{trip.CarRegNr},{trip.StartDate},{trip.EndDate},{trip.Distance}{Environment.NewLine}");
        }
    }
}
