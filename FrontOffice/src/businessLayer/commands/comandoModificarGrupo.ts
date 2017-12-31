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

    set IdUsuario(id : number)
    {
        this.idUsuario = id;
    }

    set IdGrupo(id : number)
    {
        this.grupo.setId = id;
    }

    set Nombre(nombre : string)
    {
        this.grupo.setNombre = nombre;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();

        this.grupo.setId = 0;
        this.grupo.setNombre = null;
        this.grupo.setContenidoFoto = null;
        this.grupo.setRutaFoto = null;
        this.grupo.setLider = 0;
        this.grupo.setCantidadIntegrantes = 0;
    }

    public execute()
    {
        return this.servicio.modificarGrupo(this.grupo, this.idUsuario)
        .then(datos => 
        {
            catProd.info('ModificarGrupo exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catProd.info('Fallo de ModificarGrupo (no autorizado o error interno). Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }
}