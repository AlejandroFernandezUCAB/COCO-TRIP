import { Component } from '@angular/core';
import { NavController, NavParams, AlertController, LoadingController, ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateModule } from '@ngx-translate/core'
import { RestapiService } from '../../providers/restapi-service/restapi-service';

/**
 * Generated class for the VisualizarPerfilPublicoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@Component({
  selector: 'page-visualizarperfilpublico',
  templateUrl: 'visualizarperfilpublico.html',
})
export class VisualizarPerfilPublicoPage {

  
  toast: any;
  nombreUsuario : string;
  amigo : any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });

  constructor(public navCtrl: NavController, public navParams: NavParams, public alerCtrl: AlertController,
              public restapiService: RestapiService, public loadingCtrl: LoadingController,
              public toastCtrl: ToastController, private storage: Storage , translate : TranslateModule) {
  }

  realizarToast(mensaje) {
    this.toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    this.toast.present();
  }
  
  /**
   * Metodo que carga un LoadingCTRL
   */
  cargando(){
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
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

   agregarAmigo(item){
      //alert(item.NombreUsuario);
      this.cargando();
      this.storage.get('id').then((val) => {
        
      this.restapiService.agregarAmigo(val,item.NombreUsuario)
        .then(data => {
          if (data == 0 || data == -1) {
            console.log("DIO ERROR PORQUE ENTRO EN EL IF");
          }
          else {
            //this.amigo = data;
            this.loading.dismiss();
            this.realizarToast('Agregado exitosamente');
          }
        });
      });

      this.storage.get('id').then((val) => {
        this.restapiService.enviarCorreo(val,item.Nombre,item.Correo)
          .then(data => {
            if (data == 0 || data == -1) {
              console.log("DIO ERROR PORQUE ENTRO EN EL IF");
            }
          });
        });

   }


  doConfirm(item) {
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
            this.agregarAmigo(item);
            console.log('Si clicked');
          }
        }
      ]
    });
    confirm.present()
  }

}
