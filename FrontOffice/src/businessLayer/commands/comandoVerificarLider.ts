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

    private exito: boolean;

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute()
    {
        return this.servicio.verificarLider(this.idGrupo, this.idUsuario)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('VerificarLider exitoso (es el lider). Datos: ' + datos);

            return this.exito;
        }
        , error =>
        {
            this.exito = false;
            catProd.info('Fallo de VerificarLider (no es el lider o error interno). Datos: ' + error);

            return this.exito;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}