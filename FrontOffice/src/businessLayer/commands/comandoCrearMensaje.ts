import { Mensaje } from './../../dataAccessLayer/domain/mensaje';
import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//**********************************COMANDO CREAR MENSAJE MODULO 6************************************//
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
 * Comando Crear Mensaje
 */

export class ComandoCrearMensaje extends Comando {

    
/**
 * Ejecuta el comando
 */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoAgregarMensaje");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.agregar(this._entidad);
        catProd.info("Saliendo del metodo execute de comandoAgregarMensaje");
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