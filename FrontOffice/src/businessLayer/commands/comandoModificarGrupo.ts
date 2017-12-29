import { Comando } from './comando';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
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
 * Solicita al servicio web modificar el nombre del grupo
 */
export class ComandoModificarGrupo extends Comando
{
    private grupo : Grupo;
    private idUsuario : number;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(nombreGrupo : string, idUsuario : number, idGrupo : number)
    {
        super();

        this.grupo = FabricaEntidad.crearGrupo();
        this.grupo.Id = idGrupo;
        this.grupo.Nombre = nombreGrupo;
        this.idUsuario = idUsuario;
    }

    public execute() : void 
    {
        this.servicio.modificarGrupo(this.grupo, this.idUsuario)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('ModificarGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catProd.info('Fallo de ModificarGrupo (no autorizado o error interno). Datos: ' + error);
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