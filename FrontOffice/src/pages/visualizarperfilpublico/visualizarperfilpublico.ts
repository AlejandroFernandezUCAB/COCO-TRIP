import { Component } from '@angular/core';
import { NavController, NavParams, AlertController, LoadingController, ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateModule , TranslateService  } from '@ngx-translate/core'
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';

//****************************************************************************************************// 
//***************************PAGE DE VISUALIZAR PERFIL PUBLICO MODULO 3*******************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga el perfil publico de un usuario
 */

@Component
({
  selector: 'page-visualizarperfilpublico',
  templateUrl: 'visualizarperfilpublico.html',
})

export class VisualizarPerfilPublicoPage 
{
  tituloAlert:any;
  siAlert : any;
  mensajeAlert : any;
  toast: any;
  mensajeCargando: any;
  nombreUsuario : string;
  amigo : any;
  public mensajeToast : any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });

  constructor(public navCtrl: NavController, public navParams: NavParams, public alerCtrl: AlertController,
              public restapiService: RestapiService, public loadingCtrl: LoadingController,
              public toastCtrl: ToastController, private storage: Storage , translate : TranslateModule,
              public translateService : TranslateService ) {
               
                
  }

  /**
 * Metodo que despliega un toast
 * @param mensaje Texto para el toast
 */
  realizarToast() {
    this.translateService.get(Texto.AGREGAR_MENSAJE).subscribe(
      value => {
         this.mensajeToast = value;
      }
    )
    this.toast = this.toastCtrl.create({
      message: this.mensajeToast,
      duration: ConfiguracionToast.DURACION,
      position: ConfiguracionToast.POSICION
    });
    this.toast.present();
  }
  
 /**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
  cargando(){
    this.translateService.get(Texto.CARGANDO).subscribe(
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

/**
 * Metodo que carga los datos de un amigo para visualizar su perfil
 */
   agregarAmigo(item){
      this.cargando();
      this.storage.get('id').then((val) => {
        
      this.restapiService.agregarAmigo(val,item.NombreUsuario)
        .then(data => {
          if (data == 0 || data == -1) {
            console.log("DIO ERROR PORQUE ENTRO EN EL IF");
          }
          else {
            this.loading.dismiss();
            this.realizarToast();
            this.navCtrl.pop();
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

/**
 * Metodo para agregar un usuario
 * @param item Nombre del usuario a agregar
 */
doConfirm(item) {
  this.translateService.get(Texto.TITULO_CONFIRMAR).subscribe(value => {this.tituloAlert = value;})
  this.translateService.get(Texto.MENSJAE_CONFIRMAR).subscribe(value => {this.mensajeAlert = value;})
  this.translateService.get(Texto.SI_CONFIRMAR).subscribe(value => {this.siAlert = value;})
    let confirm = this.alerCtrl.create({
      title: this.tituloAlert,
      message: this.mensajeAlert,
      buttons: [
        {
            text: 'No',
            handler: () => {
            console.log('No clicked');
          }
        },
        {
          text: this.siAlert,
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
