import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChatGrupo } from '../../dataAccessLayer/dao/daoChatGrupo';

export class ComandoModificarMensajeGrupo extends Comando {

    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO MODIFICAR MENSAJE GRUPO");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.modificarMensajeGrupo(this._entidad);

        
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }
    
}