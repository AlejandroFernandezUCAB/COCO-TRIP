import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';
import { DayConfig } from 'ion2-calendar';
import * as moment from 'moment';
import { HttpCProvider } from '../providers/http-c/http-c';
import { Storage } from '@ionic/storage';

@Injectable()

export class EventosCalendarioService {
  _eventos = [];
  _itinerarios = [];
  _daysConfig: DayConfig[] = [];
  _base_url = '../assets/images/';
  _notificaciones = Object();
  _itis:any;
  _itinerarios1= [];
  IdUsuario:any;
  constructor
  (public http: HttpCProvider,
  private storage: Storage)
  {

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
    this.storage.get('id').then((val) => {
      this.IdUsuario = val;
      this.consultarItinerarios(this.IdUsuario);
    })
  }

  public getEventosItinerario()
  {
      for (let i= 0 ; i < this._itis.length; i++){
        if (this._itis[i].Visible == true){
          console.log("entro");
          for (let j =0;  j< this._itis[i].Items_agenda.length; j++)
          {
            this._daysConfig.push({
              date: new Date(this._itis[i].Items_agenda[j].FechaInicio),
              subTitle: '*',
              marked: true,
            });
          }
        }
      }
    return this._daysConfig;
  }

  public getItinerarios()
  {
    return this._itis;
  }

  public getEventosGlobales()
  {
    return this._eventos;
  }

  public getNotifcacionesConfig()
  {
    return this._notificaciones;
  }

  public consultarItinerarios(id_usuario)
  {
    this.http.loadItinerarios(id_usuario)
    .then(data => {
      this._itis = data;
    });
  }

}
