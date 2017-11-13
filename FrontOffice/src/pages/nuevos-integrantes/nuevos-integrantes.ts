import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController, AlertController } from 'ionic-angular';
import{ModificarGrupoPage} from '../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

@IonicPage()
@Component({
  selector: 'page-nuevos-integrantes',
  templateUrl: 'nuevos-integrantes.html',
})
export class NuevosIntegrantesPage {
  amigo: any;
  toast: any;
  idGrupo: any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  constructor(public navCtrl: NavController, public navParams: NavParams,
              private storage: Storage, public loadingCtrl: LoadingController,
              private toastCtrl: ToastController, public restapiService: RestapiService,
              private alertCtrl: AlertController) {
  
  }
  onLink(url: string) {
    window.open(url);
}
  cargando(){
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  ionViewWillEnter() {
    this.cargando();

    this.storage.get('id').then((val) => {
        console.log('El id del usuario es: ', val);
        
     this.restapiService.listaAmigos(val)
     .then(data => {
       if (data == 0 || data == -1) {
         console.log("DIO ERROR PORQUE ENTRO EN EL IF");
         this.loading.dismiss();
       }
       else {
         this.amigo = data;
         this.loading.dismiss();
       }
     });
     });
 }

 realizarToast(mensaje) {
  this.toast = this.toastCtrl.create({
    message: mensaje,
    duration: 3000,
    position: 'top'
  });
  this.toast.present();
}

  agregarIntegrantes(event,nombreUsuario){

    const alert = this.alertCtrl.create({
      title: 'Por favor, confirmar',
      message: '¿Deseas agregar a: '+nombreUsuario+'?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: () => {
           
          }
        },
        {
          text: 'Aceptar',
          handler: () => {
            this.idGrupo = this.navParams.get('idGrupo');
            this.restapiService.agregarIntegrante(this.idGrupo,nombreUsuario);
            
            this.realizarToast('Agregado Exitosamente');
            }
          }
        ]
      });
      alert.present();
 }

}
