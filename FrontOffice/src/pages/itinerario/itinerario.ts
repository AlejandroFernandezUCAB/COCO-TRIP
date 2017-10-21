import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides } from 'ionic-angular';
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

  itinerarios= Array({
   nombre: 'Disney World',
   eventos:
   Array({
     tipo: 'Evento',
     imagen: '../assets/images/epcot.jpg',
     titulo: 'Epcot International Festival of the Arts',
     startTime: '01/02/2017',
     endTime: '02/02/2017',
   },
   {
     tipo: 'Evento',
     imagen: '../assets/images/disney-maraton.jpg',
     titulo: 'Walt Disney World Marathon Weekend',
     startTime: '03/01/2018',
     endTime: '07/01/2018',
   })
 },
 {
  nombre: 'Viaje a Paris',
  eventos:
  Array({
    tipo: 'Actividad',
    imagen: '../assets/images/default-avatar1.svg',
    titulo: 'Comer croissants en la Torre Eiffel',
    startTime: '01/05/2017',
    endTime: '02/05/2017',
  })
});

  calendar() {
    this.navCtrl.push(CalendarioPage);
  }
  ionViewDidLoad() {
    console.log('yujule');
  }

}
