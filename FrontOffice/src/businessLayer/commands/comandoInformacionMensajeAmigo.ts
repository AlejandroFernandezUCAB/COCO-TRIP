import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';
import { Events } from 'ionic-angular';
import {catService,catProd} from "../../logs/config"

//****************************************************************************************************//
//********************************COMANDO INFORMACION MENSAJE MODULO 6********************************//
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
 * Comando informacion de mensaje
 */
export class ComandoInformacionMensajeAmigo extends Comando 
{
    public _events : Events;

/**
 * Ejecuta el comando
 */
    public execute(): void {
        catProd.info("Entrando en el metodo execute de comandoInformacionMensaje");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.visualizar(this._entidad, this._events);
        catProd.info("Saliendo del metodo execute de comandoInformacionMensaje");
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
     * Establece un evento
     */
    set setEvents(events:Events) {
        this._events = events;
    }
    
    public return() 
    {
        throw new Error("Method not implemented.");
    }
    
}