import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
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
  userData: any;
  constructor(public http: Http) {
  }
  iniciarSesion(usuario,clave) 
  {  

    if(usuario.includes("@")){
      this.userData={correo : usuario, clave : clave};
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesioncorreo/?datos='+JSON.stringify(this.userData),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          resolve(-1);

        });
    });
    }
    else
    {
      this.userData={nombreUsuario : usuario, clave : clave};
      return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesionusuario/?datos='+JSON.stringify(this.userData),"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        resolve(-1);

      });
     });

    }
  }

  iniciarSesionFacebook(usuario)
  {
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesionsocial/?datos='+JSON.stringify(usuario),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          resolve(-1);

        });
    });
  }

  recuperarContrasena(correo)
  {
    this.userData={correo: correo};
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/CorreoRecuperar/?correo='+JSON.stringify(this.userData),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          resolve(-1);

        });
    });
  }

  
/**
 * [MODULO3]
 * Metodo para obtener la lista de amigos
 * @param usuario Nombre del usuario
 */
  listaAmigos(usuario) 
  {  
  
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/VisualizarListaAmigos/?nombreUsuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }
  
 /**
  * [MODULO 3] 
  * Metodo para eliminar un amigo
  * @param amigo nombre de usuario del amigo
  * @param usuario nombre del usuario
  */
  eliminarAmigo(amigo, usuario){
    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/EliminarAmigo/?nombreAmigo='+amigo+'&nombreUsuario='+usuario,"")
      .subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err),
        console.log(amigo),
        console.log(usuario)
      });
  });
}

/**
 * [MODULO 3] 
 * Metodo para visualizar la lista de grupos
 * @param usuario nombre del usuario
 */
  listaGrupo(usuario) 
  {  
  
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarListaGrupos/?nombreUsuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

/**
 * [MODULO 3] 
 * Metodo para visualizar el perfil del grupo
 * @param usuario nombre de usuario
 */
  verperfilGrupo(usuario) 
  {  
  
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarPerfilGrupos/?id='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

  /**
   * [MODULO 3] 
   * Metodo para visualizar la lista de integrantes de un grupo
   * @param usuario nombre de usuario
   */
  listamiembroGrupo(usuario) 
  {  
  
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarMiembroGrupo/?idgrupo='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }


}