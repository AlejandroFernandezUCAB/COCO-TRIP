import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the Restapi provider.

  See https://angular.io/docs/ts/latest/guide/dependency-injection.html
  for more info on providers and Angular 2 DI.
*/
@Injectable()
export class RestapiService {
  apiUrl = 'http://localhost:8091/api';
  data : any;
  Origin: any;
  constructor(public http: Http) {
    console.log('Hello Restapi Provider');
  }
  iniciarSesion(usuario) 
  {  
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M1_Login/iniciarsesioncorreo/?datos='+JSON.stringify(usuario))
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        });
    });
  }

  iniciarSesionFacebook(usuario)
  {
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M1_Login/iniciarsesionsocial/?datos='+JSON.stringify(usuario))
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        });
    });
  }


}