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
      userName: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú0-9])+$/),Validators.minLength(5), Validators.maxLength(20)]],
      name: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/),Validators.minLength(5), Validators.maxLength(30)]],
      lastName: ['', [Validators.required,Validators.pattern(/^([A-ZÁÉÍÓÚa-zñáéíóú])+$/),Validators.minLength(5), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email,Validators.maxLength(30)]],
      dateBirth: ['', Validators.required],
      password: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/),Validators.minLength(5), Validators.maxLength(20)]],
      passwordConfirmation: ['', [Validators.required, Validators.pattern(/^[A-ZÁÉÍÓÚa-zñáéíóú0-9.,$@$!%*?&_-]+$/),Validators.minLength(5), Validators.maxLength(20)]],
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
