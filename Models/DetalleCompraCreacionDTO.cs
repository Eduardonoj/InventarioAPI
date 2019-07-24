using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleCompraCreacionDTO
    {
        public int IdCompra { get; set; }
        public int CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public Double Precio { get; set; }
    }
}
