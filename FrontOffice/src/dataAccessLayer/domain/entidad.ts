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
export class Entidad
{
    protected _id: String;

    constructor()
    {

    }

    /**
     * Establece el identificador de la entidad
     */
    set Id(id : String) 
    {
        this._id = id;
    }

    /**
     * Obtiene el identificador de la entidad
     */
    get Id() : String 
    {
        return this._id;
    }
    
}