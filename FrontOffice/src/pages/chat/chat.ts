import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { ConversacionPage } from '../chat/conversacion/conversacion';
import { Platform, ActionSheetController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { ChatProvider } from '../../providers/chat/chat';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';



/**
 * Generated class for the ChatPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-chat',
  templateUrl: 'chat.html',
})
export class ChatPage {
  nombreUsuario: string;
  amigo: any;
  listachatRec: Array<chats> = [
    {img: 'assets/img/image1.jpeg', nombre: this.nombreUsuario, msg: 'Acabo de salir de un gran concierto...'}
  ];
  pushPage: any ;
  constructor(public navCtrl: NavController, public navParams: NavParams, 
            public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController, 
            public platform: Platform, public chatService: ChatProvider, 
            private storage: Storage, public restapiService: RestapiService) {

  let idUsuario     //Obtiene ID de Usuario
  this.storage.get('id').then((val) => {
    idUsuario = val;
  });
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ChatPage');
  }

  tapEvent(item){
    this.navCtrl.push(ConversacionPage, {
      nombreUsuario : item
    });
  }

  pressEvent(){
    let actionSheet = this.actionsheetCtrl.create({
      title: 'Opciones',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Eliminar Chat',
          role: 'destructive', // aplica color rojo de alerta
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            let decision = this.alertCtrl.create({
              message: '¿Borrar este chat?',
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

  inicializarConversacion(/*conversacion*/){
    //this.chatService.inicializarConversacion(conversacion);
    this.navCtrl.push(ConversacionPage);
  }

  abrirChat(){
    this.navCtrl.push(ConversacionPage);
  }
}


interface chats {
  img: string;
  nombre: string;
  msg: string;
}