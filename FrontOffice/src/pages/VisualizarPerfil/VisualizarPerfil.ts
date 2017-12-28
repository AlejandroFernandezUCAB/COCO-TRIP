import { Component } from '@angular/core';
import { NavController, AlertController , LoadingController, NavParams } from 'ionic-angular';
import { TranslateModule, TranslateService } from '@ngx-translate/core'
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Texto } from '../constantes/texto';

//****************************************************************************************************// 
//********************************PAGE DE VISUALIZAR PERFIL MODULO 3**********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga el perfil de un amigo
 */
@Component
({
  selector: 'page-visualizarperfil',
  templateUrl: 'visualizarperfil.html'
})

export class VisualizarPerfilPage 
{
  nombreUsuario : any;
  amigo : any;
  mensajeCargando:any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });

  constructor(public navCtrl: NavController, public alerCtrl: AlertController, 
      public restapiService: RestapiService, public loadingCtrl: LoadingController, 
      private navParams: NavParams, public translateService : TranslateService) {

  }

/**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
  cargando(){
    this.translateService.get(Texto.CARGANDO).subscribe(value => {this.mensajeCargando = value;})
    this.loading = this.loadingCtrl.create({
      content: this.mensajeCargando,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

/**
 * Metodo que carga los datos de un amigo para visualizar su perfil
 */
  ionViewWillEnter() {
    this.nombreUsuario = this.navParams.get('nombreUsuario');
    this.cargando();
     this.restapiService.obtenerPerfilPublico(this.nombreUsuario)
       .then(data => {
         if (data == 0 || data == -1) {
           console.log("DIO ERROR PORQUE ENTRO EN EL IF");

         }
         else {
           this.amigo = data;
           this.loading.dismiss();
         }
 
       });
   }
}
