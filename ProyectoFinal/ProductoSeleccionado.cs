using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal
{
    public class ProductoSeleccionado
    {
        public int IdProduct { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string Materiales { get; set; }
        public decimal Sub_Total { get; set; }
    }
}