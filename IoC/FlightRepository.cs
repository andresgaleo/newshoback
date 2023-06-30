using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class FlightRepository //: IFlightRepository
    {
        public IEnumerable<Flight> GetFlights()
        {
            return new List<Flight>();
        }
        public Flight GetFlight()
        {
            return new Flight();
        }
        public IEnumerable<Journey> GetJourneys()
        {
            return new List<Journey>();
        }
        public Journey GetJourney() 
        {
            return new Journey();
        }


    }
}
