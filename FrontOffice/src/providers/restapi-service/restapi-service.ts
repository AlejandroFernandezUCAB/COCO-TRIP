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
  apiUrl = 'http://localhost:51049/api';
  data : any;
  userData: any;
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
      this.userData={nombreUsuario : nombreUsuario,correo: correo,nombre: nombre,apellido: apellido,genero: genero,fechaNacimiento: fechaNacimiento, clave : clave,foto: foto};
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

  buscaramigo( nombreUsuario )
  {
     return new Promise( resolve => {
       this.http.get(this.apiUrl+'/M3_AmigosGrupos/BuscarAmigo/?nombre=' + nombreUsuario,"")
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
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/AgregarAmigo/?idUsuario1='+idUsuario+'&nombreUsuario2='+nombreAmigo,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);
      },error=>{
        console.log("Ocurrio un error");
      });
  });
}

  salirGrupo(usuario, idGrupo){
    return new Promise(resolve => {
      this.http.delete(this.apiUrl+'/M3_AmigosGrupos/SalirGrupo/?idGrupo='+idGrupo+'&idUsuario='+usuario,"")
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
   * @param nombreGrupo 
   * @param idUsuario 
   * @param idGrupo 
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
}