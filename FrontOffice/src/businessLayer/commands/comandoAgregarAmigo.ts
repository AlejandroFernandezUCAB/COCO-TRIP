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

    private exito: boolean;

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
            this.exito = true;
            catProd.info('AgregarAmigo exitoso. Datos: ' + datos);

            return this.exito;
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de AgregarAmigo. Datos: ' + error);

            return this.exito;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}