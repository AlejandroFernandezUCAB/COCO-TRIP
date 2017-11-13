import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import * as moment from 'moment';
import { MenuController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  constructor(public navCtrl: NavController,private storage: Storage,public menu: MenuController, public restapiService: RestapiService) {
    //console.log(this.its2);
    this.menu.enable(true);
  }
 
 ltSegunPreferencia(){

    var idUser=this.storage.get('id');
    if(idUser){
      this.restapiService.ltSegunPreferencias(idUser)
      .then(data=>{
      console.log(data)
      
      });

    }
    else prompt('Error fatal al consultar el servidor');

 } 

}
