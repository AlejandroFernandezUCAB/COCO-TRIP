import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,AlertController,LoadingController,ToastController } from 'ionic-angular';
import { GruposPage } from '../amistades-grupos/grupos/grupos';
import { Storage } from '@ionic/storage';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';
import { CrearGrupoPage } from '../crear-grupo/crear-grupo';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { Comando } from '../../businessLayer/commands/comando';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';

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
  public nombreGrupo: string;
  public requerido: string;
  public succesful: string;

  /*Elementos de la vista**/
  public toast: any;
  public loader: any;

  private comando : Comando;

  public constructor
  (
    public navCtrl: NavController, 
    public navParams: NavParams,
    public alerCtrl: AlertController,
    public loadingCtrl: LoadingController,
    public toastCtrl: ToastController,
    public myForm: FormGroup,
    public formBuilder: FormBuilder,
    private storage: Storage,
    private translateService: TranslateService
  ) 
  {
    this.myForm = this.formBuilder.group
    ({
      namegroup: ['', [Validators.required]]
    });
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
        this.comando = FabricaComando.crearComandoAgregarGrupo(idUsuario, this.nombreGrupo);
        this.comando.execute();

        if(this.comando.isSuccess)
        {
          this.comando = FabricaComando.crearComandoObtenerUltimoGrupo(idUsuario);
          this.comando.execute();

          if(this.comando.isSuccess)
          {
            this.navCtrl.push(CrearGrupoPage,
            {
              idGrupo: this.comando.return()
            });

            this.realizarToast(this.succesful);
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
