import { Component } from '@angular/core';
import { NavController , ToastController} from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';

@Component({
  selector: 'page-notificaciones',
  templateUrl: 'notificaciones.html'
})
export class NotificacionesPage {
  
  toast: any;
  mensajeToast: any;

  notificaciones : any;
  constructor(public navCtrl: NavController, public restapiService: RestapiService,  
              private storage: Storage, public toastCtrl: ToastController,
              private translateService: TranslateService) {
   
  }
//comentario
  onLink(url: string) {
    window.open(url);
}

realizarToast(mensaje) {
  this.toast = this.toastCtrl.create({
    message: mensaje,
    duration: 3000,
    position: 'top'
  });
  this.toast.present();
}

ionViewWillEnter() {
  //this.cargando();
  this.storage.get('id').then((val) => {
    this.restapiService.listaNotificaciones(val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
        //this.loading.dismiss();
      }
      else {
        this.notificaciones = data;
        //this.loading.dismiss();
      }
    });
   });
}

aceptarAmigo(nombreUsuarioAceptado,index){
  this.storage.get('id').then((val) => {
    this.restapiService.aceptarNotificacion(nombreUsuarioAceptado , val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
        //this.loading.dismiss();
      }
      else {
        if(data == 1){
          this.translateService.get('Mensaje agregar').subscribe(
            value => {
              // value is our translated string
               this.mensajeToast = value;
            }
          )
          this.realizarToast(this.mensajeToast);
          
          this.eliminarNotificacionVisual(nombreUsuarioAceptado, index);
        }else {
          this.translateService.get('Algo ha salido mal').subscribe(
            value => {
              // value is our translated string
               this.mensajeToast = value;
            }
          )
          this.realizarToast('Algo ha salido mal');
        }
        
        //this.notificaciones = data;
        //this.loading.dismiss();
      }
    });
   });
}

rechazarAmigo(nombreUsuarioRechazado, index){
  this.storage.get('id').then((val) => {
    this.restapiService.rechazarNotificacion(nombreUsuarioRechazado , val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
        //this.loading.dismiss();
      }
      else {
        
        if(data == 1){
          this.translateService.get('Peticion eliminada').subscribe(
            value => {
              // value is our translated string
               this.mensajeToast = value;
            }
          )
          this.realizarToast(this.mensajeToast);
          this.eliminarNotificacionVisual(nombreUsuarioRechazado, index);
        }else {
          this.translateService.get('Algo ha salido mal').subscribe(
            value => {
              // value is our translated string
               this.mensajeToast = value;
            }
          )
          this.realizarToast('Algo ha salido mal');
        }
      }
    });
   });
}


eliminarNotificacionVisual(nombreUsuario, index){
  let eliminado = this.notificaciones.filter(item => item.NombreUsuario === nombreUsuario)[8];
  var removed_elements = this.notificaciones.splice(index, 1);
}


}

interface notificaciones {
  img: string; //Este es el avatar
  nick_name: string; //El nickname
}
