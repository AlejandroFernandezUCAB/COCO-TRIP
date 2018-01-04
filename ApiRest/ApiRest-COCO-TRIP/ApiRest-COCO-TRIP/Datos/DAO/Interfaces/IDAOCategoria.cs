using ApiRest_COCO_TRIP.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_COCO_TRIP.Datos.DAO.Interfaces
{
    interface IDAOCategoria
    {
        Entidad ConsultarPorId(Entidad objeto);
        List<Entidad> ConsultarLista(Entidad objeto);
        List<Entidad> ObtenerCategoriasHabilitadas();
        List<Entidad> ObtenerCategoriaPorId(Entidad entidad);
        List<Entidad> ObtenerTodasLasCategorias();
        Entidad ObtenerIdCategoriaPorNombre(Entidad entidad);
        List<Entidad> ObtenerCategorias(Entidad categoria);
    }
}
