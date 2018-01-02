import { Registry } from './../../../common/registry';
import { Component, } from '@angular/core';
import { IonicPage, NavController, NavParams,Events } from 'ionic-angular';
import { ChatProvider } from '../../../providers/chat/chat';
import { Mensaje } from '../../../dataAccessLayer/domain/mensaje';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';
import {catService,catProd} from "../../../logs/config";
//****************************************************************************************************//
//***********************************Informacion Mensaje de MODULO 6*************************************//
//****************************************************************************************************//

//****************************************************************************************************//
//*****************************PAGE DE INFORMACION DE MENSAJE MODULO 6********************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 * Descripcion de la clase:
 * 
 * Informacion de un mensaje
 */

/**
 * Descripcion de la clase:
 * Informacion Mensaje
 * 
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
    catProd.info("Entrando en el metodo IonViewWillEnter de informacion-mensaje");
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
    catProd.info("Saliendo del metodo IonViewWillEnter de informacion-mensaje");
    
  }  
  ionViewDidEnter() {
    catProd.info("Entrando en el metodo ionViewDidEnter de informacion-mensaje");
    let entidad: Mensaje;
    entidad = new Mensaje("",this.Mensajerec.emisor,this.Mensajerec.receptor,0,"","",false);
    entidad.setId=this.Mensajerec.idmensaje;
    let comando = FabricaComando.crearComandoInformacionMensajeAmigo();
    comando.setEntidad = entidad;
    comando.setEvents = this.events;
    comando.execute();
    catProd.info("Saliendo del metodo ionViewDidEnter de informacion-mensaje");
  }

}
