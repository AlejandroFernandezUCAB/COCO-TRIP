import { Component } from '@angular/core';
import { NavController, NavParams, AlertController, LoadingController, ToastController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateModule , TranslateService  } from '@ngx-translate/core'
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { Comando } from '../../businessLayer/commands/comando';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { ConfiguracionImages } from '../constantes/configImages';

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
  public navCtrl: NavController;
  public navParams: NavParams;
  public alerCtrl: AlertController;
  public loadingCtrl: LoadingController;
  public toastCtrl: ToastController; 
  private storage: Storage; 
  private translate : TranslateModule;
  private translateService : TranslateService;

  public loading = this.loadingCtrl.create
  ({
    content: 'Please wait...'
  });

  private comando : Comando;
  
  constructor( ) { }

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

    this.comando = FabricaComando.crearComandoObtenerPerfilPublico(this.navParams.get('nombreUsuario'));
    this.comando.execute();

    if(this.comando.isSuccess)
    {
      let amigo = this.comando.return();
      let listaAmigos = new Array();

      if(amigo.Foto == undefined)
      {
        amigo.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
      }
      else
      {
        amigo.Foto = ConfiguracionImages.PATH + amigo.Foto;
      }

      listaAmigos.push(amigo);
      this.amigo = listaAmigos;
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
      this.comando = FabricaComando.crearComandoAgregarAmigo(idUsuario, item.NombreUsuario);
      this.comando.execute();

      if(this.comando.isSuccess)
      {
        this.realizarToast(Texto.EXITO_CONFIRMAR);
      }
      else
      {
        this.realizarToast(Texto.ERROR);
      }

      this.comando = FabricaComando.crearComandoEnviarCorreo(idUsuario, item.NombreUsuario, item.Correo)
      this.comando.execute();

      if(this.comando.isSuccess)
      {
        this.realizarToast(Texto.EXITO_CORREO);
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
