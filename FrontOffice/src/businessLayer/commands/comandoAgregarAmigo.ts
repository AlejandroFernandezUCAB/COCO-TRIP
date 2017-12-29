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
 * Solicita la peticion de amistad del usuario asociado al identificador con el usuario asociado al nombre
 */
export class ComandoAgregarAmigo extends Comando
{
    private id : number;
    private nombreUsuario : string;

    private exito: boolean;

    public constructor(id : number, nombreUsuario : string,
                        private servicio: RestapiService)
    {
        super();

        this.id = id;
        this.nombreUsuario = nombreUsuario;
    }

    public execute(): void 
    {
        this.servicio.agregarAmigo(this.id, this.nombreUsuario)
        .then(datos => 
        {
            this.exito = true;
            catProd.info('AgregarAmigo exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            catErr.info('Fallo de AgregarAmigo. Datos: ' + error);
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