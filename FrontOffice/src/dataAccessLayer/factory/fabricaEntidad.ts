import { Grupo } from '../domain/grupo';
import { Usuario } from '../domain/usuario';

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
      * Retorna la instancia de la entidad Usuario con parametros
      */
      public static crearUsuarioConParametros(
          id : number, nombre : string, 
          apellido : string, 
          correo : string, 
          nickname : string ) : Usuario
      {
          let usuario = new Usuario();
          usuario.Id = id;
          usuario.Nombre = nombre;
          usuario.Apellido = apellido;
          usuario.Correo = correo;
          usuario.NombreUsuario = nickname;
          return usuario;
      }
 }