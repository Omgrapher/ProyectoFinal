using Parcial2.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal.Productos
{
    public partial class BuscarProducto : System.Web.UI.Page
    {
        public static string conec = ConfigurationManager.ConnectionStrings["Libreria1ConnectionString"].ConnectionString;
        BaseDatos.milinqDataContext mibd = new BaseDatos.milinqDataContext(conec);

        protected void limpiar()
        {
            TextBoxBuscar.Text = "";
            GridView1Producto.DataSource = null;
        }
        private void CargarDatosProducto(int pageIndex)
        {
            var buscar = from b in mibd.Productos
                         join i in mibd.Inventarios on b.id_producto equals i.id_producto
                         where b.Tipo_Producto.nombre.Contains(TextBoxBuscar.Text) || b.id_producto.ToString().Contains(TextBoxBuscar.Text)
                         select new
                         {
                             IdProduct = b.id_producto,
                             Producto = b.Tipo_Producto.nombre,
                             Stock = i.cant_disponible,
                             Materiales = b.Material_Producto.nombre_material,
                             Precio_Venta = i.precio_venta
                         };

            var resultados = buscar.ToList();

            var productosFormateados = resultados.Select(p => new
            {
                p.IdProduct,
                p.Producto,
                p.Stock,
                p.Materiales,
                Precio_Venta = "Q " + Math.Round(p.Precio_Venta).ToString() // Redondear y formatear con "Q"
            }).ToList();

            if (productosFormateados.Count > 0)
            {
                GridView1Producto.PageIndex = pageIndex; // Establecer el índice de la página actual
                GridView1Producto.DataSource = productosFormateados;
                GridView1Producto.DataBind();
                GridView1Producto.Visible = true;
            }
            else
            {
                GridView1Producto.Visible = false; // No mostrar la tabla si no hay resultados
                string errorMessage = "No se encontraron similares...";
                Swal.Fire(errorMessage, "Alerta", SwalIcon.Warning);
                limpiar();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosProducto(0);
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CargarDatosProducto(e.NewPageIndex);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1Producto.Visible=false;

            }
        }
    }
}