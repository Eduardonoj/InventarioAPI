using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class FacturaDTO
    {
        public int NumeroFactura { get; set; }
        public string Nit { get; set; }
        public ClienteDTO Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public Double Total { get; set; }
        public List<DetalleFacturaDTO> DetalleFacturas { get; set; }
    }
}
