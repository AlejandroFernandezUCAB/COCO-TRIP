import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { DayConfig } from 'ion2-calendar';
import * as moment from 'moment';

@Injectable()

export class EventosCalendarioService {
  _eventos = [];
  _itinerarios = [];
  _daysConfig: DayConfig[] = [];
  _base_url = '../assets/images/';
  _notificaciones = Object();
  constructor(){

    this._notificaciones = {
      correo: true,
      push: false
    }

    this._eventos = [
      {
        id: 4,
        titulo: 'Aventuras en Paris',
        tipo: 'evento',
        imagen: this._base_url+'paris.jpg',
        startTime: '01/01/2018',
        horaInicio: '2:30 PM',
        endTime:'01/01/2018',
        horaFin: '3:30 PM'
      },
      {
        id: 5,
        titulo: 'Amsterdam Sightseeing',
        tipo: 'lugar',
        imagen: this._base_url+'Amsterdam.jpg',
        startTime: '01/01/2018',
        horaInicio: '2:00 PM',
        endTime: '01/01/2018',
        horaFin: '3:00 PM'
      },
      {
        id: 6,
        titulo: 'Aventuras Divertidas',
        tipo: 'actividad',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime:'01/01/2018',
        horaInicio: '4:00 PM',
        endTime: '01/01/2018',
        horaFin: '4:30 PM'
      },
      {
        id: 7,
        titulo: 'Un Lugar',
        tipo: 'lugar',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '',
        horaInicio: '',
        endTime: '',
        horaFin: ''
      },
      {
        id: 8,
        titulo: 'Un evento',
        tipo: 'lugar',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '04/02/2018',
        horaInicio: '4:30 PM',
        endTime: '04/02/2018',
        horaFin: '5:30 PM'
      },
      {
        id: 9,
        titulo: 'Una de tus actividades',
        tipo: 'actividad',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '05/02/2018',
        horaInicio: '1:00 PM',
        endTime: '05/02/2018',
        horaFin: '2:00 PM'
      },
    ];

    this._itinerarios.push({
      id: 1,
      nombre: 'Disney World',
      eventos:
      Array({
        id: 1,
        tipo: 'evento',
        imagen: '../assets/images/epcot.jpg',
        titulo: 'Epcot International Festival of the Arts',
        startTime: '01/01/2018',
        horaInicio: '03:08PM',
        endTime: '01/01/2018',
        horaFin: '05:00PM'
        },
        {
        id: 2,
        tipo: 'evento',
        imagen: '../assets/images/disney-maraton.jpg',
        titulo: 'Walt Disney World Marathon Weekend',
        startTime: '01/02/2018',
        horaInicio: '01:00 PM',
        endTime: '01/02/2018',
        horaFin: '05:00 PM'
        }),
      fechaInicio: '01/02/2018',
      fechaFin: '01/02/2018',
      visible: true
      },
      {
      id: 2,
      nombre: 'Viaje a Paris',
      eventos:
      Array({
        id: 3,
        tipo: 'actividad',
        imagen: '../assets/images/default-avatar1.svg',
        titulo: 'Comer croissants en la Torre Eiffel',
        //***************MM/DD/YYYY************
        startTime: '01/02/2018',
        horaInicio: '03:00 PM',
        endTime: '01/02/2018',
        horaFin: '05:00 PM'
        }),
      fechaInicio: '01/02/2018',
      fechaFin: '01/02/2018',
      visible: true
      });

  }

  public getEventosItinerario() {
      for (let i= 0 ; i < this._itinerarios.length; i++){
        if (this._itinerarios[i].visible == true){
          for (let j =0;  j< this._itinerarios[i].eventos.length; j++)
          { console.log(this._itinerarios[i].eventos[j].titulo)
            this._daysConfig.push({
              date: new Date(this._itinerarios[i].eventos[j].startTime),
              subTitle: '*',
              marked: true,
            });
          }
        }
      }
    return this._daysConfig;
  }

  public getItinerarios() {
    return this._itinerarios;
  }

  public getEventosGlobales(){
    return this._eventos;
  }

  public getNotifcacionesConfig(){
    return this._notificaciones;
  }

  public updateItinerarioVisible(visible){
    let up_itinerario = Array();
    this._itinerarios.forEach(iti => {
      if (iti.id == visible.id){
       iti.visible = visible.visible;
      }
    })
    console.log(this._itinerarios);
    return true;
  }
}
