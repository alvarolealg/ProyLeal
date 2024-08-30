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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            try
            {
                if (!IsPostBack)
                {
                    //CONFIF INICIAL DE LA PANTALLA

                    CategoriaNegocio negocioTipo = new CategoriaNegocio();
                    List<Categoria> listaTipo = negocioTipo.listarCategoria();
                    ddlTipo.DataSource = listaTipo;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    MarcaNegocio negocioMarca = new MarcaNegocio();
                    List<Marca> listaMarca = negocioMarca.listar();
                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();
                }
                //SI MODIFICAMOS
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo seleccionado = (negocio.listar(id))[0];

                    decimal precio = seleccionado.Precio;
                    txtPrecio.Text = precio.ToString("F0");

                    //Guardo articulo seleccionado en session
                    Session.Add("articuloSeleccionado", seleccionado);
                    //PRECARGO LOS CAMPOS
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtCodigo.Text = seleccionado.Codigo;
                    txtUnidades.Text = seleccionado.Unidades.ToString();
                    txtPrecio.Text = seleccionado.Precio.ToString("N0");
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.ImagenUrl;

                    ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                    ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e);

                    //CONFIGURAR ACCIONES
                    if (!seleccionado.Activo)
                    {
                        btnInactivar.Text = "Reactivar";
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Nombre = txtNombre.Text;
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = int.Parse(txtPrecio.Text.ToString());
                nuevo.Unidades=int.Parse(txtUnidades.Text.ToString());
                nuevo.ImagenUrl = txtImagenUrl.Text;

                nuevo.Marca=new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                nuevo.Tipo = new Categoria();
                nuevo.Tipo.Id=int.Parse(ddlTipo.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id=int.Parse(txtId.Text);
                    negocio.modificarSP(nuevo);
                }
                else
                {
                    negocio.agregarSP(nuevo);
                }
                Response.Redirect("ListadoProductos.aspx",false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo seleccionado = (Articulo)Session["articuloSeleccionado"];

                negocio.desactivarArticulo(seleccionado.Id, !seleccionado.Activo);
                Response.Redirect("listadoProductos.aspx");

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtImagenUrl.Text;
        }
    }
}