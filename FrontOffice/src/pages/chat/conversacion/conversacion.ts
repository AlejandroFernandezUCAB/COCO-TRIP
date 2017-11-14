import { Component, ViewChild, NgZone } from '@angular/core';
import { Platform, ActionSheetController, Events, Content } from 'ionic-angular';
import { IonicPage, NavParams, NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import * as moment from 'moment';
import { Firebase } from '@ionic-native/firebase';
import { ChatProvider } from '../../../providers/chat/chat';

@IonicPage()
@Component({
    selector: 'page-conversacion',
    templateUrl: 'conversacion.html'
})

export class ConversacionPage {
  @ViewChild('content') content: Content;
  conversacion: any;
  nuevoMensaje: any;
  idAmigo: any;
  idGrupo: any;
  idUsuario: any;
  todosLosMensajes = [];
  //mensajes: Array<msgs> = [
   // {contenido: '¡Adoro este sitio!', tiempo: moment().fromNow() }
  //];

constructor(public navCtrl: NavController, public navParams: NavParams, public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController, public platform: Platform, private firebase: Firebase , public chatService: ChatProvider, public events: Events, public zone: NgZone) {
  this.conversacion = this.chatService.conversacion;
  //this.scrollto();
  this.idUsuario =
  this.events.subscribe('nuevoMensaje', () => {
    this.todosLosMensajes = [];
    this.zone.run(() => {
      this.todosLosMensajes = this.chatService.mensajesConversacion;
    })
  })
 // this.firebase.getToken()
    //.then(token => console.log(`El token push es ${token}`)) //se guarda el token del lado del servidor y se usa para enviar notificaciones push.
    //.catch(error => console.log('Error obteniendo el token', error));

  //this.firebase.onTokenRefresh()
    //.subscribe((token: string) => console.log(`He obtenido un nuevo token ${token}`));

}

tapEvent1(){
  //let alert = this.alertCtrl.create({ ESTA ERA UNA ALERTA DE FUNCIONALIDAD
    //title: 'Ver Perfil',
    //message: 'Pronto visualizarás perfiles por aquí',
    //buttons: [
      //{
        //text:'Dismiss',
        //role: 'dismiss',
        //handler: () => {
          //console.log('Alerta visualizada');
        //}
      //}
    //]
  //});
  //alert.present();
  this.navCtrl.push(VisualizarPerfilPage); //PERMITE VER EL PERFIL DEL AMIGO
}

tapEvent2(){
  let alert = this.alertCtrl.create({ //ESTA ES UNA ALERTA DE FUNCIONALIDAD
    title: 'Enviar Mensaje',
    message: 'Pronto enviarás mensajes por aquí',
    buttons: [
      {
        text:'Dismiss',
        role: 'dismiss',
        handler: () => {
          console.log('Alerta visualizada');
        }
      }
    ]
  });
  alert.present();
}

pressEvent1(){
  let actionSheet = this.actionsheetCtrl.create({
    title: 'Opciones del chat',
    cssClass: 'action-sheets-basic-page',
    buttons: [
      {
        text: 'Editar mensaje',
        icon: !this.platform.is('ios') ? 'create' : null,
        handler: () => {
          let decision = this.alertCtrl.create({
            message: '¿Deseas editar este mensaje?',
            buttons: [
              {
                text: 'Sí',
                handler: () => {
                  console.log('Decisión de editar afirmativa');
                }
              },
              {
                text: 'No',
                handler: () => {
                  console.log('Decisión de editar negativa');
                }
              }
            ]
          });
          decision.present()
        }
      },
      {
        text: 'Eliminar mensaje',
        role: 'destructive', // aplica color rojo de alerta
        icon: !this.platform.is('ios') ? 'trash' : null,
        handler: () => {
          let decision = this.alertCtrl.create({
            message: '¿Borrar este mensaje?',
            buttons: [
              {
                text: 'Sí',
                handler: () => {
                  console.log('Decisión de eliminar afirmativa');
                }
              },
              {
                text: 'No',
                handler: () => {
                  console.log('Decisión de eliminar negativa');
                }
              }
            ]
          });
          decision.present()
        }
      },
      {
        text: 'Cancelar',
        role: 'cancel', //coloca el botón siempre en el último lugar.
        icon: !this.platform.is('ios') ? 'close' : null,
        handler: () => {
          console.log('Cancelar ActionSheet');
        }
      }
    ]
  });
  actionSheet.present();
  }

  agregarMensajeAmigo() {
    this.chatService.agregarNuevoMensajeAmigo(this.nuevoMensaje,this.idAmigo,this.idUsuario).then(() => {
      this.content.scrollToBottom();
      this.nuevoMensaje = '';
    })
  }

  agregarMensajeGrupo() {
    this.chatService.agregarNuevoMensajeAmigo(this.nuevoMensaje,this.idGrupo,this.idUsuario).then(() => {
      this.content.scrollToBottom();
      this.nuevoMensaje = '';
    })
  }

  ionViewDidEnter() {
    if(this.idAmigo){
      this.chatService.obtenerMensajesConversacionAmigo(this.idAmigo);
    }else if(this.idGrupo){
      this.chatService.obtenerMensajesConversacionGrupo(this.idGrupo);
    }
  }

  scrollto() {
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
  }
}



interface msgs{
  contenido: string;
  tiempo: string;
}

