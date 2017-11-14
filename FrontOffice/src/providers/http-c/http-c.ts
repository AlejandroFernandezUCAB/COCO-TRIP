import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
import {RequestOptions, Request, RequestMethod} from '@angular/http';
import 'rxjs/add/operator/map';
import {Observable} from 'rxjs/Rx';


@Injectable()
export class HttpCProvider {
apiUrl = 'http://localhost:51049/api';
  constructor(public http: HttpClient) {
}


loadItinerarios(id_usuario)
{
  let params = new HttpParams().set("id_usuario", id_usuario);
  return new Promise((resolve, reject) => {
    this.http.get(this.apiUrl+'/M5/ConsultarItinerarios', { params: params })
    .subscribe(data => resolve(data),
      err => resolve(-1)
    );
  });
}

public agregarItinerario(itinerario)
{
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M5/AgregarItinerario', itinerario).subscribe(res => {
        resolve(res);
        console.log("res");
        console.log(res);
      }, (err) => {
        err => resolve(-1)
      });
  });
}

public eliminarItinerario(idit)
{
  let params = new HttpParams().set("idit", idit);
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5/EliminarItinerario', {params:params}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public modificarItinerario(itinerario)
{
  return new Promise(resolve => {
    this.http.post(this.apiUrl+'/M5/ModificarItinerario', itinerario).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public setVisible(idusuario, iditi, visible)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/SetVisible', {params: { idusuario: idusuario , iditinerario: iditi , visible: visible}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public eliminarItem(tipo,idit, iditem)
{
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5/EliminarItem_It',{params:{ tipo: tipo , idit: idit , iditem: iditem}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public NotificacionUsuario(id_usuario)
{
  let params = new HttpParams().set("id_usuario", id_usuario);
  return new Promise((resolve, reject) => {
    this.http.get(this.apiUrl+'/M5/NotificacionCorreo', { params: params })
    .subscribe(data => resolve(data),
      err => resolve(-1)
    );
  });
}

verItem(id, tipo)
{
  let item;
  if (tipo=='Lugar Turistico'){
    let params = new HttpParams().set("id", id);
    return new Promise((resolve, reject) => {
      this.http.get(this.apiUrl+'/M7_LugaresTuristicos/GetLugar', { params: params })
      .subscribe(data => resolve(data),
        err => resolve(-1)
      );
    });
  }else {
    if(tipo=='Actividad'){
      let params = new HttpParams().set("id", id);
      return new Promise((resolve, reject) => {
        this.http.get(this.apiUrl+'/M7_LugaresTuristicos/GetActividad', { params: params })
        .subscribe(data => resolve(data),
          err => resolve(-1)
        );
      });
    }else
    if(tipo=='Evento'){
      let params = new HttpParams().set("id", id);
      return new Promise((resolve, reject) => {
        this.http.get(this.apiUrl+'/M8_Eventos/ConsultarEvento', { params: params })
        .subscribe(data => resolve(data),
          err => resolve(-1)
        );
      });
    }
  }
}

ConsultarEventos(busqueda, finicio, ffin)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/ConsultarEventos',{params:{ busqueda: busqueda , fechainicio: finicio , fechafin: ffin}}).subscribe(res => {
        resolve(res);
        console.log("ola");
        console.log(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

ConsultarLugarTuristico(busqueda){
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/ConsultarLugaresTuristicos',{params:{ busqueda: busqueda }}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

ConsultarActividades(busqueda){
  return new Promise(resolve => {
    console.log(busqueda, "busqueda");
    this.http.get(this.apiUrl+'/M5/ConsultarActividad',{params:{ busqueda: busqueda }}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

public agregarItem_It(tipo, idit,iditem,fechainicio,fechafin)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/AgregarItem_It',{params:{ tipo: tipo , idit: idit , iditem: iditem,fechaini:fechainicio,fechafin:fechafin}}
    ).subscribe(res => {
        resolve(res);
        console.log("res");
        console.log(res);
      }, (err) => {
        err => resolve(-1)
      });
  });
}

}
