import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, LoadingController, ToastController, AlertController} from 'ionic-angular';
import { ModificarGrupoPage } from '../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { ComandoObtenerMiembrosSinGrupo } from '../../businessLayer/commands/comandoObtenerMiembrosSinGrupo';
import { ComandoAgregarIntegrante } from '../../businessLayer/commands/comandoAgregarIntegrante';

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
  
  public constructor 
  (
    public navCtrl: NavController,
    public navParams: NavParams,
    public loadingCtrl: LoadingController,
    private storage: Storage,
    private toastCtrl: ToastController,
    private alertCtrl: AlertController,
    private translateService: TranslateService,
    private comandoObtenerMiembrosSinGrupo: ComandoObtenerMiembrosSinGrupo,
    private comandoAgregarIntegrante: ComandoAgregarIntegrante
  ) {}

  public loading = this.loadingCtrl.create({});

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
        
        this.comandoObtenerMiembrosSinGrupo.IdUsuario = idUsuario;
        this.comandoObtenerMiembrosSinGrupo.IdGrupo = this.navParams.get('idGrupo');

        this.comandoObtenerMiembrosSinGrupo.execute()
        .then((resultado) => 
        {
          if(resultado)
          {
            this.amigo = this.comandoObtenerMiembrosSinGrupo.return();
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
  public agregarIntegrantes(evento, nombreUsuario, index)
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
            this.comandoAgregarIntegrante.IdGrupo = this.navParams.get('idGrupo');
            this.comandoAgregarIntegrante.NombreUsuario = nombreUsuario;

            this.comandoAgregarIntegrante.execute()
            .then((resultado) => 
            {
              if(resultado)
              {
                this.amigo.splice(index, 1);
                this.realizarToast(this.succesful);
              }
              else
              {
                this.realizarToast(Texto.ERROR);
              }
            })
            .catch(() => this.realizarToast(Texto.ERROR));
          }
        }
      ]
    });
      alert.present();
 }
}
