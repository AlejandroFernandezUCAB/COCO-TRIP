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
 * Solicita al servicio web agregar el nuevo grupo
 */
@Injectable()
export class ComandoAgregarGrupo extends Comando
{
    private grupo : Grupo;

    set Lider(id : number)
    {
        this.grupo.Lider = id;
    }

    set Nombre(nombre : string)
    {
        this.grupo.Nombre = nombre;
    }

    public constructor(private servicio : RestapiService)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();
    }

    public execute()
    {
        return this.servicio.agregarGrupo(this.grupo)
        .then(datos => 
        {
            catProd.info('AgregarGrupo exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de AgregarGrupo. Datos: ' + error);
            return false;
        })
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    } 
    
}