using CarApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.ViewModel
{
    public interface Interface1
    {
        public interface ITripRepository
        {
            IEnumerable<Trip> GetAllTrips();
            void AddTrip(Trip trip);
        }
    }
}
