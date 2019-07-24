using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleCompraDTO
    {
        public int IdDetalle { get; set; }
        public int IdCompra { get; set; }
        public CompraDTO Compra { get; set; }
        public int CodigoProducto { get; set; }
        public ProductoDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public Double Precio { get; set; }

    }
}
