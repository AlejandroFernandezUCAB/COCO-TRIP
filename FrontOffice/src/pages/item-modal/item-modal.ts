import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, AlertController } from 'ionic-angular';
import 'rxjs/add/operator/debounceTime';

/**
 * Generated class for the ItemModalPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

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

  constructor(public navCtrl: NavController, public navParams: NavParams, private viewCtrl: ViewController,  public alertCtrl: AlertController) {
    this.itinerario= this.navParams.get('itinerario');
  //  this.searchControl = new FormControl();
    console.log(this.itinerario);
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
    //evento= {tipo: '', imagen: '', titulo: '', startTime: Date, endTime: Date};
      this.items = [
        {id: 4, titulo: 'Aventuras en Paris', tipo: 'evento', imagen: this.base_url + 'paris.jpg', startTime: new Date('02/02/2018'), endTime: new Date('03/02/2018')},
        {id: 5, titulo: 'Amsterdam Sightseeing', tipo: 'lugar', imagen: this.base_url + 'Amsterdam.jpg', startTime: new Date('02/02/2018'), endTime: new Date('03/02/2018')},
        {id: 6, titulo: 'Aventuras Divertidas', tipo: 'actividad', imagen: this.base_url + 'default-avatar1.svg', startTime: new Date('01/01/2018'), endTime: new Date('01/01/2018')},
        {id: 7,titulo: 'Un Lugar', tipo: 'lugar', imagen: this.base_url + 'default-avatar1.svg', startTime: new Date(), endTime: new Date()},
        {id: 8,titulo: 'Un evento', tipo: 'lugar', imagen: this.base_url + 'default-avatar1.svg', startTime: new Date('12/12/2017'), endTime: new Date('12/12/2017')},
        {id: 9,titulo: 'Una de tus actividades',tipo: 'actividad', imagen: this.base_url + 'default-avatar1.svg', startTime: new Date('11/02/2018'), endTime: new Date('11/02/2018')},
      ];
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

    verItems(item_id){
        let vlista= this.items.filter(function(e,i){ return e.id==item_id})[0];
        let alert = this.alertCtrl.create({
          title: vlista.titulo,
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
  ionViewDidLoad() {
  //  this.setFilteredItems();
  }

}
