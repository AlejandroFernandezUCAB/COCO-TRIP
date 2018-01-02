import { Entidad } from './entidad';
import { DateTime } from 'ionic-angular/components/datetime/datetime';
import { Time } from '@angular/common/src/i18n/locale_data_api';
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
export class Actividad extends Entidad
{
    private nombre:string;
    private foto:string;
    private duracion:Time;
    private descripcion:string;
    private activar:boolean
    private idLugar : number;

      /**
     * Retorna el nombre de la actividad
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre de la actividad
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

      /**
     * Retorna el nombre de la foto
     */
    get Foto() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre de la foto
     */
    set Foto(foto : string) 
    {
        this.foto = foto;
    }

      /**
     * Retorna la duracion de la actividad
     */
    get Duracion() : Time 
    {
        return this.Duracion;
    }

    /**
     * Establece la duracion de la actividad
     */
    set Duracion(duracion : Time) 
    {
        this.duracion = duracion;
    }

      /**
     * Retorna la descripcion de la actividad
     */
    get Descripcion() : string 
    {
        return this.descripcion;
    }

    /**
     * Establece la descripcion de la actividad
     */
    set Descripcion(descripcion : string) 
    {
        this.descripcion = descripcion;
    }

      /**
     * Retorna el estado  de la actividad
     */
    get Activar() : boolean 
    {
        return this.activar;
    }

    /**
     * Establece el estado de la actividad
     */
    set Actividad(activar : boolean) 
    {
        this.activar = activar;
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
}