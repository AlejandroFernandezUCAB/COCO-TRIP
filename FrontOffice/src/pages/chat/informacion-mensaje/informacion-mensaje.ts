import { Component, } from '@angular/core';
import { IonicPage, NavController, NavParams,Events } from 'ionic-angular';
import { ChatProvider } from '../../../providers/chat/chat';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';

/**
 * Generated class for the InformacionMensajePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-informacion-mensaje',
  templateUrl: 'informacion-mensaje.html',
})
export class InformacionMensajePage {
  Mensajerec:any={
    idmensaje:'idmensaje',
    emisor:'emisor',
    receptor:'receptor'
  }
  info:any={
    mensaje:'mensaje',
    fecha:'fecha',
    modificado:'modificado'

  }
 
  mensajesConversacion = [];

  constructor(public navCtrl: NavController, public navParams: NavParams,public chatService: ChatProvider) {
  }


  ionViewWillEnter() {
    alert("entro en el will");
    let mensaje: Mensaje;
    this.Mensajerec.idmensaje = this.navParams.get('idmensaje');
    this.Mensajerec.emisor = this.navParams.get('emisor');
    this.Mensajerec.receptor=this.navParams.get('receptor');
   
    
    
    let chat : ChatProvider;
    let evento : Events;
    evento = new Events;
    chat = new ChatProvider(evento);
    chat.obtenerInfoMensajeAmigo(this.Mensajerec.emisor,this.Mensajerec.receptor,this.Mensajerec.idmensaje);
    
   // str = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
   // chat.obtenerMensajesConversacionAmigo(mensaje.getUsuario, mensaje.getAmigo);

    
    
    evento.subscribe('infoMensaje', (Mensajes) => {
      alert("entro en subscribe");
      mensaje = Mensajes;
      this.info.mensaje=mensaje.getMensaje;
      this.info.fecha=mensaje.getFecha;
      this.info.modificado=mensaje.getModificado;

      alert(mensaje.getId)
    })
    



    /*mensaje=this.chatService.obtenerMensaje();
    this.events.subscribe('visualizarMensaje', (Mensajes)=> {
      otro = Mensajes;
      alert("dentro del subscribe"+entidad.getId)
    })
    //alert("en info"+mensaje.getMensaje);*/
  }  
  ionViewDidEnter() {
    //this.chatService.obtenerInfoMensajeAmigo(this.Mensajerec.emisor,this.Mensajerec.receptor,this.Mensajerec.idmensaje);
    
  }

}
