import { Entidad } from './entidad';
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Jorge Marin
 */

/**
 * Descripcion de la clase:
 * Entidad que contiene los datos de las localidades turisticas
 */
export class LugarTuristico extends Entidad
{
    private nombre:string;
    private costo:number;
    private descripcion:string;
    private direccion:string;
    private correo:string;
    private telefono:number;
    private latitud:number;
    private longitud:number;
    private activar:Boolean;
    private pk_lugar:number;

    constructor () 
    {
        super();
    }     

     /**
     * Retorna el nombre del lugar
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre del lugar
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
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
     * Retorna el descripcion del lugar
     */
    get Descripcion() : string 
    {
        return this.descripcion;
    }

    /**
     * Establece el nombre del grupo
     */
    set Descripcion(descripcion : string) 
    {
        this.descripcion = descripcion;
    }

    /**
     * Retorna el direccion del lugar
     */
    get Direccion() : string 
    {
        return this.direccion;
    }

    /**
     * Establece el direccion del lugar
     */
    set Direccion(direccion : string) 
    {
        this.direccion = direccion;
    }

    /**
     * Retorna el correo del lugar
     */
    get Correo() : string 
    {
        return this.correo;
    }

    /**
     * Establece el correo del lugar
     */
    set Correo(correo : string) 
    {
        this.correo = correo;
    }

    /**
     * Retorna el telefono del lugar
     */
    get Telefono() : number 
    {
        return this.telefono;
    }

    /**
     * Establece el telefono del lugar
     */
    set Telefono(telefono : number) 
    {
        this.telefono = telefono;
    }

    /**
     * Retorna el latitud del lugar
     */
    get Latitud() : number 
    {
        return this.latitud;
    }

    /**
     * Establece el latitud del lugar
     */
    set Latitud(latitud : number) 
    {
        this.latitud = latitud;
    }

    /**
     * Retorna el longitud del lugar
     */
    get Longitud() : number 
    {
        return this.Longitud;
    }

    /**
     * Establece el direccion del lugar
     */
    set Longitud(longitud : number) 
    {
        this.longitud = longitud;
    }

    /**
     * Retorna el activar el lugar
     */
    get Activar() : Boolean 
    {
        return this.activar;
    }

    /**
     * Establece el  activar el lugar
     */
    set Activar(activar : Boolean) 
    {
        this.activar = activar;
    }

    /**
     * Retorna el lugar padre
     */
    get LugarID() : number 
    {
        return this.pk_lugar;
    }

    /**
     * Establece el lugar padre
     */
    set LugarID(lugarPadre:number) 
    {
        this.pk_lugar = lugarPadre;
    }

}