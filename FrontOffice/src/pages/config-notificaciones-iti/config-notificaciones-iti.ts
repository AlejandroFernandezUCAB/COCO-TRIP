import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { EventosCalendarioService } from '../../services/eventoscalendario';

@IonicPage()
@Component({
  selector: 'page-config-notificaciones-iti',
  templateUrl: 'config-notificaciones-iti.html',
})
export class ConfigNotificacionesItiPage {
  _notif: Object = {
    correo: false,
    push: false
  };

  constructor(public navCtrl: NavController, public navParams: NavParams, public notificaciones: EventosCalendarioService) {
  }

  closeModal() {
    this.navCtrl.pop();
  }

  setConfig(tipo, valor){
    console.log(tipo + " " + valor);
  }

  ionViewWillEnter(){
    this._notif = this.notificaciones.getNotifcacionesConfig(); 
  }
}
