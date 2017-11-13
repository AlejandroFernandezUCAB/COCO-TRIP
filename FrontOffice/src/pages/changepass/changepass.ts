import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController, Platform, ActionSheetController } from 'ionic-angular';

import { RestapiService } from '../../providers/restapi-service/restapi-service';
import { TranslateService } from '@ngx-translate/core';
import { FormBuilder, FormGroup, Validators} from '@angular/forms';

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
  NombreUsuario: string;
  passAct: string;
  passNueva: string;
  apiRestResponse: any;


  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public platform: Platform,
    public actionsheetCtrl: ActionSheetController, private translateService: TranslateService, 
    public fb: FormBuilder, public restapiService: RestapiService) 
  {
    console.log(navParams);
    this.NombreUsuario = navParams.data;
    console.log(this.NombreUsuario);
    this.myForm = this.fb.group(
      {
        contraActual: ['', [Validators.required]],
        newpass: ['', [Validators.required]],
        confirmpass: ['',[Validators.required]]
        
      }
    )
  }

  //funcion que se ejecuta al hacer submit del formulario
  saveData(){
    //Inyectamos los datos nuevos (los que vienen del formulario)
    this.passAct = this.myForm.value.contraActual;
    this.passNueva = this.myForm.value.newpass;

    this.restapiService.cambiarPass(this.NombreUsuario, this.passAct, this.passNueva).then(data =>{
      if(data != 0){
        this.apiRestResponse = data;
        //this.regresarAvistaAnterior(this.apiRestResponse);
    }
    else{
      this.apiRestResponse = false;
      //this.regresarAvistaAnterior(this.apiRestResponse);
    }
  }
  );
  }

  /*

  regresarAvistaAnterior(apiRestResponse){
    this.showToastWithCloseButton(apiRestResponse);
    this.navCtrl.pop();
  }

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
    */
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
