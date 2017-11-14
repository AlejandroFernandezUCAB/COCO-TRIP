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
    this.NombreUsuario = navParams.data; //Este es el username, se lo asignamos a un string (NombreUsuario)
    console.log(this.NombreUsuario);
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
    this.passAct = this.myForm.value.contraActual;
    this.passNueva = this.myForm.value.newpass;

    /*Aqui haremos el llamado al restapi llamado CambiarPass, aqui le enviaremos los datos suministrados
     en el formulario.*/
    this.restapiService.cambiarPass(this.NombreUsuario, this.passAct, this.passNueva).then(data =>{
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
