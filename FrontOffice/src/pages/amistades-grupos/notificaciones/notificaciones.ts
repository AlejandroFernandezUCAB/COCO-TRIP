import { Component } from '@angular/core';
import { NavController , ToastController, LoadingController} from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';

//****************************************************************************************************// 
//***********************************PAGE DE SOLICITUDES MODULO 3*************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Carga la lista de solicitudes de amistad
 */
@Component({
  selector: 'page-notificaciones',
  templateUrl: 'notificaciones.html'
})
export class NotificacionesPage {
  
  toast: any;
  mensajeToast: any;
  loader: any;
  public loading = this.loadingCtrl.create({});

  notificaciones : any;
  constructor(public navCtrl: NavController, public restapiService: RestapiService,  
              private storage: Storage, public toastCtrl: ToastController,
              private translateService: TranslateService, public loadingCtrl: LoadingController) {
   
  }

  onLink(url: string) {
    window.open(url);
}

/**
 * Metodo que despliega un toast
 * @param mensaje Texto del toast
 */
realizarToast(mensaje) {
  this.toast = this.toastCtrl.create({
    message: mensaje,
    duration: 3000,
    position: 'top'
  });
  this.toast.present();
}

/**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
cargando(){
  this.translateService.get('Por Favor Espere').subscribe(value => {this.loader = value;})
  this.loading = this.loadingCtrl.create({
    content: this.loader,
    dismissOnPageChange: true
  });
  this.loading.present();
}

/**
 * Metodo que carga la lista de solicitudes automaticamente
 */
ionViewWillEnter() {
  this.cargando();
  this.storage.get('id').then((val) => {
    this.restapiService.listaNotificaciones(val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
        this.loading.dismiss();
      }
      else {
        this.notificaciones = data;
        this.loading.dismiss();
      }
    });
   });
}

/**
 * Metodo para aceptar la solicitud de un amigo
 * @param nombreUsuarioAceptado Nombre del usuario que fue aceptado
 * @param index Posicion de la lista
 */
aceptarAmigo(nombreUsuarioAceptado,index){
  this.storage.get('id').then((val) => {
    this.restapiService.aceptarNotificacion(nombreUsuarioAceptado , val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
      }
      else {
        if(data == 1){
          this.translateService.get('Mensaje agregar').subscribe(
            value => {
               this.mensajeToast = value;
            }
          )
          this.realizarToast(this.mensajeToast);
          
          this.eliminarNotificacionVisual(nombreUsuarioAceptado, index);
        }else {
          this.translateService.get('Algo ha salido mal').subscribe(
            value => {
               this.mensajeToast = value;
            }
          )
          this.realizarToast('Algo ha salido mal');
        }
      }
    });
   });
}

/**
 * Metodo para rechazar una solicitud
 * @param nombreUsuarioRechazado Nombre del usuario que fue rechazado
 * @param index Posicion en la lista
 */
rechazarAmigo(nombreUsuarioRechazado, index){
  this.storage.get('id').then((val) => {
    this.restapiService.rechazarNotificacion(nombreUsuarioRechazado , val)
      .then(data => {
      if (data == 0 || data == -1) {
        console.log("DIO ERROR PORQUE ENTRO EN EL IF");
      }
      else {
        
        if(data == 1){
          this.translateService.get('Peticion eliminada').subscribe(
            value => {
               this.mensajeToast = value;
            }
          )
          this.realizarToast(this.mensajeToast);
          this.eliminarNotificacionVisual(nombreUsuarioRechazado, index);
        }else {
          this.translateService.get('Algo ha salido mal').subscribe(
            value => {
               this.mensajeToast = value;
            }
          )
          this.realizarToast('Algo ha salido mal');
        }
      }
    });
   });
}

/**
 * Metodo para borrar desde pantalla
 * @param nombreUsuario nombre del usuario a quitar de la lista de notificaciones
 * @param index posicion en la lista
 */
eliminarNotificacionVisual(nombreUsuario, index){
  let eliminado = this.notificaciones.filter(item => item.NombreUsuario === nombreUsuario)[8];
  var removed_elements = this.notificaciones.splice(index, 1);
}


}


