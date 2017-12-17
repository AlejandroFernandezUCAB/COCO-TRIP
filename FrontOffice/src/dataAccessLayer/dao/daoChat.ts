import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';


export class DAOChat extends DAO {
    
    public agregar(entidad : Entidad): Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        return null;
    }

    visualizar() : Entidad{
        return null;
        
    }
    
        
    
    eliminar() : Entidad{
        return null;
        
    }

        

    modificar() : Entidad{
        return null;
    }
    
        
    

}