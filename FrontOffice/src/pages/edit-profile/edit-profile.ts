import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';

import {ChangepassPage} from '../changepass/changepass';

@IonicPage()
@Component({
  selector: 'page-edit-profile',
  templateUrl: 'edit-profile.html',
})
export class EditProfilePage {

  public event = {
    month: '1993-02-27'
  }

  change = ChangepassPage;

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController) {
  }
  showToastWithCloseButton() {
    const toast = this.toastCtrl.create({
      message: 'Se guardaron tus cambios',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad EventosActividadesPage');
  }

}
