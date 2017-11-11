import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides, reorderArray, AlertController, ModalController, LoadingController } from 'ionic-angular';
import { CalendarioPage } from '../calendario/calendario';
import * as moment from 'moment';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import { HttpCProvider } from '../../providers/http-c/http-c';
/**
 * Generated class for the ItinerarioPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-itinerario',
  templateUrl: 'itinerario.html',
})
export class ItinerarioPage {
  // @ViewChild('slider') slider: Slides;
  //****************** DECLARACION DE VARIABLES *********************
  base_url = '../assets/images/';
  items = [];
  edit = false;
  delete= false;
  count = 0;
  minDate= new Date().toISOString();
  its: any;
  newitinerario: any;
  users: any;
  list = false;
  nuevoViejo = true;
  mySlideOptions = {
    initialSlide: 0,
    loop: true
  };
  originalEventDates = Array();
  eventDatesAsInt = Array();
  noIts = false;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  //************** FIN DE DECLARACION DE VARIABLES *****************
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private modalCtrl: ModalController,
    public alertCtrl: AlertController,
    public itinerarios: EventosCalendarioService,
    public httpc: HttpCProvider,
    public loadingCtrl: LoadingController
  ) {
    //this.its = Array();
    //this.its.eventos=Array();
    for (let x = 0; x < 5; x++) {
      this.items.push(x);
    }
  }


  loadItinerarios() {
    this.itinerarios.consultarItinerarios(1);
  }

  calendar() {
    this.navCtrl.push(CalendarioPage, {itinerarios: this.its});
  }

  reorderItems(indexes) {
    this.items = reorderArray(this.items, indexes);
  }

  crear(){
    const alert = this.alertCtrl.create({
      title: 'Nuevo Itinerario',
      inputs: [
        {
          name: 'Nombre',
          placeholder: 'Nombre'
        }
      ],
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: data => {
            console.log('Cancel clicked');
          }
        },
        {
          text: 'Crear',
          handler: data => {
            if (data.Nombre!= '' && data.Nombre!= undefined) {
              console.log(data);
              if (this.its == undefined) this.its=Array();
              this.its.push({
                Nombre: data.Nombre,
                Items_agenda: Array()
              });
              let newitinerario ={ Nombre:data.Nombre,IdUsuario:2 }
              this.httpc.agregarItinerario(newitinerario)
              //this.its[this.its.length].eventos = Array();
            } else {
              // invalid login
              return false;
            }
          }
        }
      ]
    });
    alert.present();
  }

  presentConfirm(idit, index) {
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este itinerario?',
    buttons: [
      {
        text: 'Cancelar',
        role: 'cancel',
        handler: () => {
          //console.log('Cancel clicked');
        }
      },
      {
        text: 'Aceptar',
        handler: () => {
          this.eliminarItinerario(idit, index);
          this.httpc.eliminarItinerario(idit);
          }
        }
      ]
    });
    alert.present();
  }

  presentConfirmItem(id_itinerario, id_evento, index) {
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este elemento?',
    buttons: [
    {
      text: 'Cancelar',
      role: 'cancel',
      handler: () => {
        //console.log('Cancel clicked');
      }
    },
    {
      text: 'Aceptar',
      handler: () => {
        console.log("id_itinerario :: ", id_itinerario);
        console.log("id_evento :: ", id_evento);
        this.eliminarItem(id_itinerario, id_evento, index);
        this.httpc.eliminarItem(id_itinerario, id_evento);
          }
        }
      ]
    });
    alert.present();
  }

  eliminar(){
    this.delete = true;
    this.edit = false;
  }

  eliminarItinerario(id, index){
     let eliminado = this.its.filter(item => item.Id === id)[0];
     var removed_elements = this.its.splice(index, 1);
     if (this.its.length == 0){
       this.noIts = true;
       console.log("no its")
     }
   }

  eliminarItem(id_itinerario, id_evento, index){
    let iti_e_eliminado = this.its.filter(item => item.Id === id_itinerario)[0];
    console.log(iti_e_eliminado);
    var removed_elements = iti_e_eliminado.Items_agenda.splice(index, 1);
  }

  editar(){
    this.edit = true;
    this.delete = false;
    for(var i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].Nombre;
    }
  }

  done(){
    this.edit = false;
    this.delete=false;
    for(var i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].Nombre;
      let moditinerario ={Id:this.its[i].Id, Nombre:this.its[i].Nombre,FechaInicio:this.its[i].FechaInicio,FechaFin:this.its[i].FechaFin,IdUsuario:2}
      this.httpc.modificarItinerario(moditinerario)
    }
  }

  doneDeleting(){
    this.edit = false;
    this.delete = false;
  }


  ordenar(){
    this.nuevoViejo = !this.nuevoViejo;
    var dates = Array();
    if (this.nuevoViejo == true){
     for(var i = 0;i< this.its.length;i++) {
       this.its[i].Items_agenda.sort(function(a,b){
            return new Date(b.startTime).getTime() - new Date(a.startTime).getTime();
         });
       }
    }else{
      for(var i = 0;i< this.its.length;i++) {
        this.its[i].Items_agenda.sort(function(a,b){
             return new Date(a.startTime).getTime() - new Date(b.startTime).getTime();
          });
        }
    }
    // var dates = dates_as_int.map(function(dateStr) {
    // return new Date(dateStr).getTime();
    // });
    // if (this.nuevoViejo == true){
    //   for(var i = 0;i< this.its.length;i++) {
    //     this.its[i].eventos.sort(function(a,b){
    //     return new Date(b.startTime) - new Date(a.startTime);
    //   });
    //   }
    // }else{
    //   for(var i = 0;i< this.its.length;i++) {
    //     this.its[i].eventos.sort(function(a,b){
    //     return new Date(a.startTime) - new Date(b.startTime);
    //   });
    //   }
    // }
  }

  ordenarIt(){
    this.nuevoViejo = !this.nuevoViejo;
    if (this.nuevoViejo == true){
        this.its.sort(function(a,b){
          return new Date(b.fechaInicio).getTime() - new Date(a.fechaInicio).getTime();
        });

    }else{
      this.its.sort(function(a,b){
        return new Date(a.fechaInicio).getTime() - new Date(b.fechaInicio).getTime();
      });
    }
  }

  agregarItem(iti){
    console.log(iti);
    let modal = this.modalCtrl.create('ItemModalPage', {itinerario: iti});
    modal.present();
    modal.onDidDismiss(data => {
      if (data) {
        let eventoData = data;
        let itinerario_nuevo = data.itinerario;
        eventoData.Id = data.evento_nuevo.Id;
        eventoData.Nombre = data.evento_nuevo.Nombre;
        eventoData.imagen = data.evento_nuevo.imagen;
        eventoData.startTime = data.evento_nuevo.startTime;
        eventoData.endTime = data.evento_nuevo.endTime;
        for(var i = 0;i< this.its.length;i++) {
          if (this.its[i].Nombre == itinerario_nuevo) {
            //si el itinerario no tiene eventos, se inicializa el arreglo eventos
            if (this.its[i].Items_agenda == undefined){
              this.its[i].Items_agenda = Array();
            }
            this.its[i].Items_agenda.push(eventoData);
            console.log(eventoData);
          }
        }
      }
    })
  }

  verItem(evento, itinerario){
    //Si el click no es en eliminar, entra
    if (this.delete == false){
      let modal = this.modalCtrl.create('ConsultarItemModalPage', {evento: evento, itinerario: itinerario});
      modal.present();
      modal.onDidDismiss(data => {
      if (data) {
        console.log("volvio de la vista")
      }
      })
    }
  }

  listar(){
    for(var i = 0;i< this.its.length;i++) {
      if (this.its[i].Items_agenda == undefined){
        this.its[i].Items_agenda = Array();
      }
    }

    if(this.list==true){
      this.list = false;
    }
    else{
      this.list=true;
    }
  }



  presentLoading(){
      this.loading = this.loadingCtrl.create({
      content: 'Please wait...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }


  ionViewWillEnter(){
    this.presentLoading();
    this.httpc.loadItinerarios(2)
    .then(data => {
      this.its = data;
      this.loading.dismiss();
      console.log(this.its);
      if (this.its.length == 0){
        this.noIts = true;
      }
    });
  }

  public getTipoItem(evento){
    if (evento.Costo == undefined){
      let actividad = 'Actividad';
      return actividad;
    }else
      if (evento.Costo != undefined){
        let lugar = 'Lugar Turistico';
        return lugar;
      }
  }

  parseDateStrToInt(input) {
    var parts = input.split('/');
    // new Date(year, month [, day [, hours[, minutes[, seconds[, ms]]]]])
      return new Date(parts[0], parts[1]-1, parts[2]).getTime(); // Note: months are 0-based
  }

  configurarNotificaciones() {
    let modal = this.modalCtrl.create('ConfigNotificacionesItiPage');
    modal.present();
    modal.onDidDismiss(data => {
      if (data) {
        console.log(data);
      }
    })
  }

}
