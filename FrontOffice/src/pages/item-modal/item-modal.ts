import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, AlertController } from 'ionic-angular';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
  itinerario : any;
  searchForm: FormGroup;
  submitAttempt: boolean = false;
  searchTerm: string = ''; //Lo que esta buscando
  //searchControl: FormControl;
  base_url = '../assets/images/';
  items: any; //Donde metrere la busqueda
  Tipo_item: any; //Evento -----  Lugar Turistico -------   Actividad
  searching: any = false;
  showSearchBar: any =false;
  FechaInicio: any;
  FechaFin: any;
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public alertCtrl: AlertController,
    public eventos: EventosCalendarioService,
    private translateService: TranslateService,
    public http: HttpCProvider,
    public formBuilder: FormBuilder
  ) {
    this.itinerario= this.navParams.get('itinerario');
    this.searchForm = formBuilder.group({
        Tipo_item: ['', Validators.required],
        FechaInicio: ['', Validators.required],
        FechaFin: ['', Validators.required]
    });
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

    getItems(ev:any){
      this.items = Array();
      let val = ev.target.value;
      if (val && val.trim() != ''){
        if (this.searchForm.valid){
          console.log(this.searchTerm); //SOLO SI ENTRA AQUI SE LE PERMITE BUSCAR EN EL BUSCADOR Y SE LLAMAN A LAS RUTAS
          if (this.Tipo_item == 'Evento'){
            this.http.ConsultarEventos(this.searchTerm, this.FechaInicio, this.FechaFin).then(
              data =>{
              if (data==0 || data==-1){
                console.log("hubo un error");
              }else{
                this.items = data;
                console.log("EL CDLM " + this.items[0].Id)
                console.log(this.items);
              }
            }
          );
          }
          else{
            if (this.Tipo_item == 'Lugar Turistico'){
              this.http.ConsultarLugarTuristico(this.searchTerm).then(data=>
              {
                if (data==0 || data==-1){
                  console.log("hubo un error");
                }else{
                  this.items = data;
                  console.log(this.items);
                }
              }
            );
            }
            else{
              if (this.Tipo_item == 'Actividad'){
                this.http.ConsultarActividades(this.searchTerm).then(data=>
                {
                  if (data==0 || data==-1){
                    console.log("hubo un error");
                  }else{
                    this.items = data;
                    console.log(this.items[0].Id);
                  }
                }
              );
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
        }

        //this.items= this.items.filter((item) => {
          //return (item.titulo.toLowerCase().indexOf(val.toLowerCase()) > -1);
          return (this.items);
        //})
      }
    }

  agregarItem(item_id){
    //ARREGLAR ESTO
    console.log(item_id);
        let vlista= this.items.filter(function(e,i){ return e.Id==item_id})[0];
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
                  this.http.agregarItem_It(this.Tipo_item, this.itinerario.Id, item_id , this.FechaInicio,this.FechaFin);
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
