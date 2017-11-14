import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { ToastController } from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';

/**
 * Generated class for the ConfigPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-config',
  templateUrl: 'config.html',
})
export class ConfigPage {

  idioms: any[] = [];

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, private translateService: TranslateService, private storage: Storage)
  {
    this.idioms = [
      {
        value: 'es',
        label: 'Español'
      },
      {
        value: 'en',
        label: 'Ingles'
      }
    ];
  }

  choose(lang) {
    let idUsuario = this.navParams.data;
    this.storage.set(idUsuario.toString(), lang);    
    this.translateService.use(lang);
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ConfigPage');
  }

  /*
  * code source:
  * https://ionicframework.com/docs/api/components/toast/ToastController/
  */
  toast() {
    const toast = this.toastCtrl.create({
      message: 'Configuracion Actualizada!',
      duration: 2000,
      position: 'bottom',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
  
    toast.onDidDismiss(() => {
      console.log('Dismissed toast');
    });
  
    toast.present();
  }

  toggleToast() {
    const toast = this.toastCtrl.create({
      message: 'Configuracion Actualizada!',
      duration: 2000,
      position: 'bottom',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
  
    toast.onDidDismiss(() => {
      console.log('Dismissed toast');
    });
  
    toast.present();
  }

}
