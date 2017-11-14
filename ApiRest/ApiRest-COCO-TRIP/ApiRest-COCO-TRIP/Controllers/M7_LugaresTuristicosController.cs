using System.Net;
using System.Web.Http;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Http.Cors;
using ApiRest_COCO_TRIP.Models;
using ApiRest_COCO_TRIP.Models.Dato;
using ApiRest_COCO_TRIP.Models.Excepcion;
using ApiRest_COCO_TRIP.Models.BaseDeDatos;

namespace ApiRest_COCO_TRIP.Controllers
{
  /// <summary>
  /// Controlador del Modulo 7 de Gestion de Lugares Turisticos y Actividades en Lugares Turisticos
  /// </summary>
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class M7_LugaresTuristicosController : ApiController
    {
        private PeticionLugarTuristico peticion; //Clase que interactua con la clase Conexion
        //y que permite al controlador consultar/insertar/actualizar/eliminar datos en la base de datos

        //GET

        /// <summary>
        /// Consulta la lista de lugares turisticos dentro del rango establecido
        /// </summary>
        /// <param name="desde">limite inferior</param>
        /// <param name="hasta">limite superior</param>
        /// <returns>Lista de lugares turisticos con ID, nombre, costo, descripcion y estado 
        /// de cada lugar turistico. Formato JSON</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public List<LugarTuristico> GetLista (int desde, int hasta)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              var listaLugar = peticion.ConsultarListaLugarTuristico(desde, hasta);

              if(listaLugar.Count == (new List<LugarTuristico>() ).Count)
              {
                throw new HttpResponseException(HttpStatusCode.NotFound);
              }
              else
              {
                return listaLugar;
              }
            }
            catch(BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

        /// <summary>
        /// Consulta el detalle del lugar turistico y los nombres de las actividades asociadas
        /// </summary>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>Datos del lugar turistico y nombre de las actividades. Formato JSON</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public LugarTuristico GetLugar (int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              var lugar = peticion.ConsultarLugarTuristico(id);

              if (lugar.Equals(new LugarTuristico() ) )
              {
                  throw new HttpResponseException(HttpStatusCode.NotFound);
              }
              else
              {
                  return lugar;
              }
            }     
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Consulta el detalle del lugar turistico y el detalle de las actividades asociadas
        /// </summary>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>Datos del lugar turistico y datos de las actividades. Formato JSON</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public LugarTuristico GetLugarActividades (int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              var lugar = peticion.ConsultarLugarTuristicoConActividades(id);

              if(lugar.Equals( new LugarTuristico() ) )
              {
                throw new HttpResponseException(HttpStatusCode.NotFound);
              }
              else
              {
                return lugar;
              }
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Consulta las actividades asociadas a un lugar turistico
        /// </summary>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>Lista de actividades asociadas al lugar turistico. Formato JSON</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public List<Actividad> GetActividades (int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              var listaActividades = peticion.ConsultarActividades(id);

              if (listaActividades.Count == ( new List<Actividad>() ).Count )
              {
                throw new HttpResponseException(HttpStatusCode.NotFound);
              }
              else
              {
                return listaActividades;
              }
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Consulta la actividad
        /// </summary>
        /// <param name="id">ID de la actividad</param>
        /// <returns>Objeto Actividad. Formato JSON</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public Actividad GetActividad(int id)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            var actividad = peticion.ConsultarActividad(id);

            if (actividad.Equals( new Actividad() ))
            {
              throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
              return actividad;
            }
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Consulta las categorias
        /// </summary>
        /// <returns>Lista de categorias</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su repsectivo Status Code</exception>
        [HttpGet]
        public List<Categoria> GetCategoria()
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            var categoria = peticion.ConsultarCategoria();

            if (categoria.Count == (new List<Categoria>() ).Count )
            {
              throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
              return categoria;
            }
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Consulta las subcategorias de una categoria
        /// </summary>
        /// <param name="id">ID categoria</param>
        /// <returns>Lista de subcategorias</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpGet]
        public List<Categoria> GetSubCategoria(int id)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            var categoria = peticion.ConsultarSubCategoria(id);

            if (categoria.Count == (new List<Categoria>()).Count )
            {
              throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
              return categoria;
            }
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        //POST

        /// <summary>
        /// Inserta los datos del lugar turistico
        /// </summary>
        /// <param name="lugar">Objeto LugarTuristico</param>
        /// <returns>ID del lugar turistico insertado</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPost]
        public int PostLugar(LugarTuristico lugar)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              return peticion.InsertarLugarTuristico(lugar);
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (CasteoInvalidoExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ReferenciaNulaExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ArchivoExcepcion)
            {
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
    }

        /// <summary>
        /// Inserta una actividad asociada a un lugar turistico
        /// </summary>
        /// <param name="actividad">Objeto Actividad</param>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>ID de la actividad insertada</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPost]
        public int PostActividad(Actividad actividad, int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              return peticion.InsertarActividad(actividad, id);
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (CasteoInvalidoExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ReferenciaNulaExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ArchivoExcepcion)
            {
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Inserta un horario asociado a un lugar turistico
        /// </summary>
        /// <param name="horario">Objeto Horario</param>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>ID del horario insertado</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPost]
        public int PostHorario(Horario horario, int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              return peticion.InsertarHorario(horario, id);
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest); //ID de Lugar Turistico no existe
            }
            catch (CasteoInvalidoExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ReferenciaNulaExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        } 

        /// <summary>
        /// Inserta una foto asociada a un lugar turistico
        /// </summary>
        /// <param name="foto">Objeto Foto</param>
        /// <param name="id">ID del lugar turistico</param>
        /// <returns>ID de la foto insertada</returns>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPost]
        public int PostFoto(Foto foto, int id)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              return peticion.InsertarFoto(foto, id);
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (CasteoInvalidoExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ReferenciaNulaExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ArchivoExcepcion)
            {
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Inserta una categoria o subcategoria a un lugar turistico existente
        /// </summary>
        /// <param name="id">ID lugar turistico</param>
        /// <param name="idCategoria">ID categoria</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPost]
        public void PostCategoria(int id, int idCategoria)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.InsertarCategoria(id, idCategoria);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.BadRequest); //ID de Lugar Turistico o Categoria no existen
          }
        }

        //PUT

        /// <summary>
        /// Actualiza los datos del lugar turistico
        /// </summary>
        /// <param name="lugarTuristico">Objeto Lugar Turistico</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPut]
        public void PutLugar(LugarTuristico lugar)
        {
            peticion = new PeticionLugarTuristico();

            try
            {
              peticion.ActualizarLugarTuristico(lugar);
            }
            catch (BaseDeDatosExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            catch (CasteoInvalidoExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ReferenciaNulaExcepcion e)
            {
              e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (ArchivoExcepcion)
            {
              //RegistrarExcepcion(e); NLog

              throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Activa o desactiva el lugar turistico
        /// </summary>
        /// <param name="id">ID del lugar turistico</param>
        /// <param name="activar">true para activar, false para desactivar</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPut]
        public void PutActivarLugar(int id, bool activar)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.ActivarLugarTuristico(id, activar);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Activa o desactiva la actividad
        /// </summary>
        /// <param name="id">ID de la actividad</param>
        /// <param name="activar">true para activar, false para desactivar</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpPut]
        public void PutActivarActividad(int id, bool activar)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.ActivarActividad(id, activar);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        //DELETE

        /// <summary>
        /// Eliminar actividad
        /// </summary>
        /// <param name="id">ID de la actividad</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpDelete]
        public void DeleteActividad (int id)
        {
          peticion = new PeticionLugarTuristico();

              try
              {
                peticion.EliminarActividad(id);
              }
              catch (BaseDeDatosExcepcion e)
              {
                e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
                //RegistrarExcepcion(e); NLog

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
              }
              catch (ArchivoExcepcion)
              {
                //RegistrarExcepcion(e); NLog

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
              }
        }

        /// <summary>
        /// Eliminar foto
        /// </summary>
        /// <param name="id">ID de la foto</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpDelete]
        public void DeleteFoto (int id)
        {

          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.EliminarFoto(id);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
          catch (ArchivoExcepcion)
          {
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Eliminar horario
        /// </summary>
        /// <param name="id">ID del horario</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpDelete]
        public void DeleteHorario(int id)
        {

          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.EliminarHorario(id);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Eliminar categoria o subcategoria de un lugar turistico existente
        /// </summary>
        /// <param name="id">ID lugar turistico</param>
        /// <param name="idCategoria">ID categoria</param>
        /// <exception cref="HttpResponseException">Excepcion HTTP con su respectivo Status Code</exception>
        [HttpDelete]
        public void DeleteCategoria(int id, int idCategoria)
        {
          peticion = new PeticionLugarTuristico();

          try
          {
            peticion.EliminarCategoria(id, idCategoria);
          }
          catch (BaseDeDatosExcepcion e)
          {
            e.NombreMetodos.Add(this.GetType().FullName + "." + MethodBase.GetCurrentMethod().Name);
            //RegistrarExcepcion(e); NLog

            throw new HttpResponseException(HttpStatusCode.InternalServerError);
          }
        }

        /// <summary>
        /// Escribe en un log (bitacora) todos los datos almacenados en la excepcion
        /// logica generada en el web service. Esta funcionalidad sera implementada
        /// en la tercera entrega.
        /// </summary>
        /// <param name="e">Excepcion</param>
        /*[NonAction]
        private void RegistrarExcepcion (object e)
        {

        }*/
    }

}
