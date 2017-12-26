import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//**********************************COMANDO MODIFICAR MENSAJE MODULO 6********************************//
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
 * Comando Modificar un mensaje
 */
export class ComandoModificarMensaje extends Comando {
    private  _respuesta : Boolean;
    
/**
 * Ejecuta el comando
 */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoModificarMensaje");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        this._respuesta=DAO.modificar(this._entidad);
        catProd.info("Saliendo del metodo execute de comandoModificarMensaje");
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