import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';

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
@Injectable()
export class ComandoRechazarNotificacion extends Comando
{
    private id : number;
    private nombreUsuario : string;
    
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

    public execute()
    {
        return this.servicio.rechazarNotificacion(this.nombreUsuario, this.id)
        .then(datos => 
        {            
            catProd.info('RechazarNotificacion exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de RechazarNotificacion. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}