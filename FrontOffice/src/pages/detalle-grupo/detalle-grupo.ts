import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
/**
 * Generated class for the DetalleGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-detalle-grupo',
  templateUrl: 'detalle-grupo.html',
})
export class DetalleGrupoPage {
  grupo:any;
  miembro:any;
  idGrupo: any;

  constructor(public navCtrl: NavController, public navParams: NavParams,
    public restapiService: RestapiService) {
 
  }

  ionViewWillEnter() {
    this.idGrupo = this.navParams.get('idGrupo');
      this.restapiService.verperfilGrupo(this.idGrupo)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");

        }
        else {
          this.grupo = data;
          this.cargarmiembros(this.idGrupo);
        }

      });
 
  }

  cargarmiembros(id){
    this.restapiService.listamiembroGrupo(id)
    .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");

      }
      else {
        this.miembro = data;
      }

    });
  }

}

