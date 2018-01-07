import { Entidad } from '../domain/entidad';
import { Events } from 'ionic-angular';
//****************************************************************************************************//
//**********************************Dao de MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Dao
 * 
 */
export abstract class DAO{
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de agregar.
 * 
 */
    abstract agregar(entidad : Entidad) : Entidad;

/**
 * Descripcion del metodo:
 * Metodo que se encarga de visualizar.
 * 
 */
    abstract visualizar(entidad : Entidad,evento :Events) : Entidad;

    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de eliminar.
 * 
 */
    abstract eliminar(entidad : Entidad) : boolean;

     
/**
 * Descripcion del metodo:
 * Metodo que se encarga de modificar.
 * 
 */
    abstract modificar(entidad : Entidad) : boolean;

    


}