import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import{SeleccionarIntegrantesPage} from '../seleccionar-integrantes/seleccionar-integrantes';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
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
    public restapiService: RestapiService, private translateService: TranslateService) {
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


  

}
