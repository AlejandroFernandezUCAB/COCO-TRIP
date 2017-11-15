import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the Restapi provider.

  See https://angular.io/docs/ts/latest/guide/dependency-injection.html
  for more info on providers and Angular 2 DI.
*/
@Injectable()
export class RestapiService {
  apiUrl = 'http://192.168.0.104:8091/api';
  data : any;
  userData: any;
  idUser: any;
  constructor(public http: Http) {
  }
  iniciarSesion(usuario,clave)
  {

    if(usuario.includes("@")){
      this.userData={correo : usuario, clave : clave};
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesioncorreo/?datos='+JSON.stringify(this.userData),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          //console.log(this.data);
          resolve(this.data);
        },error=>{
          resolve(-1);

        });
    });
    }
    else
    {
      this.userData={nombreUsuario : usuario, clave : clave};
      return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesionusuario/?datos='+JSON.stringify(this.userData),"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        resolve(-1);
      });
     });

    }
  }

  registrarse(nombreUsuario,correo,nombre,apellido,genero,fechaNacimiento,clave,foto)
  {
      this.userData={nombreUsuario : nombreUsuario,correo: correo,nombre: nombre,apellido: apellido,genero: genero,fechaNacimiento: fechaNacimiento, clave : clave,foto: ""};
      return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/registrarusuario/?datos='+JSON.stringify(this.userData),"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        resolve(-1);

      });
     });
  }

  iniciarSesionFacebook(usuario)
  {
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/iniciarsesionsocial/?datos='+JSON.stringify(usuario),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          resolve(-1);
        });
    });
  }

  recuperarContrasena(correo)
  {
    this.userData={correo: correo};
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M1_Login/CorreoRecuperar/?datos='+JSON.stringify(this.userData),"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          resolve(-1);

        });
    });
  }
ltSegunPreferencias(idUser){
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M1_Login/LugarTuristicoSegunPreferencias/?idUsuario='+JSON.stringify(idUser),"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        resolve(-1);
      });
  });

}

eveSegunPreferencias(idUser){
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M1_Login/EventoSegunPreferencias/?idUsuario='+JSON.stringify(idUser),"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        resolve(-1);
      });
  });



  }


    /**
     * [Modulo 2]
     * Metodo para obtener la lista de preferencias del usuario
     * @param idUsuario Id de usuario
     */

   buscarPreferencias( idUsuario )
   {
      return new Promise( resolve => {
        this.http.get(this.apiUrl+'/M2_PerfilPreferencias/BuscarPreferencias?idUsuario=' + idUsuario,"")
        .map(res => res.json())
        .subscribe(data => {

          this.data = data;
          resolve(this.data);

        }, error=>{

          resolve(0);

        });
      });
   }

   buscarPreferenciasFiltrado( idUsuario, nombrePreferencia)
   {

    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/BuscarCategorias?idUsuario=' + idUsuario
        +'&preferencia=' + nombrePreferencia,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   agregarPreferencias( idUsuario, nombrePreferencia)
   {

    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/AgregarPreferencias?idUsuario=' + idUsuario
        +'&idCategoria=' + nombrePreferencia,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });

   }

   eliminarPreferencias( idUsuario ,nombrePreferencia )
   {

    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/EliminarPreferencias?idUsuario=' + idUsuario
        +'&idCategoria=' + nombrePreferencia,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   modificarDatosUsuario(usuario){
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/ModificarDatosUsuario?nombreUsuario=' +
      usuario.NombreUsuario + "&nombre=" + usuario.Nombre + "&apellido=" + usuario.Apellido +
      "&fechaDeNacimiento=" + usuario.FechaNacimiento + "&genero=" + usuario.Genero ,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   ObtenerDatosUsuario(idUsuario){
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/ObtenerDatosUsuario?idUsuario=' + idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   /**
     * [Modulo 2]
     * Metodo para cambiar la contraseña del usuario
     * @param username user del usuario
     * @param passActual contraseña actual (a cambiar)
     * @param passNueva contraseña nueva
     */

   cambiarPass(username, passActual, passNueva){
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/CambiarPass?username=' + username
       +"&passwordActual=" + passActual +"&passwordNuevo=" +passNueva ,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   /**
     * [Modulo 2]
     * Metodo para borrar al usuario
     * @param username user del usuario
     * @param passAct contraseña del usuario
     */

   borrarUser(username, passwordAct){
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/BorrarUsuario?username=' + username
       +"&password=" + passwordAct ,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

//****************************************************************************************************// 
//*************************************METODOS DEL MODULO 3*******************************************//
//****************************************************************************************************//    
/**
 * [MODULO3]
 * Metodo para obtener la lista de amigos
 * @param usuario Identificador del usuario
 */
  listaAmigos(usuario)
  {

    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/VisualizarListaAmigos/?idusuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para obtener la lista de notificaciones
   * @param usuario Identificador del usuario
   */
  listaNotificaciones(usuario)
  {

    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ObtenerListaNotificaciones/?idusuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

/**
 * [MODULO 3]
 * Metodo para aceptar una solicitud de amistad
 * @param usuarioAceptado Nombre de usuario del usuario aceptado
 * @param my_id Identificador del usuario que acepto la solicitud
 */
  aceptarNotificacion(usuarioAceptado,my_id)
  {

    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M3_AmigosGrupos/AceptarNotificacion/?nombreUsuarioAceptado='+usuarioAceptado+'&idusuario='+my_id,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para rechazar la slicitud de amistad
   * @param usuarioRechazado nombre de usuario del usuario rechazado
   * @param my_id Identificador del usuario
   */
  rechazarNotificacion(usuarioRechazado,my_id)
  {

    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/rechazarNotificacion/?nombreUsuarioRechazado='+usuarioRechazado+'&idusuario='+my_id,"")
      .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

 /**
  * [MODULO 3]
  * Metodo para eliminar un amigo
  * @param amigo Nombre de usuario del amigo
  * @param usuario Identificador del usuario
  */
  eliminarAmigo(amigo, usuario){
    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/EliminarAmigo/?nombreAmigo='
      +amigo+'&idUsuario='+usuario,"")
      .subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err),
        console.log(amigo),
        console.log(usuario)
      });
  });
}

/**
 * [MODULO 3]
 * Metodo para eliminar un grupo
 * @param usuario Identificador del usuario
 * @param idGrupo Identificador del grupo
 */
eliminarGrupo(usuario, idGrupo){
  return new Promise(resolve => {
    this.http.delete(this.apiUrl+'/M3_AmigosGrupos/EliminarGrupo/?idUsuario='+usuario+'&idGrupo='+idGrupo,"")
    .subscribe(res => {
      resolve(res);
    }, (err) => {
      console.log(err),
      console.log(usuario),
      console.log(idGrupo)
    });
});
}

/**
 * [MODULO 3]
 * Metodo para visualizar la lista de grupos
 * @param usuario nombre del usuario
 */
  listaGrupo(usuario)
  {

    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarListaGrupos/?idusuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }

  /**
 * [MODULO 3]
 * Metodo para buscar los amigos
 * @param nombreUsuario nombre del usuario o iniciales
 */

  buscaramigo( nombreUsuario ,my_id)
  {
     return new Promise( resolve => {
       this.http.get(this.apiUrl+'/M3_AmigosGrupos/BuscarAmigo/?nombre=' + nombreUsuario+'&idUsuario='+my_id,"")
       .map(res => res.json())
       .subscribe(data => {

         this.data = data;
         resolve(this.data);

       }, error=>{

         resolve(-1);

       });
     });
  }
/**
 * [MODULO 3]
 * Metodo para visualizar el perfil del grupo
 * @param usuario nombre de usuario
 */
  verperfilGrupo(usuario)
  {

    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarPerfilGrupos/?id='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }



  /**
   * [MODULO 3]
   * Metodo para visualizar la lista de integrantes de un grupo
   * @param usuario nombre de usuario
   */
  listamiembroGrupo(usuario)
  {

    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarMiembroGrupo/?idgrupo='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error")

        });
    });
  }
/**
 * [MODULO 3]
 * Metodo para visualizar el perfil del usuario
 * @param usuario nombre de usuario
 */
  obtenerPerfilPublico(usuario)
  {
    return new Promise(resolve => {
      this.http.get(this.apiUrl+'/M3_AmigosGrupos/VisualizarPerfilAmigo/?nombreUsuario='+usuario,"")
        .map(res => res.json())
        .subscribe(data => {
          this.data = data;
          resolve(this.data);
        },error=>{
          console.log("Ocurrio un error");
        });
    });
  }

/**
 * [MODULO 3]
 * Metodo para agregar el amigo solicitado
 * @param usuario nombre de usuario
 */
agregarAmigo(idUsuario,nombreAmigo) {
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M3_AmigosGrupos/AgregarAmigo/?idUsuario1='+idUsuario+'&nombreUsuario2='+nombreAmigo,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

/**
 * [MODULO 3]
 * Metodo para enviar un correo al usuario para notificar que tiene una
 * solicitud de amistad en espera
 * @param idUsuario Identificador del usuario que esta haciendo la solicitud
 * @param nombreAmigo Nombre del usuario al que se desea agregar
 * @param correoAmigo Correo del usuario al que se desea agregar
 */
enviarCorreo(idUsuario,nombreAmigo,correoAmigo) {  
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M3_AmigosGrupos/EnviarNotificacionCorreo/?nombreUsuarioRecibe='+nombreAmigo
    +'&correoElectronico='+correoAmigo +'&idUsuarioEnvia='+idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
} 

/**
* [MODULO 3] 
* Metodo para agregar el grupo 
* @param idusuario id del usuario logeado
* @param nombre nombre de grupo
* @param foto foto del grupo
*/
agregarGrupo(idusuario,nombre,foto,) {  
 return new Promise(resolve => {
   this.http.put(this.apiUrl+'/M3_AmigosGrupos/AgregarGrupo/?nombre='+nombre+'&foto='+foto+'&idusuario='+idusuario,"")
     .map(res => res.json())
     .subscribe(data => {
       this.data = data;
       resolve(this.data);
     },error=>{
       console.log("Ocurrio un error");
     });
 });
}

/**
 * [MODULO 3]
 * Metodo para salir de un grupo
 * @param usuario identificador del usuario
 * @param idGrupo identificador del grupo
 */
  salirGrupo(usuario, idGrupo){
    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/EliminarSalirGrupo/?idGrupo='+idGrupo+'&idUsuario='+usuario,"")
      .subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err),
        console.log(usuario),
        console.log(idGrupo)
      });
  });
}
  /**
   * [MODULO 3]
   * Metodo para modificar los atributos de un grupo
   * @param nombreGrupo Nombre del grupo
   * @param idUsuario Identificador del usuario lider
   * @param idGrupo identificador del grupo
   */
  modificarGrupo(nombreGrupo, idUsuario, idGrupo){
    return new Promise(resolve => {
      this.http.post(this.apiUrl+'/M3_AmigosGrupos/ModificarGrupo/?nombreGrupo='+nombreGrupo
      +'&idUsuario='+idUsuario+'&idGrupo='+idGrupo,"")
      .subscribe(res => {
          resolve(res);
        }, (err) => {
          console.log(err)
        });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para eliminar un integrante al modificar
   * @param nombreUsuario Nombre del integrante a eliminar
   * @param idGrupo Identificador del grupo
   */
  eliminarIntegrante(nombreUsuario, idGrupo){
    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/EliminarIntegranteModificar/?nombreUsuario='
      +nombreUsuario+'&idGrupo='+idGrupo,"")
      .subscribe(res => {
        resolve(res);
      }, (err) => {
        console.log(err),
        console.log(nombreUsuario),
        console.log(idGrupo)
      });
  });
}

/**
 * [MODULO 3]
 * Metodo para agregar un integrante al grupo al modificar
 * @param idGrupo Identificador del grupo
 * @param nombreAmigo Nombre del amigo a agregar
 */
agregarIntegrante(idGrupo,nombreAmigo) {
  return new Promise(resolve => {
    this.http.put(this.apiUrl+'/M3_AmigosGrupos/AgregarIntegranteModificar/?idGrupo='+idGrupo+'&nombreUsuario='+nombreAmigo,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}
/**
 * [MODULO 3]
 * Metodo para verificar que un usuario es lider
 * @param idGrupo Identificador del grupo
 * @param idUsuario Identificador del usuario
 */
verificarLider(idGrupo, idUsuario) 
{  
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/VerificarLider/?idGrupo='+idGrupo
    +'&idUsuario='+idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

/**
 * [MODULO 3]
 * Metodo para obtener al usuario lider
 * @param idGrupo identificador del grupo
 * @param idUsuario identificador del usuario 
 */
obtenerLider(idGrupo, idUsuario) 
{  
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarLider/?idGrupo='+idGrupo
    +'&idUsuario='+idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

/**
 * [MODULO 3]
 * Metodo que obtiene la lista de integrantes, sin el integrante lider
 * @param idGrupo identificador del grupo
 */
obtenerSinLider(idGrupo) 
{  
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarMiembrosSinLider/?idGrupo='+idGrupo,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

/**
 * [MODULO 3]
 * Metodo que obtiene la lista de integrantes que no estan agregados al grupo
 * @param idUsuario Identificador de usuario
 * @param idGrupo Identificador del grupo
 */
obtenerMiembrosSinGrupo(idUsuario, idGrupo) 
{  
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarMiembrosSinGrupo/?idUsuario='+idUsuario
    +'&idGrupo='+idGrupo,"")
    .map(res => res.json())
    .subscribe(data => {
      this.data = data;
      resolve(this.data);
    },error=>{
      console.log("Ocurrio un error");
    });
});
}

/**
 * Metodo que obtiene el ultimo grupo agregado por un usuario
 * @param idUsuario Identificador del usuario
 */
obtenerultimoGrupo(idUsuario) 
{  
  return new Promise(resolve => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarultimoGrupo/?idUsuario='+idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

//****************************************************************************************************// 
//********************************FIN DE LOS METODOS DEL MODULO 3*************************************//
//****************************************************************************************************//   


}
