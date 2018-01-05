import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,AlertController,LoadingController,ToastController, ActionSheetController } from 'ionic-angular';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { Storage } from '@ionic/storage';
import { FormBuilder, FormGroup, Validators, NgControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { CrearGrupoPage } from '../crear-grupo/crear-grupo';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { ComandoAgregarGrupo } from '../../businessLayer/commands/comandoAgregarGrupo';
import { ComandoObtenerUltimoGrupo } from '../../businessLayer/commands/comandoObtenerUltimoGrupo';
import { Grupo } from '../../dataAccessLayer/domain/grupo';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { ConfiguracionImages } from '../constantes/configImages';
import { Camera, CameraOptions } from 'ionic-native';

//****************************************************************************************************// 
//***********************************PAGE DATOS DEL GRUPO MODULO 3************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga la pagina para rellenar los datos de un grupo
 */
@IonicPage()
@Component
({
  selector: 'page-seleccionar-integrantes',
  templateUrl: 'seleccionar-integrantes.html',
})

export class SeleccionarIntegrantesPage 
{
  /*Texto de la vista*/
  public grupo : Array<Grupo> //Almacena la ruta de la foto de la imagen
  public nombreGrupo: string;
  public requerido: string;
  public succesful: string;

  /*Elementos de la vista**/
  public toast: any;
  public loader: any;
  public myForm : any;

  private base64Imagen : string;
  private camaraOpciones : any; //Opciones de la libreria Camera

  public constructor
  (
    public navCtrl: NavController, 
    public navParams: NavParams,
    public alerCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    public formBuilder: FormBuilder,
    private storage: Storage,
    private translateService: TranslateService,
    private comandoAgregarGrupo: ComandoAgregarGrupo,
    private comandoObtenerUltimoGrupo: ComandoObtenerUltimoGrupo
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

    this.grupo = new Array<Grupo>();

    let grupo : Grupo = FabricaEntidad.crearGrupo();
    grupo.setRutaFoto = ConfiguracionImages.DEFAULT_GROUP_PATH;

    this.grupo.push(grupo);
  }

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
  }

/**
 * Metodo que agrega el nombre y la foto del grupo
 */
  public agregarGrupo()
  {
    this.translateService.get(Texto.REQUERIDO).subscribe(value => {this.requerido = value;})
    this.translateService.get(Texto.EXITO_AGREGAR_GRUPO).subscribe(value => {this.succesful = value;})
    
    if (this.myForm.get('namegroup').errors)
    {
      this.realizarToast(this.requerido);
    }
    else
    {
      this.cargando();
      
      this.storage.get('id').then((idUsuario) => 
      {
        this.comandoAgregarGrupo.Lider = idUsuario;
        this.comandoAgregarGrupo.Nombre = this.nombreGrupo;
        this.comandoAgregarGrupo.ContenidoFoto = this.base64Imagen;
        
        this.comandoAgregarGrupo.execute()
        .then((resultado) => 
        {
          if(resultado)
          {
            this.comandoObtenerUltimoGrupo.Id = idUsuario;

            this.comandoObtenerUltimoGrupo.execute()
            .then((resultado) => 
            {
              if(resultado)
              {
                this.navCtrl.push(CrearGrupoPage,
                {
                  idGrupo: this.comandoObtenerUltimoGrupo.return().getId
                });
      
                this.realizarToast(this.succesful);
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
            this.realizarToast(Texto.ERROR);
          }
        })
        .catch(() => this.realizarToast(Texto.ERROR));

        this.loading.dismiss();
      });
    }
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
