import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,Platform, ActionSheetController,AlertController } from 'ionic-angular';
import { VisualizarPerfilPublicoPage } from '../visualizarperfilpublico/visualizarperfilpublico';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';

/**
 * Generated class for the AgregarAmigoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
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

  constructor( public navCtrl: NavController, 
      public navParams: NavParams,public platform: Platform,
      public actionsheetCtrl: ActionSheetController,
      public alerCtrl: AlertController,
      public restapiService: RestapiService,
      private storage: Storage ) {
      //this.cargarListas();       
  }

  buscar(ev){
  // set q to the value of the searchbar input if it exists
  if(ev.target.value){
    var q = ev.target.value;
  } 
  this.restapiService.buscaramigo(q)
  .then(data => {
  this.lista = data;
  });
    }
  
  /*cargarListas(){
  this.storage.get('id').then((val) => {
  this.idUsuario = val;
  this.inicializarListas();
  });
    }

  inicializarListas( ){
                
  this.restapiService.buscaramigo( this.idUsuario )
  .then(data => {
  this.lista = data;
  });
    }*/

 
  Visualizarpublico(nombre){
        this.navCtrl.push(VisualizarPerfilPublicoPage,{
          nombreUsuario : nombre
        });
      }

  doConfirm() {
      let confirm = this.alerCtrl.create({
        title: 'Recomendar app?',
        message: 'Desea recomendar la app a su amigo?',
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
