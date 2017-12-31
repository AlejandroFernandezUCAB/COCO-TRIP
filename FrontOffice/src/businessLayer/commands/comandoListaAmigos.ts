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
 * Solicita al servicio web la lista de amigos asociado al usuario
 */
@Injectable()
export class ComandoListaAmigos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaUsuarios : Array<Usuario>;

    set Id(id : number)
    {
        this.id = id;
    }
    
    public constructor(private servicio : RestapiService)
    {
        super();

        this.listaUsuarios = new Array<Usuario>();
    }

    public execute()
    {
        return this.servicio.listaAmigos(this.id)
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

            this.exito = true;
            catProd.info('ListaAmigos exitoso. Datos: ' + this.listaUsuarios);

            return this.exito;
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de ListaAmigos. Datos: ' + error);

            return this.exito;
        });
    }

    public return() 
    {
        return this.listaUsuarios;
    }
    
}