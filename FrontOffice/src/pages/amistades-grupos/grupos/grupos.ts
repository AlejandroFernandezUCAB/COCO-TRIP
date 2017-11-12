import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';
import{ModificarGrupoPage} from '../../modificar-grupo/modificar-grupo';
import { RestapiService } from '../../../providers/restapi-service/restapi-service';

@Component({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})
export class GruposPage {
  delete= false;
  edit= false;
  detail=false;
  grupo:any;
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alertCtrl: AlertController,
      public restapiService: RestapiService) {
 
   }
  
   ionViewWillEnter() {
    this.restapiService.listaGrupo(1)
      .then(data => {
        if (data == 0 || data == -1) {
          console.log("DIO ERROR PORQUE ENTRO EN EL IF");

        }
        else {
          this.grupo = data;
        }

      });
  }

  crearGrupo(){
    this.edit=false;
    this.detail=false;
    this.delete=false;

    this.navCtrl.push(CrearGrupoPage);
  }

  eliminar(){
    this.edit=false;
    this.detail=false;

    if (this.delete==false){

      this.delete = true;
    }
    else{
      this.delete=false;
    }
    
  }

  editar(){
    this.delete=false;
    this.detail=false;

    if (this.edit==false){

      this.edit = true;
    }
    else{
      this.edit=false;
    }
    
  }


  detallegrupo(){
    this.delete=false;
    this.edit=false;
    if(this.detail==false){

      this.detail = true;
    }
    else{

      this.detail=false;
    }
    
  }

  modificarGrupo(id, index){
    this.edit=false;
    this.detail=false;
    this.delete=false;
    this.navCtrl.push(ModificarGrupoPage,{
      idGrupo: id
    });
  }

  eliminarGrupo(id, index) {
    const alert = this.alertCtrl.create({
    title: 'Por favor, confirmar',
    message: '¿Desea borrar este Grupo?',
    buttons: [
      {
        text: 'Cancelar',
        role: 'cancel',
        handler: () => {
          //console.log('Cancel clicked');
        }
      },
      {
        text: 'Aceptar',
        handler: () => {
          this.eliminarGrupos(id, index);
          this.restapiService.eliminarGrupo(1,id);
          this.delete = false;
          }
        }
      ]
    });
    alert.present();
  }

/**
 * Metodo para borrar desde pantalla
 * @param nombreUsuario Nombre del amigo a eliminar
 * @param index 
 */
eliminarGrupos(id, index){
  let eliminado = this.grupo.filter(item => item.Id === id)[0];
  var removed_elements = this.grupo.splice(index, 1);
}

  verDetalleGrupo() {
    this.navCtrl.push(DetalleGrupoPage);
  
  }
  
}


