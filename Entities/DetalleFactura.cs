using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class DetalleFactura
    {
        public int CodigoDetalle { get; set; }
        public int NumeroFactura { get; set; }
        public Factura Factura { get; set; }
        public int CodigoProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Descuento { get; set; }

    }
}
