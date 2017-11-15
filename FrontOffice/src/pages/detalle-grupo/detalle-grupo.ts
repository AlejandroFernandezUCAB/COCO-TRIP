import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';

//****************************************************************************************************// 
//********************************PAGE DETALLE DE UN GRUPO MODULO 3***********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Carga el detalle de un grupo
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
    public restapiService: RestapiService, private translateService: TranslateService) {
 
  }

  /**
   * Carga los atributos del grupo, nombre y foto
   */
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

  /**
   * Carga los integrantes del grupo
   * @param id Identificador del grupo
   */
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

