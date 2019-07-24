using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class DetalleCompra
    {
        public int IdDetalle { get; set; }
        public int IdCompra { get; set; }
        public Compra Compra { get; set; }
        public int CodigoProducto { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set;}
        public Decimal Precio { get; set; }

    }
}
