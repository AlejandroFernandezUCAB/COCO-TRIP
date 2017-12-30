import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { NuevosIntegrantesPage } from '../nuevos-integrantes/nuevos-integrantes';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { ConfiguracionImages } from '../constantes/configImages';
import { ComandoVerPerfilGrupo } from '../../businessLayer/commands/comandoVerPerfilGrupo';
import { ComandoObtenerLider } from '../../businessLayer/commands/comandoObtenerLider';
import { ComandoObtenerSinLider } from '../../businessLayer/commands/comandoObtenerSinLider';
import { ComandoEliminarIntegrante } from '../../businessLayer/commands/comandoEliminarIntegrante';
import { ComandoModificarGrupo } from '../../businessLayer/commands/comandoModificarGrupo';
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

  public constructor
  (
    public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    public alerCtrl: AlertController,
    public toastCtrl: ToastController,
    private navParams: NavParams,
    private storage: Storage,
    private translateService: TranslateService,
    private comandoVerPerfilGrupo: ComandoVerPerfilGrupo,
    private comandoObtenerLider: ComandoObtenerLider,
    private comandoObtenerSinLider: ComandoObtenerSinLider,
    private comandoEliminarIntegrante: ComandoEliminarIntegrante,
    private comandoModificarGrupo: ComandoModificarGrupo

  ) {}

  public loading = this.loadingCtrl.create({});
    
/**
 * Carga la vista del grupo apenas entras a la pagina 
 * solo los datos del grupo 
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

        this.cargarLider(this.navParams.get('idGrupo'));
      }
      else
      {
        this.realizarToast(Texto.ERROR);
      }
  }

/**
 * Carga los datos del lider
 * @param id Iedntificador del grupo
 */    
  public cargarLider(id)
  {
    this.comandoObtenerLider.Id = id;
    this.comandoObtenerLider.execute();

    if(this.comandoObtenerLider.isSuccess)
    {
      let lider = this.comandoObtenerLider.return();

      if(lider.Foto == undefined)
      {
        lider.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
      }
      else
      {
        lider.Foto = ConfiguracionImages.PATH + lider.Foto;
      }

      let listaLider = new Array();
      listaLider.push(lider);

      this.lider = listaLider;

      this.cargarMiembros(id);
    }
    else
    {
      this.realizarToast(Texto.ERROR);
    }
  }

/**
 * Carga la lista de los integrantes del grupo (sin incluir al lider)
 * @param id identificador del grupo
 */
  public cargarMiembros(id)
  {
    this.comandoObtenerSinLider.Id = id;
    this.comandoObtenerSinLider.execute();

    if(this.comandoObtenerSinLider.isSuccess)
    {
      this.miembro = this.comandoObtenerSinLider.return();

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
    else
    {
      this.realizarToast(Texto.ERROR);
    }
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
              this.comandoEliminarIntegrante.execute();

              if(this.comandoEliminarIntegrante.isSuccess)
              {
                this.eliminarIntegrante(nombreUsuario, index);
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
   * Eliminar en pantalla
   * @param nombreUsuario Nombre del usuario a eliminar
   * @param index Posicion en la lista
   */    
  public eliminarIntegrante(nombreUsuario, index)
  {
    //this.miembro.filter(item => item.NombreUsuario === nombreUsuario)[0];
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
        if(this.nombreGrupo == undefined)
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
          }
          else
          {
            this.realizarToast(Texto.ERROR);
          }          
        } 
        else 
        {
          this.comandoModificarGrupo.IdUsuario = idUsuario;
          this.comandoModificarGrupo.IdGrupo = this.navParams.get('idGrupo');
          this.comandoModificarGrupo.Nombre = this.nombreGrupo;
          this.comandoModificarGrupo.execute();
          
          if(this.comandoModificarGrupo.isSuccess)
          {
            this.realizarToast(this.edited);
          }
          else
          {
            this.realizarToast(Texto.SUBTITULO_ALERTA_INTEGRANTE);
          }
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