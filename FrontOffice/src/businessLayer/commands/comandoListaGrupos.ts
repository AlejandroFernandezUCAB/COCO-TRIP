import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { catProd, catService, catErr } from '../../logs/config';

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
export class ComandoListaGrupos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaGrupos: any;

    public constructor(id : number,
        private servicio: RestapiService)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.listaGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaGrupos = datos;
            catProd.info('ListaGrupos exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaGrupos = error;
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