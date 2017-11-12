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
  fireConversacionChats = firebase.database().ref('/conversacionesChats');
  conversacion: any;
  mensajesConversacion = [];
  constructor(public events: Events) {
  }

  inicializarConversacion(conversacion){
    this.conversacion = conversacion;
  }

  agregarNuevoMensaje(mensaje,idRemitente) {
    if (this.conversacion) {
      var promise = new Promise((resolve, reject) => {
        this.fireConversacionChats.child(idRemitente).child(this.conversacion.uid).push({
          enviadorPor: idRemitente,
          mensaje: mensaje,
          tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP
        }).then(() => {
          this.fireConversacionChats.child(this.conversacion.uid).child(idRemitente).push({
            enviadorPor: idRemitente,
            mensaje: mensaje,
            tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP
          }).then(() => {
            resolve(true);
            })//.catch((err) => {
              //reject(err);
          //})
        })
      })
      return promise;
    }
  }

  obtenerMensajesConversacion(idRemitente) {
    
    let temp;
    this.fireConversacionChats.child(idRemitente).child(this.conversacion.uid).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish('nuevoMensaje');
    })
  }

}
