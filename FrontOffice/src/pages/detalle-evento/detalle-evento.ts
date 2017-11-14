import { Component } from '@angular/core';
import { IonicPage, NavController,ModalController,NavParams  } from 'ionic-angular';

/**
 * Generated class for the DetalleEventoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-detalle-evento',
  templateUrl: 'detalle-evento.html',
})
export class DetalleEventoPage {
eve:any;
  constructor(public navCtrl: NavController,public navParams: NavParams, private modalCtrl: ModalController) {
    this.eve= this.navParams.get('eventos'); 

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad DetalleEventoPage');
  }
closeModal()
  {
      this.navCtrl.pop();
  }
}
