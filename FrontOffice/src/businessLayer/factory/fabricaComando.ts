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
 * Metodo que se encarga de instanciar ComandoListaAmigos
 * @param id Identificador del usuario
 */
    public static crearComandoListaAmigos(id : number) : ComandoListaAmigos
    {
        return new ComandoListaAmigos(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarAmigo
 * @param nombreUsuario Nombre de usuario del amigo
 * @param id Identificador del usuario
 */
    public static crearComandoEliminarAmigo(nombreUsuario : string, id : number) : ComandoEliminarAmigo
    {
        return new ComandoEliminarAmigo(nombreUsuario, id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoListaGrupos
 * @param id Identificador del usuario
 */
    public static crearComandoListaGrupos(id : number) : ComandoListaGrupos
    {
        return new ComandoListaGrupos(id);
    }
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoVerificarLider
 * @param idUsuario Identificador del usuario
 * @param idgrupo Identificador del grupo
 */
    public static crearComandoVerificarLider(idGrupo : number, idUsuario : number) : ComandoVerificarLider
    {
        return new ComandoVerificarLider(idGrupo, idUsuario);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoSalirGrupo
 * @param idUsuario Identificador del usuario
 * @param idgrupo Identificador del grupo
 */
    public static crearComandoSalirGrupo(idGrupo : number, idUsuario : number) : ComandoSalirGrupo
    {
        return new ComandoSalirGrupo(idGrupo, idUsuario);
    }
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoListaNotificaciones
 * @param id Identificador del usuario
 */
    public static crearComandoListaNotificaciones(id : number) : ComandoListaNotificaciones
    {
        return new ComandoListaNotificaciones(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoAceptarNotificacion
 * @param nombreUsuario Nombre de usuario del amigo
 * @param id Identificador del usuario
 */
    public static crearComandoAceptarNotificacion(nombreUsuario : string, id : number) : ComandoAceptarNotificacion
    {
        return new ComandoAceptarNotificacion(nombreUsuario, id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoRechazarNotificacion
 * @param nombreUsuario Nombre de usuario del amigo
 * @param id Identificador del usuario
 */
    public static crearComandoRechazarNotificacion(nombreUsuario : string, id : number) : ComandoRechazarNotificacion
    {
        return new ComandoRechazarNotificacion(nombreUsuario, id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoBuscarAmigo
 * @param nombre Texto de busqueda
 * @param id  Identificador del usuario
 */
    public static crearComandoBuscarAmigo(nombre : string, id : number) : ComandoBuscarAmigo
    {
        return new ComandoBuscarAmigo(nombre, id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoAgregarIntegrante
 * @param idGrupo Identificador del grupo
 * @param nombreUsuario Nombre de usuario a agregar en el grupo
 */
    public static crearComandoAgregarIntegrante(idGrupo : number, nombreUsuario : string) : ComandoAgregarIntegrante
    {
        return new ComandoAgregarIntegrante(idGrupo, nombreUsuario);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoVerPerfilGrupo
 * @param id  Identificador del grupo
 */
    public static crearComandoVerPerfilGrupo(id : number) : ComandoVerPerfilGrupo
    {
        return new ComandoVerPerfilGrupo(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoListaMiembroGrupo
 * @param id Identificador del grupo
 */
    public static crearComandoListaMiembroGrupo(id : number) : ComandoListaMiembroGrupo
    {
        return new ComandoListaMiembroGrupo(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoObtenerLider
 * @param id Identificador del grupo 
 */
    public static crearComandoObtenerLider(id : number) : ComandoObtenerLider
    {
        return new ComandoObtenerLider(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoObtenerSinLider
 * @param id Identificador del grupo
 */
    public static crearComandoObtenerSinLider(id : number) : ComandoObtenerSinLider
    {
        return new ComandoObtenerSinLider(id);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoEliminarIntegrante
 * @param idGrupo Identificador del grupo
 * @param nombreUsuario Nombre de usuario a eliminar en el grupo
 */
    public static crearComandoEliminarIntegrante(idGrupo : number, nombreUsuario : string) : ComandoEliminarIntegrante
    {
        return new ComandoEliminarIntegrante(idGrupo, nombreUsuario);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoModificarGrupo
 * @param nombreGrupo Nuevo nombre del grupo
 * @param idUsuario Identificador del usuario que va a modificar el nombre (para validar si es o no el lider)
 * @param idGrupo Identificador del grupo 
 */
    public static crearComandoModificarGrupo(nombreGrupo : string, idUsuario : number, idGrupo : number) : ComandoModificarGrupo
    {
        return new ComandoModificarGrupo(nombreGrupo, idUsuario, idGrupo);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoObtenerMiembrosSinGrupo
 * @param idUsuario Identificador del usuario
 * @param idGrupo Identificador del grupo
 */
    public static crearComandoObtenerMiembrosSinGrupo(idUsuario : number, idGrupo : number) : ComandoObtenerMiembrosSinGrupo
    {
        return new ComandoObtenerMiembrosSinGrupo(idUsuario, idGrupo);
    }
    
/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoAgregarGrupo
 * @param idUsuario Identificador del usuario
 * @param nombreGrupo Nombre del grupo
 */
    public static crearComandoAgregarGrupo(idUsuario : number, nombreGrupo : string) : ComandoAgregarGrupo
    {
        return new ComandoAgregarGrupo(idUsuario, nombreGrupo);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoObtenerUltimoGrupo
 * @param idUsuario Identificador del usuario
 */
    public static crearComandoObtenerUltimoGrupo(idUsuario : number) : ComandoObtenerUltimoGrupo
    {
        return new ComandoObtenerUltimoGrupo(idUsuario);
    }

/**
 * Descripcion del metodo:
 * Metodo que se encarga de instanciar ComandoObtenerPerfilPublico
 * @param nombreUsuario Nombre de usuario
 */
    public static crearComandoObtenerPerfilPublico(nombreUsuario : string) : ComandoObtenerPerfilPublico
    {
        return new ComandoObtenerPerfilPublico(nombreUsuario);
    }
}