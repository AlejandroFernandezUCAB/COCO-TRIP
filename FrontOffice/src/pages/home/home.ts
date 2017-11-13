import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import * as moment from 'moment';
import { MenuController } from 'ionic-angular';
import { HttpCProvider } from '../../providers/http-c/http-c';
import { Storage } from '@ionic/storage';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
  _itis:any;
 its1: Array<any> = [{
  id: 1,
  nombre: 'Predespachill',
  tipo: 'evento',
  imagen: '../assets/images/predespachill.jpg',
  titulo: 'Fiesta ',
  fecha: '05/11/2017'
},
{
  id: 2,
  nombre: 'rumba Caracas',
  tipo: 'evento',
  imagen: '../assets/images/rc.jpg',
  titulo: 'Evento Rumba Caracas',
  fecha: '18/11/2017'
},
{
  id: 3,
  nombre: 'Holic',
  tipo: 'evento',
  imagen: '../assets/images/holic.jpg',
  titulo: 'fiesta en holic',
  fecha: '03/12/2017'
}];
its2: Array<any> = [{
  id: 1,
  nombre: 'Playa Pelua',
  tipo: 'lugar',
  imagen: '../assets/images/pelua.jpg',
  titulo: 'Playa del estado vargas',
  distancia: '5 km'
},
{
  id: 2,
  nombre: 'Paseo Los proceres',
  tipo: 'lugar',
  imagen: '../assets/images/proceres.jpg',
  titulo: 'Boulevart en honor a los proceres venezolanos',
  distancia: '12 km'
},
{
  id: 3,
  nombre: 'Sabas Nieves',
  tipo: 'lugar',
  imagen: '../assets/images/sabasnieves.jpg',
  titulo: 'Entrada al Parque Waraira Repano',
  distancia: '3 km'
}];


  constructor(public navCtrl: NavController,private storage: Storage,public menu: MenuController, public http: HttpCProvider) {
    //console.log(this.its2);
 //   this.IniciarNotificaciones();
    this.menu.enable(true);
  }
 
 /* IniciarNotificaciones() {
    this.http.NotificacionUsuario(1)
    .then(data => {
      this._itis = data;
      console.log(this._itis);
    });
  }*/

}
