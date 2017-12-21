import { ComandoVisualizarMensajeGrupo } from './../commands/comandoVisualizarMensajeGrupo';
import { ComandoVisualizarMensajeAmigo } from './../commands/comandoVisualizarMensajeAmigo';
import { ComandoVisualizarConversacionGrupo } from './../commands/comandoVisualizarConversacionGrupo';
import { ComandoVisualizarConversacionAmigo } from './../commands/comandoVisualizarConversacionAmigo';
import { ComandoModificarMensajeGrupo } from './../commands/comandoModificarMensajeGrupo';
import { ComandoModificarMensaje } from './../commands/comandoModificarMensaje';
import { ComandoEliminarMensajeGrupo } from './../commands/comandoEliminarMensajeGrupo';
import { ComandoCrearMensajeGrupo } from './../commands/comandoCrearMensajeGrupo';
import  { ComandoCrearMensaje } from '../commands/comandoCrearMensaje';
import  { Comando } from '../commands/comando';
import { Entidad } from '../../dataAccessLayer/domain/entidad'; 
import { ComandoEliminarMensaje } from '../commands/comandoEliminarMensaje';

export class FabricaComando{
    

    public static crearComandoCrearMensaje(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoCrearMensajeGrupo(){
        return new ComandoCrearMensajeGrupo();
    }

    public static crearComandoEliminarMensaje(){
        return new ComandoEliminarMensaje();
    }

    public static crearComandoEliminarMensajeGrupo(){
        return new ComandoEliminarMensajeGrupo();
    }

    public static crearComandoModificarMensaje(){
        return new ComandoModificarMensaje();
    }

    public static crearComandoModificarMensajeGrupo(){
        return new ComandoModificarMensajeGrupo();
    }

    public static crearComandoVisualizarConversacionAmigo(){
        return new ComandoVisualizarConversacionAmigo();
    }

    public static crearComandoVisualizarConversacionGrupo(){
        return new ComandoVisualizarConversacionGrupo();
    }

    public static crearComandoVisualizarMensajeAmigo(){
        return new ComandoVisualizarMensajeAmigo();
    }

    public static crearComandoVisualizarMensajeGrupo(){
        return new ComandoVisualizarMensajeGrupo();
    }

}