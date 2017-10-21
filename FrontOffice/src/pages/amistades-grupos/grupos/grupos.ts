import { Component } from '@angular/core';
import { NavController,Platform, ActionSheetController, AlertController} from 'ionic-angular';
import{CrearGrupoPage} from '../../crear-grupo/crear-grupo';
import{DetalleGrupoPage} from '../../detalle-grupo/detalle-grupo';

@Component({
  selector: 'page-grupos',
  templateUrl: 'grupos.html'
})
export class GruposPage {
  
  Grupo=[];
  
    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController) {
      
  
      
    this.Grupo=
    [
     {
 
         img: 'https://pbs.twimg.com/media/DLty-BkUIAE9LRJ.jpg', nombre: 'Informatica'
 
     },
 
     {
 
         img: 'https://pbs.twimg.com/media/DL9S8ZhUIAE_jZt.jpg', nombre: 'Desarrollo'
 
     }
 
 ]
 
   }
  
   

  CrearGrupo(){

    this.navCtrl.push(CrearGrupoPage);
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
            let confirm = this.alerCtrl.create({
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
