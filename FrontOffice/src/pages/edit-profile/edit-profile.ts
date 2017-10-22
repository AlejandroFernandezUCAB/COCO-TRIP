import { Component } from '@angular/core';
import {  IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

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

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController ){
  }
  showToastWithCloseButton() {
    const toast = this.toastCtrl.create({
      message: 'Se guardaron tus cambios',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
  }
  //Este OpenMenu es para el ActionSheet de cambiar foto
  openMenu() {
    let actionSheet = this.actionsheetCtrl.create({
      title: 'Albums',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Borrar',
          role: 'destructive',
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            console.log('Delete clicked');
          }
        },
        {
          text: 'Cambiar Foto',
          icon: !this.platform.is('ios') ? 'albums' : null,
          handler: () => {
            console.log('Change clicked');
          }
        },
        {
          text: 'Cancelar',
          role: 'cancel', // will always sort to be on the bottom
          icon: !this.platform.is('ios') ? 'close' : null,
          handler: () => {
            console.log('Cancel clicked');
          }
        }
      ]
    });
    actionSheet.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad EventosActividadesPage');
  }

}
