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
        //public List<Articulo> listar(string id = "")
        //{
        //    List<Articulo> lista = new List<Articulo>();
        //    SqlConnection conexion = new SqlConnection();
        //    SqlCommand comando = new SqlCommand();
        //    SqlDataReader lector;

        //    try
        //    {
        //        conexion.ConnectionString = "server=.\\SQLEXPRESS; database=Proyecto; integrated security = true";
        //        comando.CommandType = System.Data.CommandType.Text;
        //        comando.CommandText = "select c.id idCliente, c.nombre nombrecliente, c.apellido,c.email,p.nombre NombreProd,p.activo,p.id idProd,p.descripcion descProd,p.codigo,p.unidades,p.imagenUrl img,p.precio,m.id idMarca, m.descripcion Marca,k.descripcion Categoria, k.id idCategoria, v.id_venta, v.fecha from productos p, cliente c, venta v,marca m, categoria k where p.id=v.codigo_producto and c.id=v.id_cliente and m.id=p.idMarca and k.id=p.idCategoria ";
        //        if (id != "")
        //            comando.CommandText += " and A.Id = " + id;
        //        comando.Connection = conexion;

        //        conexion.Open();
        //        lector = comando.ExecuteReader();

        //        while (lector.Read())
        //        {
        //            Articulo aux = new Articulo();
        //            aux.Id = (int)lector["IdProd"];
        //            aux.Codigo = (string)lector["Codigo"];
        //            aux.Nombre = (string)lector["NombreProd"];
        //            aux.Descripcion = (string)lector["DescProd"];
        //            aux.Precio = (decimal)lector["Precio"];
        //            aux.Unidades = (int)lector["Unidades"];



        //            if (!(lector["Img"] is DBNull))

        //                aux.ImagenUrl = (string)lector["Img"];

        //            aux.Tipo = new Categoria();
        //            aux.Tipo.Descripcion = (string)lector["Categoria"];
        //            aux.Tipo.Id = (int)lector["IdCategoria"];
        //            aux.Marca = new Marca();
        //            aux.Marca.Descripcion = (string)lector["Marca"];
        //            aux.Marca.Id = (int)lector["IdMarca"];

        //            aux.Activo = bool.Parse(lector["Activo"].ToString());

        //            lista.Add(aux);
        //        }

        //        conexion.Close();
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }


        //}



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
                    if (!(datos.Lector["Img"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["img"];
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
                datos.setProcedimiento("AltaArticulo");
                datos.setParametros("@nombre", nuevo.Nombre);
                datos.setParametros("@des", nuevo.Descripcion);
                datos.setParametros("@unidades", nuevo.Unidades);
                datos.setParametros("@imgUrl", nuevo.ImagenUrl);
                datos.setParametros("@codigo", nuevo.Codigo);
                datos.setParametros("@idMarca", nuevo.Marca.Id);
                datos.setParametros("@idCategoria", nuevo.Tipo.Id);

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
                datos.setProcedimiento("Modificar");
                datos.setParametros("@nombre", art.Nombre);
                datos.setParametros("@desc", art.Descripcion);
                datos.setParametros("@unidades", art.Unidades);
                datos.setParametros("@imgUrl", art.ImagenUrl);
                datos.setParametros("@codigo", art.Codigo);
                datos.setParametros("@idMarca", art.Marca.Id);
                datos.setParametros("@idCategoria", art.Tipo.Id);
                datos.setParametros("@activo", art.Activo);

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
    }
}