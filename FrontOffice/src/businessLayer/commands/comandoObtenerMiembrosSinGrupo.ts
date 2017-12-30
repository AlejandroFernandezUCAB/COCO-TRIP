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
 * Solicita al servicio web los datos de los miembros asociados al usuario sin grupo
 */
@Injectable()
export class ComandoObtenerMiembrosSinGrupo extends Comando
{
    private idUsuario : number;
    private idGrupo : number;

    private listaUsuarios = new Array();

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

                this.listaUsuarios.push(usuario);
            }

            this.exito = true;
            catProd.info('MiembrosSinGrupo exitoso. Datos: ' + this.listaUsuarios);
        }
        , error =>
        {
            this.exito = false;
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