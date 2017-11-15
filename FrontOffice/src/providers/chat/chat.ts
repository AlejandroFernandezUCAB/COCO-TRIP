import { Injectable } from '@angular/core';
import firebase from 'firebase';
import { Events } from 'ionic-angular';
/*
  Generated class for the ChatProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class ChatProvider {
  fireConversacionChatsAmigo = firebase.database().ref('/chatAmigo');
  fireConversacionChatsGrupo = firebase.database().ref('/chatGrupo');
  conversacion: any;
  mensajesConversacion = [];
  constructor(public events: Events) {
  }

  inicializarConversacion(conversacion){
    this.conversacion = conversacion;
  }
/*
  agregarNuevoMensajeAmigo(mensaje,idAmigo,idRemitente) {
    if (this.conversacion) {
      var promise = new Promise((resolve, reject) => {
        this.fireConversacionChatsAmigo.child(idAmigo).push({
          enviadorPor: idRemitente,
          mensaje: mensaje,
          eliminado: false,
          modificado: false,
          tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP
        })
      })
      return promise;
    }
  }
*/
  
    
agregarNuevoMensajeAmigo(mensaje,idReceptor,idRemitente) {
  this.fireConversacionChatsAmigo.child(idRemitente).child(idRemitente).push({
    enviadorPor: idRemitente,
    mensaje: mensaje,
    eliminado: false,
    modificado: false,
    tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP         
  });
}  
    
   
  agregarNuevoMensajeGrupo(mensaje,idGrupo,idRemitente) {
    this.fireConversacionChatsGrupo.child(idGrupo).push({
      enviadorPor: idRemitente,
      eliminado: false,
      modificado: false,
      mensaje: mensaje,
      tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP
    });      
    }
  
/*
  obtenerMensajesConversacionAmigo(idAmigo) {
    let temp;
    this.fireConversacionChatsAmigo.child(idAmigo).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish('nuevoMensaje');
    })
  }
*/

  obtenerMensajesConversacionAmigo(idRemitente,idEmisor) {
    let temp;
    this.fireConversacionChatsAmigo.child(idRemitente).child(idEmisor).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish('nuevoMensaje');
    })
  }
  obtenerMensajesConversacionGrupo(idGrupo) {
    let temp;
    this.fireConversacionChatsGrupo.child(idGrupo).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish('nuevoMensaje');
    })
  }

  modificarMensajeAmigo(idRemitente,idEmisor,idMensaje,mensajeModificado){
    this.fireConversacionChatsAmigo.child(idRemitente).child(idEmisor).child(idMensaje).set({
      modificado: true,
      mensaje: "mensaje modificado:"+mensajeModificado,
      fechaDeModificacion: firebase.database.ServerValue.TIMESTAMP 
    });
    this.fireConversacionChatsAmigo.child(idEmisor).child(idRemitente).child(idMensaje).set({
      modificado: true,
      mensaje: "mensaje modificado:"+mensajeModificado,
      fechaDeModificacion: firebase.database.ServerValue.TIMESTAMP 
    });
    
  }

  modificarMensajeGrupo(idGrupo,idMensaje,mensajeModificado){  
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      modificado: true,
      mensaje: "mensaje modificado:"+mensajeModificado,
      fechaDeModificacion: firebase.database.ServerValue.TIMESTAMP 
    });    
  }

  eliminarMensajeAmigo(idRemitente,idEmisor,idMensaje,mensajeModificado){
    this.fireConversacionChatsAmigo.child(idRemitente).child(idEmisor).child(idMensaje).set({
      eliminado: true,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });
    this.fireConversacionChatsAmigo.child(idEmisor).child(idRemitente).child(idMensaje).set({
      eliminado: true,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });    
  }

  eliminarMensajeGrupo(idGrupo,idMensaje){
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      eliminado: true,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });
  }

  crearChatAmigo(idRemitente,idEmisor){
    this.fireConversacionChatsAmigo.child(idRemitente).child(idEmisor);
  }

  crearChatGrupo(idGrupo){
    this.fireConversacionChatsGrupo.child(idGrupo)
  }
  
  eliminarChatAmigo(idRemitente,idEmisor){
    this.fireConversacionChatsAmigo.child(idRemitente).remove(idEmisor);
  }

  eliminarChatGrupo(idGrupo){
    this.fireConversacionChatsGrupo.remove(idGrupo)
  }
}
