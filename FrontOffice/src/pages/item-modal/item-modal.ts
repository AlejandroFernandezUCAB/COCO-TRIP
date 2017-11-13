import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, AlertController } from 'ionic-angular';
import * as moment from 'moment';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import { TranslateService } from '@ngx-translate/core';
import { HttpCProvider } from '../../providers/http-c/http-c';

@IonicPage()
@Component({
  selector: 'page-item-modal',
  templateUrl: 'item-modal.html',
})
export class ItemModalPage {
  itinerario = ''; 
  searchTerm: string = ''; //Lo que esta buscando
  //searchControl: FormControl;
  base_url = '../assets/images/';
  items: any; //Donde metrere la busqueda
  tipo_item: any; //Evento -----  Lugar Turistico -------   Actividad
  searching: any = false;
  FechaInicio: any; 
  FechaFin: any;
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public alertCtrl: AlertController,
    public eventos: EventosCalendarioService,
    private translateService: TranslateService,
    public http: HttpCProvider
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
          //return (item.titulo.toLowerCase().indexOf(val.toLowerCase()) > -1);
          
          /////////////////////////////////////////////
          if (this.tipo_item == 'Evento'){
            this.items = this.http.ConsultarEventos(this.searchTerm, this.FechaInicio, this.FechaFin);
            console.log(this.items);
          }
          else{
            if (this.tipo_item == 'Lugar Turistico'){
              this.items = this.http.ConsultarLugarTuristico(this.searchTerm);
            }
            else{
              if (this.tipo_item == 'Actividad'){
                this.items = this.http.ConsultarActividades(this.searchTerm);
              }
              else{
                /*
                En el jardin hay algo
                que esta esperando, 
                tal cual lo dejaste 
                boca abajo quedo, 
                y cuando lo encuentres, 
                se habra descolorado, 
                mas claro es el reverso si lo haces girar, 
                todo esta, tal cual lo dejaste, 
                todo esta siempre cambiando, 
                ligeramente de dia y noche 
                un poco mas 
                pero todo estas.
                */
              }      
            }
          }
          /////////////////////////////////////////////
          
          return (this.items);
        })
      }
    }
    
  agregarItem(item_id){
    //ARREGLAR ESTO
        let vlista= this.items.filter(function(e,i){ return e.id==item_id})[1];
        console.log(vlista);
        console.log(this.translateService.currentLang);

        //Si el lenguaje es espa;ol
        if (this.translateService.currentLang == 'es'){
          let alert = this.alertCtrl.create({
            title: 'Por favor, confirmar',
            message: '¿Desea agregar '+ vlista.Nombre+ ' a su itinerario en el dia '+ '?',
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
                  console.log(this.FechaFin);
                  console.log(this.FechaInicio);
                  this.viewCtrl.dismiss({evento_nuevo: vlista, itinerario: this.itinerario});
                }
              }
            ]
          });
          alert.present();
        }else
        {
          let alert = this.alertCtrl.create({
            title: 'Please, confirm',
            message: 'Would you like to add '+ vlista.Nombre+ ' to your itinerary on '+ this.FechaInicio +'?',
            buttons: [{
              text: 'CANCEL',
              role: 'cancel',
              handler: data => {
                console.log('Cancel clicked');
                }
              } ,
              {
                text: 'ADD',
                handler: data => {
                  console.log(this.FechaFin);
                  console.log(this.FechaInicio);
                  this.viewCtrl.dismiss({evento_nuevo: vlista, itinerario: this.itinerario});
                }
              }
            ]
          });
          alert.present();
        }
    }

  closeModal() {
        this.navCtrl.pop();
    }

}
