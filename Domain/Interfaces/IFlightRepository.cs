using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFlightRepository
    {
        public Flight GetFlight();
        public List<Flight> GetFlights();
        public Journey GetJourney();
        public List<Journey> GetJourneys();
        public string SetCurrency();
    }
}
