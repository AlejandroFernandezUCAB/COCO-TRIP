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
 * Solicita la peticion de amistad del usuario asociado al identificador con el usuario asociado al nombre
 */
@Injectable()
export class ComandoAgregarAmigo extends Comando
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
    
    public constructor(private servicio : RestapiService)
    {
        super();
    }

    public execute()
    {
        return this.servicio.agregarAmigo(this.id, this.nombreUsuario)
        .then(datos => 
        {
            catProd.info('AgregarAmigo exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de AgregarAmigo. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}