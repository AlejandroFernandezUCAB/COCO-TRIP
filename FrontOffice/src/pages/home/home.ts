import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import * as moment from 'moment';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
 its1: Array<any> = [{
  id: 1,
  nombre: 'Predespachill',
  tipo: 'evento',
  imagen: '../assets/images/predespachill.jpg',
  titulo: 'Fiesta '
},
{
  id: 2,
  nombre: 'rumba Caracas',
  tipo: 'evento',
  imagen: '../assets/images/rc.jpg',
  titulo: 'Evento Rumba Caracas'
},
{
  id: 3,
  nombre: 'Holic',
  tipo: 'evento',
  imagen: '../assets/images/holic.jpg',
  titulo: 'fiesta en holic'
}];
its2: Array<any> = [{
  id: 1,
  nombre: 'Playa Pelua',
  tipo: 'lugar',
  imagen: '../assets/images/pelua.jpg',
  titulo: 'Playa del estado vargas'
},
{
  id: 2,
  nombre: 'Paseo Los proceres',
  tipo: 'lugar',
  imagen: '../assets/images/proceres.jpg',
  titulo: 'Boulevart en honor a los proceres venezolanos'
},
{
  id: 3,
  nombre: 'Sabas Nieves',
  tipo: 'lugar',
  imagen: '../assets/images/sabasnieves.jpg',
  titulo: 'Entrada al Parque Waraira Repano'
}];

  constructor(public navCtrl: NavController) {
    //console.log(this.its2);
  }
 

}
