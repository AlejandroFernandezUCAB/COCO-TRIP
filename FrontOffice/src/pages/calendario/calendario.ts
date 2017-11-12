import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ModalController, AlertController } from 'ionic-angular';
import { CalendarModal, CalendarModalOptions, DayConfig } from "ion2-calendar";
import { CalendarComponentOptions } from 'ion2-calendar';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import * as moment from 'moment';

@IonicPage()
@Component({
   selector: 'page-calendario',
 templateUrl: 'calendario.html',
 })
export class CalendarioPage {
  its= Array();
  items = Array();
  date: string;

  type: 'string'; // 'string' | 'js-date' | 'moment' | 'time' | 'object'
  _daysConfig= this.eventos.getEventosItinerario();
  options: CalendarComponentOptions = {
    color: 'primary',
    daysConfig: this._daysConfig
  };
  constructor(
    public navCtrl: NavController,
    public modalCtrl: ModalController,
    public eventos: EventosCalendarioService,
  ) {
    console.log(this._daysConfig);
   }

    ionViewWillEnter(){
      this.its = this.eventos.getItinerarios();
      console.log(this.its);
    }

    onChange($event) {
      let items_agenda = Array();
      console.log(this.its);
      this.its.forEach(it => {
        if (it.Visible ==true){
          it.Items_agenda.forEach( ev => {
            if (ev.FechaInicio == moment($event._d).format('MM/DD/YYYY')){
              items_agenda.push({
                Nombre: ev.Nombre,
                // HoraInicio: ev.HoraInicio,
                // HoraFin: ev.HoraFin,
                itinerario: it.Nombre
              });
            }
          })
        }
      })
      // console.log("items_agenda ");
      // console.log(items_agenda);
      let modal = this.modalCtrl.create('DetalleDiaPage', {date: $event , items_agenda: items_agenda});
      modal.present();
      }

  openCalendarConfig(){
    this.navCtrl.push('ConfigCalendarioPage');
  }
}
