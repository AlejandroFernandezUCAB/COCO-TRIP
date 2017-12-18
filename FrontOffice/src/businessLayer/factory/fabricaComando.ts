import { ComandoVisualizarConversacionGrupo } from './../commands/comandoVisualizarConversacionGrupo';
import { ComandoVisualizarConversacionAmigo } from './../commands/comandoVisualizarConversacionAmigo';

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

    public static crearComandoVisualizarConversacionAmigo(){
        return new ComandoVisualizarConversacionAmigo();
    }

    public static crearComandoVisualizarConversacionGrupo(){
        return new ComandoVisualizarConversacionGrupo();
    }

}