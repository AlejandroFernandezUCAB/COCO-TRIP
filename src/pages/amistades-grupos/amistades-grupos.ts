import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AmigosPage } from '../amistades-grupos/amigos/amigos';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../amistades-grupos/notificaciones/notificaciones';

/**
 * Generated class for the AmistadesGruposPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-amistades-grupos',
  templateUrl: 'amistades-grupos.html',
})
export class AmistadesGruposPage {

  // this tells the tabs component which Pages
  // should be each tab's root Page
  tab1Root: any = NotificacionesPage;
  tab2Root: any = GruposPage;
  tab3Root: any = AmigosPage;
 
  constructor() {
 
  }

}
