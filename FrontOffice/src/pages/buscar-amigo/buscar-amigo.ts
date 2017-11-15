import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,Platform, ActionSheetController,AlertController } from 'ionic-angular';
import { VisualizarPerfilPublicoPage } from '../visualizarperfilpublico/visualizarperfilpublico';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';


//****************************************************************************************************// 
//***********************************PAGE BUSCAR AMIGOS MODULO 3**************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Buscador de amigos
 */
@IonicPage()
@Component({
  selector: 'page-buscar-amigo',
  templateUrl: 'buscar-amigo.html',
})
export class BuscarAmigoPage {

  toggled: boolean;
  searchTerm: String = '';
  items:string[];
  idUsuario: any;
  lista:any;
  showList:any;
  showBar:any;
  constructor( public navCtrl: NavController, 
      public navParams: NavParams,public platform: Platform,
      public actionsheetCtrl: ActionSheetController,
      public alerCtrl: AlertController,
      public restapiService: RestapiService,
      private storage: Storage,
      private translateService: TranslateService ) {   
  }

  /**
   * Pone en false la lista y en true 
   * el showBar cuando pasas a otro page
   */
  ionViewWillEnter() {
    this.showBar = true;
    this.showList = false;
  }

  /**
   * Metodo que busca a un usuario
   * @param ev un evento
   */
  buscar(ev){
    this.showList = true;
    this.storage.get('id').then((val) =>{
      if(ev.target.value){
        var dato = ev.target.value;
      } 
      this.restapiService.buscaramigo(dato, val)
      .then(data => {
      this.lista = data;
      });
    });
    
  }
  

 /**
  * Metodo que inicia la pagina de ver el perfil publico
  * @param nombre Nombre de usuario (resultado del buscador)
  */
  Visualizarpublico(nombre){
        this.navCtrl.push(VisualizarPerfilPublicoPage,{
          nombreUsuario : nombre
        });
        this.showBar = false;
      }

}
