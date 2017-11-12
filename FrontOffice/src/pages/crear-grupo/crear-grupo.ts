import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import{SeleccionarIntegrantesPage} from '../seleccionar-integrantes/seleccionar-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
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

  amigo: any;
  toggled: boolean;
  searchTerm: String = '';
  items: string[];

  constructor(public navCtrl: NavController, public navParams: NavParams,
    public restapiService: RestapiService) {
   this.toggled = false;  
   

  }

  ionViewWillEnter() {
    //this.cargando();
     this.restapiService.listaAmigos("1")
       .then(data => {
         if (data == 0 || data == -1) {
           console.log("DIO ERROR PORQUE ENTRO EN EL IF");

         }
         else {
           this.amigo = data;
           //this.loading.dismiss();
         }
 
       });
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
