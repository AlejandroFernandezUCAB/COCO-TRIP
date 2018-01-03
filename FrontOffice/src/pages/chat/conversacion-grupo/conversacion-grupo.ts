import { Registry } from './../../../common/registry';
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
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';
import { ToastController } from 'ionic-angular';
import { InformacionMensajeGrupoPage } from '../../chat/informacion-mensaje-grupo/informacion-mensaje-grupo';

import {catService,catProd} from "../../../logs/config"

//****************************************************************************************************//
//******************************PAGE DE CONVERSACION GRUPO MODULO 6***********************************//
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
 * Conversacion de grupos
 */

@IonicPage()
@Component({
  selector: 'page-conversacion-grupo',
  templateUrl: 'conversacion-grupo.html',
})
export class ConversacionGrupoPage {
  @ViewChild('content') content: Content;
  conversacion: any;
  nuevoMensaje = "";
  /*nombreUsuario:any={
    idGrupo: 'idGrupo'
  }*/
  idAmigo: any;
  idGrupo: any;
  nombreGrupo : any = {
    nombre : 'nombre'
  };
  idUsuario: any;
  mensj:any;
  usuario: any = {
    NombreUsuario: 'NombreUsuario'
  };
  todosLosMensajes = [];
  //nombreUsuario: string;

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
    catProd.info("Entrando en el metodo ionViewWillEnter de ConversacionGrupoPage");
    this.idGrupo = this.navParams.get('idGrupo');
    this.nombreGrupo.nombre = this.navParams.get('nombreGrupo');
    
   
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
          this.events.subscribe(Registry.PUBLISH_LISTA_MENSAJE_GRUPOS, (Mensajes) => {
            this.todosLosMensajes = [];
            this.zone.run(() => {
              this.todosLosMensajes = Mensajes;
            })
          })
  
          catProd.info("Saliendo del metodo ionViewWillEnter de ConversacionGrupoPage");
   }



  /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para obtener la lista de mensajes
 * 
 */
  ionViewDidEnter() {
    catProd.info("Entrando en el metodo ionViewDidEnter de ConversacionGrupoPage");
    let entidad: Mensaje;
    entidad = new Mensaje("","","",this.idGrupo,"","",false);
    let comando = FabricaComando.crearComandoVisualizarConversacionGrupo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
    
    catProd.info("Saliendo del metodo ionViewDidEnter de ConversacionGrupoPage");
  }

  /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el menu en el cual se tendran las opciones de
 * visualizar, eliminar y modificar el mensaje.
 * 
 */
  pressEvent(idMensaje){
    catProd.info("Entrando en el metodo pressEvent de ConversacionGrupoPage");
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
                      this.eliminarMensajeGrupo(idMensaje);
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
                this.navCtrl.push(InformacionMensajeGrupoPage,{
                  idmensaje:idMensaje,
                  idgrupo:this.idGrupo
              });
              }
            }
        ]
      });
      actionSheet.present();
    }
    
    catProd.info("Saliendo del metodo pressEvent de ConversacionGrupoPage");
  }

   /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el alert en el cual se modificara el mensaje
 * 
 */
  crearalert(idMensaje){
    catProd.info("Entrando en el metodo crearalert de ConversacionGrupoPage");
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
              this.modificarMensajeGrupo(idMensaje,data.modificado);
            }else{
              this.translateService.get('Por favor').subscribe(value => {this.message = value;})
              this.presentToast(this.message);
            }
          }
        }
      ]
    });
    prompt.present();

    catProd.info("Saliendo del metodo crearalert de ConversacionGrupoPage");
  }

  /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para eliminar el mensaje
 * 
 */
  eliminarMensajeGrupo(idMensaje){
    catProd.info("Entrando en el metodo eliminarMensajeGrupo de ConversacionGrupoPage");
    this.translateService.get('Eliminado Exitosamente').subscribe(value => {this.delete = value;})
    this.translateService.get('Ocurrio un error').subscribe(value => {this.message = value;})
    let entidad: Mensaje;
    entidad = new Mensaje("",this.usuario.NombreUsuario,"",this.idGrupo,"","",false);
    entidad.setFireId = idMensaje;
    let comando = FabricaComando.crearComandoEliminarMensajeGrupo();
    comando.setEntidad = entidad;
    comando.execute();
    if(comando.getRespuesta == true){
      this.presentToast(this.delete);
    }else{
      this.presentToast(this.message);

    }
    
    catProd.info("Saliendo del metodo eliminarMensajeGrupo de ConversacionGrupoPage");
  }

   /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para crear el mensaje
 * 
 */
   agregarMensajeGrupo() {
    catProd.info("Entrando en el metodo agregarMensajeGrupo de ConversacionGrupoPage");
    this.translateService.get('Por favor').subscribe(value => {this.message = value;})
     if(this.nuevoMensaje != ""){
      let entidad: Mensaje;
      entidad = new Mensaje(this.nuevoMensaje,this.usuario.NombreUsuario,"",this.idGrupo,"","",false);
      let comando = FabricaComando.crearComandoCrearMensajeGrupo();
      comando._entidad = entidad;
      comando.execute();
      this.content.scrollToBottom();
      this.nuevoMensaje = '';
     }else{
      this.presentToast(this.message);
    }

    catProd.info("Saliendo del metodo agregarMensajeGrupo de ConversacionGrupoPage");
      

  }

 /**
 * Descripcion del metodo:
 * Metodo que se encarga de crear el comando y ejecutarlo para modificar el mensaje
 * 
 */
modificarMensajeGrupo(idMensaje,nuevoMensaje){
  catProd.info("Entrando en el metodo modificarMensajeGrupo de ConversacionGrupoPage");
  let entidad: Mensaje;
  entidad = new Mensaje(nuevoMensaje,this.usuario.NombreUsuario,"",this.idGrupo,"","",true);
  entidad.setFireId = idMensaje;
  let comando = FabricaComando.crearComandoModificarMensajeGrupo();
  comando.setEntidad = entidad;
  comando.execute();
  catProd.info("Saliendo del metodo modificarMensajeGrupo de ConversacionGrupoPage");

}
  scrollto() {
    catProd.info("Entrando en el metodo scrollto de ConversacionGrupoPage");
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
    catProd.info("Saliendo del metodo scrollto de ConversacionGrupoPage");
  }

  
  
/**
 * Descripcion del metodo:
 * Metodo que se encarga de mostrar el toast de informacion
 * 
 */
  presentToast(mensaje : string) {
    catProd.info("Entrando en el metodo presentToast de ConversacionGrupoPage");
    let toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    toast.present();
    catProd.info("Saliendo del metodo presentToast de ConversacionGrupoPage");
  }
  

}
