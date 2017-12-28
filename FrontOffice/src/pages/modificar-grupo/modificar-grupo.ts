import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';
import { NuevosIntegrantesPage } from '../nuevos-integrantes/nuevos-integrantes';
import { AlertController, LoadingController } from 'ionic-angular';
import { Storage } from '@ionic/storage';
import { TranslateService } from '@ngx-translate/core';
import { ConfiguracionToast } from '../constantes/configToast';
import { Texto } from '../constantes/texto';
import { Comando } from '../../businessLayer/commands/comando';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { ConfiguracionImages } from '../constantes/configImages';
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
  public navCtrl: NavController; 
  public loadingCtrl: LoadingController;
  public alerCtrl: AlertController;
  public toastCtrl: ToastController;
  private navParams: NavParams;
  private storage: Storage; 
  private translateService: TranslateService;

  public loading = this.loadingCtrl.create({});

  private comando : Comando;
  
  constructor() {}
    
/**
 * Carga la vista del grupo apenas entras a la pagina 
 * solo los datos del grupo 
 */
  public ionViewWillEnter() 
  {
      this.comando = FabricaComando.crearComandoVerPerfilGrupo(this.navParams.get('idGrupo'));
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
    this.comando = FabricaComando.crearComandoObtenerLider(id);
    this.comando.execute();

    if(this.comando.isSuccess)
    {
      this.lider = this.comando.return();

      if(this.lider.Foto == undefined)
      {
        this.lider.Foto = ConfiguracionImages.DEFAULT_USER_PATH;
      }
      else
      {
        this.lider.Foto = ConfiguracionImages.PATH + this.lider.Foto;
      }

      this.cargarMiembros(id);
    }
    else
    {
      this.realizarToast(Texto.ERROR);
    }
  }

/**
 * Carga la lista de los integrantes del grupo (Si incluir al lider)
 * @param id identificador del grupo
 */
  public cargarMiembros(id)
  {
    this.comando = FabricaComando.crearComandoObtenerSinLider(id);
    this.comando.execute();

    if(this.comando.isSuccess)
    {
      this.miembro = this.comando.return();

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
              this.comando = FabricaComando.crearComandoEliminarIntegrante
              (this.navParams.get('idGrupo'), nombreUsuario);
              this.comando.execute();

              if(this.comando.isSuccess)
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
    this.miembro.filter(item => item.NombreUsuario === nombreUsuario)[0];
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
          /*this.restapiService.verperfilGrupo(this.id)
          .then(data => {this.grupo = data;});
            var nombreRestApi = this.grupo.filter(item => item.Nombre)[1];
            this.realizarToast(this.edited);*/
          
        } 
        else 
        {
          this.comando = FabricaComando.crearComandoModificarGrupo(this.nombreGrupo, idUsuario, this.navParams.get('idGrupo'));
          this.comando.execute();
          
          if(this.comando.isSuccess)
          {
            this.realizarToast(this.edited);
          }
          else
          {
            this.realizarToast(Texto.ERROR);
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