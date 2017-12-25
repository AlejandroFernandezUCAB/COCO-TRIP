import { Entidad } from '../../dataAccessLayer/domain/entidad';
//****************************************************************************************************//
//*****************************************CLASE COMANDO MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Clase abstracta comando
 */

export abstract class Comando {
    
    _entidad : Entidad;

    
    public execute(): void {
        console.log("ENTRE EN COMANDO SOLO");
    }
}