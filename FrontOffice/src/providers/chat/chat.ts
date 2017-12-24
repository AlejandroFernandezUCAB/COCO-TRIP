import { Injectable } from '@angular/core';
import firebase from 'firebase';
import { Events } from 'ionic-angular';
import { Mensaje } from '../../dataAccessLayer/domain/mensaje';
import { Registry } from '../../common/registry';
import * as moment from 'moment';
import {catService,catProd} from "../../logs/config"
//****************************************************************************************************//
//*****************************************CLASE COMANDO MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Clase que se encarga de hacer las llamadas a la base de datos
 */
@Injectable()
export class ChatProvider {
  fireConversacionChatsAmigo = firebase.database().ref(Registry.REF_BASE_DATOS_AMIGOS);//.ref('/chatAmigo');
  fireConversacionChatsGrupo = firebase.database().ref(Registry.REF_BASE_DATOS_GRUPOS);//.ref('/chatGrupo');
  conversacion: any;
  mensajesConversacion = [];

  private static instancia : ChatProvider;


  
  constructor(public events: Events) {

  }


  public static obtenerInstancia (events: Events) : ChatProvider{
    if(this.instancia == null){
      this.instancia = new ChatProvider(events);
    }
    return this.instancia;
  }

  inicializarConversacion(conversacion){
    this.conversacion = conversacion;
  }
  
/**
 * Metodo que se encarga de agregar un nuevo mensaje a la base de datos
 * @param mensaje Texto del mensaje
 * @param idEmisor Identificador del emisor
 * @param idReceptor Identificador del receptor
 */    
agregarNuevoMensajeAmigo(mensaje,idEmisor,idReceptor) : String {
  catProd.info("Entrando en el metodo agregarNuevoMensajeAmigo de chat.ts");
  var myRef = this.fireConversacionChatsAmigo.child(idEmisor)
  .child(idReceptor).push();
  var key = myRef.key;

  var newData={
    key:key,
    enviadorPor: idEmisor,
    receptor: idReceptor,
    mensaje: mensaje,
    eliminado: false,
    modificado: false,
    tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a')
   }
   myRef.set(newData);
   this.fireConversacionChatsAmigo.child(idReceptor)
   .child(idEmisor).child(key).set({
    key:key,
    enviadorPor: idEmisor,
    receptor: idReceptor,
    mensaje: mensaje,
    eliminado: false,
    modificado: false,
    tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a')
  });
  catProd.info("Saliendo del metodo agregarNuevoMensajeAmigo de chat.ts");
  return key;
}  

    
 /**
  * Metodo que se encarga de agregar un nuevo mensaje de grupo en la base de datos
  * @param mensaje Texto del mensaje
  * @param idGrupo Identificador del grupo
  * @param emisor Identificador del emisor
  */  
  agregarNuevoMensajeGrupo(mensaje,idGrupo,emisor) : String {
    catProd.info("Entrando en el metodo agregarNuevoMensajeGrupo de chat.ts");
    var myRef = this.fireConversacionChatsGrupo.child(idGrupo).push();
    var key = myRef.key;
  
    var newData={
      key:key,
      enviadorPor: emisor,
      eliminado: false,
      modificado: false,
      mensaje: mensaje,
      tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a')
     }
  
     myRef.set(newData);
     catProd.info("Saliendo del metodo agregarNuevoMensajeGrupo de chat.ts");
     return key;
    }
  

/**
 * Metodo que se encarga de obtener la lista de los mensajes existentes entre
 * dos usuarios
 * @param idEmisor Identificador del emisor
 * @param idReceptor Identificador del receptor
 */
obtenerMensajesConversacionAmigo(idEmisor,idReceptor) {
  catProd.info("Entrando en el metodo obtenerMensajesConversacionAmigo de chat.ts");
  let temp;
  this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).on('value', (snapshot) => {
    this.mensajesConversacion = [];
    temp = snapshot.val();
    for (var tempkey in temp) {
      this.mensajesConversacion.push(temp[tempkey]);
    }
    this.events.publish(Registry.PUBLISH_LISTA_MENSAJE_AMIGOS,this.mensajesConversacion);
  })
  catProd.info("Saliendo del metodo obtenerMensajesConversacionAmigo de chat.ts");
}


/**
 * Metodo que se encarga de obtener la lista de los mensajes existentes en un grupo
 * @param idGrupo Identificador del grupo
 */
  obtenerMensajesConversacionGrupo(idGrupo) {
    catProd.info("Entrando en el metodo obtenerMensajesConversacionGrupo de chat.ts");
    let temp;
    this.fireConversacionChatsGrupo.child(idGrupo).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish(Registry.PUBLISH_LISTA_MENSAJE_GRUPOS, this.mensajesConversacion);
    })
    catProd.info("Saliendo del metodo obtenerMensajesConversacionAmigo de chat.ts");
  }

  /**
   * Metodo que se encarga de obtener la informacion de un mensaje en 
   * una conversacion entre dos amigos
   * @param idEmisor Identificador del emisor
   * @param idReceptor Identificador del receptor
   * @param idMensaje Identificador del mensaje
   */
  obtenerInfoMensajeAmigo(idEmisor,idReceptor,idMensaje) {
    catProd.info("Entrando en el metodo obtenerInfoMensajeAmigo de chat.ts");
    let temp;
    let entidad: Mensaje;
    let otro: Mensaje;
    this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor)
    .child(idMensaje).on('value', (snapshot) =>{
      var temp = snapshot.val(); 
      entidad = new Mensaje(temp.mensaje,temp.enviadorPor,
        "",0,temp.tiempoDeEnvio,0,temp.modificado);
      entidad.setId=temp.key;
      this.events.publish(Registry.PUBLISH_INFO_MENSAJE_AMIGOS,entidad);
    });
    catProd.info("Saliendo del metodo obtenerInfoMensajeAmigo de chat.ts");
  }

  /**
   * Metodo que se encarga de obtener la informacion de un mensaje
   * en una conversacion de grupos
   * @param idgrupo Identificador del grupo
   * @param idMensaje Identificador del mensaje
   */
  obtenerInfoMensajeGrupo(idgrupo,idMensaje) {
    catProd.info("Entrando en el metodo obtenerInfoMensajeGrupo de chat.ts");
    let temp;
    let entidad: Mensaje;
    let otro: Mensaje;
    this.fireConversacionChatsGrupo.child(idgrupo).child(idMensaje)
    .on('value', (snapshot) =>{
      var temp = snapshot.val(); 
      entidad = new Mensaje(temp.mensaje,temp.enviadorPor,"",
      0,temp.tiempoDeEnvio,0,temp.modificado);
      entidad.setId=temp.key;
      this.events.publish(Registry.PUBLISH_INFO_MENSAJE_GRUPOS,entidad);
    });
    catProd.info("Saliendo del metodo obtenerInfoMensajeGrupo de chat.ts");
  }

  /**
   * Metodo que se encarga de modificar un mensaje en una conversacion
   * entre amigos
   * @param idEmisor Identificador del emisor
   * @param idReceptor Identificador del receptor
   * @param idMensaje Identificador del mensaje
   * @param mensajeModificado Texto del mensaje modificado
   */
  modificarMensajeAmigo(idEmisor,idReceptor,idMensaje,mensajeModificado):boolean{
    catProd.info("Entrando en el metodo modificarMensajeAmigo de chat.ts");
    this.fireConversacionChatsAmigo.child(idEmisor)
    .child(idReceptor).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      eliminado: false,
      enviadorPor: idEmisor,
      receptor: idReceptor,
      mensaje: "(modificado): "+mensajeModificado,
      tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a') 
    });
    this.fireConversacionChatsAmigo.child(idReceptor).child(idEmisor).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      eliminado: false,
      enviadorPor: idEmisor,
      mensaje: "(modificado): "+mensajeModificado,
      tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a') 
    }); 
    catProd.info("Saliendo del metodo modificarMensajeAmigo de chat.ts");
    return true;
  }

  /**
   * Metodo que se encarga de modificar un mensaje en una conversacion de grupos
   * @param idGrupo Identificador del grupo
   * @param idMensaje Identificador del mensaje
   * @param mensajeModificado Texto del mensaje modificado
   * @param emisor Identificador del emisor
   */
  modificarMensajeGrupo(idGrupo,idMensaje,mensajeModificado,emisor):boolean{
    catProd.info("Entrando en el metodo modificarMensajeGrupo de chat.ts");
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      eliminado: false,
      enviadorPor: emisor,
      mensaje: "(modificado): "+mensajeModificado,
      tiempoDeEnvio: moment().format('MMMM Do YYYY, h:mm:ss a') 
    });
    catProd.info("Saliendo del metodo modificarMensajeGrupo de chat.ts");
    return true;
  }

/**
 * Metodo que se encarga de eliminar un mensaje en una conversacion de amigos
 * @param idEmisor Identificador del emisor
 * @param idReceptor Identificador del receptor
 * @param idMensaje Identificador del mensaje
 */
  eliminarMensajeAmigo(idEmisor,idReceptor,idMensaje) : boolean{
    catProd.info("Entrando en el metodo eliminarMensajeAmigo de chat.ts");
    this.fireConversacionChatsAmigo.child(idEmisor)
    .child(idReceptor).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: idEmisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: moment().format('MMMM Do YYYY, h:mm:ss a')  
    });
    this.fireConversacionChatsAmigo.child(idReceptor).child(idEmisor).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: idEmisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: moment().format('MMMM Do YYYY, h:mm:ss a')  
    });  
    catProd.info("Saliendo del metodo eliminarMensajeAmigo de chat.ts"); 
    return true;
  }

  /**
   * Metodo que se encarga de eliminar un mensaje en una conversacion de grupos
   * @param idGrupo Identificador del grupo
   * @param idMensaje Idenficador del mensaje
   * @param emisor Identificador del emisor
   */
  eliminarMensajeGrupo(idGrupo,idMensaje,emisor) : boolean{
    catProd.info("Entrando en el metodo eliminarMensajeGrupo de chat.ts");
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: emisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: moment().format('MMMM Do YYYY, h:mm:ss a')  
    });
    catProd.info("Saliendo del metodo eliminarMensajeGrupo de chat.ts"); 
    return true;
  }

}
