import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//******************************COMANDO MODIFICAR MENSAJE GRUPOS MODULO 6*****************************//
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
 * Comando Modificar un mensaje de grupos
 */
export class ComandoModificarMensajeGrupo extends Comando {
    private  _respuesta : Boolean;
    
/**
 * Ejecuta el comando
 */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoModificarMensajeGrupo");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        this._respuesta=DAO.modificarMensajeGrupo(this._entidad);
        catProd.info("Saliendo del metodo execute de comandoModificarMensajeGrupo"); 
    }

    /**
     * Obtiene la entidad
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
     * Obtiene el estatus del modificar
     */
    get getRespuesta():Boolean {
        return this._respuesta;
    }
    
}