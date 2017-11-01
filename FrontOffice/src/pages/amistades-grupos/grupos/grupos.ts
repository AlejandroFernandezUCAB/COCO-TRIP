import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';

@Component({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})
export class GruposPage {
  delete= false;
  edit= false;
  detail=false;
  Grupo=[];
  its= Array();
  noIts = false;
  
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

  eliminarItinerario(id, index){
    let eliminado = this.its.filter(item => item.id === id)[0];
    var removed_elements = this.its.splice(index, 1);
    if (this.its.length == 0){
      this.noIts = true;
      console.log("no its")
    }
  }
  

  pressEvent() {
    let actionSheet = this.actionsheetCtrl.create({
      title: '¿Que deseas hacer?',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Ver Detalle',
          icon: !this.platform.is('ios') ? 'eye' : null,
          handler: () => {
           this.navCtrl.push(DetalleGrupoPage);
            //console.log('Detalle clicked');
          }
        },{
          text: 'Modificar Grupo',
          icon: !this.platform.is('ios') ? 'create' : null,
          handler: () => {
            console.log('Modificar clicked');
          }
        },
        {
          text: 'Eliminar Grupo',
          role: 'destructive',
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            let confirm = this.alertCtrl.create({
              title: 'Eliminar Grupo',
              message: '¿Seguro que deseas eliminar a este grupo?',
              buttons: [
                {
                  text: 'No',
                  handler: () => {
                    console.log('Disagree clicked');
                  }
                },
                {
                  text: 'Si',
                  handler: () => {
                    console.log('Agree clicked');
                  }
                }
              ]
            });
            confirm.present()
          }
        },
        
        {
          text: 'Cancelar',
          role: 'cancel', // will always sort to be on the bottom
          icon: !this.platform.is('ios') ? 'close' : null,
          handler: () => {
            console.log('Cancel clicked');
          }
        }
      ]
    });
    actionSheet.present();
  }

}
