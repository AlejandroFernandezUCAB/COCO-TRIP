import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import{NuevosIntegrantesPage} from '../nuevos-integrantes/nuevos-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';

@Component({
  selector: 'modificar-grupo-page',
  templateUrl: 'modificar-grupo.html',
})
export class ModificarGrupoPage {
  grupo: any;
  miembro: any;
  lider: any;
  id: any;
  nombreGrupo: string;
  toast: any;

  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
    constructor(public navCtrl: NavController, private navParams: NavParams,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public alerCtrl: AlertController, public toastCtrl: ToastController,
      private storage: Storage) {
          
    }

    ionViewWillEnter() {
      this.id = this.navParams.get('idGrupo');
      this.restapiService.verperfilGrupo(this.id)
        .then(data => {
          if (data == 0 || data == -1) {
            console.log("DIO ERROR PORQUE ENTRO EN EL IF");
  
          }
          else {
            this.grupo = data;
            this.cargarlider(this.id);
          }
  
        });
    }
    
cargarlider(id){
  this.storage.get('id').then((val) => {
    
            this.restapiService.obtenerLider(id, val)
          .then(data => {
            if (data == 0 || data == -1) {
              console.log("DIO ERROR PORQUE ENTRO EN EL IF");
      
            }
            else {
              this.lider = data;
              this.cargarmiembros(id);
            }
      
          });
          });



}

    cargarmiembros(id){
      this.restapiService.obtenerSinLider(id)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");
  
        }
        else {
          this.miembro = data;
          
        }
  
      });
    }

/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del usuario a eliminar
 * @param index 
 */
    eliminarIntegrantes(nombreUsuario, index){
      
      const alert = this.alerCtrl.create({
        title: 'Por favor, confirmar',
        message: '¿Deseas borrar a: '+nombreUsuario+'?',
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
              this.eliminarIntegrante(nombreUsuario, index); 
              this.restapiService.eliminarIntegrante(nombreUsuario,4);
              this.realizarToast('Eliminado Exitosamente');
              
              }
            }
          ]
        });
        alert.present();
      }

      
      eliminarIntegrante(nombreUsuario, index){
        let int_eliminado = this.miembro.filter(item => item.NombreUsuario === nombreUsuario)[0];
        var removed_elements = this.miembro.splice(index, 1);
      }

modificarNombre(evento){
  this.storage.get('id').then((val) => {
  if(this.nombreGrupo == undefined){
    this.restapiService.verperfilGrupo(this.id)
    .then(data => {this.grupo = data;});
      var nombreRestApi = this.grupo.filter(item => item.Nombre)[1];
      this.realizarToast('Modificado Exitosamente');
     
  } else {
       nombreRestApi = this.nombreGrupo;
        this.restapiService.modificarGrupo(nombreRestApi,val,this.id);
        this.realizarToast('Modificado Exitosamente');
  }
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

  Integrantes(){

    this.navCtrl.push(NuevosIntegrantesPage, {
      idGrupo: this.id 
    });
  }

  }

