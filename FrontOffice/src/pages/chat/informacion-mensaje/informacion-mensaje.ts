import { Registry } from './../../../common/registry';
import { Component, } from '@angular/core';
import { IonicPage, NavController, NavParams,Events } from 'ionic-angular';
import { ChatProvider } from '../../../providers/chat/chat';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';

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

  constructor(public navCtrl: NavController, public navParams: NavParams,public chatService: ChatProvider,public events: Events) {
  }


  ionViewWillEnter() {
    let mensaje: Mensaje;
    this.Mensajerec.idmensaje = this.navParams.get('idmensaje');
    this.Mensajerec.emisor = this.navParams.get('emisor');
    this.Mensajerec.receptor=this.navParams.get('receptor');
   
      this.events.subscribe(Registry.PUBLISH_INFO_MENSAJE_AMIGOS, (Mensajes) => {
      mensaje = Mensajes;
      this.info.mensaje=mensaje.getMensaje;
      this.info.fecha=mensaje.getFecha;
      this.info.modificado=mensaje.getModificado;
    })
    
  }  
  ionViewDidEnter() {
    let entidad: Mensaje;
    entidad = new Mensaje("",this.Mensajerec.emisor,this.Mensajerec.receptor,0,"","",false);
    entidad.setId=this.Mensajerec.idmensaje;
    let comando = FabricaComando.crearComandoInformacionMensajeAmigo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
  }

}
