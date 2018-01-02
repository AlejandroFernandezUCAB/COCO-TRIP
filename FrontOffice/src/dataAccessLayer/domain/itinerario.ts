import { Entidad } from './entidad';
import { DateTime } from 'ionic-angular/components/datetime/datetime';
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 *Jorge Marin
 */

/**
 * Descripcion de la clase:
 * Entidad que contiene los datos de los itinerarios
 */
export class Itinerario extends Entidad
{
    private nombre: string; //Nombre del itinerario
    private visible: boolean; //Si esta visible o no
    private idUsuario: number; //Id del usuario propietaario
    private fechaInicio: DateTime; //fecha de inicio del itinerario
    private fechaFin: DateTime; //fecha fin del itinerairo

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre del itinerario
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre del itinerario
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

    /**
     * Retorna si es visble
     */
    get Visible() : boolean
    {
        return this.visible;
    }

    /**
     * Establece la visibilidad
     */
    set Visible(visible : boolean)
    {
        this.visible = visible;
    }

    /**
     * Retorna el id del usuario
     *      
     * */
    get IdUsuario() : number
    {
        return this.idUsuario;
    }

    /**
     * Establece el id del usuario
     */
    set IdUsuario(idUsuario : number)
    {
        this.idUsuario = idUsuario;
    }

    /**
     * Retorna la fecha inicio del itinerario
     */
    get FechaInicio() : DateTime
    {
        return this.fechaInicio;
    }

    /**
     * Establece la fecha inicio del itinerario
     */
    set FechaInicio(fechaInicio : DateTime)
    {
        this.fechaInicio = fechaInicio;
    }

    /**
     * Retorna la fecha fin del itinerario
     */
    get FechaFin() : DateTime
    {
        return this.fechaFin;
    }

    /**
     * Establece la fecha inicio del itinerario
     */
    set FechaFin(fechaFin : DateTime)
    {
        this.fechaFin = fechaFin;
    }
    
}