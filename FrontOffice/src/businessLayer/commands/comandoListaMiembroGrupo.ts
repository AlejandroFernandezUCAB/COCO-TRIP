import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';
import { Injectable } from '@angular/core';
import { ConfiguracionImages } from '../../pages/constantes/configImages';
import { Usuario } from '../../dataAccessLayer/domain/usuario';

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
    private listaMiembros : Array<Usuario>;

    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.listaMiembros = new Array<Usuario>();
    }

    public execute()
    {
        return this.servicio.listaMiembroGrupo(this.id)
        .then(datos => 
        {
            let lista : any = datos;

            if(this.listaMiembros != undefined)
            {
              let cantidad : number = this.listaMiembros.length;
              
              for(let i = 0; i < cantidad; i++)
              {
                this.listaMiembros.pop();
              }
            }

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

            return this.exito;
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de ListaMiembroGrupo. Datos: ' + error);

            return this.exito;
        });
    }

    public return() 
    {
        return this.listaMiembros;
    }
    
}