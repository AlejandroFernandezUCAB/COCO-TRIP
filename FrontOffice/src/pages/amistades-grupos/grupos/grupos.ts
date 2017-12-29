import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController, LoadingController, ToastController} from 'ionic-angular';
import { CrearGrupoPage } from '../../crear-grupo/crear-grupo';
import { SeleccionarIntegrantesPage } from '../../seleccionar-integrantes/seleccionar-integrantes';
import { DetalleGrupoPage } from '../../detalle-grupo/detalle-grupo';
import { ModificarGrupoPage } from '../../modificar-grupo/modificar-grupo';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConversacionGrupoPage } from '../../chat/conversacion-grupo/conversacion-grupo';
import { Texto } from '../../constantes/texto';
import { ConfiguracionToast } from '../../constantes/configToast';
import { FabricaComando } from '../../../businessLayer/factory/fabricaComando';
import { Comando } from '../../../businessLayer/commands/comando';
import { ConfiguracionImages } from '../../constantes/configImages';

//****************************************************************************************************// 
//*************************************PAGE DE GRUPOS MODULO 3****************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la lista de grupos de un usuario
 * Floating button para eliminar grupos, agregar grupos,
 * ver detalle del grupo y modificar grupo
 */

@Component
({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})

export class GruposPage 
{
  /*Condicionales de la vista*/
  public delete : boolean = false;
  public edit : boolean = false;
  public detail : boolean = false;
  public chat : boolean = false;

  /*Atributos que almacenan datos*/
  public grupo : any; //Arreglo de grupos

  /*Texto a mostrar en la vista*/
  public noEdit : string;
  public subtitle : string;
  public ok : string;
  public title : string;
  public accept : string;
  public cancel : string;
  public text : string;
  public message : string;
  public succesful : string;

  /*Elementos de la vista*/
  public toast : any;
  public loader : any;

  private comando : Comando;
  
  public constructor
  (
    public navCtrl : NavController,
    public platform : Platform,
    public actionsheetCtrl : ActionSheetController,
    public alertCtrl : AlertController,
    public loadingCtrl : LoadingController,
    public toastCtrl : ToastController,
    private storage : Storage,
    private translateService : TranslateService
  ) { }

  public loading = this.loadingCtrl.create({});

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
   * Metodo que carga la lista de grupos automaticamente
   * al entrar a la vista
   */
   public ionViewWillEnter() 
   {
      this.cargando();
      this.storage.get('id').then((idUsuario) => 
      {
        this.comando = FabricaComando.crearComandoListaGrupos(idUsuario);
        this.comando.execute();
 
        if(this.comando.isSuccess)
        {
          this.grupo = this.comando.return();

          for(let i = 0; i < this.grupo.length; i++)
          {
             if(this.grupo[i].RutaFoto == undefined)
             {
               this.grupo[i].RutaFoto = ConfiguracionImages.DEFAULT_GROUP_PATH;
             }
             else
             {
               this.grupo[i].RutaFoto = ConfiguracionImages.PATH + this.grupo[i].RutaFoto;
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
   * en false e inicia la pagina de crear grupo
   */
  public crearGrupo()
  {
    this.edit = false;
    this.detail = false;
    this.delete = false;

    this.navCtrl.push(SeleccionarIntegrantesPage);
  }

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
      this.chat = false;
    }  
  }
  
  public chatGrupo (IdGrupo, NombreGupo) 
  {
    this.navCtrl.push(ConversacionGrupoPage ,
    {
        idGrupo : IdGrupo,
        nombreGrupo : NombreGupo
    });
  }

  /**
   * Metodo que coloca los textos de las cartas en false
   * (Cuando dice eliminar grupo)
   */
  public eliminar()
  {
    this.edit = false;
    this.detail = false;

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
   * (Cuando dice modificar grupo)
   */
  public editar()
  {
    this.delete = false;
    this.detail = false;

    if (this.edit == false)
    {
      this.edit = true;
    }
    else
    {
      this.edit = false;
    }
  }

/**
 *Metodo que coloca los textos de las cartas en false
 (Cuando dice ver detalle del grupo) 
 */
  public detallegrupo()
  {
    this.delete = false;
    this.edit = false;
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
   * Metodo que verifica si un usario es lider o solo integrante del grupo
   * Si es lider inicia la pagina de modificar grupo, si no, envia alert
   * @param id Identificador del usuario
   * @param index Posicion en la lista
   */
  public modificarGrupo (id, index) 
  {
    this.edit = false;
    this.detail = false;
    this.delete = false;
    
    this.storage.get('id').then((idUsuario) => 
    {
      this.comando = FabricaComando.crearComandoVerificarLider(id, idUsuario);
      this.comando.execute();

      if(this.comando.isSuccess)
      {
        this.navCtrl.push(ModificarGrupoPage,
        {
          idGrupo: id
        });
      }
      else
      {
        this.alertaIntegrante();
      }
    }); 
  } 

/**
 * Alert que explica que el usuario no es lider del grupo
 */
  public alertaIntegrante() 
  {
    this.translateService.get(Texto.NO_EDITAR_ALERTA_INTEGRANTE).subscribe(value => {this.noEdit = value;})
    this.translateService.get(Texto.SUBTITULO_ALERTA_INTEGRANTE).subscribe(value => {this.subtitle = value;})
    this.translateService.get(Texto.OK_ALERTA_INTEGRANTE).subscribe(value => {this.ok = value;})
    let alert = this.alertCtrl.create
    ({
      title: this.noEdit,
      subTitle: this.subtitle,
      buttons: [this.ok]
    });
    alert.present();
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
   * Metodo que coloca un alert para confirmar que el grupo se desea eliminar
   * Verifica si es lider o solo integrante para eliminar el grupo o solo salir de el
   * @param id Identificador del grupo
   * @param index Posicion de la lista
   */
  public eliminarGrupo(id, index) 
  {
    this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
    this.translateService.get(Texto.MENSAJE_ELIMINAR_GRUPO).subscribe(value => {this.message = value;})
    this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
    this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
    this.translateService.get(Texto.EXITO_ELIMINAR_GRUPO).subscribe(value => {this.succesful = value;})
    const alert = this.alertCtrl.create
    ({
      title: this.title,
      message:this.message,
      buttons: [
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
                console.log('El id del usuario es ' + idUsuario);

                this.comando = FabricaComando.crearComandoSalirGrupo(id, idUsuario);
                this.comando.execute();
          
                if(this.comando.isSuccess)
                {
                  this.realizarToast(this.succesful);
                  this.eliminarGrupos(id, index);
                }
                else
                {
                  this.realizarToast(Texto.ERROR);
                }

                  this.delete = false;
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
 * @param index 
 */
  public eliminarGrupos(id, index)
  {
    //this.grupo.filter(item => item.Id === id)[0];
    this.grupo.splice(index, 1);
  }

/**
 * Metodo para iniciar la pagina del detalle del grupo
 * @param id Identificador del grupo
 * @param index Posicion de la lista
 */
  public verDetalleGrupo(id, index) 
  {
    this.navCtrl.push(DetalleGrupoPage,
    {
      idGrupo: id
    });
  }
}