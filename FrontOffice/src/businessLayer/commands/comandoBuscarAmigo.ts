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
 * Solicita al servicio web la lista de usuarios asociados a la busqueda
 */
@Injectable()
export class ComandoBuscarAmigo extends Comando
{
    private id : number;
    private nombre : string;

    private exito: boolean;
    private listaUsuarios = new Array();

    set Id(id : number)
    {
        this.id = id;
    }

    set Nombre(nombre : string)
    {
        this.nombre = nombre;
    }

    public constructor(private servicio: RestapiService)
    {
        super();
    }

    public execute(): void 
    {
        this.servicio.buscarAmigos(this.nombre, this.id)
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
            catProd.info('BuscarAmigos exitoso. Datos: ' + this.listaUsuarios);
        }
        , error =>
        {
            this.exito = false;
            this.listaUsuarios = error;
            catErr.info('Fallo de BuscarAmigos. Datos: ' + error);
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