using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public string Nit { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; }
    }
}
