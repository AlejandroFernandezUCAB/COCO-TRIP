import { Entidad } from './entidad';
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Entidad que contiene los datos de los grupos de amigos
 */
export class Grupo extends Entidad
{
    private Nombre: string; //Nombre del grupo
    private RutaFoto: string; //Ruta de la foto
    private ContenidoFoto: any[]; //Bytes de la foto (para cargar)
    private Lider: number; //ID del creador del grupo
    private CantidadIntegrantes: number; //Cantidad de miembros del grupo

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre del grupo
     */
    get getNombre() : string 
    {
        return this.Nombre;
    }

    /**
     * Establece el nombre del grupo
     */
    set setNombre(nombre : string) 
    {
        this.Nombre = nombre;
    }

    /**
     * Retorna la ruta de la foto (del servicio web)
     */
    get getRutaFoto() : string
    {
        return this.RutaFoto;
    }

    /**
     * Establece la ruta de la foto (del servicio web)
     */
    set setRutaFoto(rutaFoto : string)
    {
        this.RutaFoto = rutaFoto;
    }

    /**
     * Retorna los bytes de la foto
     */
    get getContenidoFoto() : any[]
    {
        return this.ContenidoFoto;
    }

    /**
     * Establece los bytes de la foto (para guardar en el servicio web)
     */
    set setContenidoFoto(contenidoFoto : any[])
    {
        this.ContenidoFoto = contenidoFoto;
    }

    /**
     * Retorna el identificador unico del lider del grupo
     */
    get getLider() : number
    {
        return this.Lider;
    }

    /**
     * Establece el identificador unico del lider del grupo
     */
    set setLider(idLider : number)
    {
        this.Lider = idLider;
    }

    /**
     * Retorna la cantidad de miembros del grupo de amigos
     */
    get getCantidadIntegrantes() : number
    {
        return this.CantidadIntegrantes;
    }

    /**
     * Establece la cantidad de miembros del grupo de amigos
     */
    set setCantidadIntegrantes(cantidadIntegrantes : number)
    {
        this.CantidadIntegrantes = cantidadIntegrantes;
    }
    
}