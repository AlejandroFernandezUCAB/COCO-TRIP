import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';

/**
 * Generated class for the RegisterPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-register',
  templateUrl: 'register.html',
})
export class RegisterPage {

  myForm: FormGroup;
  //constructor(public navCtrl: NavController, public navParams: NavParams)
  constructor(public navCtrl: NavController,public formBuilder: FormBuilder) {
    this.myForm = this.formBuilder.group({
      userName: ['', [Validators.required,Validators.pattern(/^([A-Za-z]{1}[A-Za-z0-9_-])+$/)]],
      name: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚ]{1}[a-zñáéíóú]+[\s]*)+$/)]],
      lastName: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚ]{1}[a-zñáéíóú]+[\s]*)+$/)]],
      email: ['', [Validators.required, Validators.email]],
      dateBirth: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern(/^[a-z0-9_-]{6,18}$/)]],
      passwordConfirmation: ['', [Validators.required, Validators.pattern(/^[a-z0-9_-]{6,18}$/)]],
      gender: ['', Validators.required],
    });
  }

  saveData(){
    //console.log(this.myForm.value);
    alert(JSON.stringify(this.myForm.value));
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad RegisterPage');
  }

  registrar() {
    this.navCtrl.push(RegisterPage);
  }

  
  

}
