import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams, ToastController } from 'ionic-angular';

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

  constructor(public navCtrl: NavController, public navParams: NavParams, public toastCtrl: ToastController, public fb: FormBuilder) 
  {
    console.log(navParams);
    this.NombreUsuario = navParams.data;
    console.log(this.NombreUsuario);
    this.myForm = this.fb.group(
      {
        confirmpass: ['', [Validators.required]],
        newpass: ['',[Validators.required]]
      }
    )
  }

  saveData(){
    alert(JSON.stringify(this.myForm.value));
  }
  
  showToastWithCloseButton() {
    const toast = this.toastCtrl.create({
      message: 'Se cambió tu contraseña',
      showCloseButton: true,
      closeButtonText: 'Ok'
    });
    toast.present();
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad ChangepassPage');
  }

}
