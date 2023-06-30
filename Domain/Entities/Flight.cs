using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Flight
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        [ForeignKey("Transport")]
        public virtual Transport Transport { get; set; }
    }

    public class FlightFilter
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
