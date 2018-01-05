import { Grupo } from '../domain/grupo';
import { Usuario } from '../domain/usuario';
import { Itinerario } from '../domain/itinerario';

//****************************************************************************************************//
//**********************************Fabrica Comando de MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Fabrica Entidad
 * 
 */

 export class FabricaEntidad
 {
     /**
      * Retorna la instancia de la entidad Grupo
      */
     public static crearGrupo() : Grupo
     {
        return new Grupo();
     }

     /**
      * Retorna la instancia de la entidad Usuario
      */
     public static crearUsuario() : Usuario
     {
         return new Usuario();
     }

     /**
      * Retorna la instancia de la entidad Itinerario
      */
      public static crearItinerario() : Itinerario
      {
          return new Itinerario();
      }
 }