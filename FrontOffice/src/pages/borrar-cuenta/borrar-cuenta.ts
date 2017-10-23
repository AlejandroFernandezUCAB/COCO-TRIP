import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

import { AlertController } from 'ionic-angular';

/**
 * Generated class for the BorrarCuentaPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-borrar-cuenta',
  templateUrl: 'borrar-cuenta.html',
})
export class BorrarCuentaPage {

  constructor(public navCtrl: NavController, public navParams: NavParams, public alertCtrl: AlertController) {

  }

  //source:
  //https://ionicframework.com/docs/api/components/alert/AlertController/
  presentConfirm() {
    const alert = this.alertCtrl.create({
      title: 'Confirmar Borrar',
      message: 'El usuario sera borrado permanentemente. Esta seguro?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: () => {
            console.log('Cancelar clicked');
          }
        },
        {
          text: 'Borrar',
          handler: () => {
            console.log('Borrar clicked');
          }
        }
      ]
    });
    alert.present();
  }


  ionViewDidLoad() {
    console.log('ionViewDidLoad BorrarCuentaPage');
  }

}
