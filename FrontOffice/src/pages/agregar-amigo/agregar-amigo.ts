import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,Platform, ActionSheetController,AlertController } from 'ionic-angular';
import { VisualizarPerfilPublicoPage } from '../visualizarperfilpublico/visualizarperfilpublico';

/**
 * Generated class for the AgregarAmigoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-agregar-amigo',
  templateUrl: 'agregar-amigo.html',
})
export class AgregarAmigoPage {

  toggled: boolean;
  searchTerm: String = '';
  items:string[];

  constructor( public navCtrl: NavController, 
      public navParams: NavParams,public platform: Platform,
      public actionsheetCtrl: ActionSheetController,
      public alerCtrl: AlertController ) {
      this.toggled = false;
      this.initializeItems();       
  }

  ionViewDidLoad() {
      console.log( 'ionViewDidLoad HomePage' );
  }

  toggleSearch() {
      this.toggled = this.toggled ? false : true;
  }

  initializeItems() {
      this.items = ['Mariangel Perez','Oswaldo Lopez']; 
   }  
   
    

  triggerInput( ev: any ) {
      // Reset items back to all of the items
      this.initializeItems();
      // set val to the value of the searchbar
      let val = ev.target.value;
      // if the value is an empty string don't filter the items
      if (val && val.trim() != '') {
        this.items = this.items.filter((item) => {
          return (item.toLowerCase().indexOf(val.toLowerCase()) > -1);
        })
      }  
  }

  /*pressEvent() {
      let actionSheet = this.actionsheetCtrl.create({
        title: '¿Que deseas hacer?',
        cssClass: 'action-sheets-basic-page',
        buttons: [
          {
            text: 'Ver Perfil',
            icon: !this.platform.is('ios') ? 'eye' : null,
            handler: () => {
              console.log('Modificar clicked');
            }
          },
          
          
          
        ]
      });
      actionSheet.present();
  }

  pressEventFacebook() {
      let actionSheet = this.actionsheetCtrl.create({
        title: '¿Que deseas hacer?',
        cssClass: 'action-sheets-basic-page',
        buttons: [
          {
            text: 'Recomendar APP',
            icon: !this.platform.is('ios') ? 'eye' : null,
            handler: () => {
              this.doConfirm();
              //console.log('Detalle clicked');
            }
          }
          
          
          
        ]
      });
      actionSheet.present();
  }*/

  Visualizarpublico(){
    
        this.navCtrl.push(VisualizarPerfilPublicoPage);
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
