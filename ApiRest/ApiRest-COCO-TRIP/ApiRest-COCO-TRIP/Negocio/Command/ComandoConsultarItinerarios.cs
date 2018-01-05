using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    /// <summary>
    /// Clase que consulta todos los itinerarios dado un idUsuario
    /// </summary>
    public class ComandoConsultarItinerarios : Comando
  {
    Usuario usuario;
    List<Entidad> lista;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        public ComandoConsultarItinerarios(int idUsuario)
    {
      usuario = FabricaEntidad.CrearEntidadUsuario();
      usuario.Id = idUsuario;
    }

    public override void Ejecutar()
    {
      lista = FabricaDAO.CrearDAOItinerario().ConsultarLista(usuario);
    }

    public override Entidad Retornar()
    {
      throw new NotImplementedException();
    }

    public override List<Entidad> RetornarLista()
    {
      return lista;
    }
  }
}
