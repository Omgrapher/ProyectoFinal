using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoFinal
{
    public partial class BuscarCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
                BindPagination();
            }
        }

        protected void GridViewResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewResultado.PageIndex = e.NewPageIndex;
            BindGridView();
            BindPagination();
        }

        private void BindGridView()
        {
            string searchQuery = TextBoxBuscar.Text.Trim();
            List<Cliente> datos = GetDatos(searchQuery);

            GridViewResultado.DataSource = datos;
            GridViewResultado.DataBind();
        }

        private void BindPagination()
        {
            int pageCount = (int)Math.Ceiling((double)GetDatos(TextBoxBuscar.Text.Trim()).Count / GridViewResultado.PageSize);
            
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int pageIndex = 0;

                switch (e.CommandArgument.ToString())
                {
                    case "First":
                        pageIndex = 0;
                        break;
                    case "Prev":
                        pageIndex = GridViewResultado.PageIndex - 1;
                        break;
                    case "Next":
                        pageIndex = GridViewResultado.PageIndex + 1;
                        break;
                    case "Last":
                        pageIndex = GridViewResultado.PageCount - 1;
                        break;
                }

                if (pageIndex >= 0 && pageIndex < GridViewResultado.PageCount)
                {
                    GridViewResultado.PageIndex = pageIndex;
                    BindGridView();
                    BindPagination();
                }
            }
        }

        private List<Cliente> GetDatos(string searchQuery)
        {
            // Ejemplo de datos
            List<Cliente> datos = new List<Cliente>
            {
                new Cliente { Codigo = "1", Nombre = "Juan", Apellido = "Pérez" },
                new Cliente { Codigo = "2", Nombre = "Ana", Apellido = "Gómez" },
                new Cliente { Codigo = "3", Nombre = "Carlos", Apellido = "López" },
                new Cliente { Codigo = "4", Nombre = "María", Apellido = "Rodríguez" },
                // Agrega más datos aquí para probar la paginación
            };

            var resultados = string.IsNullOrEmpty(searchQuery)
                ? datos
                : datos.Where(d => d.Nombre.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            return resultados;
        }

        public class Cliente
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
        }
    }
}