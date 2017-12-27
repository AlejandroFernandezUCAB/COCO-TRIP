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
    private nombre: string; //Nombre del grupo
    private rutaFoto: string; //Ruta de la foto
    private contenidoFoto: ByteString; //Bytes de la foto (para cargar)
    private lider: number; //ID del creador del grupo
    private cantidadIntegrantes: number; //Cantidad de miembros del grupo

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre del grupo
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre del grupo
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

    /**
     * Retorna la ruta de la foto (del servicio web)
     */
    get RutaFoto() : string
    {
        return this.rutaFoto;
    }

    /**
     * Establece la ruta de la foto (del servicio web)
     */
    set RutaFoto(rutaFoto : string)
    {
        this.rutaFoto = rutaFoto;
    }

    /**
     * Retorna los bytes de la foto
     */
    get ContenidoFoto() : ByteString
    {
        return this.contenidoFoto;
    }

    /**
     * Establece los bytes de la foto (para guardar en el servicio web)
     */
    set ContenidoFoto(contenidoFoto : ByteString)
    {
        this.contenidoFoto = contenidoFoto;
    }

    /**
     * Retorna el identificador unico del lider del grupo
     */
    get Lider() : number
    {
        return this.lider;
    }

    /**
     * Establece el identificador unico del lider del grupo
     */
    set Lider(idLider : number)
    {
        this.lider = idLider;
    }

    /**
     * Retorna la cantidad de miembros del grupo de amigos
     */
    get CantidadIntegrantes() : number
    {
        return this.cantidadIntegrantes;
    }

    /**
     * Establece la cantidad de miembros del grupo de amigos
     */
    set CantidadIntegrantes(cantidadIntegrantes : number)
    {
        this.cantidadIntegrantes = cantidadIntegrantes;
    }
    
}