import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController, LoadingController, ToastController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import { SeleccionarIntegrantesPage } from '../../seleccionar-integrantes/seleccionar-integrantes';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';
import{ModificarGrupoPage} from '../../modificar-grupo/modificar-grupo';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';

//****************************************************************************************************// 
//*************************************PAGE DE GRUPOS MODULO 3****************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Carga la lista de grupos de un usuario
 * Floating button para eliminar grupos, agregar grupos,
 * ver detalle del grupo y modificar grupo
 */
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
  public loading = this.loadingCtrl.create({});

  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alertCtrl: AlertController,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public toastCtrl: ToastController, private storage: Storage,
      private translateService: TranslateService) {
 
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
   * Metodo que carga la lista de grupos automaticamente
   * al entrar a la vista
   */
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

  /**
   * Metodo que coloca los textos de las cartas
   * en false e inicia la pagina de crear grupo
   */
  crearGrupo(){
    this.edit=false;
    this.detail=false;
    this.delete=false;

    this.navCtrl.push(SeleccionarIntegrantesPage);
  }

  /**
   * Metodo que coloca los textos de las cartas en false
   * (Cuando dice eliminar grupo)
   */
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

  /**
   * Metodo que coloca los textos de las cartas en false
   * (Cuando dice modificar grupo)
   */
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

/**
 *Metodo que coloca los textos de las cartas en false
 (Cuando dice ver detalle del grupo) 
 */
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

  /**
   * Metodo que verifica si un usario es lider o solo integrante del grupo
   * Si es lider inicia la pagina de modificar grupo, si no, envia alert
   * @param id Identificador del usuario
   * @param index Posicion en la lista
   */
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
/**
 * Alert que explica que el usuario no es lider del grupo
 */
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
   * Metodo que coloca un alert para confirmar que el grupo se desea eliminar
   * Verifica si es lider o solo integrante para eliminar el grupo o solo salir de el
   * @param id Identificador del grupo
   * @param index Posicion de la lista
   */
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

/**
 * Metodo para iniciar la pagina del detalle del grupo
 * @param id Identificador del grupo
 * @param index Posicion de la lista
 */
  verDetalleGrupo(id, index) {
    this.navCtrl.push(DetalleGrupoPage,{
      idGrupo: id
    });
  
  }
  
}


