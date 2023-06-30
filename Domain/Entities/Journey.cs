using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Journey
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        [ForeignKey("flight")]
        public virtual List<Flight> Flight { get; set; }
    }
}
