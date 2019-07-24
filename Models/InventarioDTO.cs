using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class InventarioDTO
    {
        public int CodigoInventario { get; set; }
        public int CodigoProducto { get; set; }
        public ProductoDTO Producto { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoRegistro { get; set; }
        public Double Precio { get; set; }
        public int Entrada { get; set; }
        public int Salida { get; set; }
    }
}
