import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChatGrupo } from '../../dataAccessLayer/dao/daoChatGrupo';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//*******************************COMANDO CREAR MENSAJE GRUPOMODULO 6**********************************//
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
 * Comando Crear Mensaje de Grupos
 */
export class ComandoCrearMensajeGrupo extends Comando {

    
/**
 * Ejecuta el comando
 */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoAgregarMensajeGrupo");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.agregarMensajeGrupo(this._entidad);
        catProd.info("Saliendo del metodo execute de comandoAgregarMensajeGrupo");
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
    
}