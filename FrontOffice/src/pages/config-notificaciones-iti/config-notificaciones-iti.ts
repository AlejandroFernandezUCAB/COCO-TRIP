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
  _itinerarios = Array();
  constructor(public navCtrl: NavController, public navParams: NavParams, public servicio: EventosCalendarioService) {
    this._itinerarios= this.servicio.getItinerarios();
  }

  closeModal() {
    this.navCtrl.pop();
  }

  setConfig(tipo, valor){
    console.log(tipo + " " + valor);
  }

  updateVisible(itinerario){
    console.log(itinerario);
    this.servicio.updateItinerarioVisible(itinerario);
  }

  ionViewWillEnter(){
    this._notif = this.servicio.getNotifcacionesConfig();
  }
}
