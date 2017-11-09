import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides, reorderArray, AlertController, ModalController } from 'ionic-angular';
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
  its= Array();
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
  //************** FIN DE DECLARACION DE VARIABLES *****************
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private modalCtrl: ModalController,
    public alertCtrl: AlertController,
    public itinerarios: EventosCalendarioService,
    public httpc: HttpCProvider
  ) {
    //this.its = Array();
    //this.its.eventos=Array();
    for (let x = 0; x < 5; x++) {
      this.items.push(x);
    }
    this.loadItinerarios();
    if (this.its == undefined){
      this.noIts = true;
      }
  }


  loadItinerarios() {
    this.httpc.loadItinerarios(1)
    .then(data => {
      this.users = data;
      console.log(this.users);
    });
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
                nombre: data.Nombre,
                eventos: Array()
              });                
              let newitinerario ={ Nombre:data.Nombre,IdUsuario:1 } 
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

  presentConfirm(id, index) {
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
          this.eliminarItinerario(id, index);
          this.httpc.eliminarItinerario(35);
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
          }
        }
      ]
    });
    alert.present();
  }

  eliminar(){
    this.delete = true;
  }

  eliminarItinerario(id, index){
     let eliminado = this.its.filter(item => item.id === id)[0];
     var removed_elements = this.its.splice(index, 1);
     if (this.its.length == 0){
       this.noIts = true;
       console.log("no its")
    
     }
   }

  eliminarItem(id_itinerario, id_evento, index){
    let iti_e_eliminado = this.its.filter(item => item.id === id_itinerario)[0];
    var removed_elements = iti_e_eliminado.eventos.splice(index, 1);
  }

  editar(){
    this.edit = true;
    for(var i = 0;i< this.its.length;i++) {
      this.its[i].edit = this.its[i].nombre;
    }
  }

  done(){
    this.edit = false;
  }

  doneDeleting(){
    this.delete = false;
  }


  ordenar(){
    this.nuevoViejo = !this.nuevoViejo;
    var dates = Array();
    if (this.nuevoViejo == true){
     for(var i = 0;i< this.its.length;i++) {
       this.its[i].eventos.sort(function(a,b){
            return new Date(b.startTime).getTime() - new Date(a.startTime).getTime();
         });
       }
    }else{
      for(var i = 0;i< this.its.length;i++) {
        this.its[i].eventos.sort(function(a,b){
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
    let modal = this.modalCtrl.create('ItemModalPage', {itinerario: iti});
    modal.present();
    modal.onDidDismiss(data => {
      if (data) {
        let eventoData = data;
        let itinerario_nuevo = data.itinerario;
        eventoData.id = data.evento_nuevo.id;
        eventoData.titulo = data.evento_nuevo.titulo;
        eventoData.tipo = data.evento_nuevo.tipo;
        eventoData.imagen = data.evento_nuevo.imagen;
        eventoData.startTime = data.evento_nuevo.startTime;
        eventoData.endTime = data.evento_nuevo.endTime;
        for(var i = 0;i< this.its.length;i++) {
          if (this.its[i].nombre == itinerario_nuevo) {
            //si el itinerario no tiene eventos, se inicializa el arreglo eventos
            if (this.its[i].eventos == undefined){
              this.its[i].eventos = Array();
            }
            this.its[i].eventos.push(eventoData);
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
      if (this.its[i].eventos == undefined){
        this.its[i].eventos = Array();
      }
    }

    if(this.list==true){
      this.list = false;
    }
    else{
      this.list=true;
    }
  }

  ionViewWillEnter(){
    this.its = this.itinerarios.getItinerarios();
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
