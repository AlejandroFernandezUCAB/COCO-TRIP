import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
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
 * Solicita al servicio web modificar el nombre del grupo
 */
@Injectable()
export class ComandoModificarGrupo extends Comando
{
    private grupo : Grupo;
    private idUsuario : number;

    private exito: boolean;

    set IdUsuario(id : number)
    {
        this.idUsuario = id;
    }

    set IdGrupo(id : number)
    {
        this.grupo.Id = id;
    }

    set Nombre(nombre : string)
    {
        this.grupo.Nombre = nombre;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();
    }

    public execute() : void 
    {
        this.servicio.modificarGrupo(this.grupo, this.idUsuario)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('ModificarGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catProd.info('Fallo de ModificarGrupo (no autorizado o error interno). Datos: ' + error);
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