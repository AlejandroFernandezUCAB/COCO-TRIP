import { ComandoVisualizarConversacionAmigo } from './../src/businessLayer/commands/comandoVisualizarConversacionAmigo';
import { ComandoVisualizarConversacionGrupo } from './../src/businessLayer/commands/comandoVisualizarConversacionGrupo';
import { ComandoEliminarMensaje } from './../src/businessLayer/commands/comandoEliminarMensaje';
import { ComandoModificarMensaje } from './../src/businessLayer/commands/comandoModificarMensaje';
import { ComandoModificarMensajeGrupo } from './../src/businessLayer/commands/comandoModificarMensajeGrupo';
import { ComandoCrearMensaje } from './../src/businessLayer/commands/comandoCrearMensaje';
import { ComandoInformacionMensajeAmigo } from './../src/businessLayer/commands/comandoInformacionMensajeAmigo';
import { ComandoInformacionMensajeGrupo } from './../src/businessLayer/commands/comandoInformacionMensajeGrupo';
import { Entidad } from './../src/dataAccessLayer/domain/entidad';
import { ConversacionGrupoPage } from '../src/pages/chat/conversacion-grupo/conversacion-grupo'
import { ConversacionPage } from '../src/pages/chat/conversacion/conversacion'
import { Mensaje } from '../src/dataAccessLayer/domain/mensaje'
import { ChatProvider } from '../src/providers/chat/chat';
import { config } from '../src/app/app.firebaseconfig';
import { NgZone } from '@angular/core';
import { Events } from 'ionic-angular';
import firebase from 'firebase';
import { DAO } from '../src/dataAccessLayer/dao/dao';
import { DAOChat } from '../src/dataAccessLayer/dao/daoChat';
firebase.initializeApp(config);
//****************************************************************************************************//
//**********************************PRUEBAS UNITARIAS DEL MODULO 6************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

//****************************************************************************************************//
//***********************************PRUEBAS UNITARIAS DEL DOMINIO************************************//
//****************************************************************************************************//

describe("TEST FOR MENSAJE",()=>{

 describe("Test for mensaje.setMensaje", ()=>{
    
    it("should return a message", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setMensaje = "hola";
        expect(mensaje.getMensaje).toEqual("hola");
    });
});

describe("Test for mensaje.getMensaje", ()=>{
    
    it("should return a message", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setMensaje = "hola";
        var nuevoMensaje = mensaje.getMensaje;
        expect(nuevoMensaje).toEqual("hola");
    });
});

describe("Test for mensaje.setUsuario", ()=>{
    
    it("should return an user", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setUsuario = "usuarioTest";
        expect(mensaje.getUsuario).toEqual("usuarioTest");
    });
});

describe("Test for mensaje.getUsuario", ()=>{
    
    it("should return an user", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setUsuario = "usuarioTest";
        var nuevoUsuario = mensaje.getUsuario;
        expect(nuevoUsuario).toEqual("usuarioTest");
    });
});

describe("Test for mensaje.setAmigo", ()=>{
    
    it("should return a friend", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setAmigo = "AmigoTest";
        expect(mensaje.getAmigo).toEqual("AmigoTest");
    });
});

describe("Test for mensaje.getAmigo", ()=>{
    
    it("should return a friend", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setAmigo = "AmigoTest";
        var nuevoAmigo = mensaje.getAmigo;
        expect(nuevoAmigo).toEqual("AmigoTest");
    });
});

describe("Test for mensaje.setIdGrupo", ()=>{
    
    it("should return an id of group", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setidGrupo = -1;
        expect(mensaje.getidGrupo).toEqual(-1);
    });
});

describe("Test for mensaje.getIdGrupo", ()=>{
    
    it("should return an id of group", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setidGrupo = -1;
        var nuevoIdGrupo = mensaje.getidGrupo;
        expect(-1).toEqual(nuevoIdGrupo);
    });
});

describe("Test for mensaje.setFecha", ()=>{
    
    it("should return a date", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setFecha = "-1";
        expect(mensaje.getFecha).toEqual("-1");
    });
});

describe("Test for mensaje.getFecha", ()=>{
    
    it("should return a date", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setFecha = "-1";
        var nuevaFecha = mensaje.getFecha;
        expect("-1").toEqual(nuevaFecha);
    });
});

describe("Test for mensaje.setHora", ()=>{
    
    it("should return an hour", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setHora = "-1";
        expect(mensaje.getHora).toEqual("-1");
    });
});

describe("Test for mensaje.getHora", ()=>{
    
    it("should return an hour", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setHora = "-1";
        var nuevaHora = mensaje.getHora;
        expect("-1").toEqual(nuevaHora);
    });
});

describe("Test for mensaje.setModificado", ()=>{
    
    it("should return an update", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setModificado = true;
        expect(mensaje.getModificado).toEqual(true);
    });
});

describe("Test for mensaje.getModificado", ()=>{
    
    it("should return an update", ()=>{
        let mensaje = new Mensaje("", "", "", 0,"","",false);
        mensaje.setModificado = true;
        var nuevoModificado = mensaje.getModificado;
        expect(true).toEqual(nuevoModificado);
    });
});
})

//****************************************************************************************************//
//*********************************PRUEBAS UNITARIAS DE CHAT PROVIDER*********************************//
//****************************************************************************************************//

/**
 * Prueba unitaria del metodo agregarNuevoMensajeAmigo
 */
describe("Test for ChatProvider.agregarNuevoMensajeAmigo", ()=>{
    it("should return a key instead of false", ()=>{

        var key : String = "falso";
        let mensaje = new Mensaje("MensajePruebaProviders", "usuarioTest", "usuarioTest2", 0,"","",false);
        let chat : ChatProvider;
        let events : Events;
        events = new Events;
        chat = new ChatProvider(events);
        key = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        chat.obtenerInfoMensajeAmigo("usuarioTest","usuarioTest2",key);
        events.subscribe('nuevoMensajeAmigo', (infoMensaje) => {
          expect("MensajePruebaProviders").toEqual(infoMensaje.getMensaje);
          })
    });
});

/**
 * Prueba unitaria del metodo eliminarMensajeAmigo
 */
describe("Test for ChatProvider.eliminarMensajeAmigo", ()=>{
    it("should return a key instead of false", ()=>{

        var key : String = "falso";
        let mensaje = new Mensaje("MensajePruebaProviders", "usuarioTest", "usuarioTest2", 0,"","",false);
        let chat : ChatProvider;
        let events : Events;
        events = new Events;
        chat = new ChatProvider(events);
        key = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        chat.eliminarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,key);
        chat.obtenerInfoMensajeAmigo("usuarioTest","usuarioTest2",key);
        events.subscribe('nuevoMensajeAmigo', (infoMensaje) => {
          expect("mensaje eliminado").toEqual(infoMensaje.getMensaje);
          })
    });
});

/**
 * Prueba Unitaria del metodo obtenerMensajesConversacionAmigo
 */
describe("Test for ChatProvider.obtenerMensajesConversacionAmigo", ()=>{
    it("should return a message with hola", ()=>{
        var LosMensajes = [];
        var str : String;
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", 0,"","",false);
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        str = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        chat.obtenerMensajesConversacionAmigo(mensaje.getUsuario, mensaje.getAmigo);
        
        evento.subscribe('nuevoMensajeAmigo', (Mensajes) => {
          LosMensajes = [];
          LosMensajes = Mensajes;
        expect("hola").toEqual(LosMensajes[0].mensaje);
        })
        
    });
});

/**
 * Prueba Unitaria del metodo obtenerMensajesConversacionAmigo
 */
describe("Test for ChatProvider.obtenerMensajesConversacionGrupo", ()=>{
    it("should return a message with hola", ()=>{
        var LosMensajes = [];
        var str : String;
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", -1,"","",false);
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        chat.obtenerMensajesConversacionGrupo(mensaje.getidGrupo);
        
        evento.subscribe('nuevoMensajeGrupo', (Mensajes) => {
          LosMensajes = [];
          LosMensajes = Mensajes;
        expect("hola").toEqual(LosMensajes[0].mensaje);
        })
        
    });
});

/**
 * Prueba Unitaria del metodo obtenerInfoMensajeAmigo
 */
describe("Test for ChatProvider.obtenerInfoMensajeAmigo", ()=>{
    it("should return a message with hola", ()=>{
        var str : String;
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", 0,"","",false);
        mensaje.setId="-1";
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        str = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        chat.obtenerInfoMensajeAmigo(mensaje.getUsuario, mensaje.getAmigo,mensaje.getId);
        
        evento.subscribe('infoMensaje', (Mensajes) => {
        expect("hola").toEqual(Mensajes.getMensaje);
        })
        
    });
});

/**
 * Prueba Unitaria del metodo obtenerInfoMensajeGrupo
 */
describe("Test for ChatProvider.obtenerInfoMensajeGrupo", ()=>{
    it("should return a message with hola", ()=>{
        var str : String;
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", -1,"","",false);
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        chat.obtenerInfoMensajeGrupo(mensaje.getidGrupo,mensaje.getMensaje);
        
        evento.subscribe('infoMensajeGrupo', (Mensajes) => {
        expect("hola").toEqual(Mensajes.getMensaje);
        })
        
    });
});


/**
 * Prueba Unitaria del metodo modificarMensajeAmigo
 */
describe("Test for ChatProvider.modificarMensajeAmigo", ()=>{
    it("should return an key", ()=>{
        var str : String = "falso";
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", 0,"","",false);
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        str = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        chat.modificarMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,str,"adios");
        chat.obtenerInfoMensajeAmigo(mensaje.getUsuario,mensaje.getAmigo,str);
        
        evento.subscribe('nuevoMensajeAmigo', (infoMensaje) => {
        expect("adios").toEqual(infoMensaje.getMensaje);
        })
        
    });
});

/**
 * Prueba Unitaria del metodo modificarMensajeGrupo
 */
describe("Test for ChatProvider.modificarMensajeGrupo", ()=>{
    it("should return a key", ()=>{
        var str : String = "falso";
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", 0,"","",false);
        let chat : ChatProvider;
        let evento : Events;
        evento = new Events;
        chat = new ChatProvider(evento);
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        chat.modificarMensajeGrupo(mensaje.getidGrupo,str,"adios",mensaje.getUsuario);
        chat.obtenerInfoMensajeGrupo(mensaje.getidGrupo,str);
        
        evento.subscribe('infoMensajeGrupo', (infoMensaje) => {
        expect("adios").toEqual(infoMensaje.getMensaje);
        })
        
    });
});

//****************************************************************************************************//
//************************************PRUEBAS UNITARIAS DE COMANDO************************************//
//****************************************************************************************************//

/**
 * Prueba unitaria del metodo execute de ComandoCrearMensaje
 */
describe("Test for ComandoCrearMensaje.execute", ()=>{
    it("should return a entity instead of null", ()=>{
        var key : String;
        let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2", 0,"","",false);
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let comando : ComandoCrearMensaje = new ComandoCrearMensaje;
        let entidad,respuesta : Entidad;
        comando.setEntidad = mensaje;
        comando.execute();
        key = comando.getEntidad.getId;
        chat.obtenerInfoMensajeAmigo("usuarioTest","usuarioTest2",key);
        events.subscribe('nuevoMensajeAmigo', (infoMensaje) => {
          expect("MensajePruebaComando").toEqual(infoMensaje.getMensaje);
          })
    });
});

/**
 * Prueba unitaria del metodo execute de ComandoEliminarMensaje
 */
describe("Test for ComandoEliminarMensaje.execute", ()=>{
    it("should return a entity instead of null", ()=>{
        var key : String;
        let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2", 0,"","",false);
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let comando : ComandoEliminarMensaje = new ComandoEliminarMensaje;
        let entidad : Entidad;
        let respuesta : Boolean = false;
        key = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        mensaje.setId = key;
        comando.setEntidad = mensaje;
        comando.execute();
        respuesta = comando.getRespuesta;
        expect(true).toEqual(respuesta);
    });
});
;

/**
 * Prueba unitaria del metodo execute de ComandoInformacionMensajeAmigo
 */
describe("Test for ComandoInformacionMensajeAmigo.execute", ()=>{
    it("should return a message", ()=>{
        let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2", 0,"","",false);
        mensaje.setId="-1";
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let comando : ComandoInformacionMensajeAmigo = new ComandoInformacionMensajeAmigo;
        events.subscribe('infoMensaje', (infoMensaje) => {
            expect("MensajePruebaComando").toEqual(infoMensaje.getMensaje);
            })
      });
    });

    /**
 * Prueba unitaria del metodo execute de ComandoInformacionMensajeGrupo
 */
describe("Test for ComandoInformacionMensajeGrupo.execute", ()=>{
    it("should return a message", ()=>{
        let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2",-1,"","",false);
        mensaje.setId="-1";
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let comando : ComandoInformacionMensajeGrupo = new ComandoInformacionMensajeGrupo;
        let entidad : Entidad;
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        comando.setEntidad = mensaje;
        comando.setEvents = events;
        comando.execute();
        events.subscribe('infoMensajeGrupo', (infoMensaje) => {
            expect("MensajePruebaComando").toEqual(infoMensaje.getMensaje);
            })
      });
    });

/**
    * Prueba unitaria del metodo execute de ComandoVisualizarConversacionAmigo
    */
    describe("Test for ComandoVisualizarConversacionAmigo.execute", ()=>{
        it("should return a message", ()=>{
            var LosMensajes = [];
            let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2", 0,"","",false);
            let events : Events = new Events;
            let chat : ChatProvider = new ChatProvider(events);
            let comando : ComandoVisualizarConversacionAmigo = new ComandoVisualizarConversacionAmigo;
            let entidad : Entidad;
            chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
            comando.setEntidad = mensaje;
            comando.setEvents = events;
            comando.execute();
            events.subscribe('nuevoMensajeAmigo', (Mensajes) => {
                LosMensajes = [];
                LosMensajes = Mensajes;
                expect("MensajePruebaComando").toEqual(LosMensajes[0].mensaje);
                })
          });
        });


   /**
    * Prueba unitaria del metodo execute de ComandoVisualizarConversacionGrupo
    */
       describe("Test for ComandoVisualizarConversacionGrupo.execute", ()=>{
           it("should return a message", ()=>{
               var LosMensajes = [];
               let mensaje : Mensaje = new Mensaje("MensajePruebaComando", "usuarioTest", "usuarioTest2", -1,"","",false);
               let events : Events = new Events;
               let chat : ChatProvider = new ChatProvider(events);
               let comando : ComandoVisualizarConversacionGrupo = new ComandoVisualizarConversacionGrupo;
               let entidad : Entidad;
               chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
               comando.setEntidad = mensaje;
               comando.setEvents = events;
               comando.execute();
               events.subscribe('nuevoMensajeGrupo', (Mensajes) => {
                   LosMensajes = [];
                   LosMensajes = Mensajes;
                   expect("MensajePruebaComando").toEqual(LosMensajes[0].mensaje);
                   })
             });
           });
   


//****************************************************************************************************//
//**************************************PRUEBAS UNITARIAS DE DAO**************************************//
//****************************************************************************************************//

/**
 * Prueba unitaria del metodo agregar de DAOChat
 */
describe("Test for DAO.agregar", ()=>{
    it("should return a entity instead of null", ()=>{

        var key : String;
        let mensaje = new Mensaje("MensajePruebaDAO", "usuarioTest", "usuarioTest2", 0,"","",false);
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let DAO : DAO = new DAOChat;
        let entidad,respuesta : Entidad;
        respuesta = DAO.agregar(mensaje);
        chat.obtenerInfoMensajeAmigo("usuarioTest","usuarioTest2",respuesta.getId);
        events.subscribe('nuevoMensajeAmigo', (infoMensaje) => {
          expect("MensajePruebaDAO").toEqual(infoMensaje.getMensaje);
          })
    });
});

/**
 * Prueba unitaria del metodo VisualizarLista de DAOChat
 */
describe("Test for DAO.visulizarLista", ()=>{
    it("should return an X", ()=>{
        var LosMensajes = [];
        let mensaje = new Mensaje("MensajePruebaDAO", "usuarioTest", "usuarioTest2", 0,"","",false);
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let DAO : DAOChat = new DAOChat;
        chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
        DAO.visualizarLista(mensaje, events); 

        events.subscribe('nuevoMensajeAmigo', (Mensajes) => {
            LosMensajes=[];
            LosMensajes = Mensajes;
          expect("MensajePruebaDAO").toEqual(LosMensajes[0].mensaje);
          })
    });
});

/**
 * Prueba unitaria del metodo VisualizarListaGrupo de DAOChat
 */
describe("Test for DAO.visulizarListaGrupo", ()=>{
    it("should return an X", ()=>{
        var LosMensajes = [];
        let mensaje = new Mensaje("MensajePruebaDAO", "usuarioTest", "usuarioTest2", -1,"","",false);
        let events : Events = new Events;
        let chat : ChatProvider = new ChatProvider(events);
        let DAO : DAOChat = new DAOChat;
        chat.agregarNuevoMensajeGrupo(mensaje.getMensaje,mensaje.getidGrupo,mensaje.getUsuario);
        DAO.visualizarListaGrupo(mensaje, events);

        events.subscribe('nuevoMensajeGrupo', (Mensajes) => {
            LosMensajes=[];
            LosMensajes = Mensajes;
          expect("MensajePruebaDAO").toEqual(LosMensajes[0].mensaje);
          })
    });
});
