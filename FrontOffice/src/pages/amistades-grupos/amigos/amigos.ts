import { VisualizarPerfilPage } from '../../VisualizarPerfil/VisualizarPerfil';
import { BuscarAmigoPage } from '../../buscar-amigo/buscar-amigo';
import { Component } from '@angular/core';
import { Platform, ActionSheetController } from 'ionic-angular';
import { NavController } from 'ionic-angular';
import { AlertController } from 'ionic-angular';

@Component({
  selector: 'page-amigos',
  templateUrl: 'amigos.html'
})
export class AmigosPage {
  delete= false;
  edit= false;
  detail=false;
  Grupo=[];
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


  tapEvent() {
  }
    //AQUI SE COLOCAN LAS LLAMADAS PARA ABRIR EL CHAT 


agregarAmigo(){
 this.edit=false;
  this.detail=false;
  this.delete=false;

  this.navCtrl.push(BuscarAmigoPage);
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

perfil(){
  this.delete=false;
  this.edit=false;
  if(this.detail==false){

    this.detail = true;
  }
  else{

    this.detail=false;
  }
  
}



eliminarAmigo(id, index) {
  const alert = this.alerCtrl.create({
  title: 'Por favor, confirmar',
  message: '¿Desea borrar este Amigo?',
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
        //this.eliminarAmigo(id, index);
        }
      }
    ]
  });
  alert.present();
}

verPerfil() {
  this.navCtrl.push(VisualizarPerfilPage);

}
}

interface amigos {
    img: string; //Este es el avatar
    nick_name: string; //El nickname
}