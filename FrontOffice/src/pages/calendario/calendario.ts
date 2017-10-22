import { Component, NgModule } from '@angular/core';
import { IonicPage, NavController, NavParams, ModalController, AlertController } from 'ionic-angular';
import * as moment from 'moment';
/**
 * Generated class for the CalendarioPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-calendario',
  templateUrl: 'calendario.html',
})
export class CalendarioPage {
  eventSource = [];
  viewTitle: string;
  selectedDay = new Date();

  calendar = {
    mode: 'month',
    currentDate: this.selectedDay
  };
  constructor(public navCtrl: NavController, private modalCtrl: ModalController, private alertCtrl: AlertController,  public navParams: NavParams) {
    let eventData = Array();
    eventData.itinerarios = this.navParams.get('itinerarios');
    eventData.events = Array();
    //eventData.endTime = new Date(this.navCtrl.get('endTime'));
     //moment(this.navParams.get('selectedDay')).format();
    console.log(eventData.itinerarios);
     let events = Array();
     for(var i = 0;i< eventData.itinerarios.length ;i++) {
       console.log("hola");
       for (var j = 0; j< eventData.itinerarios[i].eventos.length ;j++){
         console.log(eventData.itinerarios[i].eventos[j]);
         events.push(moment(eventData.itinerarios[i].eventos[j]).format());
       }
       //events.push(eventData.itinerario[i].eventos);
     }
     console.log(events);
     this.eventSource = [];
     setTimeout(() => {
       this.eventSource = events;
     });
     //events.push(eventData.itinerarios.eventos);
    // this.eventSource = [];
    setTimeout(() => {
      this.eventSource = events;
    });
  }




  addEvent()  {
    let modal = this.modalCtrl.create('EventModalPage', {selectedDay: this.selectedDay});
    modal.present();
    modal.onDidDismiss(data => {
      if (data) {
        let eventData = data;

        eventData.startTime = new Date(data.startTime);
        eventData.endTime = new Date(data.endTime);

        let events = this.eventSource;
        events.push(eventData);
        this.eventSource = [];
        setTimeout(() => {
          this.eventSource = events;
        });
      }
    })
  }

  onViewTitleChanged(title)  {
    this.viewTitle = title;
  }

  onEventSelected(event)  {
    let start = moment(event.startTime).format('LLLL');
    let end = moment(event.endTime).format('LLLL');

    let alert = this.alertCtrl.create({
      title: '' + event.title,
      subTitle: 'From: ' + start + '<br>To: ' + end,
      buttons: ['OK']
    })
    alert.present();
  }

  onTimeSelected(ev)  {
    this.selectedDay = ev.selectedTime;
  }

}
