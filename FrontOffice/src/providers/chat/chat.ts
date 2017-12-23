import { Injectable } from '@angular/core';
import firebase from 'firebase';
import { Events } from 'ionic-angular';
import { Mensaje } from '../../dataAccessLayer/domain/mensaje';
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
  
    
agregarNuevoMensajeAmigo(mensaje,idEmisor,idReceptor) : String {
  var myRef = this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).push();
  var key = myRef.key;  

  var newData={
    key:key,
    enviadorPor: idEmisor,
    receptor: idReceptor,
    mensaje: mensaje,
    eliminado: false,
    modificado: false,
    tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP  
   }
   myRef.set(newData);
   this.fireConversacionChatsAmigo.child(idReceptor).child(idEmisor).child(key).set({
    key:key,
    enviadorPor: idEmisor,
    receptor: idReceptor,
    mensaje: mensaje,
    eliminado: false,
    modificado: false,
    tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP        
  });

  return key;
}  

    
   
  agregarNuevoMensajeGrupo(mensaje,idGrupo,emisor) {
    var myRef = this.fireConversacionChatsGrupo.child(idGrupo).push();
    var key = myRef.key;
  
    var newData={
      key:key,
      enviadorPor: emisor,
      eliminado: false,
      modificado: false,
      mensaje: mensaje,
      tiempoDeEnvio: firebase.database.ServerValue.TIMESTAMP
     }
  
     myRef.set(newData);
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

obtenerMensajesConversacionAmigo(idEmisor,idReceptor) {
  let temp;
  this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).on('value', (snapshot) => {
    this.mensajesConversacion = [];
    temp = snapshot.val();
    for (var tempkey in temp) {
      console.log("temp[tempkey]: "+temp[tempkey].key);
      this.mensajesConversacion.push(temp[tempkey]);
    }
    this.events.publish('nuevoMensajeAmigo',this.mensajesConversacion);
  })
}

/*obtenerInfoMensajeAmigo(idEmisor,idReceptor,idMensaje) {
  let temp;
  let entidad: Mensaje;
  let otro:Mensaje;
  
  return this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).child(idMensaje).once('value').then( function(snapshot) {
    this.mensajesConversacion = [];
    temp = snapshot.val(); 
    entidad = new Mensaje(temp.mensaje,temp.enviadorPor,"",
    0,temp.tiempoDeEnvio,0,temp.modificado);
    entidad.setId=temp.key;
    alert("dentro"+entidad.getMensaje);
    return entidad;
  });
  /*alert("fuera"+entidad.getMensaje);
  return entidad;

}*/

  obtenerMensajesConversacionGrupo(idGrupo) {
    let temp;
    this.fireConversacionChatsGrupo.child(idGrupo).on('value', (snapshot) => {
      this.mensajesConversacion = [];
      temp = snapshot.val();
      for (var tempkey in temp) {
        this.mensajesConversacion.push(temp[tempkey]);
      }
      this.events.publish('nuevoMensajeGrupo', this.mensajesConversacion);
    })
  }

  obtenerInfoMensajeAmigo(idEmisor,idReceptor,idMensaje) {
    let temp;
    let entidad: Mensaje;
    let otro: Mensaje;
    this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).child(idMensaje).on('value', (snapshot) =>{
      var temp = snapshot.val(); 
      entidad = new Mensaje(temp.mensaje,temp.enviadorPor,"",0,temp.tiempoDeEnvio,0,temp.modificado);
      entidad.setId=temp.key;
      this.events.publish('infoMensaje',entidad);
      
      //alert("dentro"+entidad.getMensaje);
      //return entidad;
      //alert("dentro"+temp.mensaje);
    });
  
  }

  obtenerInfoMensajeGrupo(idgrupo,idMensaje) {
    let temp;
    let entidad: Mensaje;
    let otro: Mensaje;
    this.fireConversacionChatsGrupo.child(idgrupo).child(idMensaje).on('value', (snapshot) =>{
      var temp = snapshot.val(); 
      entidad = new Mensaje(temp.mensaje,temp.enviadorPor,"",0,temp.tiempoDeEnvio,0,temp.modificado);
      entidad.setId=temp.key;
      this.events.publish('infoMensajeGrupo',entidad);
    });
  
  }
  modificarMensajeAmigo(idEmisor,idReceptor,idMensaje,mensajeModificado){

    this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      enviadorPor: idEmisor,
      mensaje: "(modificado): "+mensajeModificado,
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });
    this.fireConversacionChatsAmigo.child(idReceptor).child(idEmisor).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      enviadorPor: idEmisor,
      mensaje: "(modificado): "+mensajeModificado,
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    }); 
    return true;
  }

  modificarMensajeGrupo(idGrupo,idMensaje,mensajeModificado,emisor){
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      key:idMensaje,
      modificado: true,
      enviadorPor: emisor,
      mensaje: "(modificado): "+mensajeModificado,
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });
    return true;
  }

  eliminarMensajeAmigo(idEmisor,idReceptor,idMensaje) : boolean{
    this.fireConversacionChatsAmigo.child(idEmisor).child(idReceptor).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: idEmisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });
    this.fireConversacionChatsAmigo.child(idReceptor).child(idEmisor).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: idEmisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP 
    });   
    return true;
  }

  eliminarMensajeGrupo(idGrupo,idMensaje,emisor) : boolean{
    this.fireConversacionChatsGrupo.child(idGrupo).child(idMensaje).set({
      key:-1,
      eliminado: true,
      enviadorPor: emisor,
      mensaje: "mensaje eliminado",
      fechaDeEliminacion: firebase.database.ServerValue.TIMESTAMP
    });
    return true;
  }

}
