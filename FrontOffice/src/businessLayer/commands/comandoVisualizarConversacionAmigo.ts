import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';

export class ComandoVisualizarConversacionAmigo extends Comando {

    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO VISUALIZAR MENSAJE");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.visualizar();
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }
    
}