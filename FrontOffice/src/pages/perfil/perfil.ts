import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

import { EditProfilePage } from '../edit-profile/edit-profile';
import { ConfigPage } from '../config/config';
import { BorrarCuentaPage } from '../borrar-cuenta/borrar-cuenta';
import { PreferenciasPage } from '../preferencias/preferencias';
import { RestapiService } from '../../providers/restapi-service/restapi-service';

/**
 * Generated class for the PerfilPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 * 
 */

@IonicPage()
@Component({
  selector: 'page-perfil',
  templateUrl: 'perfil.html',
})
export class PerfilPage {

  editProfile = EditProfilePage;
  configureProfile = ConfigPage;
  deleteAccount = BorrarCuentaPage;
  editarPreferences = PreferenciasPage;
  usuario: Object = {
    Nombre: 'Nombre',
    Apellido: 'Apellido',
    Correo: 'Correo'
  };
  idUsuario = 15;

  constructor(public navCtrl: NavController, public navParams: NavParams,public restapiService: RestapiService) {

  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad PerfilPage');
    this.cargarUsuario();
  }

  cargarUsuario(){
    this.restapiService.ObtenerDatosUsuario(this.idUsuario).then(data => {
      if(data != 0)
      {

        this.usuario = data;
        // console.log(this.usuario);
        // console.log(this.usuario.Nombre);
        // console.log(this.usuario.Correo);
        console.log(data);
      }
    });
  }

}
