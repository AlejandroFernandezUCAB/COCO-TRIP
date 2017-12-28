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
 * Solicita al servicio web verificar si el usuario actual es el lider del grupo
 */
export class ComandoVerificarLider extends Comando
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
        this.servicio.verificarLider(this.idGrupo, this.idUsuario)
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