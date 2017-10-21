import { Component } from '@angular/core';
import { NavController, NavParams, AlertController } from 'ionic-angular';

/**
 * Generated class for the VisualizarPerfilPublicoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-visualizarperfilpublico',
  templateUrl: 'visualizarperfilpublico.html',
})
export class VisualizarPerfilPublicoPage {

  constructor(public navCtrl: NavController, public navParams: NavParams, public alerCtrl: AlertController ) {
  }


  doConfirm() {
    let confirm = this.alerCtrl.create({
      title: 'Agregar?',
      message: 'Desea agregar a esta persona como amigo?',
      buttons: [
        {
          text: 'No',
          handler: () => {
            console.log('No clicked');
          }
        },
        {
          text: 'Si',
          handler: () => {
            console.log('Si clicked');
          }
        }
      ]
    });
    confirm.present()
  }

}
