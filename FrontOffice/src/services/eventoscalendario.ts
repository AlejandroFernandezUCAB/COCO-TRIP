import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { DayConfig } from 'ion2-calendar';
import * as moment from 'moment';
import { HttpCProvider } from '../providers/http-c/http-c';

@Injectable()

export class EventosCalendarioService {
  _eventos = [];
  _itinerarios = [];
  _daysConfig: DayConfig[] = [];
  _base_url = '../assets/images/';
  _notificaciones = Object();
  _itis:any;
  _itinerarios1= [];
  constructor(public http: HttpCProvider){

    this._notificaciones = {
      correo: true,
      push: false
    }

    this._eventos = [
      {
        Id: 4,
        Nombre: 'Aventuras en Paris',
        imagen: this._base_url+'paris.jpg',
        startTime: '01/01/2018',
        horaInicio: '2:30 PM',
        endTime:'01/01/2018',
        horaFin: '3:30 PM'
      },
      {
        Id: 5,
        Nombre: 'Amsterdam Sightseeing',
        imagen: this._base_url+'Amsterdam.jpg',
        startTime: '01/01/2018',
        horaInicio: '2:00 PM',
        endTime: '01/01/2018',
        horaFin: '3:00 PM'
      },
      {
        Id: 6,
        Nombre: 'Aventuras Divertidas',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime:'01/01/2018',
        horaInicio: '4:00 PM',
        endTime: '01/01/2018',
        horaFin: '4:30 PM'
      },
      {
        Id: 7,
        Nombre: 'Un Lugar',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '',
        horaInicio: '',
        endTime: '',
        horaFin: ''
      },
      {
        Id: 8,
        Nombre: 'Un evento',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '04/02/2018',
        horaInicio: '4:30 PM',
        endTime: '04/02/2018',
        horaFin: '5:30 PM'
      },
      {
        Id: 9,
        Nombre: 'Una de tus actividades',
        imagen: this._base_url + 'default-avatar1.svg',
        startTime: '05/02/2018',
        horaInicio: '1:00 PM',
        endTime: '05/02/2018',
        horaFin: '2:00 PM'
      },
    ];

    // this._itinerarios.push({
    //   Id: 1,
    //   Nombre: 'Disney World',
    //   Items_agenda:
    //   Array({
    //     id: 1,
    //     tipo: 'evento',
    //     imagen: '../assets/images/epcot.jpg',
    //     titulo: 'Epcot International Festival of the Arts',
    //     startTime: '01/01/2018',
    //     horaInicio: '03:08PM',
    //     endTime: '01/01/2018',
    //     horaFin: '05:00PM'
    //     },
    //     {
    //     id: 2,
    //     tipo: 'evento',
    //     imagen: '../assets/images/disney-maraton.jpg',
    //     titulo: 'Walt Disney World Marathon Weekend',
    //     startTime: '01/02/2018',
    //     horaInicio: '01:00 PM',
    //     endTime: '01/02/2018',
    //     horaFin: '05:00 PM'
    //     }),
    //   FechaInicio: '01/02/2018',
    //   FechaFin: '01/02/2018',
    //   Visible: true
    //   },
    //   {
    //   Id: 2,
    //   Nombre: 'Viaje a Paris',
    //   Items_agenda:
    //   Array({
    //     id: 3,
    //     tipo: 'actividad',
    //     imagen: '../assets/images/default-avatar1.svg',
    //     titulo: 'Comer croissants en la Torre Eiffel',
    //     //***************MM/DD/YYYY************
    //     startTime: '01/02/2018',
    //     horaInicio: '03:00 PM',
    //     endTime: '01/02/2018',
    //     horaFin: '05:00 PM'
    //     }),
    //   fechaInicio: '01/02/2018',
    //   fechaFin: '01/02/2018',
    //   visible: true
    //   });


      this.consultarItinerarios(2);
  }

  public getEventosItinerario() {
      for (let i= 0 ; i < this._itis.length; i++){
        if (this._itis[i].Visible == true){
          for (let j =0;  j< this._itis[i].Items_agenda.length; j++)
          { console.log(this._itis[i].Items_agenda[j].Nombre)
            this._daysConfig.push({
              date: new Date(this._itis[i].Items_agenda[j].FechaInicio),
              subTitle: '*',
              marked: true,
            });
          }
        }
      }
      console.log("perra");
      console.log(this._daysConfig);
    return this._daysConfig;
  }

  public getItinerarios() {
    return this._itis;
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

  public consultarItinerarios(id_usuario){
    this.http.loadItinerarios(id_usuario)
    .then(data => {
      this._itis = data;
      console.log(this._itis);
    });
  }

}
