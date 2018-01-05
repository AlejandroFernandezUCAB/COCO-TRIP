import { Grupo } from '../domain/grupo';
import { Usuario } from '../domain/usuario';
<<<<<<< HEAD
import { Itinerario } from '../domain/itinerario';
=======
import { DateTime } from 'ionic-angular/components/datetime/datetime';
>>>>>>> db80650b1d7fc020720012215d1353786e3d8271

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
     
    private Foto: string;
    private Genero: string;
    private Clave: string;
    private Valido: boolean;
    
      */
    public static crearUsuarioConParametros(
        id : number, nombre : string, 
        apellido : string, correo : string, 
        nickname : string, fechaNacimiento?: Date,
        foto?: string, genero?: string,
        clave?: string, valido?: boolean
    ) : Usuario
    {
        let usuario = new Usuario();
        usuario.setId = id;
        usuario.setNombre = nombre;
        usuario.setApellido = apellido;
        usuario.setCorreo = correo;
        usuario.setNombreUsuario = nickname;
        if (fechaNacimiento) {
            usuario.setFechaNacimiento = fechaNacimiento;
        }
        if (foto) {
            usuario.setFoto = foto;
        }
        if (genero) {
            usuario.setGenero = genero;
        }
        if (clave) {
            usuario.setClave = clave;
        }
        if (valido) {
            usuario.setValido = valido;
        }
        return usuario;
      }
        /**
      * Retorna la instancia de la entidad Itinerario
      */
      public static crearItinerario() : Itinerario
      {
          return new Itinerario();
      }
 }