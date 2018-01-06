import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpParams } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { catProd, catService, catErr } from '../../logs/config';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { Entidad } from '../../dataAccessLayer/domain/entidad';
import { Usuario } from '../../dataAccessLayer/domain/usuario';

/*
  Generated class for the Restapi provider.

  See https://angular.io/docs/ts/latest/guide/dependency-injection.html
  for more info on providers and Angular 2 DI.
*/
@Injectable()
export class RestapiService 
{
  public readonly apiUrl : string = 'http://localhost:8082/api';

  private data : any;
  private userData: any;
  private idUser: number;
  
  public constructor (private http: Http) {}

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

   modificarDatosUsuario(entidad: Entidad){
    let usuario = entidad as Usuario;
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/ModificarDatosUsuario?nombreUsuario=' +
      usuario.getNombreUsuario + "&nombre=" + usuario.getNombre + "&apellido=" + usuario.getApellido +
      "&fechaDeNacimiento=" + usuario.getFechaNacimiento + "&genero=" + usuario.getGenero ,"")
      .map(res => res.json())
      .subscribe(data => {

        this.data = data;
        resolve(this.data);

      }, error=>{

        resolve(0);

      });
    });
   }

   ObtenerDatosUsuario(idUsuario): Promise<any>{
    return new Promise( (resolve, reject) => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/ObtenerDatosUsuario?idUsuario=' + idUsuario,"")
      .map(res => res.json())
      .subscribe(data => {
        this.data = data;
        resolve(this.data);

      }, error=>{
        reject(error);

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

   cambiarPass(entidad: Entidad, passActual: string){
     let usuario = entidad as Usuario;
    return new Promise( (resolve) => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/CambiarPass?username=' + usuario.getNombreUsuario
       +"&passwordActual=" + passActual +"&passwordNuevo=" +usuario.getClave ,"")
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

   borrarUser(entidad : Entidad){
     let usuario = entidad as Usuario;
    return new Promise( resolve => {
      this.http.post(this.apiUrl+'/M2_PerfilPreferencias/BorrarUsuario?username=' + usuario.getNombreUsuario
       +"&password=" + usuario.getClave ,"")
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
 * @param idUsuario Identificador del usuario
 */
  public listaAmigos(idUsuario : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/VisualizarListaAmigos?idUsuario=' + idUsuario, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ListaAmigos exitoso. IdUsuario: ' + idUsuario);
        catProd.info('ListaAmigos exitoso. IdUsuario: ' + idUsuario);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de ListaAmigos. IdUsuario: ' + idUsuario);
        catErr.info('Fallo de ListaAmigos. IdUsuario: ' + idUsuario);

        reject(error);
      });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para obtener la lista de notificaciones
   * @param id Identificador del usuario
   */
  public listaNotificaciones(id : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/ObtenerListaNotificaciones?id=' + id, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ListaNotificaciones exitoso. Id: ' + id);
        catProd.info('ListaNotificaciones exitoso. Id: ' + id);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de ListaNotificaciones. Id: ' + id);
        catErr.info('Fallo de ListaNotificaciones. Id: ' + id);

        reject(error);
      });
    });
  }

/**
 * [MODULO 3]
 * Metodo para aceptar una solicitud de amistad
 * @param nombreUsuarioAceptado Nombre de usuario del usuario aceptado
 * @param id Identificador del usuario que acepto la solicitud
 */
  public aceptarNotificacion(nombreUsuarioAceptado : string, id : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.put(this.apiUrl + '/M3_AmigosGrupos/AceptarNotificacion?idUsuario=' + id 
      + '&nombreUsuarioAceptado=' + nombreUsuarioAceptado, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('AceptarNotificacion exitoso. Id: ' + id + ' NombreUsuarioAceptado: ' + nombreUsuarioAceptado);
        catProd.info('AceptarNotificacion exitoso. Id: ' + id + ' NombreUsuarioAceptado: ' + nombreUsuarioAceptado);
        
        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de AceptarNotificacion. Id: ' + id + ' NombreUsuarioAceptado: ' + nombreUsuarioAceptado);
        catErr.info('Fallo de AceptarNotificacion. Id: ' + id + ' NombreUsuarioAceptado: ' + nombreUsuarioAceptado);
        
        reject(error);
      });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para rechazar la solicitud de amistad
   * @param nombreUsuarioRechazado Nombre de usuario del usuario rechazado
   * @param idUsuario Identificador del usuario
   */
  public rechazarNotificacion(nombreUsuarioRechazado : string, idUsuario : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.delete(this.apiUrl + '/M3_AmigosGrupos/RechazarNotificacion?idUsuario=' + idUsuario 
      + '&nombreUsuarioRechazado=' + nombreUsuarioRechazado, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
          console.log('RechazarNotificacion exitoso. Id: ' + idUsuario + ' NombreUsuarioRechazado: ' + nombreUsuarioRechazado);
          catProd.info('RechazarNotificacion exitoso. Id: ' + idUsuario + ' NombreUsuarioRechazado: ' + nombreUsuarioRechazado);
          
          resolve(datos);
      }
      ,error => 
      {
          console.log('Fallo de RechazarNotificacion. Id: ' + idUsuario + ' NombreUsuarioRechazado: ' + nombreUsuarioRechazado);
          catErr.info('Fallo de RechazarNotificacion. Id: ' + idUsuario + ' NombreUsuarioRechazado: ' + nombreUsuarioRechazado);
          
          reject(error);
      });
    });
  }

 /**
  * [MODULO 3]
  * Metodo para eliminar un amigo
  * @param nombreAmigo Nombre de usuario del amigo
  * @param idUsuario Identificador del usuario
  */
  public eliminarAmigo(nombreAmigo : string, idUsuario : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.delete(this.apiUrl + '/M3_AmigosGrupos/EliminarAmigo?idUsuario=' 
      + idUsuario + '&nombreAmigo=' + nombreAmigo, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('EliminarAmigo exitoso. IdUsuario: ' + idUsuario + ' NombreAmigo: ' + nombreAmigo);
        catProd.info('EliminarAmigo exitoso. IdUsuario: ' + idUsuario + ' NombreAmigo: ' + nombreAmigo);
        
        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de EliminarAmigo. IdUsuario: ' + idUsuario + ' NombreAmigo: ' + nombreAmigo);
        catErr.info('Fallo de EliminarAmigo. IdUsuario: ' + idUsuario + ' NombreAmigo: ' + nombreAmigo);
        
        reject(error);
      });
    });
  }

/**
 * [MODULO 3]
 * Metodo para eliminar un grupo
 * @param idUsuario Identificador del usuario
 * @param idGrupo Identificador del grupo
 */
  public eliminarGrupo(idUsuario : number, idGrupo : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.delete(this.apiUrl + '/M3_AmigosGrupos/EliminarGrupo?idUsuario=' + idUsuario + '&idGrupo=' + idGrupo, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('EliminarGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
        catProd.info('EliminarGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
        
        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de EliminarGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
        catProd.info('Fallo de EliminarGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);

        reject(error);
      });
    });
  }

/**
 * [MODULO 3]
 * Metodo para visualizar la lista de grupos
 * @param idUsuario nombre del usuario
 */
  public listaGrupo(idUsuario : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarListaGrupos?idUsuario=' + idUsuario, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ListaGrupo exitoso. IdUsuario: ' + idUsuario);
        catProd.info('ListaGrupo exitoso. IdUsuario: ' + idUsuario);

        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de ListaGrupo. IdUsuario: ' + idUsuario);
        catErr.info('Fallo de ListaGrupo. IdUsuario: ' + idUsuario);

        reject(error);
      });
    });
  }

  /**
 * [MODULO 3]
 * Metodo para buscar usuarios en la aplicacion
 * @param nombre Nombre del usuario o iniciales
 * @param id Identificador del usuario que realiza la busqueda
 */

  public buscarAmigos(nombre : string, id : number)
  {
     return new Promise((resolve, reject) => 
     {
       this.http.get(this.apiUrl + '/M3_AmigosGrupos/BuscarAmigos?id=' + id + '&nombre=' + nombre, "")
       .map(respuesta => respuesta.json())
       .subscribe(datos => 
      {
        console.log('BuscarAmigos exitoso. Id: ' + id + ' Nombre: ' + nombre);
        catProd.info('BuscarAmigos exitoso. Id: ' + id + ' Nombre: ' + nombre);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de BuscarAmigos. Id: ' + id + ' Nombre: ' + nombre);
        catErr.info('Fallo de BuscarAmigos. Id: ' + id + ' Nombre: ' + nombre);

        reject(error);
      });
     });
  }
/**
 * [MODULO 3]
 * Metodo para visualizar el perfil del grupo
 * @param id ID del grupo a buscar
 */
  public verPerfilGrupo(id : number)
  {
    return new Promise ((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarPerfilGrupo?id=' + id, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('VerPerfilGrupo exitoso. Id: ' + id);
        catProd.info('VerPerfilGrupo exitoso. Id: ' + id);
        
        resolve(datos);
      }
      ,error => 
      {
        console.log('Fallo de VerPerfilGrupo. Id: ' + id);
        catErr.info('Fallo de VerPerfilGrupo. Id: ' + id);

        reject(error);
      });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para visualizar la lista de integrantes de un grupo
   * @param id Identificador del grupo
   */
  public listaMiembroGrupo(id : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarMiembroGrupo?idGrupo=' + id, "")
      .map(respuesta => respuesta.json())
        .subscribe(datos => 
        {
          console.log('listaMiembroGrupo exitoso. Id: ' + id);
          catProd.info('listaMiembroGrupo exitoso. Id: ' + id);

          resolve(datos);
        }
        ,error =>
        {
          console.log('Fallo de listaMiembroGrupo. Id: ' + id);
          catErr.info('Fallo de listaMiembroGrupo. Id: ' + id);

          reject(error);
        });
    });
  }
  
/**
 * [MODULO 3]
 * Metodo para visualizar el perfil del usuario
 * @param nombre Nombre de usuario
 */
  public obtenerPerfilPublico(nombre : string)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.get(this.apiUrl + '/M3_AmigosGrupos/VisualizarPerfilAmigo?nombre=' + nombre, "")
      .map(respuesta => respuesta.json())
        .subscribe(datos => 
        {
          console.log('ObtenerPerfilPublico exitoso. Nombre:' + nombre);
          catProd.info('ObtenerPerfilPublico exitoso. Nombre:' + nombre);

          resolve(datos);
        }
        ,error =>
        {
          console.log('Fallo de ObtenerPerfilPublico. Nombre:' + nombre);
          catErr.info('Fallo de ObtenerPerfilPublico. Nombre:' + nombre);

          reject(error);
        });
    });
  }

/**
 * [MODULO 3]
 * Metodo para agregar el amigo solicitado
 * @param id Identificacor unico del usuario
 * @param nombre Nombre de usuario del amigo
 */
  public agregarAmigo(id : number, nombre : string) 
  {
    return new Promise
    ( (resolve, reject) => 
      {
        this.http.post (this.apiUrl + '/M3_AmigosGrupos/AgregarAmigo?id=' + id + '&nombre=' + nombre, "")
        .map(respuesta => respuesta.json())
        .subscribe(datos => 
        {
          console.log('AgregarAmigo exitoso. Id: ' + id + ' Nombre: ' + nombre);
          catProd.info('AgregarAmigo exitoso. Id: ' + id + ' Nombre: ' + nombre);

          resolve(datos);
        }
        , error => 
        {
          console.log('Fallo de AgregarAmigo. Id: ' + id + ' Nombre: ' + nombre);
          catErr.info('Fallo de AgregarAmigo. Id: ' + id + ' Nombre: ' + nombre);

          reject(error);
        });
      }
    );
  }

/**
 * [MODULO 3]
 * Metodo que envia un correo a un usuario para notificar que tiene una
 * solicitud de amistad en espera
 * @param id Identificador del usuario que esta realizando la solicitud
 * @param nombre Nombre del usuario al que se desea agregar
 * @param correo Correo del usuario al que se desea agregar
 */
  public enviarCorreo(id : number, nombre : string, correo : string) 
  {
    return new Promise((resolve, reject) => 
    {
      this.http.post(this.apiUrl+'/M3_AmigosGrupos/EnviarNotificacionCorreo?correo=' + correo 
      + '&id=' + id + '&nombre=' + nombre, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('EnviarCorreo exitoso. Id: ' + id + ' Nombre: ' + nombre + ' Correo: ' + correo);
        catProd.info('EnviarCorreo exitoso. Id: ' + id + ' Nombre: ' + nombre + ' Correo: ' + correo);

        resolve(datos);
      }
      ,error =>
      {
        console.log('Fallo de EnviarCorreo. Id: ' + id + ' Nombre: ' + nombre + ' Correo: ' + correo);
        catErr.info('Fallo de EnviarCorreo. Id: ' + id + ' Nombre: ' + nombre + ' Correo: ' + correo);

        reject(error);
      });
    });
  }

/**
* [MODULO 3]
* Metodo para agregar el grupo
* @param grupo Datos del grupo
*/
  public agregarGrupo(grupo : Entidad) 
  {
    return new Promise((resolve, reject) => 
    { 
      console.log('restapi-service json: ' + JSON.stringify(grupo));
      
      this.http.post(this.apiUrl + '/M3_AmigosGrupos/AgregarGrupo', grupo)
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('AgregarGrupo exitoso. Grupo: ' + grupo);
        catProd.info('AgregarGrupo exitoso. Grupo: ' + grupo);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de AgregarGrupo. Grupo: ' + grupo);
        catErr.info('Fallo de AgregarGrupo. Grupo: ' + grupo);

        reject(error);
      });
    });
  }

/**
 * [MODULO 3]
 * Metodo para salir de un grupo
 * @param idUsuario Identificador del usuario
 * @param idGrupo Identificador del grupo
 */
  public salirGrupo(idUsuario : number, idGrupo : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.delete(this.apiUrl + '/M3_AmigosGrupos/SalirGrupo?idGrupo=' + idGrupo + '&idUsuario=' + idUsuario, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('SalirGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
        catProd.info('SalirGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);

        resolve(datos);
      }, (error) => 
      {
        console.log('Fallo de SalirGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
        catErr.info('Fallo de SalirGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);

        reject(error);
      });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para modificar los atributos de un grupo
   * @param grupo Datos a modificar del grupo
   * @param idUsuario Identificador del usuario lider
   */
  public modificarGrupo(grupo : Entidad, idUsuario : number)
  {
    console.log(JSON.stringify(grupo));

    return new Promise((resolve, reject) => 
    {
      this.http.put(this.apiUrl + '/M3_AmigosGrupos/ModificarGrupo?id=' + idUsuario, grupo)
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ModificarGrupo exitoso. IdUsuario: ' + idUsuario + ' Grupo: ' + grupo);
        catProd.info('ModificarGrupo exitoso. IdUsuario: ' + idUsuario + ' Grupo: ' + grupo);

        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de ModificarGrupo. IdUsuario: ' + idUsuario + ' Grupo: ' + grupo);  
        catProd.info('Fallo de ModificarGrupo. IdUsuario: ' + idUsuario + ' Grupo: ' + grupo);      

        reject(error);
      });
    });
  }

  /**
   * [MODULO 3]
   * Metodo para eliminar un integrante al modificar
   * @param nombreUsuario Nombre del integrante a eliminar
   * @param idGrupo Identificador del grupo
   */
  public eliminarIntegrante(nombreUsuario : string, idGrupo : number)
  {
    return new Promise((resolve, reject) => 
    {
      this.http.delete(this.apiUrl + '/M3_AmigosGrupos/EliminarIntegrante?idGrupo=' + idGrupo + 
      '&nombreUsuario=' + nombreUsuario, "")
      .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('EliminarIntegrante exitoso. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreUsuario);
        catProd.info('EliminarIntegrante exitoso. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreUsuario);
        
        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de EliminarIntegrante. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreUsuario);
        catErr.info('Fallo de EliminarIntegrante. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreUsuario);
        
        reject(error);
      });
    });
  }

/**
 * [MODULO 3]
 * Metodo para agregar un integrante al grupo al modificar
 * @param idGrupo Identificador del grupo
 * @param nombreAmigo Nombre del amigo a agregar
 */
public agregarIntegrante(idGrupo : number, nombreAmigo : string) 
{
  return new Promise((resolve, reject) => 
  {
    this.http.post(this.apiUrl+'/M3_AmigosGrupos/AgregarIntegrante?idGrupo=' + idGrupo + '&nombreUsuario=' + nombreAmigo, "")
    .map(respuesta => respuesta.json())
    .subscribe(datos => 
    {
      console.log('AgregarIntegrante exitoso. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreAmigo);
      catProd.info('AgregarIntegrante exitoso. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreAmigo);
      
      resolve(datos);
    }
    , error =>
    {
      console.log('Fallo de AgregarIntegrante. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreAmigo);
      catErr.info('Fallo de AgregarIntegrante. IdGrupo: ' + idGrupo + ' NombreUsuario: ' + nombreAmigo);
      
      reject(error);
    });
  });
}

/**
 * [MODULO 3]
 * Metodo para verificar que un usuario es lider
 * @param idGrupo Identificador del grupo
 * @param idUsuario Identificador del usuario
 */
public verificarLider(idGrupo : number, idUsuario : number)
{
  return new Promise((resolve, reject) => 
  {
    this.http.get(this.apiUrl + '/M3_AmigosGrupos/VerificarLider?idGrupo=' + idGrupo
    + '&idUsuario=' + idUsuario, "")
    .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('VerificarLider exitoso. IdGrupo: ' + idGrupo + ' IdUsuario: ' + idUsuario);
        catProd.info('VerificarLider exitoso. IdGrupo: ' + idGrupo + ' IdUsuario: ' + idUsuario);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de VerificarLider. IdGrupo: ' + idGrupo + ' IdUsuario: ' + idUsuario);
        catProd.info('Fallo de VerificarLider. IdGrupo: ' + idGrupo + ' IdUsuario: ' + idUsuario);

        reject(error);
      });
  });
}

/**
 * [MODULO 3]
 * Metodo para obtener al usuario lider
 * @param idGrupo Identificador del grupo
 */
public obtenerLider(idGrupo : number)
{
  return new Promise((resolve, reject) => 
  {
    this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarLider?idGrupo=' + idGrupo, "")
    .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ObtenerLider exitoso. IdGrupo: ' + idGrupo);
        catProd.info('ObtenerLider exitoso. IdGrupo: ' + idGrupo);

        resolve(datos);
      }
      , error =>
      {
        console.log('Fallo de ObtenerLider. IdGrupo: ' + idGrupo);
        catErr.info('Fallo de ObtenerLider. IdGrupo: ' + idGrupo);

        reject(error);
      });
  });
}

/**
 * [MODULO 3]
 * Metodo que obtiene la lista de integrantes, sin el integrante lider
 * @param idGrupo identificador del grupo
 */
public obtenerSinLider(idGrupo : number)
{
  return new Promise((resolve, reject) => 
  {
    this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarMiembroSinLider/?idGrupo=' + idGrupo, "")
    .map(respuesta => respuesta.json())
    .subscribe(datos => 
    {
      console.log('ObtenerSinLider exitoso. IdGrupo: ' + idGrupo);
      catProd.info('ObtenerSinLider exitoso. IdGrupo: ' + idGrupo);

      resolve(datos);
    }
    , error => 
    {
      console.log('Fallo de ObtenerSinLider. IdGrupo: ' + idGrupo);
      catErr.info('Fallo de ObtenerSinLider. IdGrupo: ' + idGrupo);

      reject(error);
    });
  });
}

/**
 * [MODULO 3]
 * Metodo que obtiene la lista de integrantes que no estan agregados al grupo
 * @param idUsuario Identificador de usuario
 * @param idGrupo Identificador del grupo
 */
public obtenerMiembrosSinGrupo(idUsuario : number, idGrupo : number)
{
  return new Promise((resolve, reject) => 
  {
    this.http.get(this.apiUrl + '/M3_AmigosGrupos/ConsultarMiembroSinGrupo/?idGrupo=' + idGrupo
    + '&idUsuario=' + idUsuario, "")
    .map(respuesta => respuesta.json())
    .subscribe(datos => 
    {
      console.log('ObtenerMiembrosSinGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
      catProd.info('ObtenerMiembrosSinGrupo exitoso. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);

      resolve(datos);
    }
    , error =>
    {
      console.log('Fallo de ObtenerMiembrosSinGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
      catErr.info('Fallo de ObtenerMiembrosSinGrupo. IdUsuario: ' + idUsuario + ' IdGrupo: ' + idGrupo);
      
      reject(error);
    });
  });
}

/**
 * Metodo que obtiene el ultimo grupo agregado por un usuario
 * @param idUsuario Identificador del usuario
 */
public obtenerUltimoGrupo(idUsuario : number)
{
  return new Promise((resolve, reject) => {
    this.http.get(this.apiUrl+'/M3_AmigosGrupos/ConsultarUltimoGrupo/?idUsuario=' + idUsuario, "")
    .map(respuesta => respuesta.json())
      .subscribe(datos => 
      {
        console.log('ObtenerUltimoGrupo exitoso. IdUsuario: ' + idUsuario);
        catProd.info('ObtenerUltimoGrupo exitoso. IdUsuario: ' + idUsuario);

        resolve(datos);
      }
      , error => 
      {
        console.log('Fallo de ObtenerUltimoGrupo. IdUsuario: ' + idUsuario);
        catErr.info('Fallo de ObtenerUltimoGrupo. IdUsuario: ' + idUsuario);

        reject(error);
      });
  });
}

//****************************************************************************************************//
//********************************FIN DE LOS METODOS DEL MODULO 3*************************************//
//****************************************************************************************************//

}
