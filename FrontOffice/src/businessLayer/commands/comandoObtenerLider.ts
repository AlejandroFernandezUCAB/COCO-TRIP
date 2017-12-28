import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

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

    private servicio: RestapiService;

    public constructor(id : number)
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
        }
        , error =>
        {
            this.exito = false;
            this.usuario = error;
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