import  { ComandoCrearMensaje } from '../commands/comandoCrearMensaje';
import  { Comando } from '../commands/comando';
import { Entidad } from '../../dataAccessLayer/domain/entidad'; 

export class FabricaComando{
    

    public static crearComandoMensaje(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoMensajeGrupo(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoEliminarMensaje(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoEliminarMensajeGrupo(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoVisualizarConversacionAmigo(){
        return new ComandoCrearMensaje();
    }

    public static crearComandoVisualizarConversacionGrupo(){
        return new ComandoCrearMensaje();
    }

}