import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';

@Component({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})
export class AmigosPage {
    lista: Array<amigos> = [{ img: 'https://pbs.twimg.com/profile_images/920719751843909633/NLNA_kQu_400x400.jpg', nick_name: 'Mariangel Perez'},
        { img: 'https://pbs.twimg.com/profile_images/501872189436866560/IR71NKjR_400x400.jpeg', nick_name: 'Oswaldo Lopez' },
        { img: 'https://scontent-mia3-2.xx.fbcdn.net/v/t1.0-9/15055703_10210361491814247_7941784320471131940_n.jpg?oh=de1951a0057f57fde8ac45593b5fd6e8&oe=5A740E18', nick_name: 'Aquiles Pulido' },
        { img: 'https://i.pinimg.com/736x/35/8c/57/358c57c204a2fec21fa50b917a0728aa--rainbow-face-rainbow-prism.jpg', nick_name: 'Sr. Bigotes' }];

    constructor(public navCtrl: NavController, public platform: Platform,
      public actionsheetCtrl: ActionSheetController,public alerCtrl: AlertController) {
      
  }
  
  onLink(url: string) {
      window.open(url);
  }

  pressEvent() {
    let actionSheet = this.actionsheetCtrl.create({
      title: '¿Que deseas hacer?',
      cssClass: 'action-sheets-basic-page',
      buttons: [
        {
          text: 'Ver Perfil',
          icon: !this.platform.is('ios') ? 'eye' : null,
          handler: () => {
            console.log('profile clicked');
          }
        },
        {
          text: 'Eliminar de mis Amigos',
          role: 'destructive',
          icon: !this.platform.is('ios') ? 'trash' : null,
          handler: () => {
            let confirm = this.alerCtrl.create({
              title: 'Eliminar Amigo',
              message: '¿Seguro que deseas eliminar a este amigo?',
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

  tapEvent() {
  }
    //AQUI SE COLOCAN LAS LLAMADAS PARA ABRIR EL CHAT 
  }



interface amigos {
    img: string; //Este es el avatar
    nick_name: string; //El nickname
}