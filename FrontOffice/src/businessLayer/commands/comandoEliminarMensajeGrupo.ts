import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//*******************************COMANDO ELIMINAR MENSAJE GRUPOS MODULO 6*****************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Comando eliminar mensaje de grupos
 */
export class ComandoEliminarMensajeGrupo extends Comando {

    private  _respuesta : Boolean;

    /**
     * Ejecucion del comando
     */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoEliminarMensajeGrupo");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        this._respuesta = DAO.eliminarMensajeGrupo(this._entidad);
        catProd.info("Saliendo del metodo execute de comandoEliminarMensajeGrupo");
    }

    /**
     * Obtiene entidad
     */
    get getEntidad():Entidad {
        return this._entidad;
    }

    /**
     * Establece la entidad
     */
    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }

    /**
     * Obtiene el status del eliminar
     */
    get getRespuesta():Boolean {
        return this._respuesta;
    }
    
    public return() 
    {
        throw new Error("Method not implemented.");
    }
    
    public isSuccess(): boolean 
    {
        throw new Error("Method not implemented.");
    }
}