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
 * Solicita al servicio web la lista de miembros pertenecientes al grupo
 */
export class ComandoListaMiembroGrupo extends Comando
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
        this.servicio.listaMiembroGrupo(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaMiembros = datos;
        }
        , error =>
        {
            this.exito = false;
            this.listaMiembros = error;
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