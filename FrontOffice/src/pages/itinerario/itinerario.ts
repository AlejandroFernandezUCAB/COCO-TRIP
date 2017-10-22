import { Component, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Slides, reorderArray, AlertController, ModalController } from 'ionic-angular';
import { CalendarioPage } from '../calendario/calendario';

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
  items = [];
  //************** DECLARACION DE VARIABLES *****************
  base_url = '../assets/images/';
  edit = false;
  delete= false;
  count = 0;
//  evento= {tipo: '', imagen: '', titulo: '', startTime: Date, endTime: Date};
  its= Array();

  constructor(public navCtrl: NavController, public navParams: NavParams, private modalCtrl: ModalController, public alertCtrl: AlertController) {
    //this.its = Array();
    //this.its.eventos=Array();
    for (let x = 0; x < 5; x++) {
      this.items.push(x);
    }
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
    this.its.push({
        id: 1,
        nombre: 'Disney World',
        eventos:
        Array({
          id: 1,
          tipo: 'evento',
          imagen: '../assets/images/epcot.jpg',
          titulo: 'Epcot International Festival of the Arts',
          startTime: new Date('01/01/2018'),
          endTime: new Date('03/01/2018'),
        },
        {
          id: 2,
          tipo: 'evento',
          imagen: '../assets/images/disney-maraton.jpg',
          titulo: 'Walt Disney World Marathon Weekend',
          startTime: new Date('03/01/2018'),
          endTime: new Date('03/01/2018'),
        })
      },
      { id: 2,
        nombre: 'Viaje a Paris',
        eventos:
        Array({
          id: 3,
          tipo: 'actividad',
          imagen: '../assets/images/default-avatar1.svg',
          titulo: 'Comer croissants en la Torre Eiffel',
          startTime: new Date('03/01/2018'),
          endTime: new Date('03/01/2018'),
        })
      });

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



  ionViewWillEnter(){
    this.cargarItinerarios();
  }

  ionViewDidLoad() {
    console.log('yujule');
  }


}
