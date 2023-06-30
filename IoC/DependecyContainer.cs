using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces;
using Application.Services;
using Application.Interfaces;
using FlightData.Interfaces;
using FlightData.Configuration;

namespace IoC
{
    public class DependecyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightServices>();
            services.AddScoped<IWebserviceConfig, WebserviceConfig>();
        }
    }
}
