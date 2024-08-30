using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=Proyecto; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select p.nombre NombreProd, p.activo, p.id idProd, p.descripcion descProd, p.codigo,p.unidades,p.imagenUrl img,p.precio,m.id idMarca,m.descripcion marca,c.descripcion categoria,c.id idCategoria from Productos p, marca m, categoria c where m.id=p.idMarca and c.id=p.idCategoria ";
                if (id != "")
                    comando.CommandText += " and P.Id = " + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["IdProd"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["NombreProd"];
                    aux.Descripcion = (string)lector["DescProd"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Unidades = (int)lector["Unidades"];



                    if (!(lector["Img"] is DBNull))

                        aux.ImagenUrl = (string)lector["Img"];

                    aux.Tipo = new Categoria();
                    aux.Tipo.Descripcion = (string)lector["Categoria"];
                    aux.Tipo.Id = (int)lector["IdCategoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)lector["Marca"];
                    aux.Marca.Id = (int)lector["IdMarca"];

                    aux.Activo = bool.Parse(lector["Activo"].ToString());

                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



        public List<Articulo> listarSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setProcedimiento("listarSP");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["idProd"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["NombreProd"];
                    aux.Descripcion = (string)datos.Lector["descProd"];
                    aux.Unidades = (int)datos.Lector["Unidades"];
                    if (!(datos.Lector["imagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["imagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Tipo = new Categoria();
                    aux.Tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["idMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregarSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setProcedimiento("AltaProducto");
                datos.setParametros("@nombre", nuevo.Nombre);
                datos.setParametros("@desc", nuevo.Descripcion);
                datos.setParametros("@unidades", nuevo.Unidades);
                datos.setParametros("@imgUrl", nuevo.ImagenUrl);
                datos.setParametros("@codigo", nuevo.Codigo);
                datos.setParametros("@idMarca", nuevo.Marca.Id);
                datos.setParametros("@idCategoria", nuevo.Tipo.Id);
                datos.setParametros("Activo", nuevo.Activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void modificarSP(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setProcedimiento("ModificarSp");
                datos.setParametros("id", art.Id);
                datos.setParametros("@nombre", art.Nombre);
                datos.setParametros("@desc", art.Descripcion);
                datos.setParametros("@unidades", art.Unidades);
                datos.setParametros("@img", art.ImagenUrl);
                datos.setParametros("@codigo", art.Codigo);
                datos.setParametros("@idMarca", art.Marca.Id);
                datos.setParametros("@idCategoria", art.Tipo.Id);
                datos.setParametros("@activo", art.Activo);
                datos.setParametros("Precio", art.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public void desactivarArticulo(int id, bool activo = false)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setConsulta("Update productos set activo=@activo where id=@id");
                datos.setParametros("@id", id);
                datos.setParametros("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Articulo> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select p.id idArticulo , p.idMarca, p.idCategoria, p.codigo, p.nombre nombreProducto,p.descripcion descProducto,p.unidades, p.imagenUrl imagen, p.precio, c.descripcion Tipo, m.descripcion as Marca, p.activo from\r\nProductos p, Categoria c, Marca m where c.id = p.idCategoria and m.id = p.idMarca and ";
                if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "c.descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "c.descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "c.descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Nombre like '" + filtro + "%'";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }

                if (estado == "Activos")
                    consulta += " and a.Activo=1";
                else if (estado == "Inactivos")
                    consulta += " and a.activo =0";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdProducto"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["NombreProducto"];
                    aux.Descripcion = (string)datos.Lector["DescProducto"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Unidades = (int)datos.Lector["Unidades"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["Imagen"];
                    aux.Tipo = new Categoria();
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }
                return lista;


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}