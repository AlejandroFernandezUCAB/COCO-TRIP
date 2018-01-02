import { Entidad } from "./entidad";
import { DateTime } from "ionic-angular/components/datetime/datetime";
import { Time } from "@angular/common/src/i18n/locale_data_api";

//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 *Jorge Marin
 */

/**
 * Descripcion de la clase:
 * Entidad que contiene los datos de las actividades de los usuarios
 */
export class Evento extends Entidad
{
    private nombre:string;
    private descripcion:string;
    private costo:number;
    private fechaInicio:DateTime;
    private fechaFin:DateTime;
    private horaInicio:Time;
    private horaFin:Time;
    private foto:string;
    private idLugar:number;

       /**
     * Retorna el nombre del evento
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre del evento
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

      /**
     * Retorna la descripcion del evento
     */
    get Descripcion() : string 
    {
        return this.descripcion;
    }

    /**
     * Establece la descripcion del evento
     */
    set Descripcion(descripcion : string) 
    {
        this.descripcion = descripcion;
    }

      /**
     * Retorna el id del lugar
     */
    get Idlugar() : number
    {
        return this.idLugar;
    }

    /**
     * Establece el id del lugar
     */
    set Idlugar(id : number) 
    {
        this.idLugar = id;
    }

    /**
     * Retorna el  costo
     */
    get Costo() : number 
    {
        return this.costo;
    }

    /**
     * Establece el costo
     */
    set Costo(costo : number) 
    {
        this.costo = costo;
    }

      /**
     * Retorna la ruta de la foto
     */
    get Foto() : string 
    {
        return this.nombre;
    }

    /**
     * Establece la ruta de la foto
     */
    set Foto(foto : string) 
    {
        this.foto = foto;
    }

      /**
     * Retorna la fecha de inicio del evento
     */
    get FechaInicio() : DateTime 
    {
        return this.fechaInicio;
    }

    /**
     * Establece la fecha inicio del evento
     */
    set FechaInicio(fecha:DateTime) 
    {
        this.fechaInicio = fecha;
    }

    /**
     * Retorna la fecha fin del evento
     */
    get FechaFin() : DateTime 
    {
        return this.fechaInicio;
    }

    /**
     * Establece la fecha fin del evento
     */
    set FechaFin(fecha:DateTime) 
    {
        this.fechaFin = fecha;
    }

    /**
     * Retorna la Hora de inicio del evento
     */
    get HoraInicio() : Time 
    {
        return this.horaInicio;
    }

    /**
     * Establece la hora inicio del evento
     */
    set HoraInicio(hora:Time) 
    {
        this.horaInicio = hora;
    }

    /**
     * Retorna la Hora fin del evento
     */
    get HoraFin() : Time 
    {
        return this.horaFin;
    }

    /**
     * Establece la hora fin del evento
     */
    set HoraFin(hora:Time) 
    {
        this.horaFin = hora;
    }
    
}
