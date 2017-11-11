import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, AlertController } from 'ionic-angular';
import * as moment from 'moment';
import { EventosCalendarioService } from '../../services/eventoscalendario';

@IonicPage()
@Component({
  selector: 'page-item-modal',
  templateUrl: 'item-modal.html',
})
export class ItemModalPage {
  itinerario = '';
  searchTerm: string = '';
  //searchControl: FormControl;
  base_url = '../assets/images/';
  items: any;
  tipo_item: any;
  searching: any = false;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public alertCtrl: AlertController,
    public eventos: EventosCalendarioService
  ) {
    this.itinerario= this.navParams.get('itinerario');
    this.initializeItems();
  }
  filterItems(searchTerm){

      return this.items.filter((item) => {
          return item.title.toLowerCase().indexOf(searchTerm.toLowerCase()) > -1;
      });

  }

  onSearchInput(){
       this.searching = true;
   }

  setFilteredItems() {
        this.items = this.filterItems(this.searchTerm);
        this.searching = false;
    }

  initializeItems(){
    this.items = this.eventos.getEventosGlobales();
    }

    getItems(ev: any){
      this.initializeItems();
      let val = ev.target.value;
      if (val && val.trim() != ''){
        this.items= this.items.filter((item) => {
          return (item.titulo.toLowerCase().indexOf(val.toLowerCase()) > -1);
        })
      }
    }

  agregarItem(item_id){
    //ARREGLAR ESTO
        let vlista= this.items.filter(function(e,i){ return e.id==item_id})[1];
        console.log(vlista);
        let alert = this.alertCtrl.create({
          title: 'Por favor, confirmar',
          message: '¿Desea agregar '+ vlista.Nombre+ ' a su itinerario?',
          buttons: [{
            text: 'CANCELAR',
            role: 'cancel',
            handler: data => {
              console.log('Cancel clicked');
              }
            } ,
            {
              text: 'AGREGAR',
              handler: data => {
                this.viewCtrl.dismiss({evento_nuevo: vlista, itinerario: this.itinerario});
              }
            }
          ]
        });
        alert.present();
    }

  closeModal() {
        this.navCtrl.pop();
    }

}
