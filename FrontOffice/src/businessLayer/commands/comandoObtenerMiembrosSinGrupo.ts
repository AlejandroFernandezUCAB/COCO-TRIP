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
 * Solicita al servicio web los datos de los miembros asociados al usuario sin grupo
 */
@Injectable()
export class ComandoObtenerMiembrosSinGrupo extends Comando
{
    private idUsuario : number;
    private idGrupo : number;

    private listaUsuarios : any;

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

    public execute(): void 
    {
        this.servicio.obtenerMiembrosSinGrupo(this.idUsuario, this.idGrupo)
        .then(datos => 
        {
            this.exito = true;
            this.listaUsuarios = datos;
            catProd.info('MiembrosSinGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaUsuarios = error;
            catErr.info('Fallo de MiembrosSinGrupo. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaUsuarios;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
    
}