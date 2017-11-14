import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import{NuevosIntegrantesPage} from '../nuevos-integrantes/nuevos-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';


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
  title: any;
  accept: any;
  cancel: any;
  text: any;
  message: any;
  succesful: any;
  edited: any;

  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
    constructor(public navCtrl: NavController, private navParams: NavParams,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public alerCtrl: AlertController, public toastCtrl: ToastController,
      private storage: Storage, private translateService: TranslateService) {
          
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
      this.translateService.get('Por favor, Confirmar').subscribe(value => {this.title = value;})
      this.translateService.get('Deseas Borrar a:').subscribe(value => {this.message = value;})
      this.translateService.get('Cancelar').subscribe(value => {this.cancel = value;})
      this.translateService.get('Aceptar').subscribe(value => {this.accept = value;})
      this.translateService.get('Eliminado Exitosamente').subscribe(value => {this.succesful = value;})
      
      const alert = this.alerCtrl.create({
        title: this.title,
        message: '¿'+this.message+nombreUsuario+'?',
        buttons: [
          {
            text: this.cancel,
            role: 'cancel',
            handler: () => {
             
            }
          },
          {
            text: this.accept,  
            handler: () => {
              this.eliminarIntegrante(nombreUsuario, index); 
              this.restapiService.eliminarIntegrante(nombreUsuario,4);
              this.realizarToast(this.succesful);
              
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
  this.translateService.get('Modificado Exitosamente').subscribe(value => {this.edited = value;})
  this.storage.get('id').then((val) => {
  if(this.nombreGrupo == undefined){
    this.restapiService.verperfilGrupo(this.id)
    .then(data => {this.grupo = data;});
      var nombreRestApi = this.grupo.filter(item => item.Nombre)[1];
      this.realizarToast(this.edited);
     
  } else {
       nombreRestApi = this.nombreGrupo;
        this.restapiService.modificarGrupo(nombreRestApi,val,this.id);
        this.realizarToast(this.edited);
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

