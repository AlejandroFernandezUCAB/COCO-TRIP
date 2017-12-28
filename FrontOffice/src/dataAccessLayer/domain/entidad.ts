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
    private _id: number;

    constructor()
    {

    }

    /**
     * Establece el identificador de la entidad
     */
    set Id(id : number) 
    {
        this._id = id;
    }

    /**
     * Obtiene el identificador de la entidad
     */
    get Id() : number
    {
        return this._id;
    }
    
}