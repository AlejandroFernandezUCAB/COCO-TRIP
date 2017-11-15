import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ViewController, AlertController,ToastController, LoadingController } from 'ionic-angular';
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
  NoResults = false;
  loading:any;
  searchForm: FormGroup;
  submitAttempt: boolean = false;
  searchTerm: string = ''; //Lo que esta buscando
  //searchControl: FormControl;
  base_url = '../assets/images/';
  items: any; //Donde metrere la busqueda
  Tipo_item: any; //Evento -----  Lugar Turistico -------   Actividad
  searching: any = false;
  toast:any
  showSearchBar: any =false;
  FechaInicio: any;
  FechaFin: any;
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private viewCtrl: ViewController,
    public alertCtrl: AlertController,
    public toastCtrl: ToastController,
    public eventos: EventosCalendarioService,
    private translateService: TranslateService,
    public http: HttpCProvider,
    public formBuilder: FormBuilder,
  public loadingCtrl: LoadingController,

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

    public presentLoading()
    {
        this.loading = this.loadingCtrl.create({
        content: 'Please wait...',
        dismissOnPageChange: true
      });
      this.loading.present();
    }

    getItems(ev:any){
      this.items = Array();
      let val = ev.target.value;
      if (val && val.trim() != ''){
        if (this.searchForm.valid &&this.searchTerm!=undefined && this.searchTerm!=''){ //SOLO SI ENTRA AQUI SE LE PERMITE BUSCAR EN EL BUSCADOR Y SE LLAMAN A LAS RUTAS
          if (this.Tipo_item == 'Evento'){
            this.http.ConsultarEventos(this.searchTerm, this.FechaInicio, this.FechaFin).then(
              data =>{
              if (data==-1 || data==0){
                this.NoResults = true;
              }else{
                this.NoResults= false;
                this.items = data;
              }
            }
          );
          }
          else{
            if (this.Tipo_item == 'Lugar Turistico'){
              this.http.ConsultarLugarTuristico(this.searchTerm).then(data=>
              {
                if (data==0 || data==-1){
                  this.NoResults= true;
                  console.log("hubo un error");
                }else{
                  this.NoResults= false;
                  this.items = data;
                }
              }
            );
            }
            else{
              if (this.Tipo_item == 'Actividad'){
                this.http.ConsultarActividades(this.searchTerm).then(data=>
                {
                  if (data==0 || data==-1){
                    this.NoResults= true;
                    console.log("hubo un error");
                  }else{
                    this.NoResults= false;
                    this.items = data;
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
        }else{
          this.NoResults=false;
        }

        //this.items= this.items.filter((item) => {
          //return (item.titulo.toLowerCase().indexOf(val.toLowerCase()) > -1);
          return (this.items);
        //})
      }
    }

  agregarItem(item_id){
        let vlista= this.items.filter(function(e,i){ return e.Id==item_id})[0];
        vlista.Tipo= this.Tipo_item;
        //Si el lenguaje es espa;ol
        if (this.translateService.currentLang == 'es'){
          let alert = this.alertCtrl.create({
            title: 'Por favor, confirmar',
            message: '¿Desea agregar '+ vlista.Nombre+ ' a su itinerario en el dia '+ '?',
            buttons: [{
              text: 'CANCELAR',
              role: 'cancel',
              handler: data => {
                }
              } ,
              {
                text: 'AGREGAR',
                handler: data => {
                  if (this.FechaInicio > this.FechaFin)
                  {
                    this.realizarToast('Fecha inicio debe ser menor que fecha fin');
                  }
                  else
                  if (this.FechaFin > this.itinerario.FechaFin)
                  {
                    this.realizarToast('Fecha fin del item debe ser menor que fecha fin del itinerario');
                  }
                  else
                  if (this.FechaInicio < this.itinerario.FechaInicio)
                  {
                    this.realizarToast('Fecha inicio del item debe ser mayor que fecha inicio del itinerario');
                  }
                  else
                  {
                    vlista.FechaInicio = this.FechaInicio;
                    vlista.FechaFin = this.FechaFin;
                    this.http.agregarItem_It(this.Tipo_item, this.itinerario.Id, item_id , this.FechaInicio,this.FechaFin).then(data=>{
                      if (data == null){
                        this.realizarToast('El item ya esta en el itinerario');
                        this.viewCtrl.dismiss();
                      }else {
                        this.viewCtrl.dismiss({evento_nuevo: vlista, itinerario: this.itinerario});
                      }
                    });
                   }
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
                }
              } ,
              {
                text: 'AGREGAR',
                handler: data => {
                  if (this.FechaInicio > this.FechaFin)
                  {
                    this.realizarToast('Start date must be less than end date');
                  }
                  else
                  if (this.FechaFin > this.itinerario.FechaFin)
                  {
                    this.realizarToast('The items end date must be less than the itinerarys');
                  }
                  else
                  if (this.FechaInicio < this.itinerario.FechaInicio)
                  {
                    this.realizarToast('The items start date must be greater than the itinerarys');
                  }
                  else
                  {
                    vlista.FechaInicio = this.FechaInicio;
                    vlista.FechaFin = this.FechaFin;
                    this.http.agregarItem_It(this.Tipo_item, this.itinerario.Id, item_id , this.FechaInicio,this.FechaFin).then(data=>{
                      if (data==null){
                        this.realizarToast('Item is already added');
                        this.viewCtrl.dismiss();
                      }else {
                        this.viewCtrl.dismiss({evento_nuevo: vlista, itinerario: this.itinerario});
                      }
                    });
                   }
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

    public realizarToast(mensaje)
    {
        this.toast = this.toastCtrl.create({
          message: mensaje,
          duration: 3000,
          position: 'middle'
        });
        this.toast.present();
    }

}
