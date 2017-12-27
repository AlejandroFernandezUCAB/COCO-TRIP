import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { NuevosIntegrantesPage } from '../nuevos-integrantes/nuevos-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
//****************************************************************************************************// 
//**********************************PAGE MODIFICAR GRUPO MODULO 3*************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga los datos de un grupo para modificarlos y eliminar
 * los integrantes de ese grupo
 */
@Component
({
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
  public loading = this.loadingCtrl.create({});

    constructor(public navCtrl: NavController, private navParams: NavParams,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public alerCtrl: AlertController, public toastCtrl: ToastController,
      private storage: Storage, private translateService: TranslateService) {
          
    }
    
/**
 * Carga la vista del grupo apenas entras a la pagina 
 * solo los datos del grupo 
 */
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

/**
 * Carga los datos del lider
 * @param id Iedntificador del grupo
 */    
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

/**
 * Carga la lista de los integrantes del grupo (Si incluir al lider)
 * @param id identificador del grupo
 */
    cargarmiembros(id){
      this.restapiService.obtenerSinLider(id)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("La data es 0 o -1");
  
        }
        else {
          this.miembro = data;
          
        }
  
      });
    }

/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del usuario a eliminar
 * @param index Posicion en la lista
 */
    eliminarIntegrantes(nombreUsuario, index){
      this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
      this.translateService.get(Texto.MENSAJE_ELIMINAR_INTEGRANTE).subscribe(value => {this.message = value;})
      this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
      this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
      this.translateService.get(Texto.EXITO_ELIMINAR_INTEGRANTE).subscribe(value => {this.succesful = value;})
      
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
              this.restapiService.eliminarIntegrante(nombreUsuario,this.id);
              this.realizarToast(this.succesful);
              
              }
            }
          ]
        });
        alert.present();
      }

  /**
   * Eliminar en pantalla
   * @param nombreUsuario Nombre del usuario a eliminar
   * @param index Posicion en la lista
   */    
  eliminarIntegrante(nombreUsuario, index){
    let int_eliminado = this.miembro.filter(item => item.NombreUsuario === nombreUsuario)[0];
    var removed_elements = this.miembro.splice(index, 1);
  }

/**
 * Metodo que verifica si el nombre del grupo
 * se modifico o no
 * @param evento evento
 */
modificarNombre(evento){
  this.translateService.get(Texto.MODIFICAR_EXITOSO).subscribe(value => {this.edited = value;})
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
 * Metodo que inicia la pagina para agregar a integrantes
 */
  Integrantes(){

    this.navCtrl.push(NuevosIntegrantesPage, {
      idGrupo: this.id 
    });
  }

  }

