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
 * Solicita al servicio web la lista de grupos asociados al usuario
 */
@Injectable()
export class ComandoListaGrupos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaGrupos: any;
    
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
        this.servicio.listaGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaGrupos = datos;
            catProd.info('ListaGrupos exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaGrupos = error;
            catErr.info('Fallo de ListaGrupos. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaGrupos;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}