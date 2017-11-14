import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import * as moment from 'moment';
import { MenuController } from 'ionic-angular';
import { HttpCProvider } from '../../providers/http-c/http-c';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { LoginPage } from '../login/login';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
_itis : any;
lts : any;
eve: any;
idUser: any;


  constructor(public navCtrl: NavController,private storage: Storage,public menu: MenuController,public restapiService : RestapiService, public http: HttpCProvider) {
    //console.log(this.its2);
    this.IniciarNotificaciones();
    this.menu.enable(true);
    this.eveSegunPreferencia();
    this.ltSegunPreferencia();
  }
 
 ltSegunPreferencia(){


    this.storage.get('id').then(idUser=>{
      this.idUser=idUser;
      console.log(this.idUser+"id en el .get");

      console.log(this.idUser+"id despues del .get");
      if(this.idUser!=null){
        this.restapiService.ltSegunPreferencias(this.idUser)
        .then(data=>{
  
          if(data==-1){
            console.log('error al recibir del webservice');
            //this.navCtrl.setRoot(LoginPage);
  
  
          }
  
  
          else{
          this.lts = data;
          console.log(this.lts);
          }
        });
  
      }
      else{
      console.log('error al recibir el id del storage');
      //this.navCtrl.setRoot(LoginPage);
      }
    });
    
 } 

 eveSegunPreferencia(){

      this.storage.get('id').then(idUser=>{      
        this.idUser=idUser;
        if(this.idUser){
        this.restapiService.eveSegunPreferencias(this.idUser)
        .then(data=>{

          if(data==-1){
            console.log('error al recibir del webservice');
            //this.navCtrl.setRoot(LoginPage);

          }
          else{
          this.eve = data;
          }
        });
  
      }
      else{
      console.log('error al recibir el id del storage');
      //this.navCtrl.setRoot(LoginPage);
      }}) ;   

   } 

    IniciarNotificaciones() {
      console.log("Llego al metodo");
      this.storage.get('id').then(idUser=>{      
        this.idUser=idUser;
        
        if(this.idUser){
          this.http.agregarNotificacion(this.idUser).then(agre => {
            if(agre == true){
              this.http.getNotificacionesConfig(this.idUser).then(confic => {
                
                console.log("Llego al get notificaciones");
                console.log(confic);
                
                if(confic == true){
                  setInterval(() => {
                    this.http.NotificacionUsuario(this.idUser).then(data => {
                      this._itis = data;
                      
                      console.log(this._itis);
                    });
                  }, 10000);
                  /*
                  this.http.NotificacionUsuario(this.idUser).then(data => {
                    this._itis = data;
                    
                    console.log(this._itis);
                  });            
                  */
                }
                else{
                  console.log("No desea recibir notificaciones.");
                }
              });
            }
            else{
              console.log("Algo ocurrio agregando.");
            }
          });          
        }
        else{
          console.log('error al recibir el id del storage');
        }
      });  
  }

}
