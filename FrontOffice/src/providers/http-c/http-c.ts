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

// addUser(data) {
//   return new Promise((resolve, reject) => {
//     this.http.post(this.apiUrl+'/users', JSON.stringify(data))
//       .subscribe(res => {
//         resolve(res);
//       }, (err) => {
//         reject(err);
//       });
//   });
// }

agregarItinerario(itinerario){
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

eliminarItinerario(idit){
  let params = new HttpParams().set("idit", idit);
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5/EliminarItinerario', {params:params}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

modificarItinerario(itinerario){
  return new Promise(resolve => {
    this.http.post(this.apiUrl+'/M5/ModificarItinerario', itinerario).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

setVisible(idusuario, iditi, visible){
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/SetVisible', {params: { idusuario: idusuario , iditinerario: iditi , visible: visible}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

eliminarItem(tipo,idit, iditem){
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5/EliminarItem_It',{params:{ tipo: tipo , idit: idit , iditem: iditem}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

verItem(id, tipo)
{ let item;
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
    }
  }
}


}
