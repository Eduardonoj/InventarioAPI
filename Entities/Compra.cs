using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public int NumeroDocumento { get; set; }
        public int CodigoProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Total { get; set; }
        public Proveedor Proveedor { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; }
    }
}
