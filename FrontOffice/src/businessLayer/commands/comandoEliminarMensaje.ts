import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';

export class ComandoEliminarMensaje extends Comando {
    private  _respuesta : Boolean;
    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO ELIMINAR MENSAJE AMIGO");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        this._respuesta = DAO.eliminar(this._entidad);
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }

    get getRespuesta():Boolean {
        return this._respuesta;
    }


    
}