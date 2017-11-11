import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
import {RequestOptions, Request, RequestMethod} from '@angular/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the HttpCProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class HttpCProvider {
apiUrl = 'http://localhost:51049/api';
  constructor(public http: HttpClient) {
  console.log('Hello RestServiceProvider Provider');
}


loadItinerarios(id_usuario) {
  let params = new HttpParams().set("id_usuario", id_usuario);

  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M5/ConsultarItinerarios', { params: params }).subscribe(data => {
      resolve(data);
    }, err => {
      console.log(err);
    });
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
      }, (err) => {
        console.log(err)
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

eliminarItem(idit, idag){
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M5/EliminarItem_It',{params:{"idit": idit , "idag": idag}}).subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err)
      });
  });
}

}
