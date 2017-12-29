import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';
import { Component, ViewChild, NgZone } from '@angular/core';
import { Platform, ActionSheetController, Events, Content } from 'ionic-angular';
import { IonicPage, NavParams, NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';
import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import * as moment from 'moment';
import { Firebase } from '@ionic-native/firebase';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';
import { ChatProvider } from '../../../providers/chat/chat';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';
import { ToastController } from 'ionic-angular';
import { InformacionMensajePage } from '../../chat/informacion-mensaje/informacion-mensaje';
import { Registry } from '../../../common/registry';

import {catService,catProd} from "../../../logs/config"

//****************************************************************************************************//
//**********************************PAGE DE CONVERSACION MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Conversacion entre amigos
 */

@IonicPage()
@Component({
    selector: 'page-conversacion',
    templateUrl: 'conversacion.html'
})

export class ConversacionPage {
  @ViewChild('content') content: Content;
  
  conversacion: any;
  nuevoMensaje = "";
  nombreUsuario:any={
    NombreAmigo: 'NombreAmigo'
  }
  idAmigo: any;
  mensj:any;
  idGrupo: any;
  idUsuario: any;
  usuario: any = {
    NombreUsuario: 'NombreUsuario'
  };
  todosLosMensajes = [];

  title: any;
  accept: any;
  delete: any;
  text: any;
  message: any;
  succesful: any;
  cancel: any;
  edit: any;
  info: any;
  new: any;

constructor(public navCtrl: NavController, public navParams: NavParams,
  public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController,
  public platform: Platform, private firebase: Firebase , public chatService: ChatProvider,
  public events: Events, public zone: NgZone, private storage: Storage,public restapiService: RestapiService,
  public toastCtrl: ToastController, private translateService: TranslateService) {

}

/**
 * Descripcion del metodo:
 * Metodo que se encarga de obtener los datos necesarios como la id del usuario
 * loggeado, el nombre de usuario amigo y la lista de mensajes entre usuarios
 * 
 */
ionViewWillEnter() {
  catProd.info("Entrando en el metodo ionViewWillEnter de Conversacion");
  this.nombreUsuario.NombreAmigo = this.navParams.get('nombreUsuario');
 
  this.storage.get('id').then((val) => {
    
          this.idUsuario = val;
          // hacemos la llamada al apirest con el id obtenido
          this.restapiService.ObtenerDatosUsuario(this.idUsuario).then(data => {
            if(data != 0)
            {  
              this.usuario = data;
    
            }
          });
    
        });
  
    this.conversacion = this.chatService.conversacion; //Añade y muestra los mensajes de cada conversación
    this.scrollto();
  
    this.idUsuario =
    this.events.subscribe(Registry.PUBLISH_LISTA_MENSAJE_AMIGOS, (Mensajes) => {
      this.todosLosMensajes = [];
      this.zone.run(() => {
        this.todosLosMensajes = Mensajes;
      })
    })
    catProd.info("Saliendo del metodo ionViewWillEnter de Conversacion");
 }

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el menu en el cual se tendran las opciones de
 * visualizar, eliminar y modificar el mensaje.
 * 
 */
 pressEvent(idMensaje,emisor,receptor){

  catProd.info("Entrando en el metodo pressEvent de Conversacion");
  this.translateService.get('Opciones').subscribe(value => {this.title = value;})
  this.translateService.get('Eliminar').subscribe(value => {this.delete = value;})
  this.translateService.get('Borrar Mensaje').subscribe(value => {this.message = value;})
  this.translateService.get('Aceptar').subscribe(value => {this.accept = value;})
  this.translateService.get('Cancelar').subscribe(value => {this.cancel = value;})
  this.translateService.get('Modificar').subscribe(value => {this.edit = value;})
  this.translateService.get('Informacion').subscribe(value => {this.info = value;})

  if(idMensaje!=-1){
    let actionSheet = this.actionsheetCtrl.create({
      title: this.title,
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: this.delete,
          role: 'destructive', // aplica color rojo de alerta
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            let decision = this.alertCtrl.create({
              message: '¿'+this.message+'?',
              buttons: [
                {
                  text: this.accept,
                  handler: () => {

                    this.eliminarMensajeAmigo(idMensaje);
                  }
                },
                {
                  text: this.cancel,
                  handler: () => {
                    console.log('Decisión de eliminar negativa');
                  }
                }
              ]
            });
            decision.present()
          }
        },
        {
          text: this.cancel,
          role: 'cancel', //coloca el botón siempre en el último lugar.
          icon: !this.platform.is('ios') ? 'close' : null,
          handler: () => {
            console.log('Cancelar ActionSheet');
          }
        },
        {
          text: this.edit,
          role: 'Modificar', //coloca el botón siempre en el último lugar.
          icon: !this.platform.is('ios') ? 'create' : null,
          handler: () => {
            
            this.crearalert(idMensaje);
          }

          },
          {
            text: this.info,
            role: 'Informacion', //coloca el botón siempre en el último lugar.
            icon: !this.platform.is('ios') ? 'create' : null,
            handler: () => {
              this.navCtrl.push(InformacionMensajePage,{
                idmensaje:idMensaje,
                emisor:emisor,
                receptor:receptor
            });
            }
          }
      ]
    });
    actionSheet.present();
  }
  catProd.info("Saliendo del metodo pressEvent de Conversacion");
}

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el alert en el cual se modificara el mensaje
 * 
 */
crearalert(idMensaje){
  catProd.info("Entrando en el metodo crearalert de Conversacion");
  this.translateService.get('Modificar Mensaje').subscribe(value => {this.title = value;})
  this.translateService.get('Escribe nuevo mensaje').subscribe(value => {this.message = value;})
  this.translateService.get('Cancelar').subscribe(value => {this.cancel = value;})
  this.translateService.get('Modificar').subscribe(value => {this.edit = value;})
  this.translateService.get('Mensaje').subscribe(value => {this.new = value;})

  let prompt = this.alertCtrl.create({
    title: this.title,
    message: this.message,
    inputs: [
      {
        name: 'modificado',
        placeholder: this.new
      },
    ],
    buttons: [
      {
        text: this.cancel,
        handler: data => {
          console.log('Cancel clicked');
      
        }
      },
      {
        text: this.edit,
        handler: data => {
          if(data.modificado != ""){
            this.ModificarMensajeAmigo(idMensaje,data.modificado);
          }else{
            this.translateService.get('Por favor').subscribe(value => {this.message = value;})
            this.presentToast(this.message);
          }
          
        }
      }
    ]
  });
  prompt.present();
  catProd.info("Saliendo del metodo crearalert de Conversacion");
}

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para modificar el mensaje
 * 
 */

  ModificarMensajeAmigo(idMensaje,nuevoMensaje){
    catProd.info("Entrando en el metodo ModificarMensajeAmigo de Conversacion");
    let entidad: Mensaje;
    entidad = new Mensaje(nuevoMensaje,this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,0,"","",true);
    entidad.Id = idMensaje;
    let comando = FabricaComando.crearComandoModificarMensaje();
    comando.setEntidad = entidad;
    comando.execute();
    catProd.info("Saliendo del metodo ModificarMensajeAmigo de Conversacion");
  }

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para eliminar el mensaje
 * 
 */
  eliminarMensajeAmigo(idMensaje){
    catProd.info("Entrando en el metodo eliminarMensajeAmigo de Conversacion");
    this.translateService.get('Eliminado Exitosamente').subscribe(value => {this.delete = value;})
    this.translateService.get('Ocurrio un error').subscribe(value => {this.message = value;})
    let entidad: Mensaje;
    entidad = new Mensaje("",this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,0,"","",false);
    entidad.Id = idMensaje;
    let comando = FabricaComando.crearComandoEliminarMensaje();
    comando.setEntidad = entidad;
    comando.execute();
    if(comando.getRespuesta == true){
      this.presentToast(this.delete);
    }else{
      this.presentToast(this.message);

    }
    catProd.info("Saliendo del metodo eliminarMensajeAmigo de Conversacion");
  }

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para crear el mensaje
 * 
 */
  agregarMensajeAmigo() {
    catProd.info("Entrando en el metodo agregarMensajeAmigo de Conversacion");
    this.translateService.get('Por favor').subscribe(value => {this.message = value;})
    if(this.nuevoMensaje != ""){
      let entidad: Mensaje;
      entidad = new Mensaje(this.nuevoMensaje,this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,0,"","",false);
      let comando = FabricaComando.crearComandoCrearMensaje();
      comando._entidad = entidad;
      comando.execute();
      this.content.scrollToBottom();
      this.nuevoMensaje = '';
    }else{
      this.presentToast(this.message);

    }
    catProd.info("Saliendo del metodo agregarMensajeAmigo de Conversacion");
  }

  /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para obtener la lista de mensajes
 * 
 */
  ionViewDidEnter() {
    catProd.info("Entrando en el metodo IonViewDidEnter de Conversacion");
    let entidad: Mensaje;
    entidad = new Mensaje("",this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,0,"","",false);
    let comando = FabricaComando.crearComandoVisualizarConversacionAmigo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
    catProd.info("Saliendo del metodo IonViewDidEnter de Conversacion");
  }

  
  /**
 * Descripcion del metodo:
 * Metodo que se encarga de mover la lista de mensajes hasta el final
 * 
 */
  scrollto() {
    catProd.info("Entrando en el metodo scrollTo de Conversacion");
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
    catProd.info("Saliendo del metodo scrollTo de Conversacion");
  }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de mostrar el toast de informacion
 * 
 */
  presentToast(mensaje : string) {
    catProd.info("Entrando en el metodo presentToast de Conversacion");
    let toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    toast.present();
    catProd.info("Saliendo del metodo presentToast de Conversacion");
  }
}




