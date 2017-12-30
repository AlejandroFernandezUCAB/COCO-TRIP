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
 * Solicita al servicio web la lista de miembros pertenecientes al grupo
 */
@Injectable()
export class ComandoListaMiembroGrupo extends Comando
{
    private id : number;

    private exito: boolean;
    private listaMiembros: any;

    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute(): void 
    {
        this.servicio.listaMiembroGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaMiembros = datos;
            catProd.info('ListaMiembroGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaMiembros = error;
            catErr.info('Fallo de ListaMiembroGrupo. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaMiembros;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}