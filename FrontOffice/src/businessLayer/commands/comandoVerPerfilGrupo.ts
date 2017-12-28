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
 * Solicita al servicio web los datos asociados al identificador del grupo
 */
export class ComandoVerPerfilGrupo extends Comando
{
    private id : number;

    private exito: boolean;
    private grupo: any;

    private servicio: RestapiService;

    public constructor(id : number)
    {
        super();

        this.id = id;
    }

    public execute(): void 
    {
        this.servicio.verPerfilGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.grupo = datos;
        }
        , error =>
        {
            this.exito = false;
            this.grupo = error;
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