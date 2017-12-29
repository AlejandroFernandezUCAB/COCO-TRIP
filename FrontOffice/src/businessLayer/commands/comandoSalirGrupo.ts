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
 * Solicita al servicio web eliminar el grupo (si es el lider) o eliminar el usuario del grupo
 */
export class ComandoSalirGrupo extends Comando
{
    private idGrupo : number;
    private idUsuario : number;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(idGrupo : number, idUsuario : number)
    {
        super();

        this.idGrupo = idGrupo;
        this.idUsuario = idUsuario;
    }

    public execute() : void 
    {
        this.servicio.salirGrupo(this.idUsuario, this.idGrupo)
        .then(datos => 
        {
            this.exito = true;
        }
        , error =>
        {
            this.exito = false;
        });
    }

    public return() 
    {
        throw new Error("Method not implemented.");
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }

}