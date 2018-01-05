import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { ComandoCambiarPassword } from '../../businessLayer/commands/comandoCambiarPassword';
import { Usuario } from '../../dataAccessLayer/domain/usuario';

/**
 * Generated class for the ChangepassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-changepass',
  templateUrl: 'changepass.html',
})
export class ChangepassPage {

  myForm: FormGroup;
  usuario: Usuario;
  passAct: string;


  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, 
    public fb: FormBuilder, public restapiService: RestapiService, private comando: ComandoCambiarPassword) 
  {
    console.log(navParams);
    this.usuario = navParams.data as Usuario;
    console.log(this.usuario);
    this.myForm = this.fb.group( 
      {
        //Estas seran las validaciones para los campos de la vista.
        contraActual: ['', [Validators.required]],
        newpass: ['', [Validators.required]],
        confirmpass: ['',[Validators.required]]
        
      }
    )
  }

  //funcion que se ejecuta al hacer submit del formulario
  saveData(){
    //Inyectamos los datos que vienen del formulario.
    this.comando.setContrasenaActual = this.myForm.value.contraActual;
    this.usuario.setClave = this.myForm.value.newpass;
    this.comando._entidad = this.usuario;

    this.comando.execute().then(data => {
      this.regresarAvistaAnterior(data);
    }, error => {
      this.regresarAvistaAnterior(false);
    })

    
    
  }

  
  /*Aqui te regresa a la ventana anterior (Edit-Profile) junto con el toast*/ 
  regresarAvistaAnterior(apiRestResponse){
    this.showToastWithCloseButton(apiRestResponse);
    this.navCtrl.pop();
  }

  /*Te dispara un toast dependiendo de lo que haya respondido saveData(), en caso de ser verdadero (true)
  lanzara un toast con el mensaje "Seguardaron tus cambios", en caso contrario te lanzará un toast con el
  mensaje "Error Modificando los datos"*/ 
  showToastWithCloseButton(apiRestResponse) {
    let result;
    if (apiRestResponse == true) {
      this.translateService.get("Se guardaron tus cambios").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    else{
      this.translateService.get("Error Modificando los datos").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    
    const toast = this.toastCtrl.create({
      message: result ,
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
  }
  

  ionViewDidLoad() {
    console.log('ionViewDidLoad ChangepassPage');
  }

}
