using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFlightService
    {
        //Flight GetFlight();
        Task<List<Flight>> GetFlights();
        Journey GetJourney();
        Task<Journey> GetJourneys(string origin, string destination);
        string SetCurrency();
    }
}
