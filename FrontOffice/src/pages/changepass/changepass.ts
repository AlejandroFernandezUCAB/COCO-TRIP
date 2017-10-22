import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';

/**
 * Generated class for the ChangepassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-changepass',
  templateUrl: 'changepass.html',
})
export class ChangepassPage {

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController) {
  }
  
  showToastWithCloseButton() {
    const toast = this.toastCtrl.create({
      message: 'Se cambió tu contraseña',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ChangepassPage');
  }

}
