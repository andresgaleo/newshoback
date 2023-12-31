﻿using Application.Interfaces;
using Application.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using FlightData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FlightData;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class FlightServices : IFlightService
    {
        private readonly ILogger _logger;
        public bool isFindOk = false;
        public IFlightRepository _flightRepository { get; set; }
        public IWebserviceConfig _webserviceConfig { get; set; }
        List<Flight> TotalFlights2 = new List<Flight>();
        public FlightServices(IWebserviceConfig webserviceConfig, ILogger<FlightServices> logger) 
        {
            _webserviceConfig = webserviceConfig;
            _logger = logger;
        }
        public async Task<List<Flight>> GetFlights()
        {
            string url = "https://recruiting-api.newshore.es/api/flights/2";
            string data = await _webserviceConfig.GetDataFromUrl(url);
            List<dynamic> jsonData = JsonConvert.DeserializeObject<List<dynamic>>(data);
            List<Flight> flightsData = new List<Flight>();
            foreach(var json in jsonData)
            {
                Flight flightData = new Flight();
                flightData.Destination = json.arrivalStation;
                flightData.Origin = json.departureStation;
                flightData.Price = json.price;
                flightData.Transport = new Transport();
                flightData.Transport.FlightNumber = json.flightNumber;
                flightData.Transport.FlightCarrier = json.flightCarrier;
                flightsData.Add(flightData);
            }
            return flightsData;
        }
        public async Task<Journey> GetJourneys(string origin, string destination)
        {
            _logger.LogInformation("CONSULTA DEL SERVICIO: ",
            DateTime.UtcNow.ToLongTimeString());
            Journey jy = new Journey();
            //Obtengo todos lo vuelos
            List<Flight> listFlights = await this.GetFlights();
            List<Flight> TotalFlights = new List<Flight>();
            //Si se encuentra un vuelo directo
            List<Flight> listFlightsDef = listFlights
                .Where(m => m.Origin == origin && m.Destination == destination).ToList();
            if (listFlightsDef.Count == 0)
            {
                //Obtengo todos los vuelos programados por el origen
                List<Flight> listFlightsByOrigin = listFlights.
                Where(m => m.Origin == origin).ToList();
                FindFlightsForJourney(listFlights,origin,destination,listFlightsByOrigin);
            }
            else
            {
                isFindOk = true;
                TotalFlights2 = listFlightsDef;
            }

            double price = 0;
            jy.Origin = origin;
            jy.Destination = destination;
            if (isFindOk)
            {
                jy.Flight = TotalFlights2;
                foreach (var flights in TotalFlights2)
                {
                    price += flights.Price;
                }
            }
            else
            {
                jy.Flight = new List<Flight>();
            }
            jy.Price = price;

            return jy;
        }
        private void FindFlightsForJourney(List<Flight> listFlights, string origin, string destination, List<Flight> listFlightsByOrigin)
        {
            try
            {

                //Obtengo todos los vuelos programados como origen si tienen escala para encontrar el destino.
                foreach (var flightByOrigin in listFlightsByOrigin)
                {
                    List<Flight> listFlightsFinal = listFlights
                        .Where(m => m.Origin == flightByOrigin.Destination && m.Destination == destination).ToList();
                    if (listFlightsFinal.Count == 0)
                    {
                        List<Flight> listFlightsByDestination = listFlights
                        .Where(m => m.Origin == flightByOrigin.Destination && m.Destination != origin).ToList();

                        if(listFlightsByDestination.Count == 0)
                        {
                            isFindOk = false;
                            continue;
                        }
                        isFindOk = true;
                        TotalFlights2.Add(flightByOrigin);

                        origin = flightByOrigin.Destination;
                        listFlightsByOrigin = listFlightsByDestination;
                        FindFlightsForJourney(listFlights, origin,destination, listFlightsByOrigin);
                    }
                    else
                    {
                        isFindOk = true;
                        TotalFlights2.Add(flightByOrigin);
                        foreach (var flightByDestination in listFlightsFinal)
                        {
                            TotalFlights2.Add(flightByDestination);
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                message = message + " Strack trace " + ex.StackTrace;
                _logger.LogInformation("ERROR EN EL APLICATIVO: "+ message,
                DateTime.UtcNow.ToLongTimeString());
            }
            //return TotalFlights;
        }
        public async Task<double> SetCurrency(string currencyFrom, string currencyTo, double price)
        {
            string url = "https://api.api-ninjas.com/v1/convertcurrency?want="+currencyTo+"&have="+currencyFrom+"&amount="+price;
            string data = await _webserviceConfig.GetDataFromUrl(url);
            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(data);
            return Convert.ToDouble(jsonData.new_amount);
        }
    }
}
