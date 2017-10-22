import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides, reorderArray, AlertController, ModalController } from 'ionic-angular';
import { CalendarioPage } from '../calendario/calendario';
import * as moment from 'moment';
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
  list = false;
  nuevoViejo = true;
  mySlideOptions = {
    initialSlide: 0,
    loop: true
  };
  noIts = false;
  //************** fFIN DE DECLARACION DE VARIABLES *****************
  constructor(public navCtrl: NavController, public navParams: NavParams, private modalCtrl: ModalController, public alertCtrl: AlertController) {
    //this.its = Array();
    //this.its.eventos=Array();
    for (let x = 0; x < 5; x++) {
      this.items.push(x);
    }

    if (this.its == undefined){
      this.noIts = true;
    }
  }


  // goToSlide(index) {
  //   this.slides.slideTo(1 ,500);
  // }
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
              if (this.its == undefined) this.its=Array();
              this.its.push({nombre: data.Nombre});
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

  cargarItinerarios(){
    if (this.its.length == 0){
      this.its.push({
        id: 1,
        nombre: 'Disney World',
        eventos:
        Array({
          id: 1,
          tipo: 'evento',
          imagen: '../assets/images/epcot.jpg',
          titulo: 'Epcot International Festival of the Arts',
          startTime: moment('01/01/2018').format(),
          endTime: moment('01/04/2018').format(),
        },
        {
          id: 2,
          tipo: 'evento',
          imagen: '../assets/images/disney-maraton.jpg',
          titulo: 'Walt Disney World Marathon Weekend',
          startTime: moment('01/01/2018').format(),
          endTime: moment('01/06/2018').format(),
        }),
        fechaInicio: moment('02/01/2018').format(),
        fechaFin: moment('02/02/2018').format()
      },
      { id: 2,
        nombre: 'Viaje a Paris',
        eventos:
        Array({
          id: 3,
          tipo: 'actividad',
          imagen: '../assets/images/default-avatar1.svg',
          titulo: 'Comer croissants en la Torre Eiffel',
          //***************MM/DD/YYYY************
          startTime: moment('03/01/2018').format(),
          endTime: moment('03/03/2018').format(),
        }),
        fechaInicio: moment('03/01/2018').format(),
        fechaFin: moment('04/01/2018').format()
      });
    }
  }

  ordenar(){
    this.nuevoViejo = !this.nuevoViejo;
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
    // if (this.nuevoViejo == true){
    //     this.its.sort(function(a,b){
    //       return new Date(b.fechaInicio) - new Date(a.fechaInicio);
    //     });
    //
    // }else{
    //   this.its.sort(function(a,b){
    //     return new Date(a.fechaInicio) - new Date(b.fechaInicio);
    //   });
    // }
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
    this.cargarItinerarios();

  }

  ionViewDidLoad() {
    console.log('yujule');
  }


}
