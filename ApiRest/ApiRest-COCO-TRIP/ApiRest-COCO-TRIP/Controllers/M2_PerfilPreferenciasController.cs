using Npgsql;
using System.Web.Http;
using ApiRest_COCO_TRIP.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Negocio.Command;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Negocio.Fabrica;
using System;

namespace ApiRest_COCO_TRIP.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class M2_PerfilPreferenciasController : ApiController
    {

        protected PeticionPerfil peticion;
        Models.Usuario usuario;
        private Entidad usuario2;
        private int idUsuario;
        private Comando comando;

        /// <summary>
        /// Metodo Post para agregar una preferencia del usuario, hará una llamda a base de datos para buscar id de usuario
        /// y id de categoria para agregarlo en la tabla de preferencias
        /// </summary>
        /// <param name="idUsuario"> Id usuario </param>
        /// <param name="idCategoria"> Id Categoria</param>
        /// <returns>Lista de preferencias del usuario</returns>
        // POST api/<controller>/<action>/prefencia
        [HttpPost]
        public List<Datos.Entity.Categoria> AgregarPreferencias(int idUsuario, int idCategoria)
        {

            usuario2 = FabricaEntidad.CrearEntidadUsuario(idUsuario);
            Entidad categoria = FabricaEntidad.CrearEntidadCategoria();
            categoria.Id = idCategoria;
            comando = FabricaComando.CrearComandoAgregarPreferencia(usuario2, categoria);
            comando.Ejecutar();
           
            List<Datos.Entity.Categoria> preferencias = ((ComandoAgregarPreferencia)comando).RetornarListaCategoria();
            
            return preferencias; //Retorna una lista de de categorias

        }

        /// <summary>
        /// Metodo Post que devuelve la lista de preferencias actualizada
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <param name="idCategoria">idCategoria</param>
        /// <returns>Retorna  una lista de  categorias</returns>
        [HttpPost]
        public List<Datos.Entity.Categoria> EliminarPreferencias(int idUsuario, int idCategoria)
        {
            
            usuario2 = FabricaEntidad.CrearEntidadUsuario(idUsuario);
            Entidad categoria = FabricaEntidad.CrearEntidadCategoria();
            categoria.Id = idCategoria;
            comando = FabricaComando.CrearComandoEliminarPreferencia(usuario2, categoria);
            comando.Ejecutar();

            List<Datos.Entity.Categoria> preferencias = ((ComandoEliminarPreferencia)comando).RetornarListaCategoria();

            return preferencias; //Retorna una lista de de categorias
        }


        /// <summary>
        /// Devuelve la lista de  preferencias de un usuario
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Lista de preferencias</returns>
        [HttpGet]
        public List<Datos.Entity.Categoria> BuscarPreferencias(int idUsuario)
        {

            usuario2 = FabricaEntidad.CrearEntidadUsuario(idUsuario);
            comando = FabricaComando.CrearComandoBuscarPreferencias(usuario2);
            comando.Ejecutar();
            List<Datos.Entity.Categoria> preferencias = ((ComandoBuscarPreferencias)comando).RetornarListaCategoria();
            return preferencias;

        }

        /// <summary>
        /// Metodo Post para actualizar la informacion del usuario. hará dos llamdas a base de datos, una para buscar id de usuario
        /// y otra para modificar la informacion con los parametros recibidos
        /// </summary>
        /// <param name="idUsuario">Id del usuario </param>
        /// <param name="nombreUsuario">Id de la categoria</param>
        /// <param name="nombre">Id del usuario </param>
        /// <param name="apellido">Id de la categoria</param>
        /// <param name="fechaDeNacimiento">Id del usuario </param>
        /// <param name="genero">Id de la categoria</param>
        /// <returns>Retorna verdadero si el procedimiento fue exitoso, o falso para cualquier otro caso</returns>
        [HttpPost]
        public bool ModificarDatosUsuario(string nombreUsuario, string nombre, string apellido, string fechaDeNacimiento, string genero)
        {
            try
            {
                usuario2 = FabricaEntidad.CrearEntidadUsuario(nombre, apellido, nombreUsuario, fechaDeNacimiento, genero);
                comando = FabricaComando.CrearComandoModificarDatosUsuario(usuario2);
                comando.Ejecutar();
                usuario2 = comando.Retornar();

                if (usuario2 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
            
            catch (NpgsqlException)
            {
                return false;
            }

        }

        /// <summary>
        /// Metodo Post para actualizar la contraseña del usuario del usuario. hará dos llamdas a base de datos, una para buscar id de usuario
        /// y otra para modificar la contraseña con los parametros recibidos
        /// </summary>
        /// <param name="idUsuario">Id del usuario </param>
        /// <param name="username">Username del usuario</param>
        /// <param name="password">Contraseña del usuario </param>
        /// <returns>bool</returns>
        [HttpPost]
        public bool CambiarPass(string username, string passwordActual, string passwordNuevo)
        {
            try
            {
                usuario2 = FabricaEntidad.CrearEntidadUsuario(username, passwordActual);
                comando = FabricaComando.CrearComandoCambiarPassword(usuario2, passwordNuevo);
                comando.Ejecutar();
                usuario2 = comando.Retornar();
                if (usuario2 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (NpgsqlException e)
            {
                return false;
            }


        }

        /// <summary>
        /// Metodo Post para borrar al usuario. hará dos llamdas a base de datos, una para buscar id de usuario
        /// y otra para borrar al usuario con el parametro usado
        /// </summary>
        /// <param name="username">Username del usuario</param>
        /// <param name="password">Contraseña del usuario </param>
        /// <returns>bool</returns>
        [HttpPost]
        public bool BorrarUsuario(string username, string password)
        {
            try
            {
                usuario2 = FabricaEntidad.CrearEntidadUsuario(username, password);
                comando = FabricaComando.CrearComandoBorrarUsuario(usuario2);
                comando.Ejecutar();
                usuario2 = comando.Retornar();

                if (usuario2 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch(Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo Post para obtener los datos del usuario.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns>Retorna un Objeto Usuario, o null en caso de fallo</returns>
        [HttpPost]
        public Datos.Entity.Usuario ObtenerDatosUsuario(int idUsuario)
        {
            
            usuario2 = FabricaEntidad.CrearEntidadUsuario(idUsuario);
            comando = FabricaComando.CrearComandoObtenerDatosUsuario(usuario2);
            comando.Ejecutar();
            usuario2 = comando.Retornar();
            if (usuario2 != null)
            {
                return (Datos.Entity.Usuario)usuario2;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Metodo que devuelve las preferencias que el usuario aun no tenga
        /// para luego agregarlas
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <param name="preferencia"> String de preferencia del usuario</param>
        /// <returns>Lista de categorias que hacen match con preferencia</returns>
        [HttpPost]
        public List<Datos.Entity.Categoria> BuscarCategorias(int idUsuario, string preferencia)
        {

            usuario2 = FabricaEntidad.CrearEntidadUsuario(idUsuario);
            comando = FabricaComando.CrearComandoBuscarCategorias(usuario2, preferencia);
            comando.Ejecutar();
            List<Datos.Entity.Categoria> preferencias = ((ComandoBuscarCategorias)comando).RetornarListaCategoria();
            return preferencias;
            
        }

    }

}
