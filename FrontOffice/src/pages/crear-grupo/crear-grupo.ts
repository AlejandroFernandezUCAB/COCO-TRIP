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
//**********************************PAGE AGREGAR INTEGRANTES MODULO 3*********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de amigos de un usuario
 * Para agregarlos a un grupo
 */
@IonicPage()
@Component
({
  selector: 'page-crear-grupo',
  templateUrl: 'crear-grupo.html',
})

export class CrearGrupoPage 
{
  /*Atribusos que almacenan datos*/
  public amigo: any; //Arreglo de usuarios

  /*Texto a mostrar en la vista*/
  public title: string;
  public accept: string;
  public cancel: string;
  public text: string;
  public message: string;
  public succesful: string;

  /*Elementos de la vista*/
  public toast: any;
  public loader: any;

  public loadingCtrl: LoadingController;
  public navCtrl: NavController;
  public navParams: NavParams;
  private storage: Storage; 
  private toastCtrl: ToastController;
  private alertCtrl: AlertController; 
  private translateService: TranslateService;

  public loading = this.loadingCtrl.create({content: 'Please wait...'});

  private comando : Comando;
  
  constructor() { }

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
 * Metodo que carga la lista de amigos apenas se abre la vista
 */
  public ionViewWillEnter() 
  {
    this.cargando();

    this.storage.get('id').then((idUsuario) => 
    {
        console.log('El id del usuario es: ' + idUsuario);
        
        this.comando = FabricaComando.crearComandoListaAmigos(idUsuario);
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
 * Metodo que agrega un usuario de la lista de amigos
 * @param evento evento
 * @param nombreUsuario Nombre del usuario a ser agregado
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
          handler: () => {
           
          }
        },
        {
          text: this.accept,
          handler: () => 
          {
            this.comando = FabricaComando.crearComandoAgregarIntegrante
            (this.navParams.get('idGrupo'), nombreUsuario);
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

 /**
  * Metodo que hace pop a las page anteriores y devuelve a la pagina inicial
  * @param evento evento
  */
  public finalizar (event)
  {
    this.translateService.get(Texto.GRUPO_EXITOSO).subscribe(value => {this.succesful = value;})
    this.realizarToast(this.succesful);
    this.navCtrl.popToRoot();
  }
  
}
