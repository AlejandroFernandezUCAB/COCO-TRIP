import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import firebase from 'firebase';
import { Facebook } from '@ionic-native/facebook';
import { FacebookLoginResponse } from "@ionic-native/facebook";
import { HomePage } from '../home/home';
import { RegisterPage } from '../register/register';
import { GoogleAuth, User } from '@ionic/cloud-angular';
/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})
export class LoginPage {
  userData: any;
  constructor(public navCtrl: NavController,public facebook: Facebook, public googleAuth: GoogleAuth, public user: User,public navParams: NavParams) {
    
  }

  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }
  continuar(){
    this.navCtrl.push(HomePage);
  }
  
  Login(){

    
  }
  facebookLogin() {
    
    this.facebook.login(['email', 'public_profile']).then((response: FacebookLoginResponse) => {
      this.facebook.api('me?fields=id,name,email,first_name,picture.width(720).height(720).as(picture_large)', []).then(profile => {
        this.userData = {email: profile['email'], first_name: profile['first_name'], picture: profile['picture_large']['data']['url'], username: profile['name']}
      });
    });
  }

  googleLogin(){
    this.googleAuth.login();

  }

  registrar(){
    this.navCtrl.push(RegisterPage);
  }
}
