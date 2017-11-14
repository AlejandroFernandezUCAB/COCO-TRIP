import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController, LoadingController, ToastController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import { SeleccionarIntegrantesPage } from '../../seleccionar-integrantes/seleccionar-integrantes';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';
import{ModificarGrupoPage} from '../../modificar-grupo/modificar-grupo';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';

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
  loader: any;
  NoEdit: any;
  subtitle: any;
  ok: any;
  title: any;
  accept: any;
  cancel: any;
  text: any;
  message: any;
  succesful: any;


  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });

  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alertCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public toastCtrl: ToastController, private storage: Storage,
      private translateService: TranslateService) {
 
   }
   
   cargando(){
    this.translateService.get('Por Favor Espere').subscribe(value => {this.loader = value;})
    this.loading = this.loadingCtrl.create({
      content: this.loader,
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
    
    this.storage.get('id').then((val) => {
      this.restapiService.verificarLider(id,val)
      .then(data => {
        if (data == 0 || data == -1) {
    
          this.alertaIntegrante();

        }
        else {
          
          this.navCtrl.push(ModificarGrupoPage,{
            idGrupo: id
          });
        }

      });
      this.delete = false;
    });
   
  } 

  alertaIntegrante() {
    this.translateService.get('No puedes modificar').subscribe(value => {this.NoEdit = value;})
    this.translateService.get('No eres lider').subscribe(value => {this.subtitle = value;})
    this.translateService.get('Esta bien').subscribe(value => {this.ok = value;})
    let alert = this.alertCtrl.create({
      title: this.NoEdit,
      subTitle: this.subtitle,
      buttons: [this.ok]
    });
    alert.present();
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
    this.translateService.get('Por favor, Confirmar').subscribe(value => {this.title = value;})
    this.translateService.get('Borrar Grupo').subscribe(value => {this.message = value;})
    this.translateService.get('Cancelar').subscribe(value => {this.cancel = value;})
    this.translateService.get('Aceptar').subscribe(value => {this.accept = value;})
    this.translateService.get('Salir Grupo').subscribe(value => {this.succesful = value;})
    const alert = this.alertCtrl.create({
    title: this.title,
    message:this.message,
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
                this.realizarToast(this.succesful);
                 
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


