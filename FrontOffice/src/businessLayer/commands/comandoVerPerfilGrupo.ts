import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';
import { ConfiguracionImages } from '../../pages/constantes/configImages';
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
 * Solicita al servicio web los datos asociados al identificador del grupo
 */
@Injectable()
export class ComandoVerPerfilGrupo extends Comando
{
    private id : number;

    private exito: boolean;
    private grupo : Array<Grupo>;

    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.grupo = Array<Grupo>();
    }

    public execute(): void 
    {
        this.servicio.verPerfilGrupo(this.id)
        .then(datos => 
        {
            let grupo : any = datos;

            if(grupo.RutaFoto == undefined)
            {
              grupo.RutaFoto = ConfiguracionImages.DEFAULT_GROUP_PATH;
            }
            else
            {
              grupo.RutaFoto = ConfiguracionImages.PATH + grupo.RutaFoto;
            }

            if(this.grupo != undefined)
            {
              this.grupo.pop();
            }

            this.grupo.push(grupo);
            
            this.exito = true;
            catProd.info('VerPerfilGrupo exitoso. Datos: ' + this.grupo);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de VerPerfilGrupo. Datos: ' + error);
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