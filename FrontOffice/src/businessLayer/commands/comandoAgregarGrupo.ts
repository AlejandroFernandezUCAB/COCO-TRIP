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
 * Solicita al servicio web agregar el nuevo grupo
 */
export class ComandoAgregarGrupo extends Comando
{
    private grupo : Grupo;
    private exito: boolean;

    public constructor(idUsuario : number, nombreGrupo : string,
        private servicio?: RestapiService)
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
            catProd.info('AgregarGrupo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de AgregarGrupo. Datos: ' + error);
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