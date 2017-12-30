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
 * Solicita al servicio web agregar un integrante en el grupo
 */
@Injectable()
export class ComandoAgregarIntegrante extends Comando
{
    private idGrupo : number;
    private nombreUsuario : string;

    private exito: boolean;

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

    public execute() : void 
    {
        this.servicio.agregarIntegrante(this.idGrupo, this.nombreUsuario)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('AgregarIntegrante exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de AgregarIntegrante. Datos: ' + error);
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }

}