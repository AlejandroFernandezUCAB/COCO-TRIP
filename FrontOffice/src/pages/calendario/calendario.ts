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
    public modalCtrl: ModalController,
    public eventos: EventosCalendarioService,
  ) {

   }

    ionViewWillEnter(){
      this.its = this.eventos.getItinerarios();
    }

    onChange($event) {
      let eventos = Array();
      console.log(this.its);
      this.its.forEach(it => {
        it.eventos.forEach( ev => {
          if (ev.startTime == moment($event._d).format('MM/DD/YYYY')){
            console.log("OLA");
            eventos.push(ev);
          }
        })
      })
      let modal = this.modalCtrl.create('DetalleDiaPage', {date: $event , eventos: eventos});
      modal.present();
      }


}
