using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class FlightViewModel
    {
        public IEnumerable<Journey> Journeys { get; set; }
        public Journey Journey { get; set; }
        public IEnumerable<Flight> Flights { get; set; }
        public Flight flight { get; set; }
        public IEnumerable<Transport> Transports { get; set; }
        public Transport Transport { get; set; }
    }
}
