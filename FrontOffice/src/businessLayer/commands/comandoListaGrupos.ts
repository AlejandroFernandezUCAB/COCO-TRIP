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
 * Solicita al servicio web la lista de grupos asociados al usuario
 */
@Injectable()
export class ComandoListaGrupos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaGrupos : Array<Grupo>;
    
    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.listaGrupos = new Array<Grupo>();
    }

    public execute(): void 
    {
        this.servicio.listaGrupo(this.id)
        .then(datos => 
        {
            let lista : any = datos;            

            if(this.listaGrupos != undefined)
            {
              let cantidad : number = this.listaGrupos.length;
              
              for(let i = 0; i < cantidad; i++)
              {
                this.listaGrupos.pop();
              }
            }

            for(let grupo of lista)
            {
                if(grupo.RutaFoto == undefined)
                {
                    grupo.RutaFoto = ConfiguracionImages.DEFAULT_GROUP_PATH;
                }
                else
                {
                    grupo.RutaFoto = ConfiguracionImages.PATH + grupo.RutaFoto;
                }

                this.listaGrupos.push(grupo);
            }

            this.exito = true;
            catProd.info('ListaGrupos exitoso. Datos: ' + this.listaGrupos);
        }
        , error =>
        {
            this.exito = false;
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