import { Component } from '@angular/core';
import { NavController,ModalController,NavParams } from 'ionic-angular';
import * as moment from 'moment';
import { MenuController } from 'ionic-angular';
import { HttpCProvider } from '../../providers/http-c/http-c';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { LoginPage } from '../login/login';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {
_itis : any;
lts : any;
eve: any;
idUser: any;
apiUrl = 'http://localhost:8091/fotos/';
aux: string;

  constructor(public navCtrl: NavController,private storage: Storage,public navParams: NavParams,public menu: MenuController,public restapiService : RestapiService, public http: HttpCProvider,private modalCtrl: ModalController,public translateService: TranslateService) {
    //console.log(this.its2);
    this.IniciarNotificaciones();
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
            
            //console.log(data);  
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
detalleLTS(lugares){
  let modal = this.modalCtrl.create('DetalleLtPage', {lugares:lugares});
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
            //console.log(data);  
          this.eve = data;
          this.eve.forEach(eve => {
            //console.log(eve.LocalFotoRuta);
            this.aux = eve.LocalFotoRuta;
            //console.log(this.aux);
            eve.LocalFotoRuta = this.apiUrl + this.aux;
            //console.log(this.eve.LocalFotoRuta);
            eve.FechaInicio= moment(eve.FechaInicio).format('DD-MM-YYYY');
            eve.FechaFin= moment(eve.FechaFin).format('DD-MM-YYYY');
            //console.log(this.eve);
          });
          }
        });
  
      }
      else{
      //console.log('error al recibir el id del storage');
      //this.navCtrl.setRoot(LoginPage);
      }}) ;   

   } 

    IniciarNotificaciones() {
      this.storage.get('id').then(idUser=>{      
        this.idUser=idUser;
        
        if(this.idUser){
          let idusu ={ id_usuario: idUser }
          this.http.agregarNotificacion(this.idUser).then(agre => {
            if(agre == true){
              this.http.getNotificacionesConfig(this.idUser).then(confic => {

                if(confic == true){
                  setInterval(() => {
                    this.http.NotificacionUsuario(this.idUser).then(data => {
                      this._itis = data;
                      
                      console.log(this._itis);
                    });
                  }, 1000 * 60 * 12);
                 
                }
                else{
                  //No desea recibir notificaciones.
                }
              });
            }
            else{
              console.log("Error agregando.");
            }
          });          
        }
        else{
          console.log('Error al recibir el id del storage');
        }
      });  
  }

}
