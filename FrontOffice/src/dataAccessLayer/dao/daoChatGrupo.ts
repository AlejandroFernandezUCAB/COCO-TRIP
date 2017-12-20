import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';


export class DAOChatGrupo extends DAO {
    chat:ChatProvider;
    
    public agregar(entidad : Entidad){
       return null;
    }

    visualizar() : Entidad{
        return null;
        
    }
    
        
    
    eliminar(entidad : Entidad) : boolean{
        return null;
        
    }

        

    modificar(entidad : Entidad) : Entidad{
        return null;
    }
    
        
    

}