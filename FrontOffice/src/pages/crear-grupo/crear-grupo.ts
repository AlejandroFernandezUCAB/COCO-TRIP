import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController, AlertController } from 'ionic-angular';
import{ModificarGrupoPage} from '../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';

//****************************************************************************************************// 
//**********************************PAGE AGREGAR INTEGRANTES MODULO 3*********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Carga la lista de amigos de un usuario
 * Para agregarlos a un grupo
 */
@IonicPage()
@Component({
  selector: 'page-crear-grupo',
  templateUrl: 'crear-grupo.html',
})
export class CrearGrupoPage {

  amigo: any;
  toast: any;
  idGrupo: any;
  title: any;
  accept: any;
  cancel: any;
  text: any;
  message: any;
  succesful: any;
  loader: any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  constructor(public navCtrl: NavController, public navParams: NavParams,
              private storage: Storage, public loadingCtrl: LoadingController,
              private toastCtrl: ToastController, public restapiService: RestapiService,
              private alertCtrl: AlertController, private translateService: TranslateService) {
  
  }
  onLink(url: string) {
    window.open(url);
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
 * Metodo que carga la lista de amigos apenas se abre la vista
 */
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

 /**
 * Metodo que despliega un toast
 * @param mensaje Texto para el toast
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
 * Metodo que agrega un usuario de la lista de amigos
 * @param event evento
 * @param nombreUsuario Nombre del usuario a ser agregado
 */
  agregarIntegrantes(event,nombreUsuario){
    this.translateService.get('Por favor, Confirmar').subscribe(value => {this.title = value;})
    this.translateService.get('Deseas Agregar a:').subscribe(value => {this.message = value;})
    this.translateService.get('Cancelar').subscribe(value => {this.cancel = value;})
    this.translateService.get('Aceptar').subscribe(value => {this.accept = value;})
    this.translateService.get('Agregado exitosamente').subscribe(value => {this.succesful = value;})
    const alert = this.alertCtrl.create({
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
            this.idGrupo = this.navParams.get('idGrupo');
            this.restapiService.agregarIntegrante(this.idGrupo,nombreUsuario);
            this.realizarToast(this.succesful);
            }
          }
        ]
      });
      alert.present();
 }

 /**
  * Metodo que hace pop a las page anteriores y devuelve a la pagina inicial
  * @param event evento
  */
 finalizar(event)
 {this.translateService.get('Grupo Exitoso').subscribe(value => {this.succesful = value;})
 this.realizarToast(this.succesful);
  this.navCtrl.popToRoot();
 }
  

}
