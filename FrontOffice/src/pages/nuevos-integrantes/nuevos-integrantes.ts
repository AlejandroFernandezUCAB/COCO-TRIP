import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController, AlertController } from 'ionic-angular';
import { ModificarGrupoPage } from '../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { Comando } from '../../businessLayer/commands/comando';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { ConfiguracionImages } from '../constantes/configImages';

//****************************************************************************************************// 
//****************************PAGE AGREGAR INTEGRANTES AL MODIFICAR MODULO 3**************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de amigos que no estan agregados 
 * en ese grupo
 */
@IonicPage()
@Component
({
  selector: 'page-nuevos-integrantes',
  templateUrl: 'nuevos-integrantes.html',
})

export class NuevosIntegrantesPage 
{
  /*Atributos que almacenan datos*/
  public amigo: any; //Lista de usuarios

  /*Texto de la vista*/
  public title: string;
  public accept: string;
  public cancel: string;
  public text: string;
  public message: string;
  public succesful: string;

  /*Elementos de la vista*/
  public toast: any;
  public loader: any;
  public navCtrl: NavController;
  public navParams: NavParams;
  public loadingCtrl: LoadingController;
  private storage: Storage; 
  private toastCtrl: ToastController;
  private alertCtrl: AlertController;
  private translateService: TranslateService;

  public loading = this.loadingCtrl.create({});

  private comando : Comando;
  
  constructor () {}

  public onLink(url: string) 
  {
    window.open(url);
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
 * Metodo que carga la lista de amigos que
 * no estan agregados al grupo
 */
  public ionViewWillEnter() 
  {
      this.cargando();

      this.storage.get('id').then((idUsuario) => 
      {
        console.log('El id del usuario es: ', idUsuario);
        
        this.comando = FabricaComando.crearComandoObtenerMiembrosSinGrupo(idUsuario, this.navParams.get('idGrupo'));
        this.comando.execute();

        if(this.comando.isSuccess)
        {
          this.amigo = this.comando.return();

          for(let i = 0; i < this.amigo.length; i++)
          {
             if(this.amigo[i].Foto == undefined)
             {
               this.amigo[i].Foto = ConfiguracionImages.DEFAULT_USER_PATH;
             }
             else
             {
               this.amigo[i].Foto = ConfiguracionImages.PATH + this.amigo[i].Foto;
             }
          }
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }

        this.loading.dismiss();
      });
  }

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
 * Metodo que agrega a los integrantes al grupo
 * @param evento evento
 * @param nombreUsuario Nombre del usuario a agregar
 */
  public agregarIntegrantes(evento, nombreUsuario)
  {
    this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
    this.translateService.get(Texto.MENSAJE_AGREGAR_INTEGRANTE).subscribe(value => {this.message = value;})
    this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
    this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
    this.translateService.get(Texto.EXITO_AGREGAR_INTEGRANTE).subscribe(value => {this.succesful = value;})
    const alert = this.alertCtrl.create
    ({
      title: this.title,
      message: '¿'+this.message+nombreUsuario+'?',
      buttons: 
      [
        {
          text: this.cancel,
          role: 'cancel',
          handler: () => {}
        },
        {
          text: this.accept,
          handler: () => 
          {
            this.comando = FabricaComando.crearComandoAgregarIntegrante(this.navParams.get('idGrupo'), nombreUsuario);
            this.comando.execute();

            if(this.comando.isSuccess)
            {
              this.realizarToast(this.succesful);
            }
            else
            {
              this.realizarToast(Texto.ERROR);
            }
          }
        }
      ]
    });
      alert.present();
 }
}
