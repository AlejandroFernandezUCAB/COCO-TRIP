import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChatGrupo } from '../../dataAccessLayer/dao/daoChatGrupo';

export class ComandoCrearMensajeGrupo extends Comando {

    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO AGREGAR MENSAJE GRUPO");
        let DAO = FabricaDAO.crearFabricaDAOChatGrupo();
        DAO.agregar(this._entidad);
        
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }
    
}