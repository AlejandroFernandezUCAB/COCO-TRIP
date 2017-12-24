import { Mensaje } from '../domain/mensaje';
import { Entidad } from '../domain/entidad';
import { DAO } from './dao';
import { ChatProvider } from './../../providers/chat/chat';
import firebase from 'firebase';
import { Events } from 'ionic-angular';
import {catService,catProd} from "../../logs/config"
//****************************************************************************************************//
//**********************************Dao chat de MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Dao chat
 * 
 */
export class DAOChat extends DAO {

/**
 * Descripcion del metodo:
 * Metodo que se encarga de agregar un nuevo mensaje del amigo por medio del provider chat.ts
 * 
 */
    public agregar(entidad : Entidad): Entidad{
        catProd.info("Entrando en el metodo agregar de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        mensaje.setId = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo); 
        catProd.info("Saliendo del metodo agrega de DAO ");
        return mensaje;
        
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de agregar un nuevo mensaje al grupo por medio del provider chat.ts
 * 
 */    

    public agregarMensajeGrupo(entidad : Entidad): Entidad{
        catProd.info("Entrando en el metodo agregarMensajeGrupo de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        mensaje.setId = chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        catProd.info("Saliendo del metodo agregarMensajeGrupo de DAOChat");
        return mensaje;
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de visualizar el detalle del mensaje por medio del provider chat.ts
 * 
 */  
    visualizar(entidad: Entidad, events: Events):Entidad{
        catProd.info("Entrando en el metodo visualizar de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerInfoMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId);
        catProd.info("Saliendo del metodo visualizar de DAOChat");
        return null;
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de visualizar el detalle del mensaje del grupo por medio del provider chat.ts
 * 
 */
    visualizarGrupo(entidad: Entidad, events: Events):Entidad{
        catProd.info("Entrando en el metodo visualizarGrupo de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerInfoMensajeGrupo(mensaje.getidGrupo,mensaje.getId);
        catProd.info("Saliendo del metodo visualizarGrupo de DAOChat");
        return null;
    }
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de visualizar la lista de mensajes del chat por medio del provider chat.ts
 * 
 */    
    visualizarLista(entidad: Entidad, events: Events) : Entidad{
        catProd.info("Entrando en el metodo visualizarlista de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerMensajesConversacionAmigo(mensaje.getUsuario,mensaje.getAmigo);
        catProd.info("Saliendo del metodo visualizarLista de DAOChat");
        return null;
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de visualizar la lista de mensajes del chat del grupo por medio del provider chat.ts
 * 
 */ 
    
    visualizarListaGrupo(entidad: Entidad, events: Events) : Entidad{
        catProd.info("Entrando en el metodo visualizarListaGrupo de DAOChat");
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(events);
        chat.obtenerMensajesConversacionGrupo(mensaje.getidGrupo);
        catProd.info("Saliendo del metodo visualizarListaGrupo de DAOChat");
        return null;
    }
        
/**
 * Descripcion del metodo:
 * Metodo que se encarga de eliminar el mensaje con la id obtenida por medio del provider chat.ts
 * 
 */ 
    eliminar(entidad : Entidad) : boolean{
        catProd.info("Entrando en el metodo eliminar de DAOChat");
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId);
        catProd.info("Saliendo del metodo eliminar de DAOChat");
        return respuesta;
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de eliminar el mensaje del grupo con la id obtenida por medio del provider chat.ts
 * 
 */ 

    eliminarMensajeGrupo(entidad : Entidad) : boolean{
        catProd.info("Entrando en el metodo eliminarMensajeGrupo de DAOChat");
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.eliminarMensajeGrupo(mensaje.getidGrupo,mensaje.getId,mensaje.getUsuario); 
        catProd.info("Saliendo del metodo eliminarMensajeGrupo de DAOChat");
        return respuesta;
    }
        
/**
 * Descripcion del metodo:
 * Metodo que se encarga de modificar el mensaje con la id obtenida por medio del provider chat.ts
 * 
 */ 
    modificar(entidad : Entidad) : boolean{
        catProd.info("Entrando en el metodo modificar de DAOChat");
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.modificarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,mensaje.getId,mensaje.getMensaje);
        catProd.info("Saliendo del metodo modificar de DAOChat");
        return respuesta;
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de modificar el mensaje del grupo con la id obtenida por medio del provider chat.ts
 * 
 */ 
    modificarMensajeGrupo(entidad : Entidad) : boolean{
        catProd.info("Entrando en el metodo modificarMensajeGrupo de DAOChat");
        let respuesta : boolean;
        respuesta = false;
        let mensaje = <Mensaje> entidad;
        let chat : ChatProvider;
        chat = new ChatProvider(null);
        respuesta = chat.modificarMensajeGrupo(mensaje.getidGrupo,mensaje.getId,mensaje.getMensaje,mensaje.getUsuario); 
        catProd.info("Saliendo del metodo modificarMensajeGrupo de DAOChat");
        return respuesta;
    }
    
}