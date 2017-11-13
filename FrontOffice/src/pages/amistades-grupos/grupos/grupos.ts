import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController, LoadingController, ToastController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import { SeleccionarIntegrantesPage } from '../../seleccionar-integrantes/seleccionar-integrantes';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';
import{ModificarGrupoPage} from '../../modificar-grupo/modificar-grupo';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';

@Component({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})
export class GruposPage {
  delete= false;
  edit= false;
  detail=false;
  toast : any;
  grupo:any;

  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });

  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alertCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public toastCtrl: ToastController, private storage: Storage ) {
 
   }
   
   cargando(){
    this.loading = this.loadingCtrl.create({
      content: 'Por favor espere...',
      dismissOnPageChange: true
    });
    this.loading.present();
  }

   ionViewWillEnter() {
     this.cargando();
    this.storage.get('id').then((val) => {
    this.restapiService.listaGrupo(val)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");
          this.loading.dismiss();
        }
        else {
          this.grupo = data;
          this.loading.dismiss();
        }

      });
    });
  }

  crearGrupo(){
    this.edit=false;
    this.detail=false;
    this.delete=false;

    this.navCtrl.push(SeleccionarIntegrantesPage);
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

  editar(){
    this.delete=false;
    this.detail=false;

    if (this.edit==false){

      this.edit = true;
    }
    else{
      this.edit=false;
    }
    
  }


  detallegrupo(){
    this.delete=false;
    this.edit=false;
    if(this.detail==false){

      this.detail = true;
    }
    else{

      this.detail=false;
    }
    
  }

  modificarGrupo(id, index){
    this.edit=false;
    this.detail=false;
    this.delete=false;
    this.navCtrl.push(ModificarGrupoPage,{
      idGrupo: id
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

  eliminarGrupo(id, index) {
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este Grupo?',
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
          
          
          this.storage.get('id').then((val) => {
            console.log('El id del usuario es ', val);
            this.restapiService.salirGrupo(val,id)
            .then(data => {
              if (data == 0 || data == -1) {
                console.log("DIO ERROR PORQUE ENTRO EN EL IF");
          
                this.realizarToast('Hubo un error');

              }
              else {
                
                console.log("la data es "+data);
                this.realizarToast('Haz salido del grupo exitosamente');
                 
                this.eliminarGrupos(id, index);
              }
      
            });
            this.delete = false;
          });
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
eliminarGrupos(id, index){
  let eliminado = this.grupo.filter(item => item.Id === id)[0];
  var removed_elements = this.grupo.splice(index, 1);
}

  verDetalleGrupo(id, index) {
    this.navCtrl.push(DetalleGrupoPage,{
      idGrupo: id
    });
  
  }
  
}


