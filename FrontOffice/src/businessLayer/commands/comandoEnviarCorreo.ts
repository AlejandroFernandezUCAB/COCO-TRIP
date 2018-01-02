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
 * Solicita el envio de un correo notificando al usuario sobre la peticion de amistad
 */
@Injectable()
export class ComandoEnviarCorreo extends Comando
{
    private idUsuario : number;
    private nombreDestinatario : string;
    private correoDestinatario : string;

    set IdUsuario(id : number)
    {
        this.idUsuario = id;
    }

    set NombreUsuario(nombreUsuario : string)
    {
        this.nombreDestinatario = nombreUsuario;
    }

    set Correo(correo : string)
    {
        this.correoDestinatario = correo;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute()
    {
        return this.servicio.enviarCorreo(this.idUsuario, this.nombreDestinatario, this.correoDestinatario)
        .then(datos => 
        {
            catProd.info('EnviarCorreo exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de EnviarCorreo. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}