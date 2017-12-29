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
 * Solicita al servicio web agregar un integrante en el grupo
 */
export class ComandoAgregarIntegrante extends Comando
{
    private idGrupo : number;
    private nombreUsuario : string;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(idGrupo : number, nombreUsuario : string)
    {
        super();

        this.idGrupo = idGrupo;
        this.nombreUsuario = nombreUsuario;
    }

    public execute() : void 
    {
        this.servicio.agregarIntegrante(this.idGrupo, this.nombreUsuario)
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