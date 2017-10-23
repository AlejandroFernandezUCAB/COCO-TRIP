import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';

@Component({
  selector: 'page-sitios-turisticos',
  templateUrl: 'sitios-turisticos.html'
})
export class SitiosTuristicosPage {
  
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController) {
      
  CrearGrupo(){
    this.navCtrl.push(CrearGrupoPage);
  }
}
