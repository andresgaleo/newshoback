using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;

namespace ApiFlights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightServices;
        public FlightsController(IFlightService flightServices) 
        { 
            _flightServices = flightServices;
        }
        [HttpGet("find/all")]
        public async Task<IActionResult> GetFlights()
        {
            List<Flight> data = await _flightServices.GetFlights();
            return Ok(data);
        }
        [HttpPost("find/one")]
        public async Task<IActionResult> GetJourneys(FlightFilter flight)
        {
            Journey data = await _flightServices.GetJourneys(flight.Origin, flight.Destination);
            if(data.Price == 0)
            {
                return Ok(new { journey = "Ruta no encontrada" });
            }
            return Ok(new { journey = data });
            
        }
        [HttpGet("currency/{currencyFrom}/{currencyTo}/{price}")]
        public async Task<IActionResult> GetCurrencyValue(string currencyFrom, string currencyTo,double price)
        {
            double newPrice = await _flightServices.SetCurrency(currencyFrom, currencyTo, price);
            return Ok(new { price = newPrice });
        }
    }
}

//Metodos compuestos, principios lab, ley de conway, principios graph
//Patrones diseño: Creacionales, comportamiento estrucutrales