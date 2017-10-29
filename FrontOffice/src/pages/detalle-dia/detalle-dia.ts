import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,  ModalController } from 'ionic-angular';
import { CalendarModal, CalendarModalOptions, DayConfig } from "ion2-calendar";
import { CalendarComponentOptions } from 'ion2-calendar';
import * as moment from 'moment';
/**
 * Generated class for the DetalleDiaPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-detalle-dia',
  templateUrl: 'detalle-dia.html',
})
export class DetalleDiaPage {
  date= '';
  eventos = Array();
  constructor(public navCtrl: NavController, public navParams: NavParams, public modalCtrl: ModalController) {
    console.log(this.navParams.get('date'));
    this.date = moment(this.navParams.get('date')).format('MMMM Do YYYY');
    this.eventos = this.navParams.get('eventos')
    console.log(this.date);

  }

  closeModal() {
        this.navCtrl.pop();
    }


}
