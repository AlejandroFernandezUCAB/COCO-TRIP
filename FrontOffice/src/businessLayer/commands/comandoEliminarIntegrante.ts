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
 * Solicita al servicio web eliminar un integrante del grupo
 */
export class ComandoEliminarIntegrante extends Comando
{
    private idGrupo : number;
    private nombreUsuario : string;

    private exito: boolean;

    public constructor(idGrupo : number, nombreUsuario : string,
        private servicio: RestapiService)
    {
        super();

        this.idGrupo = idGrupo;
        this.nombreUsuario = nombreUsuario;
    }

    public execute() : void 
    {
        this.servicio.eliminarIntegrante(this.nombreUsuario, this.idGrupo)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('EliminarIntegrante exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de EliminarIntegrante. Datos: ' + error);
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