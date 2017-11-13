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

  agregarNuevoMensajeAmigo(mensaje,idAmigo,idRemitente) {
    if (this.conversacion) {
      this.fireConversacionChatsAmigo.child(idAmigo).push({
        enviadorPor: idRemitente,
        mensaje: mensaje,
        eliminado: false,
        modificado: false,
        fechaDeEnvio: firebase.database.ServerValue.TIMESTAMP
      })    
    }
  }

  agregarNuevoMensajeGrupo(mensaje,idGrupo,idRemitente) {
    if (this.conversacion) {
      this.fireConversacionChatsGrupo.child(idGrupo).push({
        enviadorPor: idRemitente,
        eliminado: false,
        modificado: false,
        mensaje: mensaje,
        fechaDeEnvio: firebase.database.ServerValue.TIMESTAMP
      })   
    }
  }

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

  modificarMensajeAmigo(idAmigo,idMensaje,mensajeModificado){
    if(this.conversacion){
      this.fireConversacionChatsAmigo.child(idAmigo).set({
        modificado: true,
        mensaje: "mensaje modificado:"+mensajeModificado,
        fechaDeModificacion: firebase.database.ServerValue.TIMESTAMP 
      });
    }
  }

  modificarMensajeGrupo(idGrupo,idMensaje,mensajeModificado){
    if(this.conversacion){  
      this.fireConversacionChatsGrupo.child(idGrupo).set({
        modificado: true,
        mensaje: "mensaje modificado:"+mensajeModificado,
        fechaDeModificacion: firebase.database.ServerValue.TIMESTAMP 
      });
    }
  }

  eliminarMensajeAmigo(idAmigo,idMensaje,mensajeModificado){
    if(this.conversacion){
      this.fireConversacionChatsAmigo.child(idAmigo).set({
        eliminado: true,
        mensaje: "mensaje eliminado",
        fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
      });
    }
  }

  eliminarMensajeGrupo(idGrupo,idMensaje){
    if(this.conversacion){
      this.fireConversacionChatsGrupo.child(idGrupo).set({
        eliminado: true,
        mensaje: "mensaje eliminado",
        fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
      });
    }
  }

  crearChatAmigo(idAmigo){
    this.fireConversacionChatsAmigo.child(idAmigo)
  }

  crearChatGrupo(idGrupo){
    this.fireConversacionChatsGrupo.child(idGrupo)
  }
  
  eliminarChatAmigo(idAmigo){
    this.fireConversacionChatsAmigo.remove(idAmigo)
  }

  eliminarChatGrupo(idGrupo){
    this.fireConversacionChatsGrupo.remove(idGrupo)
  }
}
