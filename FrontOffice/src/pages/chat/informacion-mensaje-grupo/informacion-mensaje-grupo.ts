import { Registry } from './../../../common/registry';
import { Component, } from '@angular/core';
import { IonicPage, NavController, NavParams,Events } from 'ionic-angular';
import { ChatProvider } from '../../../providers/chat/chat';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';

/**
 * Generated class for the InformacionMensajeGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-informacion-mensaje-grupo',
  templateUrl: 'informacion-mensaje-grupo.html',
})
export class InformacionMensajeGrupoPage {
  Mensajerec:any={
    idmensaje:'idmensaje',
    idgrupo:'idgrupo'
  }
  info:any={
    mensaje:'mensaje',
    fecha:'fecha',
    modificado:'modificado'

  }

  constructor(public navCtrl: NavController, public navParams: NavParams,public chatService: ChatProvider,public events: Events) {
  }

  ionViewWillEnter() {
    let mensaje: Mensaje;
    this.Mensajerec.idmensaje = this.navParams.get('idmensaje');
    this.Mensajerec.idgrupo = this.navParams.get('idgrupo');
    
    
      this.events.subscribe(Registry.PUBLISH_INFO_MENSAJE_GRUPOS, (Mensajes) => {
      mensaje = Mensajes;
      this.info.mensaje=mensaje.getMensaje;
      this.info.fecha=mensaje.getFecha;
      this.info.modificado=mensaje.getModificado;
    })
    
  }  
  ionViewDidEnter() {
    let entidad: Mensaje;
    entidad = new Mensaje("","","",this.Mensajerec.idgrupo,"","",false);
    entidad.setId=this.Mensajerec.idmensaje;
    let comando = FabricaComando.crearComandoInformacionMensajeGrupo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
  }

}
