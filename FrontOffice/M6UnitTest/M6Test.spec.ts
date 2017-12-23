import { ConversacionPage } from '../src/pages/chat/conversacion/conversacion'
import { Mensaje } from '../src/dataAccessLayer/domain/mensaje'
import { ChatProvider } from '../src/providers/chat/chat';
import { config } from '../src/app/app.firebaseconfig';
import { NgZone } from '@angular/core';
import { Events } from 'ionic-angular';
import firebase from 'firebase';
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

//***********************************PRUEBAS UNITARIAS DEL DOMINIO************************************//
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


//*********************************PRUEBAS UNITARIAS DE CHAT PROVIDER*********************************//

/**
 * CASO DE FALLO:
 * Prueba unitaria del metodo agregarNuevoMensajeAmigo
 */
describe("Test for agregarNuevoMensajeAmigo", ()=>{
    it("should return a key instead of false", ()=>{

        var str : String = "falso";
        let mensaje = new Mensaje("hola", "usuarioTest", "usuarioTest2", 0, "","", false);
        let chat : ChatProvider;
        let events : Events;
        events = new Events;
        chat = new ChatProvider(events);
        str = chat.agregarNuevoMensajeAmigo(mensaje.getMensaje,mensaje.getUsuario,mensaje.getAmigo);
          expect("falso").not.toEqual(str);
    });
});

/**
 * Prueba Unitaria del metodo obtenerMensajesConversacionAmigo
 */
describe("Test for obtenerMensajesConversacionAmigo", ()=>{
    it("should return a hola", ()=>{
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

