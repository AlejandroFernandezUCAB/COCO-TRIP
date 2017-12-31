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
    private id: number;

    constructor()
    {

    }

    /**
     * Establece el identificador de la entidad
     */
    set setId(id : number) 
    {
        this.id = id;
    }

    /**
     * Obtiene el identificador de la entidad
     */
    get getId() : number
    {
        return this.id;
    }
    
}