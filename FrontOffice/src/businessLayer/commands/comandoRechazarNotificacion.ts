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
 * Solicita al servicio web rechazar la notificacion (solicitud de amistad)
 */
export class ComandoRechazarNotificacion extends Comando
{
    private id : number;
    private nombreUsuario : string;

    private exito: boolean;

    set Id(id : number)
    {
        this.id = id;
    }

    set NombreUsuario(nombreUsuario : string)
    {
        this.nombreUsuario = nombreUsuario;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute(): void 
    {
        this.servicio.rechazarNotificacion(this.nombreUsuario, this.id)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('RechazarNotificacion exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de RechazarNotificacion. Datos: ' + error);
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