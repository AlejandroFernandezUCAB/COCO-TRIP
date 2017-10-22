import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { IonicPage, NavParams, NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';

@IonicPage()
@Component({
    selector: 'page-conversacion',
    templateUrl: 'conversacion.html'
})

export class ConversacionPage {

    //public lista
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

}
