import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { AmigosPage } from '../amistades-grupos/amigos/amigos';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { NotificacionesPage } from '../amistades-grupos/notificaciones/notificaciones';
import { TranslateModule } from '@ngx-translate/core';

//****************************************************************************************************// 
//**************************************PAGE DE TABS MODULO 3*****************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Carga las tabs
 */

@IonicPage()
@Component({
  selector: 'page-amistades-grupos',
  templateUrl: 'amistades-grupos.html',
})
export class AmistadesGruposPage {

  tab1Root: any = NotificacionesPage;
  tab2Root: any = GruposPage;
  tab3Root: any = AmigosPage;
 
  constructor() {
 
  }

}
