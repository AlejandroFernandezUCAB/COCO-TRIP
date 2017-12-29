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
 * Solicita el envio de un correo notificando al usuario sobre la peticion de amistad
 */
export class ComandoEnviarCorreo extends Comando
{
    private idUsuario : number;
    private nombreDestinatario : string;
    private correoDestinatario : string;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(id : number, nombre : string, correo : string)
    {
        super();

        this.idUsuario = id;
        this.nombreDestinatario = nombre;
        this.correoDestinatario = correo;
    }

    public execute(): void 
    {
        this.servicio.enviarCorreo(this.idUsuario, this.nombreDestinatario, this.correoDestinatario)
        .then(datos => 
        {
            this.exito = true;
        }
        , error =>
        {
            this.exito = false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
    
}