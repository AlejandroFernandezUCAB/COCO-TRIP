import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { NuevosIntegrantesPage } from '../nuevos-integrantes/nuevos-integrantes';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { ComandoVerPerfilGrupo } from '../../businessLayer/commands/comandoVerPerfilGrupo';
import { ComandoObtenerLider } from '../../businessLayer/commands/comandoObtenerLider';
import { ComandoObtenerSinLider } from '../../businessLayer/commands/comandoObtenerSinLider';
import { ComandoEliminarIntegrante } from '../../businessLayer/commands/comandoEliminarIntegrante';
import { ComandoModificarGrupo } from '../../businessLayer/commands/comandoModificarGrupo';
import { FormBuilder, Validators } from '@angular/forms';
import { Camera, CameraOptions } from 'ionic-native';
//****************************************************************************************************// 
//**********************************PAGE MODIFICAR GRUPO MODULO 3*************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga los datos de un grupo para modificarlos y eliminar
 * los integrantes de ese grupo
 */
@Component
({
  selector: 'modificar-grupo-page',
  templateUrl: 'modificar-grupo.html',
})

export class ModificarGrupoPage 
{
  /*Atributos que almacenan datos*/
  public grupo : any; //Datos del grupo
  public miembro : any; //Lista de miembros del grupo
  public lider : any; //Datos del lider del grupo

  /*Texto a mostrar en la vista*/
  public nombreGrupo: string; //Nombre del grupo
  public title: string;
  public accept: string;
  public cancel: string;
  public text: string;
  public message: string;
  public succesful: string;
  public edited: string;

  /*Elementos de la vista*/
  public toast :  any;
  public myForm : any;

  private base64Imagen : string; //Foto en Base64
  private camaraOpciones : any; //Opciones de la libreria Camera

  public constructor
  (
    public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    public alerCtrl: AlertController,
    public toastCtrl: ToastController,
    public formBuilder: FormBuilder,
    private navParams: NavParams,
    private storage: Storage,
    private translateService: TranslateService,
    private comandoVerPerfilGrupo: ComandoVerPerfilGrupo,
    private comandoObtenerLider: ComandoObtenerLider,
    private comandoObtenerSinLider: ComandoObtenerSinLider,
    private comandoEliminarIntegrante: ComandoEliminarIntegrante,
    private comandoModificarGrupo: ComandoModificarGrupo

  ) 
  {
    this.myForm = this.formBuilder.group
    ({
      namegroup: ['', [Validators.required, Validators.maxLength(300)]]
    });

    this.camaraOpciones = 
    {
      sourceType: Camera.PictureSourceType.PHOTOLIBRARY,
      destinationType: Camera.DestinationType.DATA_URL,
      quality: 100,
      targetWidth: 8000,
      targetHeight: 8000,
      encodingType: Camera.EncodingType.JPEG,      
      correctOrientation: true
    }
  }

  public loading = this.loadingCtrl.create({});
    
/**
 * Carga la vista del grupo apenas entras a la pagina 
 * solo los datos del grupo 
 */
  public ionViewWillEnter() 
  {
      this.comandoVerPerfilGrupo.Id = this.navParams.get('idGrupo');

      this.comandoVerPerfilGrupo.execute()
      .then((resultado) => 
      {
        if(resultado)
        {
          this.grupo = this.comandoVerPerfilGrupo.return();
          this.nombreGrupo = this.grupo[0].Nombre;

          this.cargarLider(this.navParams.get('idGrupo'));
        }
        else
        {
          this.realizarToast(Texto.ERROR);
        }
      })
      .catch(() => this.realizarToast(Texto.ERROR));
  }

/**
 * Carga los datos del lider
 * @param id Iedntificador del grupo
 */    
  public cargarLider(id)
  {
    this.comandoObtenerLider.Id = id;

    this.comandoObtenerLider.execute()
    .then((resultado) => 
    {
      if(resultado)
      {
        this.lider = this.comandoObtenerLider.return();
        this.cargarMiembros(id);
      }
      else
      {
        this.realizarToast(Texto.ERROR);
      }
    })
    .catch(() => this.realizarToast(Texto.ERROR));
  }

/**
 * Carga la lista de los integrantes del grupo (sin incluir al lider)
 * @param id identificador del grupo
 */
  public cargarMiembros(id)
  {
    this.comandoObtenerSinLider.Id = id;

    this.comandoObtenerSinLider.execute()
    .then((resultado) => 
    {
      if(resultado)
      {
        this.miembro = this.comandoObtenerSinLider.return();
      }
      else
      {
        this.realizarToast(Texto.ERROR);
      }
    })
    .catch(() => this.realizarToast(Texto.ERROR));
  }

/**
 * Metodo que carga una foto desde la galeria de imagenes del celular
 */
public agregarFoto()
{
  Camera.getPicture(this.camaraOpciones)
  .then
  (
    base64 => 
    {
      this.base64Imagen = base64;
    }
    , permisoDenegado => 
    console.log('Acceso denegado')
  )

  Camera.cleanup();
}

/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del usuario a eliminar
 * @param index Posicion en la lista
 */
  public eliminarIntegrantes(nombreUsuario, index)
  {
      this.translateService.get(Texto.TITULO).subscribe(value => {this.title = value;})
      this.translateService.get(Texto.MENSAJE_ELIMINAR_INTEGRANTE).subscribe(value => {this.message = value;})
      this.translateService.get(Texto.CANCELAR).subscribe(value => {this.cancel = value;})
      this.translateService.get(Texto.ACEPTAR).subscribe(value => {this.accept = value;})
      this.translateService.get(Texto.EXITO_ELIMINAR_INTEGRANTE).subscribe(value => {this.succesful = value;})
      
      const alert = this.alerCtrl.create
      ({
        title: this.title,
        message: '¿'+this.message+nombreUsuario+'?',
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
              this.comandoEliminarIntegrante.IdGrupo = this.navParams.get('idGrupo');
              this.comandoEliminarIntegrante.NombreUsuario = nombreUsuario;

              this.comandoEliminarIntegrante.execute()
              .then((resultado) => 
              {
                if(resultado)
                {
                  this.eliminarIntegrante(nombreUsuario, index);
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

  /**
   * Eliminar en pantalla
   * @param nombreUsuario Nombre del usuario a eliminar
   * @param index Posicion en la lista
   */    
  public eliminarIntegrante(nombreUsuario, index)
  {
    this.miembro.splice(index, 1);
  }

/**
 * Metodo que verifica si el nombre del grupo
 * se modifico o no
 * @param evento evento
 */
  public modificarNombre(evento)
  {
      this.translateService.get(Texto.MODIFICAR_EXITOSO).subscribe(value => {this.edited = value;})
      
      this.storage.get('id').then((idUsuario) => 
      {
        if(this.myForm.get('namegroup').errors)
        {
          this.realizarToast(Texto.REQUERIDO);     
        } 
        else 
        {
          this.comandoModificarGrupo.IdUsuario = idUsuario;
          this.comandoModificarGrupo.IdGrupo = this.navParams.get('idGrupo');
          this.comandoModificarGrupo.Nombre = this.nombreGrupo;
          this.comandoModificarGrupo.ContenidoFoto = this.base64Imagen;
          
          this.comandoModificarGrupo.execute()
          .then((resultado) => 
          {
            if(resultado)
            {
              this.comandoVerPerfilGrupo.Id = this.navParams.get('idGrupo');

              this.comandoVerPerfilGrupo.execute()
              .then((resultado) => 
              {
                if(resultado)
                {
                  this.grupo = this.comandoVerPerfilGrupo.return();
                  this.realizarToast(this.edited);
                }
                else
                {
                  this.realizarToast(Texto.ERROR);
                }
              })
              .catch(() => this.realizarToast(Texto.ERROR));
            }
            else
            {
              this.realizarToast(Texto.SUBTITULO_ALERTA_INTEGRANTE);
            }
          })
          .catch(() => this.realizarToast(Texto.ERROR));
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
 * Metodo que inicia la pagina para agregar a integrantes
 */
  Integrantes()
  {
    this.navCtrl.push(NuevosIntegrantesPage, 
    {
      idGrupo: this.navParams.get('idGrupo')
    });
  } 
}