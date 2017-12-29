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
 * Solicita al servicio web verificar si el usuario actual es el lider del grupo
 */
export class ComandoVerificarLider extends Comando
{
    private idGrupo : number;
    private idUsuario : number;

    private exito: boolean;

    public constructor(idGrupo : number, idUsuario : number,
        private servicio?: RestapiService)
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
            catProd.info('VerificarLider exitoso (es el lider). Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catProd.info('Fallo de VerificarLider (no es el lider o error interno). Datos: ' + error);
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