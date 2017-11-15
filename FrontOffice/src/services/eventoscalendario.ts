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

    this.storage.get('id').then((val) => {
      this.IdUsuario = val;
      this.consultarItinerarios(this.IdUsuario);
    })
  }

  public getEventosItinerario()
  {
      for (let i= 0 ; i < this._itis.length; i++){
        if (this._itis[i].Visible == true){
          this.consultarItinerarios(this.IdUsuario);
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

  public getNotifcacionesConfig(idusuario)
  {
    this.http.getNotificacionesConfig(idusuario)
    .then(data =>{
      this._notificaciones.correo = data;
      this._notificaciones.push = false;
      return this._notificaciones;
    })
  }

  public consultarItinerarios(id_usuario)
  {
    this.http.loadItinerarios(id_usuario)
    .then(data => {
      this._itis = data;
    });
  }

}
