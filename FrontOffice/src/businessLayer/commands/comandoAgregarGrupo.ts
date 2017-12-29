import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';

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
 * Solicita al servicio web agregar el nuevo grupo
 */
export class ComandoAgregarGrupo extends Comando
{
    private grupo : Grupo;
    private exito: boolean;

    private servicio: RestapiService;

    public constructor(idUsuario : number, nombreGrupo : string)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();
        this.grupo.Lider = idUsuario;
        this.grupo.Nombre = nombreGrupo;
    }

    public execute() : void 
    {
        this.servicio.agregarGrupo(this.grupo)
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