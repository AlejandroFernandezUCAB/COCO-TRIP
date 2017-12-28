import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController, ToastController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConversacionPage } from '../../chat/conversacion/conversacion';
import { Texto } from '../../constantes/texto';
import { ConfiguracionToast } from '../../constantes/configToast';
import { Comando } from '../../../businessLayer/commands/comando';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';
import { ConfiguracionImages } from '../../constantes/configImages';

//****************************************************************************************************//
//*************************************PAGE DE AMIGOS MODULO 3****************************************//
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
 * Floating button para eliminar amigos, agregar amigos y ver perfil
 */
@Component
({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})

export class AmigosPage 
{
  /*Condicionales de la vista*/
  public delete : boolean = false; 
  public edit : boolean = false;
  public detail : boolean = false; 
  public chat : boolean = false;
  
  /*Atributos que almacenan datos*/
  public amigo : any; //Es un arreglo de usuarios

  /*Texto a mostrar en la vista*/
  public title : string;
  public accept : string;
  public cancel : string;
  public text : string;
  public message : string;
  public succesful : string;

  /*Elementos de la vista*/
  public toast : any;
  public loader : any;

  public navCtrl : NavController;
  public platform : Platform
  public actionsheetCtrl : ActionSheetController;
  public alerCtrl : AlertController;
  public loadingCtrl : LoadingController;
  public toastCtrl : ToastController; 
  private storage : Storage;
  private translateService : TranslateService;

  public loading = this.loadingCtrl.create({});

  private comando : Comando; //Superclase comando

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
    this.translateService.get(Texto.CARGANDO)
    .subscribe(value => {this.loader = value;})
    this.loading = this.loadingCtrl
    .create
    ({
      content: this.loader,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

/**
 * Metodo que carga la lista de amigos automaticamente
 * al entrar a la vista
 */
   public ionViewWillEnter() 
   {
     this.cargando();
     this.storage.get('id')
     .then(idUsuario => 
     {
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

       this.loading.dismiss();
     });
  }

/**
 * Metodo que coloca los textos de las cartas
 * en false e inicia la pagina de buscar amigos
 * para agregarlo
 */
  public agregarAmigo()
  {
    this.edit = false;
    this.detail = false;
    this.delete = false;
    this.chat = false;

    this.navCtrl.push(BuscarAmigoPage);
  }

/**
 * Metodo que coloca los textos de las cartas en false
 */
  public verChat()
  {
    this.edit = false;
    this.detail = false;
    this.delete = false;
    
    if (this.chat == false)
    {
      this.chat = true;
    }
    else
    {
      this.chat=false;
    }
 }

/**
 * Metodo que coloca los textos de las cartas en false
 */
  public eliminar()
  {
    this.edit = false;
    this.detail = false;
    this.chat = false;

    if (this.delete == false)
    {
      this.delete = true;
    }
    else
    {
      this.delete = false;
    }
  }

/**
 * Metodo que coloca los textos de las cartas en false
 */
  public perfil()
  {
    this.delete = false;
    this.edit = false;
    this.chat = false;

    if(this.detail == false)
    {
      this.detail = true;
    }
    else
    {
      this.detail = false;
    }
  }

/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del amigo a eliminar
 * @param index posicion de la lista
 */
  public eliminarAmigo (nombreUsuario, index) 
  {
    this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
    this.translateService.get(Texto.MENSAJE_ELIMINAR_AMIGO).subscribe(value => {this.message = value;})
    this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
    this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
    this.translateService.get(Texto.EXITO_ELIMINAR_AMIGO).subscribe(value => {this.succesful = value;})

    const alert = this.alerCtrl.create(
    {
      title: this.title,
      message: '¿' + this.message + nombreUsuario + '?',
      buttons: 
      [
        {
          text: this.cancel,
          role: 'cancel',
          handler: () => { }
        },
        {
          text: this.accept,
          handler: () => 
          {
            this.storage.get('id').then((idUsuario) => 
            {
              this.comando = FabricaComando.crearComandoEliminarAmigo(nombreUsuario, idUsuario);
              this.comando.execute();

              if(this.comando.isSuccess)
              {
                this.eliminarAmigos(nombreUsuario, index);
                this.realizarToast(this.succesful);

                this.delete = false;
              }
              else
              {
                this.realizarToast(Texto.ERROR);
              }
            });
          }
        }
      ]
    });
    alert.present();
  }

/**
 * Metodo para borrar desde pantalla
 * @param nombreUsuario Nombre del amigo a eliminar
 * @param index Posicion de la lista
 */
  public eliminarAmigos(nombreUsuario, index)
  {
    //this.amigo.filter(item => item.nombreUsuario == nombreUsuario)[8];
    this.amigo.splice(index, 1);
  }

/**
 * Metodo para ingresar a la pagina de visualizar
 * el perfil de un amigo
 * @param item Nombre del usuario seleccionado
 */
  public verPerfil (item) 
  {
    this.navCtrl.push(VisualizarPerfilPage,
    {
        nombreUsuario : item
    });
  }

/**
 * Metodo que inicia un chat 
 * @param item Nombre del usuario seleccionado
 */
  public chatAmigo (item) 
  {
    this.navCtrl.push(ConversacionPage,
    {
        nombreUsuario : item
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
  
}
