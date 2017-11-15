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
  isToggled:boolean;
  nombreUsuario;

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, private translateService: TranslateService, private storage: Storage)
  {
    console.log(navParams.data)
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
    this.isToggled = true;
  }

  choose(lang) {
    let idUsuario = this.navParams.data.idUsuario;
    this.storage.set(idUsuario.toString(), lang);    
    this.translateService.use(lang);
  }

  ionViewWillLoad() {
    console.log('ionViewDidLoad ConfigPage');
    this.nombreUsuario = this.navParams.data.NombreUsuario;
          // cargamos la configuracion almacenada de notificaciones
          this.storage.get(this.nombreUsuario).then((val) => {
            //verificamos que posee configuracion previa
            if(val != null || val != undefined){
              this.isToggled = val;
            }
          });
  }

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

  // metodo para guardar la nueva configuracion del usuario
  // y luego mostrar una notificacion toast
  toggleToast() {
    // guardo el nuevo estado
    this.toggleSaveNotifConfig();
    // creo y muestro la notificacion
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

  // metodo para guardar la configuracion de notificaciones del usuario
  toggleSaveNotifConfig(){
    this.storage.set(this.nombreUsuario, this.isToggled); 
  }

}
