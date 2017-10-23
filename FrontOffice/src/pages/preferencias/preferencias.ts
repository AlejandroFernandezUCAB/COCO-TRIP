import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { ToastController } from 'ionic-angular';

/**
 * Generated class for the PreferenciasPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-preferencias',
  templateUrl: 'preferencias.html',
})
export class PreferenciasPage {

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController) {
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad PreferenciasPage');
  }

  aviso( str ){
    if(str == "agregado"){
        const toast = this.toastCtrl.create({
          message: 'Tu categoría fue agregada exitosamente',
          showCloseButton: true,
          closeButtonText: 'Ok'
        });
        toast.present();
    }else{
      const toast = this.toastCtrl.create({
        message: 'Tu categoría fue eliminada exitosamente',
        showCloseButton: true,
        closeButtonText: 'Ok'
      });
      toast.present();
    }
  }

}
