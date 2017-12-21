import { Mensaje } from './../../dataAccessLayer/domain/mensaje';
import { FabricaDAO } from '../factory/fabricaDao';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import  { Comando } from './comando';
import { DAOChat } from '../../dataAccessLayer/dao/daoChat';

export class ComandoCrearMensaje extends Comando {

    

    public execute(): void {
        console.log("ENTRANDO EN EXECUTE DE COMANDO AGREGAR MENSAJE");
        let DAO = FabricaDAO.crearFabricaDAOChat();
        DAO.agregar(this._entidad);
        
    }

    
    get getEntidad():Entidad {
        return this._entidad;
    }

    set setEntidad(entidad:Entidad) {
        this._entidad = entidad;
    }


    
}