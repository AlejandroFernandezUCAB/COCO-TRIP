import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { EditProfilePage } from '../edit-profile/edit-profile';
import { ConfigPage } from '../config/config';
import { BorrarCuentaPage } from '../borrar-cuenta/borrar-cuenta';
import { PreferenciasPage } from '../preferencias/preferencias';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
// usos del @ionic/storage:
// para acceder a las variables que guarde en la vista de 'Configuracion'
// para acceder a la variable id alamcenada durante el login
import { Storage } from '@ionic/storage'; 

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

  // valores por defecto para el usuario
  usuario: any = {
    Id: 0,
    Nombre: 'Nombre',
    Apellido: 'Apellido',
    Correo: 'Correo',
  };
  idUsuario = 0;

  //parametros para la pagina de configuracion
  configParams: any = {
    idUsuario: 0,
    NombreUsuario: 'default'
  };

  constructor(public navCtrl: NavController, public navParams: NavParams,public restapiService: RestapiService, private storage: Storage, private translateService: TranslateService) {
  
  }

  // este metodo se dispara 1 sola vez
  // por tanto, lo utilizamos para cargar los datos del usuario desde el
  // restapi al cargar la vista en memoria/cache
  ionViewDidLoad() {
    console.log('ionViewDidLoad PerfilPage');
    this.cargarUsuario();
  }

  // metodo para obtener desde el apirest la informacion del usuario
  // ademas obtiene el lenguaje previamente seleccionado de la memoria del
  // dispositivo
  cargarUsuario(){
    // obtenemos el id ya almacenado desde el login
    this.storage.get('id').then((val) => { 

      this.idUsuario = val;
      // hacemos la llamada al apirest con el id obtenido
      this.restapiService.ObtenerDatosUsuario(this.idUsuario).then(data => {
        if(data != 0)
        {  
          this.usuario = data;
          this.usuario.id = this.idUsuario; 

              // cargamos el idioma
              this.storage.get(this.usuario.id.toString()).then((val) => {
                //verificamos que posee configuracion previa de idioma
                if(val != null || val != undefined){
                  this.translateService.use(val);
                }
              });

              // cargamos los datos para la vista de configuracion
              this.configParams.idUsuario = this.idUsuario;
              this.configParams.NombreUsuario = this.usuario.NombreUsuario;
        }
      });

    });
  }

}
