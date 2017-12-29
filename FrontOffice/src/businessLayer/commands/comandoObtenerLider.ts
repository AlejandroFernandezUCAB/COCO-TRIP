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
 * Solicita al servicio web los datos del lider del grupo asociado
 */
export class ComandoObtenerLider extends Comando
{
    private id : number;

    private exito: boolean;
    private usuario: any;

    public constructor(id : number,
        private servicio: RestapiService)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.obtenerLider(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.usuario = datos;
            catProd.info('ObtenerLider exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.usuario = error;
            catErr.info('Fallo de ObtenerLider. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.usuario;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}