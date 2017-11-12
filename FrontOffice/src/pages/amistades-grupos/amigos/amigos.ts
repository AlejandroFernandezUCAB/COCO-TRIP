import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController, LoadingController } from 'ionic-angular';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';

@Component({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})
export class AmigosPage {
  delete= false;
  edit= false;
  detail=false;
  amigo: any;
  /*public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });*/
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController) {
      
  }
  
  onLink(url: string) {
      window.open(url);
  }


  tapEvent() {
  }
    //AQUI SE COLOCAN LAS LLAMADAS PARA ABRIR EL CHAT 

/**
 * Metodo que carga un LoadingCTRL
 */
  /*cargando(){
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }*/

  /**
   * Metodo para cargar la lista de amigos
   */
   ionViewWillEnter() {
     //this.cargando();
      this.restapiService.listaAmigos("1")
        .then(data => {
          if (data == 0 || data == -1) {
            console.log("DIO ERROR PORQUE ENTRO EN EL IF");

          }
          else {
            this.amigo = data;
            //this.loading.dismiss();
          }
  
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
        this.eliminarAmigos(nombreUsuario, index);
        this.restapiService.eliminarAmigo(nombreUsuario,"usuario1");
        
        
        this.delete = false;
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

verPerfil() {
  this.navCtrl.push(VisualizarPerfilPage);
}

}
