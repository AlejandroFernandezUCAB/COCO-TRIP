//****************************************************************************************************//
//*****************************************CLASE ENTIDAD MODULO 6*************************************//
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
 * Clase entidad
 */
export class Entidad{
    protected _id: String;

    constructor(){

    }

    /**
     * Establece el identificador de la entidad
     */
    set setId(id : String) {
        this._id = id;
    }

    /**
     * Obtiene el identificador de la entidad
     */
    get getId():String {
        return this._id;
    }
}