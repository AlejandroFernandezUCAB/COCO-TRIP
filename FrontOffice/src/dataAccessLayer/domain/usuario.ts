import { Entidad } from './entidad';
import { DateTime } from 'ionic-angular/components/datetime/datetime';
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
 * Entidad que contiene los datos de los usuarios
 */
export class Usuario extends Entidad
{
    private nombreUsuario: string;
    private nombre: string;
    private apellido: string;
    private correo: string;
    private foto: string;
    private genero: string;
    private clave: string;
    private valido: boolean;
    private fechaNacimiento: DateTime;

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre de usuario
     */
    get NombreUsuario() : string 
    {
        return this.nombreUsuario;
    }

    /**
     * Establece el nombre de usuario
     */
    set NombreUsuario(nombreUsuario : string) 
    {
        this.nombreUsuario = nombreUsuario;
    }
    
    /**
     * Retorna el nombre
     */
    get Nombre() : string 
    {
        return this.nombre;
    }

    /**
     * Establece el nombre
     */
    set Nombre(nombre : string) 
    {
        this.nombre = nombre;
    }

    /**
     * Retorna el apellido
     */
    get Apellido() : string 
    {
        return this.apellido;
    }

    /**
     * Establece el apellido
     */
    set Apellido(apellido : string) 
    {
        this.apellido = apellido;
    }

    /**
     * Retorna el correo
     */
    get Correo() : string 
    {
        return this.correo;
    }

    /**
     * Establece el correo
     */
    set Correo(correo : string) 
    {
        this.correo = correo;
    }

    /**
     * Retorna la ruta de la foto
     */
    get Foto() : string 
    {
        return this.foto;
    }

    /**
     * Establece el apellido
     */
    set Foto(foto : string)
    {
        this.foto = foto;
    }

    /**
     * Retorna el genero del usuario
     */
    get Genero() : string 
    {
        return this.genero;
    }

    /**
     * Establece el genero del usuario
     */
    set Genero(genero : string)
    {
        this.genero = genero;
    }

    /**
     * Retorna la clave del usuario
     */
    get Clave() : string 
    {
        return this.clave;
    }

    /**
     * Establece la clave del usuario
     */
    set Clave(clave : string)
    {
        this.clave = clave;
    }

    /**
     * Retorna la confirmacion del usuario
     */
    get Valido() : boolean
    {
        return this.valido;
    }

    /**
     * Establece la confirmacion del usuario
     */
    set Valido(valido : boolean)
    {
        this.valido = valido;
    }

    /**
     * Retorna la fecha de nacimiento del usuario
     */
    get FechaNacimiento() : DateTime
    {
        return this.fechaNacimiento;
    }

    /**
     * Establece la fecha de nacimiento del usuario
     */
    set FechaNacimiento(fechaNacimiento : DateTime)
    {
        this.fechaNacimiento = fechaNacimiento;
    }
}