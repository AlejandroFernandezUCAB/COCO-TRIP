import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';

export class ComandoEliminarMensaje extends Comando {

    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO ELIMINAR MENSAJE");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.eliminar();
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }
    
}