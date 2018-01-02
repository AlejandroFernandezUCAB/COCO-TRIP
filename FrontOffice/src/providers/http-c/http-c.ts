import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
import {RequestOptions, Request, RequestMethod} from '@angular/http';
import 'rxjs/add/operator/map';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import {Observable} from 'rxjs/Rx';
import { listenToElementOutputs } from '@angular/core/src/view/element';
import { Itinerario } from '../../dataAccessLayer/domain/itinerario';


@Injectable()
export class HttpCProvider {
//apiUrl = 'http://localhost:8082/api';
apiUrl = this.restapiService.apiUrl; //obtener direccion global del API Rest
  constructor(public http: HttpClient, public restapiService: RestapiService) {
}


loadItinerarios(id_usuario)
{
  let params = new HttpParams().set("id_usuario", id_usuario);
  return new Promise((resolve, reject) => {
    this.http.get(this.apiUrl+'/M5_Itinerario/ConsultarItinerarios', { params: params })
    .subscribe(data => resolve(data),
      err => resolve(-1)
    );
    
  });
}

public agregarItinerario(itinerario)
{
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M5_Itinerario/AgregarItinerario', itinerario).subscribe(res => {
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
    this.http.delete(this.apiUrl+'/M5_Itinerario/EliminarItinerario', {params:params}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public modificarItinerario(itinerario)
{
  console.log("http-c");
  console.log(itinerario.Id);
  console.log(itinerario.IdUsuario);
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M5_Itinerario/ModificarItinerario', itinerario).subscribe(res => {
        resolve(res);
        console.log("res: ");
        console.log(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public setVisible(idusuario, iditi, visible)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5_Itinerario/SetVisible', {params: { idusuario: idusuario , iditinerario: iditi , visible: visible}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

public eliminarItem(tipo,idit, iditem)
{
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5_Itinerario/EliminarItem_It',{params:{ tipo: tipo , idit: idit , iditem: iditem}}).subscribe(res => {
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
    this.http.get(this.apiUrl+'/M5_Itinerario/NotificacionCorreo', { params: params })
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
    this.http.get(this.apiUrl+'/M5_Itinerario/ConsultarEventos',{params:{ busqueda: busqueda , fechainicio: finicio , fechafin: ffin}}).subscribe(res => {
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
    this.http.get(this.apiUrl+'/M5_Itinerario/ConsultarLugaresTuristicos',{params:{ busqueda: busqueda }}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

ConsultarActividades(busqueda){
  return new Promise(resolve => {
    console.log(busqueda, "busqueda");
    this.http.get(this.apiUrl+'/M5_Itinerario/ConsultarActividad',{params:{ busqueda: busqueda }}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

public agregarItem_It(tipo, idit,iditem,fechainicio,fechafin)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5_Itinerario/AgregarItem_It',{params:{ tipo: tipo , idit: idit , iditem: iditem,fechaini:fechainicio,fechafin:fechafin}}
    ).subscribe(res => {
        resolve(res);
        console.log(res);
      }, (err) => {
        err => resolve(-1)
      });
  });
}
/*
public agregarNotificacion(idusuario)
{
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M5_Itinerario/AgregarNotificacionConfiguracion', idusuario).subscribe(res => {
        resolve(res);
        console.log("res");
        console.log(res);
      }, (err) => {
        err => resolve(-1)
      });
  });
}
*/

public agregarNotificacion(idusuario)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5_Itinerario/AgregarNotificacionConfiguracion', {params :{id_usuario: idusuario}}).subscribe(res => {
        resolve(res);
        console.log("res");
        console.log(res);
      }, (err) => {
        err => resolve(-1)
      });
  });
}

public modificarNotificacionCorreo(idusuario, correo)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5_Itinerario/ModificarNotificacionConfiguracion', {params :{id_usuario: idusuario, correo: correo}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

public getNotificacionesConfig(idusuario)
{
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5_Itinerario/ConsultarNotificacion', {params :{id_usuario: idusuario}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        err => resolve(-1);
      });
  });
}

}
