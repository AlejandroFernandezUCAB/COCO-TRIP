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

@IonicPage()
@Component({
    selector: 'page-conversacion',
    templateUrl: 'conversacion.html'
})

export class ConversacionPage {
  @ViewChild('content') content: Content;
  conversacion: any;
  nuevoMensaje: any;
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
  //nombreUsuario: string;

constructor(public navCtrl: NavController, public navParams: NavParams,
  public actionsheetCtrl: ActionSheetController, public alertCtrl: AlertController,
  public platform: Platform, private firebase: Firebase , public chatService: ChatProvider,
  public events: Events, public zone: NgZone, private storage: Storage,public restapiService: RestapiService) {
 /* let idUsuario     //Obtiene ID de Usuario
  this.storage.get('id').then((val) => {
    idUsuario = val;
  });*/

  //this.idAmigo = this.navParams.get('nombreUsuario');
 // this.nombreUsuario = this.idAmigo;



 // this.firebase.getToken()
    //.then(token => console.log(El token push es ${token})) //se guarda el token del lado del servidor y se usa para enviar notificaciones push.
    //.catch(error => console.log('Error obteniendo el token', error));

  //this.firebase.onTokenRefresh()
    //.subscribe((token: string) => console.log(He obtenido un nuevo token ${token}));

}

ionViewWillEnter() {
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
    this.events.subscribe('nuevoMensaje', () => {
      this.todosLosMensajes = [];
      this.zone.run(() => {
        this.todosLosMensajes = this.chatService.mensajesConversacion;
      })
    })

 }

 pressEvent(idMensaje){
  if(idMensaje!=-1){
    let actionSheet = this.actionsheetCtrl.create({
      title: 'Opciones',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Eliminar mensaje',
          role: 'destructive', // aplica color rojo de alerta
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            let decision = this.alertCtrl.create({
              message: '¿Borrar este mensaje?',
              buttons: [
                {
                  text: 'Sí',
                  handler: () => {

                    this.eliminarMensajeAmigo(this.usuario.NombreUsuario,
                      this.nombreUsuario.NombreAmigo,idMensaje);
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
          icon: !this.platform.is('ios') ? 'close' : null,
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
        text: 'Save',
        handler: data => {
          alert(data.modificado)
          this.chatService.modificarMensajeAmigo(this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,idMensaje,data.modificado);
          
        }
      }
    ]
  });
  prompt.present();
}



  eliminarMensajeAmigo(NombreUsuario,NombreAmigo,idMensaje){
    let entidad: Mensaje;
    entidad = new Mensaje("",NombreUsuario,NombreAmigo,0);
    entidad.setId = idMensaje;
    let comando = FabricaComando.crearComandoEliminarMensaje();
    comando.setEntidad = entidad;
    comando.execute();
  }

  agregarMensajeAmigo() {
    let entidad: Mensaje;
    entidad = new Mensaje(this.nuevoMensaje,this.usuario.NombreUsuario,this.nombreUsuario.NombreAmigo,0);
    let comando = FabricaComando.crearComandoCrearMensaje();
    comando._entidad = entidad;
    comando.execute();
    this.content.scrollToBottom();
    this.nuevoMensaje = '';
  }

  

  ionViewDidEnter() {
    this.chatService.obtenerMensajesConversacionAmigo(this.nombreUsuario.NombreAmigo,this.usuario.NombreUsuario);
  }

  scrollto() {
    setTimeout(() => {
      this.content.scrollToBottom();
    }, 1000);
  }
}



