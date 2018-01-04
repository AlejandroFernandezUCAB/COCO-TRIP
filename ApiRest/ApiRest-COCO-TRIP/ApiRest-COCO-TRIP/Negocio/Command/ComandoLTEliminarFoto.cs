using System;
using System.Collections.Generic;
using ApiRest_COCO_TRIP.Datos.Entity;
using ApiRest_COCO_TRIP.Datos.Fabrica;
using ApiRest_COCO_TRIP.Datos.DAO.Interfaces;
using ApiRest_COCO_TRIP.Comun.Excepcion;
using NLog;

namespace ApiRest_COCO_TRIP.Negocio.Command
{
    public class ComandoLTEliminarFoto : Comando
    {
        private IDAOFoto iDAOFoto;
        private Entidad _foto;
        private static Logger log = LogManager.GetCurrentClassLogger();

        public ComandoLTEliminarFoto(Entidad foto){
            iDAOFoto = FabricaDAO.CrearDAOFoto();
        }

        public override void Ejecutar()
        {
            throw new NotImplementedException();
        }

        public override Entidad Retornar()
        {
            throw new NotImplementedException();
        }

        public override List<Entidad> RetornarLista()
        {
            throw new NotImplementedException();
        }
    }
}