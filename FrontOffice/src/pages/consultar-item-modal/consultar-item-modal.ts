import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController } from 'ionic-angular';

/**
 * Generated class for the ConsultarItemModalPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-consultar-item-modal',
  templateUrl: 'consultar-item-modal.html',
})
export class ConsultarItemModalPage {
  evento: any;
  itinerario: any;
  fotos: any;
  base_url = '../assets/images/';
  constructor(public navCtrl: NavController, public navParams: NavParams, private viewCtrl: ViewController) {
    this.evento= this.navParams.get('evento');
    this.itinerario = this.navParams.get('itinerario');
    console.log('itinerario > '+ this.itinerario.nombre)
    this.fotos = [
      {id: 1, foto: this.base_url+'epcot.jpg'},
      {id: 2, foto: this.base_url+'epcot-2.jpg'},
      {id: 1, foto: this.base_url+'epcot-3.jpg'},
      {id: 1, foto: this.base_url+'epcot-4.jpg'}
    ]
    console.log(this.evento);
    console.log(this.itinerario);
  }

  closeModal() {
        this.navCtrl.pop();
    }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ConsultarItemModalPage');
  }

}
