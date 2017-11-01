import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { DayConfig } from 'ion2-calendar';

@Injectable()

export class EventosCalendarioService {
  eventos = [];
  _daysConfig: DayConfig[] = [];
  constructor(){

  }

  public getEventosItinerario() {
    this.eventos = [
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

    console.log(this.eventos);
      for (let i= 0 ; i < this.eventos.length; i++){
      this._daysConfig.push({
        date: new Date(this.eventos[i].startTime),
        subTitle: '*',
        marked: true,
      });
    }
    console.log(this._daysConfig);
    return this._daysConfig;
  }

}
