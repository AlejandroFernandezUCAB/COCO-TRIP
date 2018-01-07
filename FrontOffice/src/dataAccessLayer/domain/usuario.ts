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
    private NombreUsuario: string;
    private Nombre: string;
    private Apellido: string;
    private Correo: string;
    private Foto: string;
    private Genero: string;
    private Clave: string;
    private Valido: boolean;
    private FechaNacimiento: Date;

    constructor () //Por ahora vacio
    {
        super();
    } 

    /**
     * Retorna el nombre de usuario
     */
    get getNombreUsuario() : string 
    {
        return this.NombreUsuario;
    }

    /**
     * Establece el nombre de usuario
     */
    set setNombreUsuario(nombreUsuario : string) 
    {
        this.NombreUsuario = nombreUsuario;
    }
    
    /**
     * Retorna el nombre
     */
    get getNombre() : string 
    {
        return this.Nombre;
    }

    /**
     * Establece el nombre
     */
    set setNombre(nombre : string) 
    {
        this.Nombre = nombre;
    }

    /**
     * Retorna el apellido
     */
    get getApellido() : string 
    {
        return this.Apellido;
    }

    /**
     * Establece el apellido
     */
    set setApellido(apellido : string) 
    {
        this.Apellido = apellido;
    }

    /**
     * Retorna el correo
     */
    get getCorreo() : string 
    {
        return this.Correo;
    }

    /**
     * Establece el correo
     */
    set setCorreo(correo : string) 
    {
        this.Correo = correo;
    }

    /**
     * Retorna la ruta de la foto
     */
    get getFoto() : string 
    {
        return this.Foto;
    }

    /**
     * Establece el apellido
     */
    set setFoto(foto : string)
    {
        this.Foto = foto;
    }

    /**
     * Retorna el genero del usuario
     */
    get getGenero() : string 
    {
        return this.Genero;
    }

    /**
     * Establece el genero del usuario
     */
    set setGenero(genero : string)
    {
        this.Genero = genero;
    }

    /**
     * Retorna la clave del usuario
     */
    get getClave() : string 
    {
        return this.Clave;
    }

    /**
     * Establece la clave del usuario
     */
    set setClave(clave : string)
    {
        this.Clave = clave;
    }

    /**
     * Retorna la confirmacion del usuario
     */
    get getValido() : boolean
    {
        return this.Valido;
    }

    /**
     * Establece la confirmacion del usuario
     */
    set setValido(valido : boolean)
    {
        this.Valido = valido;
    }

    /**
     * Retorna la fecha de nacimiento del usuario
     */
    get getFechaNacimiento() : Date
    {
        return this.FechaNacimiento;
    }

    /**
     * Establece la fecha de nacimiento del usuario
     */
    set setFechaNacimiento(fechaNacimiento : Date)
    {
        this.FechaNacimiento = fechaNacimiento;
    }
}