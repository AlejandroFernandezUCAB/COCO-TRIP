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
 * Solicita al servicio web la lista de notificaciones asociado al usuario
 */
export class ComandoListaNotificaciones extends Comando
{
    private id : number;

    private exito: boolean;
    private listaNotificaciones: any;

    private servicio: RestapiService;

    public constructor(id : number)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.listaNotificaciones(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaNotificaciones = datos;
            catProd.info('ListaNotificaciones exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaNotificaciones = error;
            catErr.info('Fallo de ListaNotificaciones. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaNotificaciones;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}