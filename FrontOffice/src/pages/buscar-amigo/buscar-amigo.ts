import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams,Platform, ActionSheetController,AlertController } from 'ionic-angular';
import { VisualizarPerfilPublicoPage } from '../visualizarperfilpublico/visualizarperfilpublico';
import { Storage } from '@ionic/storage';
import { ComandoBuscarAmigo } from '../../businessLayer/commands/comandoBuscarAmigo';

//****************************************************************************************************// 
//***********************************PAGE BUSCAR AMIGOS MODULO 3**************************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Buscador de amigos
 */
@IonicPage()
@Component
({
  selector: 'page-buscar-amigo',
  templateUrl: 'buscar-amigo.html',
})

export class BuscarAmigoPage 
{
  /*Condicionales de la vista*/
  public toggled : boolean;
  public showList: boolean;
  public showBar : boolean;

  /*Atributos que almacenan datos*/
  public lista : any; //Lista de personas

  public constructor
  (
    public navCtrl: NavController, 
    public navParams: NavParams,
    public platform: Platform,
    public actionsheetCtrl: ActionSheetController,
    public alerCtrl: AlertController,
    private storage: Storage,
    private comandoBuscarAmigo: ComandoBuscarAmigo
  ) { }

  /**
   * Pone en false la lista y en true 
   * el showBar cuando pasas a otro page
   */
  public ionViewWillEnter() 
  {
    this.showBar = true;
    this.showList = false;
  }

  /**
   * Metodo que busca a un usuario
   * @param evento un evento
   */
  public buscar(evento)
  {
    this.showList = true;
    this.storage.get('id').then((idUsuario) =>
    {
      let dato;

      if(evento.target.value)
      {
        dato = evento.target.value;
      } 

      this.comandoBuscarAmigo.Nombre = dato;
      this.comandoBuscarAmigo.Id = idUsuario;
      this.comandoBuscarAmigo.execute();

      if(this.comandoBuscarAmigo.isSuccess)
      {
        this.lista = this.comandoBuscarAmigo.return();
      }
    });    
  }
  

 /**
  * Metodo que inicia la pagina de ver el perfil publico
  * @param nombre Nombre de usuario (resultado del buscador)
  */
  public visualizarPublico (nombre)
  {
        this.navCtrl.push(VisualizarPerfilPublicoPage,
        {
          nombreUsuario : nombre
        });
        this.showBar = false;
  }
}
