import { Component } from '@angular/core';
import { NavController, AlertController  } from 'ionic-angular';

@Component({
  selector: 'page-visualizarperfil',
  templateUrl: 'visualizarperfil.html'
})
export class VisualizarPerfilPage {

  constructor(public navCtrl: NavController, public alerCtrl: AlertController ) {

  }

  doConfirm() {
    let confirm = this.alerCtrl.create({
      title: 'Agregar?',
      message: 'Desea agregar a esta persona como amigo?',
      buttons: [
        {
          text: 'No',
          handler: () => {
            console.log('No clicked');
          }
        },
        {
          text: 'Si',
          handler: () => {
            console.log('Si clicked');
          }
        }
      ]
    });
    confirm.present()
  }
}
