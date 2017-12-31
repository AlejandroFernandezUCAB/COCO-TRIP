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
 * Solicita al servicio web eliminar el grupo (si es el lider) o eliminar el usuario del grupo
 */
@Injectable()
export class ComandoSalirGrupo extends Comando
{
    private idGrupo : number;
    private idUsuario : number;

    private exito: boolean;

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
        return this.servicio.salirGrupo(this.idUsuario, this.idGrupo)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('SalirGrupo exitoso. Datos: ' + datos);

            return this.exito;
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de SalirGrupo. Datos: ' + error);

            return this.exito;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}