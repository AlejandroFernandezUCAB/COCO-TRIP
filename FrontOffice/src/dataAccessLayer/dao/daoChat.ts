import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';
import { Events } from 'ionic-angular';

export class DAOChat extends DAO {

    public agregar(entidad : Entidad): Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        mensaje.setId = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo); 
        return mensaje;
    }

    public agregarMensajeGrupo(entidad : Entidad): Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        mensaje.setId = chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        return mensaje;
    }

    visualizar(entidad: Entidad):Entidad{
        return null;
    }

    
    visualizarLista(entidad: Entidad, events: Events) : Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerMensajesConversacionAmigo(mensaje.getUsuario,mensaje.getAmigo);
        
        return null;

    }
    
    visualizarListaGrupo(entidad: Entidad, events: Events) : Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerMensajesConversacionGrupo(mensaje.getidGrupo);
        
        return null;
    }

    informacionMensajeAmigo(entidad: Entidad, events: Events) : Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerInfoMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId);
        return null;
    }
    informacionMensajeGrupo(entidad: Entidad, events: Events) : Entidad{
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerInfoMensajeGrupo(mensaje.getidGrupo,mensaje.getId);
        return null;
    }
    
        
    
    eliminar(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId);
        return respuesta;
    }

    eliminarMensajeGrupo(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeGrupo(mensaje.getidGrupo,mensaje.getId,mensaje.getUsuario); 
        return respuesta;
    }
        

    modificar(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.modificarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId,mensaje.getMensaje);
        return respuesta;
    }
    modificarMensajeGrupo(entidad : Entidad) : boolean{
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.modificarMensajeGrupo(mensaje.getidGrupo,mensaje.getId,mensaje.getMensaje,mensaje.getUsuario); 
        return respuesta;
    }
    
        
    

}