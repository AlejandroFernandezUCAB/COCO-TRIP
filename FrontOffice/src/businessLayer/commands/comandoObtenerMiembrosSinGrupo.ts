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
 * Solicita al servicio web los datos de los miembros asociados al usuario sin grupo
 */
@Injectable()
export class ComandoObtenerMiembrosSinGrupo extends Comando
{
    private idUsuario : number;
    private idGrupo : number;

    private listaUsuarios : Array<Usuario>;

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

        this.listaUsuarios = new Array<Usuario>();
    }

    public execute()
    {
        return this.servicio.obtenerMiembrosSinGrupo(this.idUsuario, this.idGrupo)
        .then(datos => 
        {
            let lista : any = datos;
            
            if(this.listaUsuarios != undefined)
            {
              let cantidad : number = this.listaUsuarios.length;
              
              for(let i = 0; i < cantidad; i++)
              {
                this.listaUsuarios.pop();
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

                this.listaUsuarios.push(usuario);
            }

            catProd.info('MiembrosSinGrupo exitoso. Datos: ' + this.listaUsuarios);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de MiembrosSinGrupo. Datos: ' + error);
            return false;
        });
    }

    public return() 
    {
        return this.listaUsuarios;
    }
}