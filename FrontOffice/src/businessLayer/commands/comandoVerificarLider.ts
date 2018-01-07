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
 * Solicita al servicio web verificar si el usuario actual es el lider del grupo
 */
@Injectable()
export class ComandoVerificarLider extends Comando
{
    private idGrupo : number;
    private idUsuario : number;

    set IdUsuario(id : number)
    {
        this.idUsuario = id;
    }

    set IdGrupo(id : number)
    {
        this.idGrupo = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute()
    {
        return this.servicio.verificarLider(this.idGrupo, this.idUsuario)
        .then(datos => 
        {
            catProd.info('VerificarLider exitoso (es el lider). Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catProd.info('Fallo de VerificarLider (no es el lider o error interno). Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}