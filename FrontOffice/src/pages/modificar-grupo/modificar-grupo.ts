import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import{NuevosIntegrantesPage} from '../nuevos-integrantes/nuevos-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { AlertController, LoadingController } from 'ionic-angular';


@Component({
  selector: 'modificar-grupo-page',
  templateUrl: 'modificar-grupo.html',
})
export class ModificarGrupoPage {
  grupo: any;
  miembro: any;
  id: any;
  public loading = this.loadingCtrl.create({
    content: 'Please wait...'
  });
    constructor(public navCtrl: NavController, private navParams: NavParams,
      public restapiService: RestapiService, public loadingCtrl: LoadingController,
      public alerCtrl: AlertController) {
          
    }

    ionViewWillEnter() {
      this.id = this.navParams.get('idGrupo');
      this.restapiService.verperfilGrupo(this.id)
        .then(data => {
          if (data == 0 || data == -1) {
            console.log("DIO ERROR PORQUE ENTRO EN EL IF");
  
          }
          else {
            this.grupo = data;
            this.cargarmiembros(this.id);
          }
  
        });
    }

    cargarmiembros(id){
      this.restapiService.listamiembroGrupo(id)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");
  
        }
        else {
          this.miembro = data;
        }
  
      });
    }

/**
 * Metodo para confirmar eliminacion de un amigo
 * @param nombreUsuario Nombre del usuario a eliminar
 * @param index 
 */
    eliminarIntegrantes(nombreUsuario, index){
      console.log(nombreUsuario);
      
      const alert = this.alerCtrl.create({
        title: 'Por favor, confirmar',
        message: '¿Deseas borrar a: '+nombreUsuario+'?',
        buttons: [
          {
            text: 'Cancelar',
            role: 'cancel',
            handler: () => {
             
            }
          },
          {
            text: 'Aceptar',
            handler: () => {
              this.eliminarIntegrante(nombreUsuario, index);
              this.restapiService.eliminarIntegrante(nombreUsuario,1);
              
              }
            }
          ]
        });
        alert.present();
      }


  eliminarIntegrante(nombreUsuario, index){
    let eliminado = this.miembro.filter(item => item.NombreUsuario === nombreUsuario)[8];
    var removed_elements = this.miembro.splice(index, 1);
  }

  Integrantes(){
    this.navCtrl.push(NuevosIntegrantesPage);
  }

  }

