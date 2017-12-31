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
 * Solicita al servicio web eliminar un integrante del grupo
 */
@Injectable()
export class ComandoEliminarIntegrante extends Comando
{
    private idGrupo : number;
    private nombreUsuario : string;

    set IdGrupo(idGrupo : number)
    {
        this.idGrupo = idGrupo;
    }

    set NombreUsuario (nombreUsuario : string)
    {
        this.nombreUsuario = nombreUsuario;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute()
    {
        return this.servicio.eliminarIntegrante(this.nombreUsuario, this.idGrupo)
        .then(datos => 
        {
            catProd.info('EliminarIntegrante exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de EliminarIntegrante. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}