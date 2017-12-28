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
 * Solicita al servicio web los datos de los miembros asociados al usuario sin grupo
 */
export class ComandoObtenerMiembrosSinGrupo extends Comando
{
    private idUsuario : number;
    private idGrupo : number;

    private listaUsuarios : any;

    private exito: boolean;

    private servicio: RestapiService;

    public constructor(idUsuario : number, idGrupo : number)
    {
        super();

        this.idUsuario = idUsuario;
        this.idGrupo = idGrupo;
    }

    public execute(): void 
    {
        this.servicio.obtenerMiembrosSinGrupo(this.idUsuario, this.idGrupo)
        .then(datos => 
        {
            this.exito = true;
            this.listaUsuarios = datos;
        }
        , error =>
        {
            this.exito = false;
            this.listaUsuarios = error;
        });
    }

    public return() 
    {
        return this.listaUsuarios;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
    
}