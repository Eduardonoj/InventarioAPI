using System;
using System.Collections.Generic;

namespace InventarioAPI.Models
{
    public class CompraDTO
    {
        public int IdCompra { get; set; }
        public int NumeroDocumento { get; set; }
        public int CodigoProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Total { get; set; }
        public ProveedorDTO Proveedor { get; set; }
        public List<DetalleCompraDTO> DetalleCompras { get; set; }
    }
}
