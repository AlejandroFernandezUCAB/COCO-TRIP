import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import * as moment from 'moment';
import { MenuController } from 'ionic-angular';
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

  constructor(public navCtrl: NavController,private storage: Storage,public menu: MenuController, public restapiService: RestapiService) {
    //console.log(this.its2);
    this.menu.enable(true);
  }
 
 ltSegunPreferencia(){

    var idUser=this.storage.get('id');
    if(idUser){
      this.restapiService.ltSegunPreferencias(idUser)
      .then(data=>{

        if(data==-1){
          console.log('error al recibir del webservice');
          this.navCtrl.setRoot(LoginPage);


        }


        else{
        this.lts = data;
        }
      });

    }
    else{
    console.log('error al recibir el id del storage');
    this.navCtrl.setRoot(LoginPage);
    }
 } 

 eveSegunPreferencia(){
  
      var idUser=this.storage.get('id');
      if(idUser){
        this.restapiService.eveSegunPreferencias(idUser)
        .then(data=>{

          if(data==-1){
            console.log('error al recibir del webservice');
            this.navCtrl.setRoot(LoginPage);

          }
          else{
          this.eve = data;
          }
        });
  
      }
      else{
      console.log('error al recibir el id del storage');
      this.navCtrl.setRoot(LoginPage);
      }
   } 

}
