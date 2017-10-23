import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { EventosPage } from '../mapa/eventos/eventos';
import { SitiosTuristicosPage } from '../mapa/sitios-turisticos/sitios-turisticos';

/**
 * Generated class for the AmistadesGruposPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-mapa',
  templateUrl: 'mapa.html',
})
export class MapaPage {

  // this tells the tabs component which Pages
  // should be each tab's root Page
  tab1Root: any = SitiosTuristicosPage;
  tab2Root: any = EventosPage;
 
  constructor() {
 
  }

}
