import { Component } from '@angular/core';
import { NavController, AlertController , LoadingController, NavParams } from 'ionic-angular';
import { TranslateModule, TranslateService } from '@ngx-translate/core'
import { Texto } from '../constantes/texto';
import { ComandoObtenerPerfilPublico } from '../../businessLayer/commands/comandoObtenerPerfilPublico';

//****************************************************************************************************// 
//********************************PAGE DE VISUALIZAR PERFIL MODULO 3**********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga el perfil de un amigo
 */
@Component
({
  selector: 'page-visualizarperfil',
  templateUrl: 'visualizarperfil.html'
})

export class VisualizarPerfilPage 
{
  /*Atributo que almacena datos*/
  public amigo : any; //Usuario

  /*Texto en la vista*/
  public mensajeCargando : string;

  public constructor
  (
    public navCtrl : NavController,
    public alerCtrl : AlertController,
    public loadingCtrl : LoadingController, 
    private navParams : NavParams,
    private translateService : TranslateService,
    private comandoObtenerPerfilPublico : ComandoObtenerPerfilPublico
  ) {}

  public loading = this.loadingCtrl.create
  ({
    content: 'Please wait...'
  });

/**
 * Metodo que carga un loading controller al iniciar 
 * la lista de amigos
 * (Por favor espere/ please wait)
 */
  public cargando()
  {
    this.translateService.get(Texto.CARGANDO).subscribe(value => {this.mensajeCargando = value;})
    this.loading = this.loadingCtrl.create
    ({
      content: this.mensajeCargando,
      dismissOnPageChange: true
    });
    this.loading.present();
  }

/**
 * Metodo que carga los datos de un amigo para visualizar su perfil
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

    this.loading.dismiss();
   }   
}
