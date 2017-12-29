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
 * Solicita al servicio web el ultimo grupo creado por el usuario
 */
export class ComandoObtenerUltimoGrupo extends Comando
{
    private id : number;

    private exito: boolean;
    private grupo: any;

    public constructor(id : number,
        private servicio?: RestapiService)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.obtenerUltimoGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.grupo = datos;
            catProd.info('ObtenerUltimoGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.grupo = error;
            catErr.info('Fallo de ObtenerUltimoGrupo. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.grupo;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}