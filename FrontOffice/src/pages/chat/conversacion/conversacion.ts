import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { IonicPage, NavParams, NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import * as moment from 'moment';

@IonicPage()
@Component({
    selector: 'page-conversacion',
    templateUrl: 'conversacion.html'
})

export class ConversacionPage {


  mensajes: Array<msgs> = [
    {contenido: '¡Adoro este sitio!', tiempo: moment().fromNow() }
  ];

constructor(public navCtrl: NavController, public navParams: NavParams, public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController, public platform: Platform) {

//this.lista = navParams.get("let item of listachatRec");

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
}

interface msgs{
  contenido: string;
  tiempo: string;
}
