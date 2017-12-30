import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';
import { Grupo } from '../../dataAccessLayer/domain/grupo';

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
 * Solicita al servicio web el ultimo grupo creado por el usuario
 */
@Injectable()
export class ComandoObtenerUltimoGrupo extends Comando
{
    private id : number;

    private exito: boolean;
    private grupo: Grupo;

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
        this.servicio.obtenerUltimoGrupo(this.id)
        .then(datos => 
        {
            let grupo : any = datos;

            this.grupo.Id = grupo.Id;
            this.grupo.Nombre = grupo.Nombre;
            this.grupo.RutaFoto = grupo.RutaFoto;
            
            this.exito = true;
            catProd.info('ObtenerUltimoGrupo exitoso. Datos: ' + this.grupo);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de ObtenerUltimoGrupo. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.grupo;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}