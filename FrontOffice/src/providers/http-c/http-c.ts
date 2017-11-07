import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
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


}