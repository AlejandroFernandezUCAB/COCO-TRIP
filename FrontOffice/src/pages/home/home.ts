import { Component } from '@angular/core';
import { NavController,ModalController,NavParams } from 'ionic-angular';
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
lts : any;
eve: any;
idUser: any;
apiUrl = 'http://localhost:8091/fotos/';
aux: string;

  constructor(public navCtrl: NavController,private storage: Storage,public navParams: NavParams,public menu: MenuController,public restapiService : RestapiService, public http: HttpCProvider,private modalCtrl: ModalController) {
    //console.log(this.its2);
 //   this.IniciarNotificaciones();
    this.menu.enable(true);
    this.eveSegunPreferencia();
    this.ltSegunPreferencia();
  }
 
 ltSegunPreferencia(){


    this.storage.get('id').then(idUser=>{
      this.idUser=idUser;
      //console.log(this.idUser+"id en el .get");

      //console.log(this.idUser+"id despues del .get");
      if(this.idUser!=null){
        this.restapiService.ltSegunPreferencias(this.idUser)
        .then(data=>{
  
          if(data==-1){
            console.log('error al recibir del webservice');
            //this.navCtrl.setRoot(LoginPage);
  
  
          }
  
  
          else{
          this.lts = data;
          //console.log(this.lts);
          }
        });
  
      }
      else{
      console.log('error al recibir el id del storage');
      //this.navCtrl.setRoot(LoginPage);
      }
    });
    
 } 
detalleEvento(eventos){
  let modal = this.modalCtrl.create('DetalleEventoPage', {eventos:eventos});
  modal.present();
}
 eveSegunPreferencia(){
     // var ev= Array();
      this.storage.get('id').then(idUser=>{      
        this.idUser=idUser;
        if(this.idUser){
        this.restapiService.eveSegunPreferencias(this.idUser)
        .then(data=>{

          if(data==-1){
            //console.log('error al recibir del webservice');
            //this.navCtrl.setRoot(LoginPage);

          }
          else{
          this.eve = data;
          this.eve.forEach(eve => {
            //console.log(eve.LocalFotoRuta);
            this.aux = eve.LocalFotoRuta;
            //console.log(this.aux);
            eve.LocalFotoRuta = this.apiUrl + this.aux;
            //console.log(this.eve.LocalFotoRuta);
            eve.FechaInicio= moment(eve.FechaInicio).format('DD-MM-YYYY');
            eve.FechaFin= moment(eve.FechaFin).format('DD-MM-YYYY');
            console.log(this.eve);
          });
          }
        });
  
      }
      else{
      //console.log('error al recibir el id del storage');
      //this.navCtrl.setRoot(LoginPage);
      }}) ;   

   } 

    /* IniciarNotificaciones() {
    this.http.NotificacionUsuario(1)
    .then(data => {
      this._itis = data;
      console.log(this._itis);
    });
  }*/
}
