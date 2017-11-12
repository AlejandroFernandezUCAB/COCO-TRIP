import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { ConversacionPage } from '../chat/conversacion/conversacion';
import { Platform, ActionSheetController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import {ChatProvider} from '../../providers/chat/chat';

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
  listachatRec: Array<chats> = [
    {img: 'https://pbs.twimg.com/profile_images/854769171326017536/dwsgeSUR_400x400.jpg', nombre: 'Ana', msg: '¡Adoro este sitio!'},
    {img: 'https://pbs.twimg.com/profile_images/3101201902/3d557cb152d2be8810e65cc4f04610b0_400x400.jpeg', nombre: 'Darío', msg: 'Me encantó conocer este lugar.'},
    {img: 'assets/img/image1.jpeg', nombre: 'Javier', msg: 'Acabo de salir de un gran concierto...'}
  ];
  listachatOn: Array<chats> = [
    {img: 'https://pbs.twimg.com/profile_images/920120145347010561/0oup_HVP_400x400.jpg', nombre: 'Erbin', msg: 'COCO-TRIP maneja excelentemente mi itinerario'},
    {img: 'https://pbs.twimg.com/profile_images/425480087375712256/p8sgGveQ_400x400.jpeg', nombre: 'Alejandro', msg: 'Tengo un calendario organizado gracias a COCO-TRIP'},
    {img: 'https://pbs.twimg.com/profile_images/2823397360/02c5385f5d19899df586bc9462c1d7fe_400x400.jpeg', nombre: 'Mauricio', msg: 'COCO-TRIP es una app muy segura.'},
    {img: 'assets/img/image2.jpeg', nombre: 'Félix', msg: '¡Me encanta desarrollar para COCO-TRIP!'}
  ];
  pushPage: any;
  constructor(public navCtrl: NavController, public navParams: NavParams, public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController, public platform: Platform, public chatService: ChatProvider) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ChatPage');
  }

  tapEvent(){
    this.navCtrl.push(ConversacionPage, {
      lista: this.listachatRec
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

  inicializarConversacion(conversacion){
    this.chatService.inicializarConversacion(conversacion);
    this.navCtrl.push('conversacionPage');
  }
}


interface chats {
  img: string;
  nombre: string;
  msg: string;
}
