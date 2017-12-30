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
 * Solicita al servicio web la lista de amigos asociado al usuario
 */
export class ComandoListaAmigos extends Comando
{
    private id : number;

    private exito: boolean;
    private listaAmigos: any;

    set Id(id : number)
    {
        this.id = id;
    }
    
    public constructor(private servicio : RestapiService)
    {
        super();
    }

    public execute(): void 
    {
        this.servicio.listaAmigos(this.id)
        .then(datos => 
        {
            this.exito = true;
            this.listaAmigos = datos;
            catProd.info('ListaAmigos exitoso. Datos: ' + datos);
        }
        , error =>
        {
            this.exito = false;
            this.listaAmigos = error;
            catErr.info('Fallo de Listamaigos. Datos: ' + error);
        });
    }

    public return() 
    {
        return this.listaAmigos;
    }

    public isSuccess(): boolean 
    {
        return this.exito;
    }
}