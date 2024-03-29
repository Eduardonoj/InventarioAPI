﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ProductoDTO
    {
        public int CodigoProducto { get; set; }
        public int CodigoCategoria { get; set; }
        public int CodigoEmpaque { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public TipoEmpaqueDTO TipoEmpaque { get; set; }
        public string Descripcion { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public Decimal PrecioPorDocena { get; set; }
        public Decimal PrecioPorMayor { get; set; }
        public int Existencia { get; set; }
        public string Imagen { get; set; }
    }
}
