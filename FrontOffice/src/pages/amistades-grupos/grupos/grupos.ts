import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';
import{ModificarGrupoPage} from '../../modificar-grupo/modificar-grupo';

@Component({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})
export class GruposPage {
  delete= false;
  edit= false;
  detail=false;
  Grupo=[];
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alertCtrl: AlertController) {
      
  
      
    this.Grupo=
    [
     {
 
         id:1, img: 'https://pbs.twimg.com/media/DLty-BkUIAE9LRJ.jpg', nombre: 'Informatica'
 
     },
 
     {
 
         id:2, img: 'https://pbs.twimg.com/media/DL9S8ZhUIAE_jZt.jpg', nombre: 'Desarrollo'
 
     }
 
 ]
 
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

    this.navCtrl.push(ModificarGrupoPage);
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
          //this.eliminarItinerario(id, index);
          }
        }
      ]
    });
    alert.present();
  }

  verDetalleGrupo() {
    this.navCtrl.push(DetalleGrupoPage);
  
  }
  
}
