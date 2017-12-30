import { Component } from '@angular/core';
import { NavController, NavParams, AlertController, LoadingController, ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateModule , TranslateService  } from '@ngx-translate/core'
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { ComandoObtenerPerfilPublico } from '../../businessLayer/commands/comandoObtenerPerfilPublico';
import { ComandoAgregarAmigo } from '../../businessLayer/commands/comandoAgregarAmigo';
import { ComandoEnviarCorreo } from '../../businessLayer/commands/comandoEnviarCorreo';

//****************************************************************************************************// 
//***************************PAGE DE VISUALIZAR PERFIL PUBLICO MODULO 3*******************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga el perfil publico de un usuario
 */

@Component
({
  selector: 'page-visualizarperfilpublico',
  templateUrl: 'visualizarperfilpublico.html',
})

export class VisualizarPerfilPublicoPage 
{
  /*Atributos que almacenan datos*/
  public amigo : any; //Datos del usuario

  /*Texto en la vista*/
  public tituloAlert : string;
  public siAlert : string;
  public mensajeAlert : string;
  public mensajeCargando : string;
  public mensajeToast : string;

  /*Elementos de la vista*/
  public toast: any;
  
  public constructor
  ( 
    public navCtrl: NavController,
    public navParams: NavParams,
    public alerCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController, 
    private storage: Storage,
    private translate : TranslateModule,
    private translateService : TranslateService,
    private comandoObtenerPerfilPublico : ComandoObtenerPerfilPublico,
    private comandoAgregarAmigo : ComandoAgregarAmigo,
    private comandoEnviarCorreo : ComandoEnviarCorreo
  ) { }

  public loading = this.loadingCtrl.create
  ({
    content: 'Please wait...'
  });

/**
 * Metodo que despliega un toast
 * @param mensaje Texto para el toast
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
    this.translateService.get(Texto.CARGANDO).subscribe
    (
      value => 
      {
        // value es nuestro texto traducido
         this.mensajeCargando = value;
      }
    )

    this.loading = this.loadingCtrl.create
    ({
      content: this.mensajeCargando,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

  /**
   * Metodo para cargar la lista de amigos
   */
  public ionViewWillEnter() 
  {
    this.cargando();
    
    this.comandoObtenerPerfilPublico.NombreUsuario = this.navParams.get('nombreUsuario');
    this.comandoObtenerPerfilPublico.execute();

    if(this.comandoObtenerPerfilPublico.isSuccess)
    {
      this.amigo = this.comandoObtenerPerfilPublico.return();
    }
    else
    {
      this.realizarToast(Texto.ERROR);
    }

    this.loading.dismiss();
  }

/**
 * Metodo que genera una solicitud de peticion de amistad y el envio de un correo
 * notificando al usuario que recibe la peticion
 * @param item Datos del usuario a agregar
 */
  public agregarAmigo (item) 
  {
    this.cargando();

    this.storage.get('id').then((idUsuario) => 
    {
      this.comandoAgregarAmigo.Id = idUsuario;
      this.comandoAgregarAmigo.NombreUsuario = item.NombreUsuario;
      this.comandoAgregarAmigo.execute();

      if(this.comandoAgregarAmigo.isSuccess)
      {
        this.realizarToast(Texto.EXITO_CONFIRMAR);

        this.comandoEnviarCorreo.IdUsuario = idUsuario;
        this.comandoEnviarCorreo.NombreUsuario = item.NombreUsuario;
        this.comandoEnviarCorreo.Correo = item.Correo;
        this.comandoEnviarCorreo.execute();
  
        if(this.comandoEnviarCorreo.isSuccess)
        {
          this.realizarToast(Texto.EXITO_CORREO);
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }
      }
      else
      {
        this.realizarToast(Texto.ERROR);
      }

      this.loading.dismiss();
      this.navCtrl.pop();
    });
  }

/**
 * Metodo para agregar un usuario
 * @param item Datos del usuario a agregar
 */
  public doConfirm (item) 
  {
    this.translateService.get(Texto.TITULO_CONFIRMAR).subscribe(value => {this.tituloAlert = value;})
    this.translateService.get(Texto.MENSJAE_CONFIRMAR).subscribe(value => {this.mensajeAlert = value;})
    this.translateService.get(Texto.SI_CONFIRMAR).subscribe(value => {this.siAlert = value;})

    let confirm = this.alerCtrl.create
    ({
        title: this.tituloAlert,
        message: this.mensajeAlert,
        buttons: 
        [
          {
            text: 'No',
            handler: () => 
            {
              console.log('No');
            }
          },
          {
            text: this.siAlert,
            handler: () => 
            {
              this.agregarAmigo(item);
              console.log('Si');
            }
          }
        ]
      });
      confirm.present()
  }
}
