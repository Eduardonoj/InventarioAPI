using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class FacturaCreacionDTO
    {
        public string Nit { get; set; }
        public DateTime Fecha { get; set; }
        public Double Total { get; set; }
    }
}
