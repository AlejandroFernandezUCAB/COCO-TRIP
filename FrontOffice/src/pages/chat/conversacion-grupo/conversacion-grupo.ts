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


/**
 * Generated class for the ConversacionGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
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

  constructor(public navCtrl: NavController, public navParams: NavParams,
  public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController,
  public platform: Platform, private firebase: Firebase , public chatService: ChatProvider,
  public events: Events, public zone: NgZone, private storage: Storage,public restapiService: RestapiService,
  public toastCtrl: ToastController) {
  }

  ionViewWillEnter() {
 
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
          this.events.subscribe('nuevoMensajeGrupo', (Mensajes) => {
            this.todosLosMensajes = [];
            this.zone.run(() => {
              this.todosLosMensajes = Mensajes;
            })
          })
  
   }


  ionViewDidEnter() {
    let entidad: Mensaje;
    entidad = new Mensaje("","","",this.idGrupo,"","",false);
    let comando = FabricaComando.crearComandoVisualizarConversacionGrupo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
    
  }

  pressEvent(idMensaje){
    if(idMensaje!=-1){
      let actionSheet = this.actionsheetCtrl.create({
        title: 'Opciones',
        cssClass: 'action-sheets-basic-page',
        buttons: [
          {
            text: 'Eliminar Chat',
            role: 'destructive', // aplica color rojo de alerta
            icon: !this.platform.is('ios') ? 'trash' : null,
            handler: () => {
              let decision = this.alertCtrl.create({
                message: '¿Borrar este chat?',
                buttons: [
                  {
                    text: 'Sí',
                    handler: () => {
                      this.eliminarMensajeGrupo(idMensaje);
                    }
                  },
                  {
                    text: 'No',
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
            text: 'Cancelar',
            role: 'cancel', //coloca el botón siempre en el último lugar.
            icon: !this.platform.is('ios') ? 'close' : null,
            handler: () => {
              console.log('Cancelar ActionSheet');
            }
          },
          {
            text: 'Modificar',
            role: 'Modificar', //coloca el botón siempre en el último lugar.
            icon: !this.platform.is('ios') ? 'create' : null,
            handler: () => {
              this.crearalert(idMensaje);
            }
            }
        ]
      });
      actionSheet.present();
    }
  }

  crearalert(idMensaje){
    
    let prompt = this.alertCtrl.create({
      title: 'Modificar Mensaje',
      message: "Escribe el nuevo mensaje",
      inputs: [
        {
          name: 'modificado',
          placeholder: 'Nuevo mensaje'
        },
      ],
      buttons: [
        {
          text: 'Cancel',
          handler: data => {
            console.log('Cancel clicked');
        
          }
        },
        {
          text: 'Modificar',
          handler: data => {
            if(data.modificado != ""){
              this.modificarMensajeGrupo(idMensaje,data.modificado);
            }else{
              this.presentToast("Por favor escriba un mensaje");
            }
          }
        }
      ]
    });
    prompt.present();

  }

  
  eliminarMensajeGrupo(idMensaje){
    let entidad: Mensaje;
    entidad = new Mensaje("",this.usuario.NombreUsuario,"",this.idGrupo,"","",false);
    entidad.setId = idMensaje;
    let comando = FabricaComando.crearComandoEliminarMensajeGrupo();
    comando.setEntidad = entidad;
    comando.execute();
    if(comando.getRespuesta == true){
      this.presentToast("Se ha eliminado exitosamente");
    }else{
      this.presentToast("Ha ocurrido un error");

    }
  }

   agregarMensajeGrupo() {
     if(this.nuevoMensaje != ""){
      let entidad: Mensaje;
      entidad = new Mensaje(this.nuevoMensaje,this.usuario.NombreUsuario,"",this.idGrupo,"","",false);
      let comando = FabricaComando.crearComandoCrearMensajeGrupo();
      comando._entidad = entidad;
      comando.execute();
      this.content.scrollToBottom();
      this.nuevoMensaje = '';
     }else{
      this.presentToast("Por favor escriba un mensaje");
    }

      

  }

modificarMensajeGrupo(idMensaje,nuevoMensaje){
  let entidad: Mensaje;
  entidad = new Mensaje(nuevoMensaje,this.usuario.NombreUsuario,"",this.idGrupo,"","",true);
  entidad.setId = idMensaje;
  let comando = FabricaComando.crearComandoModificarMensajeGrupo();
  comando.setEntidad = entidad;
  comando.execute();

}
  scrollto() {
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
  }

  
  presentToast(mensaje : string) {
    let toast = this.toastCtrl.create({
      message: mensaje,
      duration: 3000,
      position: 'top'
    });
    toast.present();
  }
  

}
