using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace FlightTest
{
    public class FlightControllerTest : TestBuider
    {
        [Fact]
        public void ConsultarVueloExito()
        {
            var vuelo = new
            {
                origen= "MZL",
                destino="BCN"
            };
            var carga = this.Test.GetAsync("https://localhost:7059/Flights/find/one?Destination="+vuelo.destino+"&Origin="+vuelo.origen).Result;
            carga.EnsureSuccessStatusCode();
            Assert.True(true, "Rutas Consultadas Correctamente.");
        }
        [Fact]
        public void ConsultarVueloError()
        {
            var vuelo = new
            {
                origen = "MED",
                destino = "BCN"
            };
            var carga = this.Test.GetAsync("https://localhost:7059/Flights/find/one?Destination=" + vuelo.destino + "&Origin=" + vuelo.origen).Result;
            carga.EnsureSuccessStatusCode();
            Assert.True(false, "Ruta no existe.");
        }
        [Fact]
        public void ConvertirValorxMoneda()
        {
            string monedaNueva = "COP";
            double valorConvertir = 200;
            var carga = this.Test.GetAsync("https://localhost:7059/Flights/currency/USD/"+monedaNueva+"/"+valorConvertir).Result;
            carga.EnsureSuccessStatusCode();
            Assert.True(true, "Conversion hecha correctamente.s");
        }
    }
}
