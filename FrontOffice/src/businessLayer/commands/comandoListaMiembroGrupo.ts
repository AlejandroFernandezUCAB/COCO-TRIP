import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';
import { ConfiguracionImages } from '../../pages/constantes/configImages';

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
    private listaMiembros = new Array();

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
            let lista : any = datos;

            for(let usuario of lista)
            {
               if(lista.Foto == undefined)
               {
                 lista.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
               }
               else
               {
                 lista.Foto = ConfiguracionImages.PATH + lista.Foto;
               }

               this.listaMiembros.push(usuario);
            }

            this.exito = true;
            catProd.info('ListaMiembroGrupo exitoso. Datos: ' + this.listaMiembros);
        }
        , error =>
        {
            this.exito = false;
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