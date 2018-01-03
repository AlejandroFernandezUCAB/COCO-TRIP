import { Component } from '@angular/core';
import { NavController , ToastController, LoadingController, Platform, ActionSheetController, AlertController} from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { Storage } from '@ionic/storage';
import { Texto } from '../../constantes/texto';
import { ConfiguracionToast } from '../../constantes/configToast';
import { ComandoListaNotificaciones } from '../../../businessLayer/commands/comandoListaNotificaciones';
import { ComandoAceptarNotificacion } from '../../../businessLayer/commands/comandoAceptarNotificacion';
import { ComandoRechazarNotificacion } from '../../../businessLayer/commands/comandoRechazarNotificacion';

//****************************************************************************************************// 
//***********************************PAGE DE SOLICITUDES MODULO 3*************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de solicitudes de amistad
 */
@Component
({
  selector: 'page-notificaciones',
  templateUrl: 'notificaciones.html'
})

export class NotificacionesPage 
{
  /*Atributos que almacenan datos*/
  public notificaciones : any; //Arreglo de usuarios

  /*Elementos de la vista*/
  public toast: any;
  public loader: any;

  public constructor
  (
    public navCtrl : NavController,
    public loadingCtrl : LoadingController,
    public toastCtrl : ToastController,
    private storage : Storage,
    private translateService : TranslateService,
    private comandoListaNotificaciones : ComandoListaNotificaciones,
    private comandoAceptarNotificacion : ComandoAceptarNotificacion,
    private comandoRechazarNotificacion : ComandoRechazarNotificacion,
  ) { }

  public loading = this.loadingCtrl.create({});

  public onLink(url: string) 
  {
    window.open(url);
  }

/**
 * Metodo que despliega un toast
 * @param mensaje Texto del toast
 */
public realizarToast(mensaje : string) 
{
  let mensajeTraducido;

  this.translateService.get(mensaje).subscribe(value => {mensajeTraducido = value;})

  this.toast = this.toastCtrl.create(
  {
    message: mensajeTraducido,
    duration: ConfiguracionToast.DURACION,
    position: ConfiguracionToast.POSICION
  });
  this.toast.present();
}

/**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
  public cargando()
  {
    this.translateService.get(Texto.CARGANDO).subscribe(value => {this.loader = value;})
    this.loading = this.loadingCtrl.create
    ({
      content: this.loader,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

/**
 * Metodo que carga la lista de solicitudes automaticamente
 */
  public ionViewWillEnter() 
  {
    this.cargando();
    this.storage.get('id').then((idUsuario) => 
    {
      this.comandoListaNotificaciones.Id = idUsuario;

      this.comandoListaNotificaciones.execute()
      .then((resultado) => 
      {
        if(resultado)
        {
          this.notificaciones = this.comandoListaNotificaciones.return();
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }
      })
      .catch(() => this.realizarToast(Texto.ERROR));

      this.loading.dismiss();
    });
  }

/**
 * Metodo para aceptar la solicitud de un amigo
 * @param nombreUsuarioAceptado Nombre del usuario que fue aceptado
 * @param index Posicion de la lista
 */
  public aceptarAmigo(nombreUsuarioAceptado, index)
  {
    this.storage.get('id').then((idUsuario) => 
    {
      this.comandoAceptarNotificacion.NombreUsuario = nombreUsuarioAceptado; 
      this.comandoAceptarNotificacion.Id = idUsuario;

      this.comandoAceptarNotificacion.execute()
      .then((resultado) => 
      {
        if(resultado)
        {
          this.realizarToast(Texto.ACEPTAR_PETICION);
          this.eliminarNotificacionVisual(nombreUsuarioAceptado, index);
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }
      })
      .catch(() => this.realizarToast(Texto.ERROR));
    });
  }

/**
 * Metodo para rechazar una solicitud
 * @param nombreUsuarioRechazado Nombre del usuario que fue rechazado
 * @param index Posicion en la lista
 */
  public rechazarAmigo(nombreUsuarioRechazado, index)
  {
    this.storage.get('id').then((idUsuario) => 
    {
      this.comandoRechazarNotificacion.NombreUsuario = nombreUsuarioRechazado;
      this.comandoRechazarNotificacion.Id = idUsuario;

      this.comandoRechazarNotificacion.execute()
      .then((resultado) =>
      {
        if(resultado)
        {
          this.realizarToast(Texto.RECHAZAR_PETICION);
          this.eliminarNotificacionVisual(nombreUsuarioRechazado, index);
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }
      })
      .catch(() => this.realizarToast(Texto.ERROR));
    });
  }

/**
 * Metodo para borrar desde pantalla
 * @param nombreUsuario nombre del usuario a quitar de la lista de notificaciones
 * @param index posicion en la lista
 */
  public eliminarNotificacionVisual(nombreUsuario, index)
  {
    //this.notificaciones.filter(item => item.NombreUsuario === nombreUsuario)[8];
    this.notificaciones.splice(index, 1);
  }
  
}