// import { Component, NgModule } from '@angular/core';
// import { IonicPage, NavController, NavParams, ModalController, AlertController } from 'ionic-angular';
// import * as moment from 'moment';
// /**
//  * Generated class for the CalendarioPage page.
//  *
//  * See https://ionicframework.com/docs/components/#navigation for more info on
//  * Ionic pages and navigation.
//  */
//
// @IonicPage()
// @Component({
//   selector: 'page-calendario',
//   templateUrl: 'calendario.html',
// })
// export class CalendarioPage {
//   eventSource = [];
//   viewTitle: string;
//   selectedDay = new Date();
//
//   calendar = {
//     mode: 'month',
//     currentDate: this.selectedDay
//   };
//   constructor(public navCtrl: NavController, private modalCtrl: ModalController, private alertCtrl: AlertController,  public navParams: NavParams) {
//     let itinerarios = this.navParams.get('itinerarios');
//     let eventData = {
//       title: '',
//       startTime: new Date(),
//       endTime: new Date()
//     };
//     let events= [{}];
//     itinerarios.forEach(element => {
//       element.eventos.forEach(evento => {
//         console.log(evento);
//         console.log(events);
//         events = this.eventSource;
//         events.push({
//           title: evento.titulo,
//           startTime: new Date(evento.startTime),
//           endTime: new Date(evento.endTime)
//
//         });
//         this.eventSource = events;
//       })
//     })
//
//
//     // for(var i = 0;i< itinerarios.length ;i++) {
//     //   for (var j = 0; j< itinerarios[i].eventos.length ;j++){
//     //     // eventData.push({
//     //     //   title: itinerarios[i].eventos[j].titulo,
//     //     //   startTime: new Date(itinerarios[i].eventos[j].startTime),
//     //     //   endTime: new Date(itinerarios[i].eventos[j].endTime)
//     //     // });
//     //     console.log(itinerarios[i].eventos[j].titulo);
//     //     console.log(itinerarios[i].eventos[j].startTime);
//     //     console.log(itinerarios[i].eventos[j].endTime);
//     //     eventData.title = itinerarios[i].eventos[j].titulo;
//     //     eventData.startTime= new Date(itinerarios[i].eventos[j].startTime);
//     //     eventData.endTime= new Date(itinerarios[i].eventos[j].endTime);
//     //     events = this.eventSource;
//     //     events.push(eventData);
//     //     console.log(events);
//     //     this.eventSource = [];
//     //     setTimeout(() => {
//     //       this.eventSource = events;
//     //     });
//     //   }
//     // }
//     // eventData.startTime= new Date('01/01/2018');
//     // eventData.endTime= new Date('01/04/2018');
//     // // eventData.itinerarios = this.navParams.get('itinerarios');
//     // // eventData.events = Array();
//     // //eventData.endTime = new Date(this.navCtrl.get('endTime'));
//     //  //moment(this.navParams.get('selectedDay')).format();
//     // console.log(eventData.itinerarios);
//     //  let events = this.eventSource;
//     //  //let prueba = this.eventSource;
//     // //  for(var i = 0;i< eventData.itinerarios.length ;i++) {
//     // //    console.log("hola");
//     // //    for (var j = 0; j< eventData.itinerarios[i].eventos.length ;j++){
//     // //      console.log(eventData.itinerarios[i].eventos[j].titulo);
//     // //      console.log(eventData.itinerarios[i].eventos[j].startTime);
//     // //      console.log(eventData.itinerarios[i].eventos[j].endTime);
//     // //      if (j==0){
//     // //        prueba.title= eventData.itinerarios[i].eventos[j].titulo;
//     // //        prueba.startTime = eventData.itinerarios[i].eventos[j].startTime;
//     // //        prueba.endTime = eventData.itinerarios[i].eventos[j].endTime;
//     // //      }
//     // //     //  events.push({
//     // //     //    title: eventData.itinerarios[i].eventos[j].titulo,
//     // //     //    startTime: new Date(eventData.itinerarios[i].eventos[j].startTime),
//     // //     //    endTime: new Date(eventData.itinerarios[i].eventos[j].endTime)
//     // //     //  });
//     // //    }
//     // //    //events.push(eventData.itinerario[i].eventos);
//     // //  }
//     //  events.push(eventData)
//     //  console.log(events);
//     //  this.eventSource = [];
//     //  setTimeout(() => {
//     //    this.eventSource = events;
//     //  });
//      //events.push(eventData.itinerarios.eventos);
//     // this.eventSource = [];
//     // setTimeout(() => {
//     //   this.eventSource = events;
//     // });
//   }
//
//
//
//
//   addEvent()  {
//     let modal = this.modalCtrl.create('EventModalPage', {selectedDay: this.selectedDay});
//     modal.present();
//     modal.onDidDismiss(data => {
//       if (data) {
//         let eventData = data;
//
//         eventData.startTime = new Date(data.startTime);
//         eventData.endTime = new Date(data.endTime);
//
//         let events = this.eventSource;
//         events.push(eventData);
//         this.eventSource = [];
//         setTimeout(() => {
//           this.eventSource = events;
//         });
//       }
//     })
//   }
//
//   onViewTitleChanged(title)  {
//     this.viewTitle = title;
//   }
//
//   onEventSelected(event)  {
//     let start = moment(event.startTime).format('LLLL');
//     let end = moment(event.endTime).format('LLLL');
//
//     let alert = this.alertCtrl.create({
//       title: '' + event.title,
//       subTitle: 'From: ' + start + '<br>To: ' + end,
//       buttons: ['OK']
//     })
//     alert.present();
//   }
//
//   onTimeSelected(ev)  {
//     this.selectedDay = ev.selectedTime;
//   }
//
// }


import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ModalController, AlertController } from 'ionic-angular';
import { CalendarModal, CalendarModalOptions, DayConfig } from "ion2-calendar";
import { CalendarComponentOptions } from 'ion2-calendar';
import { EventosCalendarioService } from '../../services/eventoscalendario';
import * as moment from 'moment';

@IonicPage()
@Component({
   selector: 'page-calendario',
 templateUrl: 'calendario.html',
 })
export class CalendarioPage {
  its= Array();
  items = Array();
  date: string;

  type: 'string'; // 'string' | 'js-date' | 'moment' | 'time' | 'object'
  _daysConfig= this.eventos.getEventosItinerario();
  options: CalendarComponentOptions = {
    color: 'primary',
    daysConfig: this._daysConfig
  };
  constructor(
    public modalCtrl: ModalController,
    public eventos: EventosCalendarioService,
  ) {

   }

    openCalendar() {
      let _daysConfig: DayConfig[] = [];
      this.items = [
        {
          id: 4,
          titulo: 'Aventuras en Paris',
          tipo: 'evento',
          imagen: 'paris.jpg',
          startTime: '01/01/2018',
          endTime:'01/01/2018'
        },
        {
          id: 5,
          titulo: 'Amsterdam Sightseeing',
          tipo: 'lugar',
          imagen: 'Amsterdam.jpg',
          startTime: '02/02/2018',
          endTime: '02/03/2018'
        },
        {
          id: 6,
          titulo: 'Aventuras Divertidas',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime:'01/02/2018',
          endTime: '01/02/2018'
        },
        {
          id: 7,
          titulo: 'Un Lugar',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: new Date(),
          endTime: new Date()
        },
        {
          id: 8,
          titulo: 'Un evento',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: '04/02/2018',
          endTime: '04/02/2018'
        },
        {
          id: 9,
          titulo: 'Una de tus actividades',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime: '05/02/2018',
          endTime: '05/02/2018'
        },
      ];
      console.log(this.items);
        for (let i= 0 ; i < this.items.length; i++){
        _daysConfig.push({
          date: new Date(this.items[i].startTime),
          subTitle: '*',
          marked: true,
        });
      }

      const options: CalendarModalOptions = {
        from: new Date(2018, 0, 1),
        to: new Date(2018, 11.1),
        daysConfig: _daysConfig,
        pickMode: 'range',
        title: 'RANGE',
        color: 'danger'
      };

      let myCalendar =  this.modalCtrl.create(CalendarModal, {
        options: options
      });

      myCalendar.present();

      myCalendar.onDidDismiss(date => {
        console.log(date);
      });
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
            startTime: '2018/01/01',
            endTime: '2018/01/01',
          },
          {
            id: 2,
            tipo: 'evento',
            imagen: '../assets/images/disney-maraton.jpg',
            titulo: 'Walt Disney World Marathon Weekend',
            startTime: '2018/02/01',
            endTime: '2018/02/01',
          }),
          fechaInicio: '2018/02/01',
          fechaFin: '2018/02/01'
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
            startTime: '2018/03/01',
            endTime: '2018/03/01',
          }),
          fechaInicio: '2018/01/01',
          fechaFin: '2018/01/01'
        });
      }

      this.items = [
        {
          id: 4,
          titulo: 'Aventuras en Paris',
          tipo: 'evento',
          imagen:  'paris.jpg',
          startTime: '01/01/2018 3:45 a',
          endTime:'01/01/2018'
        },
        {
          id: 5,
          titulo: 'Amsterdam Sightseeing',
          tipo: 'lugar',
          imagen: 'Amsterdam.jpg',
          startTime: '02/02/2018',
          endTime: '02/03/2018'
        },
        {
          id: 6,
          titulo: 'Aventuras Divertidas',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime:'01/02/2018',
          endTime: '01/02/2018'
        },
        {
          id: 7,
          titulo: 'Un Lugar',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: new Date(),
          endTime: new Date()
        },
        {
          id: 8,
          titulo: 'Un evento',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: '04/02/2018',
          endTime: '04/02/2018'
        },
        {
          id: 9,
          titulo: 'Una de tus actividades',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime: '05/02/2018',
          endTime: '05/02/2018'
        },
      ];
    }

    ionViewWillEnter(){
      this._daysConfig = [];
      this.items = [
        {
          id: 4,
          titulo: 'Aventuras en Paris',
          tipo: 'evento',
          imagen: 'paris.jpg',
          startTime: '01/01/2018',
          endTime:'01/01/2018'
        },
        {
          id: 5,
          titulo: 'Amsterdam Sightseeing',
          tipo: 'lugar',
          imagen: 'Amsterdam.jpg',
          startTime: '02/02/2018',
          endTime: '02/03/2018'
        },
        {
          id: 6,
          titulo: 'Aventuras Divertidas',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime:'01/02/2018',
          endTime: '01/02/2018'
        },
        {
          id: 7,
          titulo: 'Un Lugar',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: new Date(),
          endTime: new Date()
        },
        {
          id: 8,
          titulo: 'Un evento',
          tipo: 'lugar',
          imagen: 'default-avatar1.svg',
          startTime: '04/02/2018',
          endTime: '04/02/2018'
        },
        {
          id: 9,
          titulo: 'Una de tus actividades',
          tipo: 'actividad',
          imagen: 'default-avatar1.svg',
          startTime: '05/02/2018',
          endTime: '05/02/2018'
        },
      ];
        for (let i= 0 ; i < this.items.length; i++){
        this._daysConfig.push({
          date: new Date(this.items[i].startTime),
          subTitle: '*',
          marked: true,
        });
      }

      this.options.daysConfig = this._daysConfig;
    }

    onChange($event) {
      console.log($event._d);
       let eventos = this.items.filter(item => {
         return (item.startTime == moment($event._d).format('DD/MM/YYYY'))
       })
       console.log(eventos);
      let modal = this.modalCtrl.create('DetalleDiaPage', {date: $event , eventos: eventos});
      modal.present();
      modal.onDidDismiss(data => {
        console.log(data);
       });

      }


}
