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
 * Solicita al servicio web los datos del lider del grupo asociado
 */
@Injectable()
export class ComandoObtenerLider extends Comando
{
    private id : number;

    private usuario : Array<Usuario>;

    set Id(id : number)
    {
        this.id = id;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.usuario = new Array<Usuario>();
    }

    public execute()
    {
        return this.servicio.obtenerLider(this.id)
        .then(datos => 
        {
            let usuario : any = datos;

            if(usuario.Foto == undefined)
            {
              usuario.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
            }
            else
            {
              usuario.Foto = ConfiguracionImages.PATH + usuario.Foto;
            }

            if(this.usuario != undefined)
            {
              this.usuario.pop();
            }

            this.usuario.push(usuario);

            catProd.info('ObtenerLider exitoso. Datos: ' + this.usuario);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de ObtenerLider. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        return this.usuario;
    }
}