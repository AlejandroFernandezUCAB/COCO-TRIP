import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { EditProfilePage } from '../edit-profile/edit-profile';
import { ConfigPage } from '../config/config';
import { BorrarCuentaPage } from '../borrar-cuenta/borrar-cuenta';
import { PreferenciasPage } from '../preferencias/preferencias';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { FabricaEntidad } from '../../dataAccessLayer/factory/fabricaEntidad';
import { FabricaComando } from '../../businessLayer/factory/fabricaComando';
import { Comando } from '../../businessLayer/commands/comando';
import { TranslateService } from '@ngx-translate/core';

// usos del @ionic/storage:
// para acceder a las variables que guarde en la vista de 'Configuracion'
// para acceder a la variable id alamcenada durante el login
import { Storage } from '@ionic/storage'; 
import { Usuario } from '../../dataAccessLayer/domain/usuario';

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
  private comando : Comando;

  // valores por defecto para el usuario
  usuario : Usuario = FabricaEntidad.crearUsuarioConParametros(0, 'Nombre', 'Apellido', 'Correo', 'default');
  //parametros para la pagina de configuracion
  /*
  configParams: any = {
    idUsuario: 0,
    NombreUsuario: 'default'
  };
  */

  constructor(public navCtrl: NavController, public navParams: NavParams, private storage: Storage, private translateService: TranslateService, public restapiService: RestapiService) {
  
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
    
    this.comando = FabricaComando.crearComandoVerPerfil(
      this.usuario, this.storage, this.translateService, this.restapiService);
    this.comando.execute().then( () => {
      if (this.comando.isSuccess()) {
        this.usuario = this.comando.return() as Usuario;
      }
    });
  
  }

}
