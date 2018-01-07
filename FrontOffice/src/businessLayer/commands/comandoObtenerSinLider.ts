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
 * Solicita al servicio web la lista de miembros pertenecientes al grupo a excepcion del lider
 */
@Injectable()
export class ComandoObtenerSinLider extends Comando
{
    private id : number;

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
        return this.servicio.obtenerSinLider(this.id)
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
               if(usuario.Foto == undefined)
               {
                 usuario.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
               }
               else
               {
                 usuario.Foto = ConfiguracionImages.PATH + usuario.Foto;
               }

               this.listaMiembros.push(usuario);
            }

            catProd.info('ObtenerSinLider exitoso. Datos: ' + this.listaMiembros);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de ObtenerSinLider. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        return this.listaMiembros;
    }
}