import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';

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

    private grupo: Grupo;

    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();
    }

    public execute()
    {
        return this.servicio.obtenerUltimoGrupo(this.id)
        .then(datos => 
        {
            let grupo : any = datos;
            
            this.grupo.setId = grupo.Id;
            console.log("en comando id: " + this.grupo.getId);
            catProd.info('ObtenerUltimoGrupo exitoso. Datos: ' + this.grupo);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de ObtenerUltimoGrupo. Datos: ' + error);
            return false;
        })
    }

    public return() 
    {
        return this.grupo;
    }
}