using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto1
{
    public partial class ListadoProductos : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.listarSP());
                dgvProductos.DataSource = Session["listaArticulos"];
                dgvProductos.DataBind();
                cargarDatosEnGridView();
            }
        }

        private void cargarDatosEnGridView()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session["listaArticulos"] = negocio.listarSP();
            dgvProductos.DataSource = Session["listaArticulos"];
            dgvProductos.DataBind();
        }
        protected void dgvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvProductos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }
        //protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ddlCriterio.Items.Clear();
        //    if (ddlCampo.SelectedItem.ToString() == "Nombre" || ddlCampo.SelectedItem.ToString() == "Marca" || ddlCampo.SelectedItem.ToString() == "Categoria")
        //    {
        //        ddlCriterio.Items.Add("Contiene");
        //        ddlCriterio.Items.Add("Comienza con");
        //        ddlCriterio.Items.Add("Termina con");

        //    }
        //}



        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ArticuloNegocio negocio = new ArticuloNegocio();
        //        dgvProductos.DataSource = negocio.filtrar(
        //            ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(),
        //            txtFiltroAvanzado.Text, ddlEstado.SelectedItem.ToString());
        //        dgvProductos.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        Session.Add("error", ex);
        //        throw;
        //    }
        //}
        //protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        //{
        //    FiltroAvanzado = chkAvanzado.Checked;
        //    txtFiltroAvanzado.Enabled = !FiltroAvanzado;
        //}


        protected void dgvProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProductos.PageIndex= e.NewPageIndex;
            dgvProductos.DataBind();
            cargarDatosEnGridView();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x =>
            (x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()))
            || x.Marca.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper())
            || x.Tipo.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvProductos.DataSource = listaFiltrada;
            dgvProductos.DataBind();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            //Limpiar filtro
            txtFiltro.Text = string.Empty;
            //Recargar datos
            cargarDatosEnGridView();
        }
    }
}  