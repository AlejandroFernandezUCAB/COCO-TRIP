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
 * Solicita al servicio web la lista de notificaciones asociado al usuario
 */
@Injectable()
export class ComandoListaNotificaciones extends Comando
{
    private id : number;

    private exito: boolean;
    private listaNotificaciones = new Array();

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
        this.servicio.listaNotificaciones(this.id)
        .then(datos => 
        {
            let lista : any = datos;

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

               this.listaNotificaciones.push(usuario);
            }

            this.exito = true;
            catProd.info('ListaNotificaciones exitoso. Datos: ' + this.listaNotificaciones);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de ListaNotificaciones. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaNotificaciones;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}