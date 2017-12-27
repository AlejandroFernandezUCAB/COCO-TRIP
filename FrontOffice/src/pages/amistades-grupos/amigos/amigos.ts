import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController, ToastController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController, LoadingController } from 'ionic-angular';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConversacionPage } from '../../chat/conversacion/conversacion';

//****************************************************************************************************//
//*************************************PAGE DE AMIGOS MODULO 3****************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de amigos de un usuario
 * Floating button para eliminar amigos, agregar amigos y ver perfil
 */
@Component
({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})

export class AmigosPage 
{
  /*Condicionales*/
  delete = false; 
  edit = false;
  detail = false; 
  chat = false; 
  amigo: any; //Arreglo de amigos
  toast: any;
  title: any;
  accept: any;
  cancel: any;
  text: any;
  message: any;
  succesful: any;
  loader: any;
  nombreUsuario: string;

  public loading = this.loadingCtrl.create({});


    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public toastCtrl: ToastController, private storage: Storage,
      private translateService: TranslateService ) {

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
 * Metodo que carga la lista de amigos automaticamente
 * al entrar a la vista
 */
   ionViewWillEnter() {
     this.cargando();
     this.storage.get('id').then((val) => {
      this.restapiService.listaAmigos(val)
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
 * Metodo que coloca los textos de las cartas
 * en false e inicia la pagina de buscar amigos
 * para agregarlo
 */
agregarAmigo(){
 this.edit=false;
  this.detail=false;
  this.delete=false;
  this.chat=false;

  this.navCtrl.push(BuscarAmigoPage);
}

/**
 * Metodo que coloca los textos de las cartas en false
 */
verChat(){
  this.edit=false;
   this.detail=false;
   this.delete=false;
   if (this.chat==false){
    
        this.chat = true;
      }
      else{
        this.chat=false;
      }
    
 }

/**
 * Metodo que coloca los textos de las cartas en false
 */
eliminar(){
  this.edit=false;
  this.detail=false;
  this.chat=false;

  if (this.delete==false){

    this.delete = true;
  }
  else{
    this.delete=false;
  }

}

/**
 * Metodo que coloca los textos de las cartas en false
 */
perfil(){
  this.delete=false;
  this.edit=false;
  this.chat=false;
  if(this.detail==false){

    this.detail = true;
  }
  else{

    this.detail=false;
  }

}


/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del amigo a eliminar
 * @param index posicion de la lista
 */
eliminarAmigo(nombreUsuario, index) {
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
        this.eliminarAmigos(nombreUsuario, index);
        this.storage.get('id').then((val) => {
        this.restapiService.eliminarAmigo(nombreUsuario,val);
        });
        this.delete = false;
        this.realizarToast(this.succesful);
        }
      }
    ]
  });
  alert.present();
}

/**
 * Metodo para borrar desde pantalla
 * @param nombreUsuario Nombre del amigo a eliminar
 * @param index Posicion de la lista
 */
eliminarAmigos(nombreUsuario, index){
  let eliminado = this.amigo.filter(item => item.NombreUsuario === nombreUsuario)[8];
  var removed_elements = this.amigo.splice(index, 1);
}

/**
 * Metodo para ingresar a la pagina de visualizar
 * el perfil de un amigo
 * @param item Nombre del usuario seleccionado
 */
verPerfil(item) {
  this.navCtrl.push(VisualizarPerfilPage,{
      nombreUsuario : item
  });
}

/**
 * Metodo que inicia un chat 
 * @param item Nombre del usuario seleccionado
 */
chatAmigo(item) {
  this.navCtrl.push(ConversacionPage,{
      nombreUsuario : item
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

}
