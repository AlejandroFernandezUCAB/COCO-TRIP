import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { CalendarioPage } from '../calendario/calendario';

/**
 * Generated class for the ItinerarioPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-itinerario',
  templateUrl: 'itinerario.html',
})
export class ItinerarioPage {

  constructor(public navCtrl: NavController, public navParams: NavParams) {
  }

  calendar() {
    this.navCtrl.push(CalendarioPage);
  }
  ionViewDidLoad() {
    console.log('ionViewDidLoad ItinerarioPage');
  }

}
