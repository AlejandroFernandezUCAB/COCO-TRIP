import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { TranslateService } from '@ngx-translate/core';
import { EditProfilePage } from '../edit-profile/edit-profile';
import { ConfigPage } from '../config/config';
import { BorrarCuentaPage } from '../borrar-cuenta/borrar-cuenta';
import { PreferenciasPage } from '../preferencias/preferencias';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { Storage } from '@ionic/storage'; //para acceder a las variables que guarde en la vista de 'Editar Datos Personales'

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
  usuario: any = {
    Id: 0,
    Nombre: 'Nombre',
    Apellido: 'Apellido',
    Correo: 'Correo',
  };
  idUsuario = 0;

  constructor(public navCtrl: NavController, public navParams: NavParams,public restapiService: RestapiService, private storage: Storage, private translateService: TranslateService) {
  
  }

  // este metodo se dispara 1 sola vez
  // por tanto, lo utilizamos para cargar los datos del usuario desde el
  // restapi al cargar la vista en memoria/cache
  ionViewDidLoad() {
    console.log('ionViewDidLoad PerfilPage');
    this.cargarUsuario();
  }

  cargarUsuario(){
    //obtenemos el id ya almacenado desde el login
    this.storage.get('id').then((val) => { //obtieniene el id guardado localmente

      this.idUsuario = val;
      console.log('id de usuario => ',this.idUsuario);
      //hacemos la llamada al apirest con el id obtenido
      this.restapiService.ObtenerDatosUsuario(this.idUsuario).then(data => {
        if(data != 0)
        {  
          this.usuario = data;
          this.usuario.id = this.idUsuario; 
          console.log(data);

              //cargamos el idioma
              this.storage.get(this.usuario.id.toString()).then((val) => {
                console.log('idioma guardado => ', val);
                if(val != null || val != undefined){
                  this.translateService.use(val);
                }
              });

        }
      });

    });
  }


  // este metodo se dispara cada vez que se entra en esta pagina
  // antes de que este activa
  ionViewWillEnter(){
    // this.storage.get('nombre').then((val) => {
    //   console.log('Nombre guardado', val);
    // });
  }


}
