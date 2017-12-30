import { ComandoInformacionMensajeGrupo } from './../commands/comandoInformacionMensajeGrupo';
import { ComandoInformacionMensajeAmigo } from './../commands/comandoInformacionMensajeAmigo';
import { ComandoVisualizarConversacionGrupo } from './../commands/comandoVisualizarConversacionGrupo';
import { ComandoVisualizarConversacionAmigo } from './../commands/comandoVisualizarConversacionAmigo';
import { ComandoModificarMensajeGrupo } from './../commands/comandoModificarMensajeGrupo';
import { ComandoModificarMensaje } from './../commands/comandoModificarMensaje';
import { ComandoEliminarMensajeGrupo } from './../commands/comandoEliminarMensajeGrupo';
import { ComandoCrearMensajeGrupo } from './../commands/comandoCrearMensajeGrupo';
import  { ComandoCrearMensaje } from '../commands/comandoCrearMensaje';
import { ComandoListaAmigos } from '../commands/comandoListaAmigos';
import { ComandoEliminarAmigo } from '../commands/comandoEliminarAmigo';
import { ComandoListaGrupos } from '../commands/comandoListaGrupos';
import { ComandoVerificarLider } from '../commands/comandoVerificarLider';
import { ComandoSalirGrupo } from '../commands/comandoSalirGrupo';
import { ComandoListaNotificaciones } from '../commands/comandoListaNotificaciones';
import  { Comando } from '../commands/comando';
import { Entidad } from '../../dataAccessLayer/domain/entidad'; 
import { ComandoEliminarMensaje } from '../commands/comandoEliminarMensaje';
import { catService, catProd } from "../../logs/config";
import { ComandoAceptarNotificacion } from '../commands/comandoAceptarNotificacion';
import { ComandoRechazarNotificacion } from '../commands/comandoRechazarNotificacion';
import { ComandoBuscarAmigo } from '../commands/comandoBuscarAmigo';
import { ComandoAgregarIntegrante } from '../commands/comandoAgregarIntegrante';
import { ComandoVerPerfilGrupo } from '../commands/comandoVerPerfilGrupo';
import { ComandoListaMiembroGrupo } from '../commands/comandoListaMiembroGrupo';
import { ComandoObtenerLider } from '../commands/comandoObtenerLider';
import { ComandoObtenerSinLider } from '../commands/comandoObtenerSinLider';
import { ComandoEliminarIntegrante } from '../commands/comandoEliminarIntegrante';
import { ComandoModificarGrupo } from '../commands/comandoModificarGrupo';
import { ComandoObtenerMiembrosSinGrupo } from '../commands/comandoObtenerMiembrosSinGrupo';
import { ComandoAgregarGrupo } from '../commands/comandoAgregarGrupo';
import { ComandoObtenerUltimoGrupo } from '../commands/comandoObtenerUltimoGrupo';
import { ComandoObtenerPerfilPublico } from '../commands/comandoObtenerPerfilPublico';
import { ComandoAgregarAmigo } from '../commands/comandoAgregarAmigo';
import { ComandoEnviarCorreo } from '../commands/comandoEnviarCorreo';
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
}
