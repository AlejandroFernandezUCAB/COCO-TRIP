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
 * Solicita al servicio web la lista de miembros pertenecientes al grupo a excepcion del lider
 */
export class ComandoObtenerSinLider extends Comando
{
    private id : number;

    private exito: boolean;
    private listaMiembros: any;

    private servicio: RestapiService;

    public constructor(id : number)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.obtenerSinLider(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaMiembros = datos;
            catProd.info('ObtenerSinLider exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaMiembros = error;
            catErr.info('Fallo de ObtenerSinLider. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaMiembros;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}