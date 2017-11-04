import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import{SeleccionarIntegrantesPage} from '../seleccionar-integrantes/seleccionar-integrantes';
/**
 * Generated class for the CrearGrupoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-crear-grupo',
  templateUrl: 'crear-grupo.html',
})
export class CrearGrupoPage {

  Amigo=[];
  toggled: boolean;
  searchTerm: String = '';
  items: string[];

  constructor(public navCtrl: NavController, public navParams: NavParams) {
   this.toggled = false;  
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

  toggleSearch() {
    this.toggled = this.toggled ? false : true;
}

triggerInput( ev: any ) {
  // Reset items back to all of the items
  // set val to the value of the searchbar
  let val = ev.target.value;
  // if the value is an empty string don't filter the items
  if (val && val.trim() != '') {
    this.items = this.items.filter((item) => {
      return (item.toLowerCase().indexOf(val.toLowerCase()) > -1);
    })
  }  
}

  ionViewDidLoad() {
    console.log('ionViewDidLoad GrupoPage');
  }

  CrearGrupo(){
    
       this.navCtrl.push(SeleccionarIntegrantesPage);
      }

}
