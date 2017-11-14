import { Component } from '@angular/core';
import { NavController, AlertController , LoadingController, NavParams } from 'ionic-angular';
import { TranslateModule, TranslateService } from '@ngx-translate/core'
import { RestapiService } from '../../providers/restapi-service/restapi-service';

@Component({
  selector: 'page-visualizarperfil',
  templateUrl: 'visualizarperfil.html'
})
export class VisualizarPerfilPage {

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
   * Metodo que carga un LoadingCTRL
   */
  cargando(){
    this.translateService.get('Por favor, espere').subscribe(
      value => {
        // value is our translated string
         this.mensajeCargando = value;
      }
    )

    this.loading = this.loadingCtrl.create({
      content: this.mensajeCargando,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  /**
   * Metodo para cargar la lista de amigos
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

  doConfirm() {
    let confirm = this.alerCtrl.create({
      title: 'Agregar?',
      message: 'Desea agregar a esta persona como amigo?',
      buttons: [
        {
          text: 'No',
          handler: () => {
            console.log('No clicked');
          }
        },
        {
          text: 'Si',
          handler: () => {
            console.log('Si clicked');
          }
        }
      ]
    });
    confirm.present()
  }
}
