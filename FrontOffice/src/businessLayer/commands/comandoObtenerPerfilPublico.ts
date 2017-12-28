import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Joaquin Camacho
 */

//*************************************************************************************//
//*************************************MODULO 3****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web el perfil publico del identificador de usuario asociado
 */
export class ComandoObtenerPerfilPublico extends Comando
{
    private nombreUsuario : string;

    private exito: boolean;
    private usuario: any;

    private servicio: RestapiService;

    public constructor(nombreUsuario : string)
    {
        super();

        this.nombreUsuario = nombreUsuario;
    }

    public execute(): void 
    {
        this.servicio.obtenerPerfilPublico(this.nombreUsuario)
        .then(datos => 
        {
            this.exito = true;
            this.usuario = datos;
        }
        , error =>
        {
            this.exito = false;
            this.usuario = error;
        });
    }

    public return() 
    {
        return this.usuario;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}