import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';

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

    public constructor(id : number, nombre : string, correo : string,
        private servicio: RestapiService)
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
            catProd.info('EnviarCorreo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de EnviarCorreo. Datos: ' + error);
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