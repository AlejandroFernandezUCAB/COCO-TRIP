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
            catProd.info('SalirGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de SalirGrupo. Datos: ' + error);
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