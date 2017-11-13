import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { AlertController } from 'ionic-angular';
import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';
import { LoginPage } from '../login/login';

@IonicPage()
@Component({
  selector: 'page-borrar-cuenta',
  templateUrl: 'borrar-cuenta.html',
})
export class BorrarCuentaPage {

  myForm : FormGroup;
  NombreUsuario: string;
  password: string;
  apiRestResponse: any;

  constructor(public navCtrl: NavController, public navParams: NavParams, public alertCtrl: AlertController, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, 
    public fb: FormBuilder, public restapiService: RestapiService)
    {
      console.log(navParams);
      this.NombreUsuario = navParams.data;
      console.log(this.NombreUsuario);
      this.myForm = this.fb.group(
        {
          pass: ['', [Validators.required]]
        }
      )
    }

  borrarData(){
    this.password = this.myForm.value.pass;
    this.restapiService.borrarUser(this.NombreUsuario, this.password).then(data =>{
      if(data != 0){
        this.apiRestResponse = data;
        this.regresarAvistaAnterior(this.apiRestResponse);
    }
    else{
      this.apiRestResponse = false;
      this.regresarAvistaAnterior(this.apiRestResponse);
    }
  }
  );
  }

  regresarAvistaAnterior(apiRestResponse){

    if (apiRestResponse == true){
      this.showToastWithCloseButton(apiRestResponse);
      this.navCtrl.setRoot(LoginPage);
    }
  }

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

  /*
  presentConfirm() {
    const alert = this.alertCtrl.create({
      title: 'Confirmar Borrar',
      message: 'El usuario sera borrado permanentemente. Esta seguro?',
      buttons: [
        {
          text: 'Cancelar',
          role: 'cancel',
          handler: () => {
            console.log('Cancelar clicked');
          }
        },
        {
          text: 'Borrar',
          handler: () => {
            console.log('Borrar clicked');
          }
        }
      ]
    });
    alert.present();
  }*/


  ionViewDidLoad() {
    console.log('ionViewDidLoad BorrarCuentaPage');
  }

}
