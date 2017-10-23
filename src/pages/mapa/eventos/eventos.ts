import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';

@Component({
  selector: 'page-eventos',
  templateUrl: 'eventos.html'
})
export class EventosPage {
    
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController) {
      
  }
  
  onLink(url: string) {
      window.open(url);
  }

  /*esto se llama cuando se presiona el simbolo +*/
  agregaramigo()
{
  this.navCtrl.push(BuscarAmigoPage);

}

}

  


interface amigos {
    img: string; //Este es el avatar
    nick_name: string; //El nickname
}