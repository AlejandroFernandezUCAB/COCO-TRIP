import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController, AlertController } from 'ionic-angular';
import{ModificarGrupoPage} from '../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';

//****************************************************************************************************// 
//****************************PAGE AGREGAR INTEGRANTES AL MODIFICAR MODULO 3**************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de amigos que no estan agregados 
 * en ese grupo
 */
@IonicPage()
@Component
({
  selector: 'page-nuevos-integrantes',
  templateUrl: 'nuevos-integrantes.html',
})

export class NuevosIntegrantesPage 
{
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
  public loading = this.loadingCtrl.create({});
  constructor(public navCtrl: NavController, public navParams: NavParams,
              private storage: Storage, public loadingCtrl: LoadingController,
              private toastCtrl: ToastController, public restapiService: RestapiService,
              private alertCtrl: AlertController, private translateService: TranslateService) {
                this.idGrupo = this.navParams.get('idGrupo');  
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
  this.translateService.get(Texto.CARGANDO).subscribe(value => {this.loader = value;})
  this.loading = this.loadingCtrl.create({
    content: this.loader,
    dismissOnPageChange: true
  });
  this.loading.present();
}

/**
 * Metodo que carga la lista de amigos que
 * no estan agregados al grupo
 */
  ionViewWillEnter() {
    this.cargando();

    this.storage.get('id').then((val) => {
        console.log('El id del usuario es: ', val);
   
     this.restapiService.obtenerMiembrosSinGrupo(val,this.idGrupo)
     .then(data => {
       if (data == 0 || data == -1) {
        
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
    duration: ConfiguracionToast.DURACION,
    position: ConfiguracionToast.POSICION
  });
  this.toast.present();
}

/**
 * Metodo que agrega a los integrantes al grupo
 * @param event evento
 * @param nombreUsuario Nombre del usuario a agregar
 */
  agregarIntegrantes(event,nombreUsuario){
    this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
    this.translateService.get(Texto.MENSAJE_AGREGAR_INTEGRANTE).subscribe(value => {this.message = value;})
    this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
    this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
    this.translateService.get(Texto.EXITO_AGREGAR_INTEGRANTE).subscribe(value => {this.succesful = value;})
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

            this.restapiService.agregarIntegrante(this.idGrupo,nombreUsuario);
            
            this.realizarToast(this.succesful);
            }
          }
        ]
      });
      alert.present();
 }

}
