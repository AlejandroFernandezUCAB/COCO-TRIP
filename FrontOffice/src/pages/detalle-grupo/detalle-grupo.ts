import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionImages } from '../constantes/configImages';
import { ComandoVerPerfilGrupo } from '../../businessLayer/commands/comandoVerPerfilGrupo';
import { ComandoListaMiembroGrupo } from '../../businessLayer/commands/comandoListaMiembroGrupo';

//****************************************************************************************************// 
//********************************PAGE DETALLE DE UN GRUPO MODULO 3***********************************//
//****************************************************************************************************//  

/**
 * Autores:
 * Joaquin Camacho
 * Jose Herrera
 * Sabina Quiroga
 */

/**
 * Descripcion de la clase:
 * Carga el detalle de un grupo
 */
@IonicPage()
@Component
({
  selector: 'page-detalle-grupo',
  templateUrl: 'detalle-grupo.html',
})

export class DetalleGrupoPage 
{
  /*Atributos que almacenan datos*/
  public grupo : any; //Datos del grupo
  public miembro : any; //Lista de usuarios

  public constructor
  (
    public navCtrl: NavController,
    public navParams: NavParams,
    private translateService: TranslateService,
    private comandoVerPerfilGrupo: ComandoVerPerfilGrupo,
    private comandoListaMiembroGrupo: ComandoListaMiembroGrupo
  ) { }
  
  /**
   * Carga los atributos del grupo, nombre y foto
   */
  public ionViewWillEnter() 
  {
    this.comandoVerPerfilGrupo.Id = this.navParams.get('idGrupo');
    this.comandoVerPerfilGrupo.execute();

    if(this.comandoVerPerfilGrupo.isSuccess)
    {
      let grupo = this.comandoVerPerfilGrupo.return();

      if(grupo.RutaFoto == undefined)
      {
        grupo.RutaFoto = ConfiguracionImages.DEFAULT_GROUP_PATH;
      }
      else
      {
        grupo.RutaFoto = ConfiguracionImages.PATH + grupo.RutaFoto;
      }
      
      let listaGrupo = new Array();
      listaGrupo.push(grupo);

      this.grupo = listaGrupo;

      this.cargarMiembros(this.navParams.get('idGrupo'));
    }
  }

  /**
   * Carga los integrantes del grupo
   * @param id Identificador del grupo
   */
  public cargarMiembros (id) 
  {
    this.comandoListaMiembroGrupo.Id = id;
    this.comandoListaMiembroGrupo.execute();

    if(this.comandoListaMiembroGrupo.isSuccess)
    {
      this.miembro = this.comandoListaMiembroGrupo.return();

      for(let i = 0; i < this.miembro.length; i++)
      {
         if(this.miembro[i].Foto == undefined)
         {
           this.miembro[i].Foto = ConfiguracionImages.DEFAULT_USER_PATH;
         }
         else
         {
           this.miembro[i].Foto = ConfiguracionImages.PATH + this.miembro[i].Foto;
         }
      }
    }
  }
  
}