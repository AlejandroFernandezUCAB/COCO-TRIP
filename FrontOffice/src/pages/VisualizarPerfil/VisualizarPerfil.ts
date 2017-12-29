import { Component } from '@angular/core';
import { NavController, AlertController , LoadingController, NavParams } from 'ionic-angular';
import { TranslateModule, TranslateService } from '@ngx-translate/core'
import { Texto } from '../constantes/texto';
import { Comando } from '../../businessLayer/commands/comando';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { ConfiguracionImages } from '../constantes/configImages';

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
  templateUrl: 'VisualizarPerfil.html'
})

export class VisualizarPerfilPage 
{
  /*Atributo que almacena datos*/
  public amigo : any; //Usuario

  /*Texto en la vista*/
  public mensajeCargando : string;

  private comando : Comando;

  public constructor
  (
    public navCtrl : NavController,
    public alerCtrl : AlertController,
    public loadingCtrl : LoadingController, 
    private navParams : NavParams,
    private translateService : TranslateService
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

    this.loading.dismiss();
   }
   
}
