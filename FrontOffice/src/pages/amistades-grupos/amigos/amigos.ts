import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController, ToastController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController, LoadingController } from 'ionic-angular';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})


export class AmigosPage {
  delete= false;
  edit= false;
  detail=false;
  amigo: any;
  toast: any;
  title: any;
  accept: any;
  cancel: any;
  text: any;
  message: any;
  succesful: any;
  loader: any;
  
  nombreUsuario : string;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public toastCtrl: ToastController, private storage: Storage,
      private translateService: TranslateService ) {
      
  }
  
  onLink(url: string) {
      window.open(url);
  }


  tapEvent() {
  }

/**
 * Metodo que carga un LoadingCTRL
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
   * Metodo para cargar la lista de amigos
   */
   ionViewWillEnter() {
     this.cargando();
     this.storage.get('id').then((val) => {
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

agregarAmigo(){
 this.edit=false;
  this.detail=false;
  this.delete=false;

  this.navCtrl.push(BuscarAmigoPage);
}

eliminar(){
  this.edit=false;
  this.detail=false;

  if (this.delete==false){

    this.delete = true;
  }
  else{
    this.delete=false;
  }
  
}

perfil(){
  this.delete=false;
  this.edit=false;
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
 * @param index 
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
 * @param index 
 */
eliminarAmigos(nombreUsuario, index){
  let eliminado = this.amigo.filter(item => item.NombreUsuario === nombreUsuario)[8];
  var removed_elements = this.amigo.splice(index, 1);
}

verPerfil(item) {
  this.navCtrl.push(VisualizarPerfilPage,{
      nombreUsuario : item
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

}
