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
 * Solicita al servicio web aceptar la notificacion (solicitud de amistad)
 */
export class ComandoAceptarNotificacion extends Comando
{
    private id : number;
    private nombreUsuario : string;

    private exito: boolean;

    public constructor(nombreUsuario : string, id : number,
                        private servicio: RestapiService)
    {
        super();

        this.id = id;
        this.nombreUsuario = nombreUsuario;
    }

    public execute(): void 
    {
        this.servicio.aceptarNotificacion(this.nombreUsuario, this.id)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('AceptarNotificacion exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de AcpetarNotificacion. Datos: ' + error);
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