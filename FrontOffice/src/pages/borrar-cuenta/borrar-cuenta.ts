import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { AlertController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { LoginPage } from '../login/login';
import { ComandoBorrarCuenta } from '../../businessLayer/commands/comandoBorrarCuenta';
import { Usuario } from '../../dataAccessLayer/domain/usuario';

@IonicPage()
@Component({
  selector: 'page-borrar-cuenta',
  templateUrl: 'borrar-cuenta.html',
})
export class BorrarCuentaPage {

  myForm : FormGroup;
  usuario: Usuario;

  constructor(public navCtrl: NavController, public navParams: NavParams, public alertCtrl: AlertController, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, 
    public fb: FormBuilder, private comando: ComandoBorrarCuenta)
    {
      console.log(navParams);
      this.usuario = navParams.data as Usuario;
      console.log(this.usuario);
      this.myForm = this.fb.group(
        {
          //Esta será las validación para el campos de la vista.
          pass: ['', [Validators.required]]
        }
      )
    }

  //funcion que se ejecuta al hacer submit del formulario
  borrarData(){
    //Inyectamos los datos que vienen del formulario.
    this.usuario.setClave = this.myForm.value.pass;
    this.comando._entidad = this.usuario;
    this.comando.execute().then(data => {
      this.regresarAvistaAnterior(data);
    }, error => {
      this.regresarAvistaAnterior(false);
    })

  }
  
/*Aqui te regresa a la ventana de login (en caso de ser true) o a la ventana anterior de caso contrario*/ 
  regresarAvistaAnterior(apiRestResponse){

    if (apiRestResponse == true){
      this.showToastWithCloseButton(apiRestResponse);
      this.navCtrl.setRoot(LoginPage);
    }
    else{
      this.showToastWithCloseButton("incorrecto");
      this.navCtrl.pop();
    }
  }

  /*Te dispara un toast dependiendo de lo que haya respondido borrarData()*/
  showToastWithCloseButton(apiRestResponse) {
    let result;
    if (apiRestResponse == true) {
      this.translateService.get("Se borro la cuenta").subscribe(
        value => {
          // value is our translated string
          result = value;
        }
      );
    }
    else if(apiRestResponse == "incorrecto"){
      this.translateService.get("Contraseña incorrecta").subscribe(
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
    console.log('ionViewDidLoad BorrarCuentaPage');
  }

}
