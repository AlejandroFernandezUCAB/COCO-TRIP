import { ComandoInformacionMensajeGrupo } from './../commands/comandoInformacionMensajeGrupo';
import { ComandoInformacionMensajeAmigo } from './../commands/comandoInformacionMensajeAmigo';
import { ComandoVisualizarConversacionGrupo } from './../commands/comandoVisualizarConversacionGrupo';
import { ComandoVisualizarConversacionAmigo } from './../commands/comandoVisualizarConversacionAmigo';
import { ComandoModificarMensajeGrupo } from './../commands/comandoModificarMensajeGrupo';
import { ComandoModificarMensaje } from './../commands/comandoModificarMensaje';
import { ComandoEliminarMensajeGrupo } from './../commands/comandoEliminarMensajeGrupo';
import { ComandoCrearMensajeGrupo } from './../commands/comandoCrearMensajeGrupo';
import  { ComandoCrearMensaje } from '../commands/comandoCrearMensaje';
import { ComandoEliminarMensaje } from '../commands/comandoEliminarMensaje';
import { catService, catProd } from "../../logs/config";
import { ComandoAgregarAmigo } from '../commands/comandoAgregarAmigo';
import {ComandoAgregarItinerario} from "../commands/comandoAgregarItinerario";
import { ComandoEliminarItinerario } from '../commands/comandoEliminarItinerario';
import { ComandoEliminarItemItinerario } from '../commands/comandoEliminarItemItinerario';
import { ComandoModificarItinerario } from '../commands/comandoModificarItinerario';
import { ComandoVerItem } from '../commands/comandoVerItem';
import { ComandoGetNotificacionesConfig } from '../commands/comandoGetNotificacionesConfig';
import { ComandoConsultarEventos } from '../commands/comandoConsultarEventos';
import { ComandoConsultarActividades } from '../commands/comandoConsultarActividades';
import { ComandoConsultarLugarTuristico } from '../commands/comandoConsultarLugarTuristico';
import { ComandoAgregarItemItinerario } from '../commands/comandoAgregarItemItinerario';
import { ComandoModificarNotificacionCorreo } from '../commands/comandoModificarNotificacionCorreo';
import { ComandoSetVisibleNotificacion } from '../commands/comandoSetVisibleNotificacion';
import { ComandoConsultarItinerarios } from '../commands/comandoConsultarItinerarios';
import { Itinerario } from '../../dataAccessLayer/domain/itinerario';
import { DateTime } from 'ionic-angular/components/datetime/datetime';
//****************************************************************************************************//
//**********************************Fabrica Comando de MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * Fabrica Comando
 * 
 */
export class FabricaComando{
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoCrearMensaje
 * 
 */

    public static crearComandoCrearMensaje(){
    catProd.info("Entrando en el metodo crearComandoCrearMensaje de fabricaComando");
        return new ComandoCrearMensaje();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoCrearMensajeGrupo
 * 
 */
    public static crearComandoCrearMensajeGrupo(){
    catProd.info("Entrando en el metodo ComandoCrearMensajeGrupo de fabricaComando");
        return new ComandoCrearMensajeGrupo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarMensaje
 * 
 */
    public static crearComandoEliminarMensaje(){
    catProd.info("Entrando en el metodo ComandoEliminarMensaje de fabricaComando");    
        return new ComandoEliminarMensaje();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarMensajeGrupo
 * 
 */
    public static crearComandoEliminarMensajeGrupo(){
    catProd.info("Entrando en el metodo ComandoEliminarMensajeGrupo de fabricaComando");    
        return new ComandoEliminarMensajeGrupo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoModificarMensaje
 * 
 */
    public static crearComandoModificarMensaje(){
    catProd.info("Entrando en el metodo ComandoModificarMensaje de fabricaComando");    
        return new ComandoModificarMensaje();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoModificarMensajeGrupo
 * 
 */
    public static crearComandoModificarMensajeGrupo(){
    catProd.info("Entrando en el metodo ComandoModificarMensajeGrupo de fabricaComando");    
        return new ComandoModificarMensajeGrupo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoVisualizarConversacionAmigo
 * 
 */
    public static crearComandoVisualizarConversacionAmigo(){
    catProd.info("Entrando en el metodo ComandoVisualizarConversacionAmigo de fabricaComando");    
        return new ComandoVisualizarConversacionAmigo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoVisualizarConversacionGrupo
 * 
 */
    public static crearComandoVisualizarConversacionGrupo(){
    catProd.info("Entrando en el metodo ComandoVisualizarConversacionGrupo de fabricaComando");    
        return new ComandoVisualizarConversacionGrupo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoInformacionMensajeAmigo
 * 
 */
    public static crearComandoInformacionMensajeAmigo(){
    catProd.info("Entrando en el metodo ComandoInformacionMensajeAmigo de fabricaComando");    
        return new ComandoInformacionMensajeAmigo();
    }
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoInformacionMensajeGrupo
 * 
 */
    public static crearComandoInformacionMensajeGrupo(){
    catProd.info("Entrando en el metodo ComandoInformacionMensajeGrupo de fabricaComando");    
        return new ComandoInformacionMensajeGrupo();
    }

    /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoAgregarItinerario
 * 
 */
public static crearComandoAgregarItinerario(itinerario : Itinerario){
    catProd.info("Entrando en el metodo ComandoAgregarItinerario de fabricaComando");    
        return new ComandoAgregarItinerario(itinerario);
    }

        /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarItinerario
 * 
 */
public static crearComandoEliminarItinerario(idIt :number){
    catProd.info("Entrando en el metodo ComandoEliminarItinerario de fabricaComando");    
        return new ComandoEliminarItinerario(idIt);
    }
        /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarItemItinerario
 * 
 */
public static crearComandoEliminarItemItinerario(tipo:string,idIT:number,idItem:number){
    catProd.info("Entrando en el metodo ComandoEliminarItemItinerario de fabricaComando");    
        return new ComandoEliminarItemItinerario(tipo,idIT,idItem);
    }

        /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoModificarItinerario
 * 
 */
public static crearComandoModificarItinerario(itinerario:Itinerario){
    catProd.info("Entrando en el metodo ComandoModificarItinerario de fabricaComando");    
        return new ComandoModificarItinerario(itinerario);
    }

            /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoVerItem
 * 
 */
public static crearComandoVerItem(idItem : number,tipo : string){
    catProd.info("Entrando en el metodo ComandoVerItem de fabricaComando");    
        return new ComandoVerItem(idItem,tipo);
    }
            /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoGetNotificacionesConfig
 * 
 */
public static crearComandoGetNotificacionesConfig(idUsuario:number){
    catProd.info("Entrando en el metodo ComandoGetNotificacionesConfig de fabricaComando");    
        return new ComandoGetNotificacionesConfig(idUsuario);
    }

                /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoConsultarEventos
 * 
 */
public static crearComandoConsultarEventos(nombre:string,fechai:DateTime,fechaf:DateTime){
    catProd.info("Entrando en el metodo ComandoConsultarEventos de fabricaComando");    
        return new ComandoConsultarEventos(nombre,fechai,fechaf);
    }

                    /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoConsultarActividades
 * 
 */
public static crearComandoConsultarActividades(nombre:string){
    catProd.info("Entrando en el metodo ComandoConsultarActividades de fabricaComando");    
        return new ComandoConsultarActividades(nombre);
    }

                    /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoConsultarLugarTuristico
 * 
 */
public static crearComandoConsultarLugarTuristico(nombre:string){
    catProd.info("Entrando en el metodo ComandoConsultarLugarTuristico de fabricaComando");    
        return new ComandoConsultarLugarTuristico(nombre);
    }

                        /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoAgregarItemItinerario
 * 
 */
public static crearComandoAgregarItemItinerario(tipo:string,idIt:number,itemId:number,fechai:DateTime,
    fechaf:DateTime){
    catProd.info("Entrando en el metodo ComandoAgregarItemItinerario de fabricaComando");    
        return new ComandoAgregarItemItinerario(tipo,idIt,itemId,fechai,fechaf);
    }

                            /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoModificarNotificacionCorreo
 * 
 */
public static crearComandoModificarNotificacionCorreo(idUsuario :number,valor){
    catProd.info("Entrando en el metodo ComandoModificarNotificacionCorreo de fabricaComando");    
        return new ComandoModificarNotificacionCorreo(idUsuario,valor);
    }

                                /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoSetVisibleNotificacion
 * 
 */
public static crearComandoSetVisibleNotificacion(idUsuario:number,idIt:number,visible:boolean){
    catProd.info("Entrando en el metodo ComandoSetVisibleNotificacion de fabricaComando");    
        return new ComandoSetVisibleNotificacion(idUsuario,idIt,visible);
    }

                            /**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoConsultarItinerarios
 * 
 */
public static ComandoConsultarItinerarios(idUsuario:number){
    catProd.info("Entrando en el metodo ComandoConsultarItinerarios de fabricaComando");    
        return new ComandoConsultarItinerarios(idUsuario);
    }
}
