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
 * Solicita al servicio web el perfil publico del identificador de usuario asociado
 */
@Injectable()
export class ComandoObtenerPerfilPublico extends Comando
{
    private nombreUsuario : string;

    private usuario : Array<Usuario>;

    set NombreUsuario(nombreUsuario : string)
    {
        this.nombreUsuario = nombreUsuario;
    }

    public constructor(private servicio: RestapiService)
    {
        super();

        this.usuario = new Array<Usuario>();
    }

    public execute() 
    {
        return this.servicio.obtenerPerfilPublico(this.nombreUsuario)
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

            catProd.info('ObtenerPerfilPublico exitoso. Datos: ' + this.usuario); 
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de ObtenerPerfilPublico. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        return this.usuario;
    }   
}