import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

/**
 * Generated class for the DetalleGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-detalle-grupo',
  templateUrl: 'detalle-grupo.html',
})
export class DetalleGrupoPage {

  Grupo=[];
  Amigo=[];
  constructor(public navCtrl: NavController, public navParams: NavParams) {
    this.Grupo=
    [
     {
 
         img: 'https://pbs.twimg.com/media/DL9S8ZhUIAE_jZt.jpg',nombre: 'Desarrollo'
 
     }
 
     
 
 ]
 this.Amigo=
 [
  {

      img: 'https://pbs.twimg.com/profile_images/920719751843909633/NLNA_kQu_400x400.jpg', nombre: 'Mariangel Perez'

  },
  {
    
       img: 'https://pbs.twimg.com/profile_images/501872189436866560/IR71NKjR_400x400.jpeg', nombre: 'Oswaldo Lopez'
    
  },

  {

      img: 'https://scontent-mia3-2.xx.fbcdn.net/v/t1.0-9/15055703_10210361491814247_7941784320471131940_n.jpg?oh=de1951a0057f57fde8ac45593b5fd6e8&oe=5A740E18', nombre: 'Aquiles Pulido'

  }

]

 
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad DetalleGrupoPage');
  }

}
